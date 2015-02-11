using System;
using Android.Content;
using Android.Net.Wifi;
using Android.Telephony;
using Android.Util;
using System.Threading;
using System.Collections.Generic;

namespace TEGACore
{
	public class WebSocketManager
	{
		private const string TAG = "WebSocketManager";
		private static WebSocketManager instance = null;
		private bool _isConnected = false;
		private bool _isIdentified = false;
		private bool _isHeartBeating = false;
		private WsIdentification deviceIdentification = null;
		private string _deviceAddress = CoreConstants.WebSocketDefaultDeviceName + CoreConstants.WebSocketAddressSuffix;
		private Thread heartBeatThread = null;
		private Dictionary<long, LogiAssistContext> qidContextMap = null;
		private Dictionary<long, WsMessage> messageCache = null;
		
		private WebSocketManager ()
		{
			qidContextMap = new Dictionary<long, LogiAssistContext> ();
			messageCache = new Dictionary<long, WsMessage> ();
		}
		
		public static WebSocketManager Instance {
			get {
				if (instance == null) {
					instance = new WebSocketManager ();
				}
				return instance;
			}
		}
		
		public bool isConnected {
			get {
				return _isConnected;
			}
			set {
				_isConnected = value;
			}
		}
		
		public bool isIdentified {
			get {
				return _isIdentified;
			}
		}
		
		public bool isHeartBeating {
			get {
				return _isHeartBeating;
			}
		}
		
		public string deviceAddress {
			get {
				return _deviceAddress;
			}
		}
		
		public void identifyDevice (Context context)
		{
			if (_isIdentified == false) {
				deviceIdentification = new WsIdentification ();
				deviceIdentification.deviceId = Android.Provider.Settings.Secure.GetString (context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
				deviceIdentification.deviceRef = Android.OS.Build.Device;
				deviceIdentification.deviceModel = Android.OS.Build.Model;
				deviceIdentification.deviceManufacturer = Android.OS.Build.Manufacturer;
				deviceIdentification.deviceBrand = Android.OS.Build.Brand;
				_deviceAddress = deviceIdentification.deviceId + CoreConstants.WebSocketAddressSuffix;
				_isIdentified = true;
			}
		}
		
		public void connect ()
		{
			if (_isConnected == false) {
				WebSocketConnectionFactory.Instance.getActiveConnection ();
			}
		}
		
		public void disconnect ()
		{
			if (_isConnected == true) {
				WebSocketConnectionFactory.Instance.closeActiveConnection ();
			}
		}
		
		public void connectionOpened ()
		{
			// Set the connection indicator
			_isConnected = true;
			
			// Send the Connect Token
			sendMessage (null, WsMessageProtocol.Message, CoreConstants.WebSocketServerAddress, CoreConstants.WsMsgTypeConnectDevice, null);
			
			// Send the Identification Token
			sendDeviceIdentification ();
			
			// Start HeartBeating
			startHeartBeating ();
		}
		
		public void connectionClosed ()
		{
			// Set the connection indicator
			_isConnected = false;
			
			// Stop HeartBeating
			stopHeartBeating ();
			
			// Close the active connection
			WebSocketConnectionFactory.Instance.closeActiveConnection ();
		}
		
		public void sendMessage (LogiAssistContext context, WsMessageProtocol protocol, string to, string type, string data)
		{
			if (_isConnected == false) {
				Log.Error (TAG, "Could not send " + protocol + " with type " + type + " to " + to + " because the device is not connected to the Server");
				return;
			}
			
			if (protocol == WsMessageProtocol.Query && context == null) {
				Log.Error (TAG, "Could not send query with type " + type + " to " + to + " without specified context");
				return;
			}
			
			// Check if the request is associated to a context
			if (context != null) {
				qidContextMap.Add (WebSocketConnectionFactory.Instance.getActiveConnection ().getNextQid (), context);
			}
			
			WebSocketConnectionFactory.Instance.getActiveConnection ().sendMessage (protocol, _deviceAddress, to, type, data);
		}
		
		private void sendDeviceIdentification ()
		{
			if (deviceIdentification == null) {
				Log.Error (TAG, "Could not send the IdentifyDevice Message to Server, the Device must be identified first");
				return;
			}
			
			sendMessage (null, WsMessageProtocol.Message, CoreConstants.WebSocketServerAddress, CoreConstants.WsMsgTypeIdentifyDevice, SimpleJson.SimpleJson.SerializeObject (deviceIdentification));
		}
		
		public void startHeartBeating ()
		{
			if (heartBeatThread == null) {
				heartBeatThread = new Thread (new ThreadStart (() =>
				                                               {
					// Beat only while connected
					while (isConnected) {
						Thread.Sleep (20000);
						WebSocketConnectionFactory.Instance.getActiveConnection ().sendHeartBeat (_deviceAddress);
					}
				}));
			}
			if (heartBeatThread.ThreadState != ThreadState.Running) {
				heartBeatThread.Start ();
			}
			_isHeartBeating = true;
		}
		
		public void stopHeartBeating ()
		{
			if (heartBeatThread != null) {
				heartBeatThread.Abort ();
			}
			_isHeartBeating = false;
		}
		
		public void dispatchReceivedMessage (string message)
		{
			Log.Info (TAG, "Dispatching received message : " + message);
			WsMessage receivedMessage = WsMessage.Deserialize (message);
			if (receivedMessage == null) {
				Log.Error (TAG, "The received message was malformed or is malformated");
				return;
			}
			
			if (receivedMessage.totalParts > 1 && receivedMessage.part <= receivedMessage.totalParts) {
				Log.Info (TAG, string.Format ("The received message is chunk {0} of {1}", receivedMessage.part, receivedMessage.totalParts));
				Log.Info (TAG, "Checking for previous chunks");
				if (messageCache.ContainsKey (receivedMessage.qid) == false) {
					// CASE : it wasn't cached before (probably the 1st chunk)
					Log.Info (TAG, "Caching received chunk");
					messageCache.Add (receivedMessage.qid, receivedMessage);
					return;
					
				} else {
					// CASE : Another chunk was already cached
					WsMessage otherChunk = messageCache [receivedMessage.qid];
					receivedMessage.data = otherChunk.data + receivedMessage.data;
					Log.Info (TAG, string.Format ("Already received chunk updated {0} of {1}", receivedMessage.part, receivedMessage.totalParts));
					
					// Check if it's now complete
					messageCache.Remove (receivedMessage.qid);
					if (receivedMessage.part != receivedMessage.totalParts) {
						// CASE : it is not complete yet
						Log.Info (TAG, "Caching received chunk");
						messageCache.Add (receivedMessage.qid, receivedMessage);
						return;
					} else {
						Log.Info (TAG, "Received chunk complete");
						// CASE : it is complete -> proceed to message dispatching
					}
				}
			}
			
			LogiAssistContext context = qidContextMap [receivedMessage.qid];
			if (context == null) {
				Log.Warn (TAG, "No Context found to callback the reply of the Server");
				return;
			}
			
			context.messageReceived (receivedMessage);
			qidContextMap.Remove (receivedMessage.qid);
		}
	}
}

