using System;
using WebSocket4Net;
using SuperSocket.ClientEngine;
using Android.Util;

namespace TEGACore
{
	public class WebSocketConnection
	{
			private const string TAG = "WebSocketConnection";
			private WebSocket websocket = null;
			private long lastSentQid = 0;
			
			public WebSocketConnection ()
			{
				string serverUrl = string.Format ("{0}{1}", CoreConstants.ServerUrl, CoreConstants.ServerContext);
				Log.Info (TAG, "Trying to connect to Server : " + serverUrl);
				websocket = new WebSocket (serverUrl, CoreConstants.ServerProtocol);
				websocket.Opened += new EventHandler (websocket_Opened);
				websocket.Error += new EventHandler<ErrorEventArgs> (websocket_Error);
				websocket.Closed += new EventHandler (websocket_Closed);
				websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs> (websocket_MessageReceived);
			}
			
			private void websocket_Opened (object sender, EventArgs e)
			{
				Log.Info (TAG, "WebSocket connection opened");
				WebSocketManager.Instance.connectionOpened ();
			}
			
			private void websocket_Error (object sender, ErrorEventArgs e)
			{
				Log.Info (TAG, "WebSocket connection error : type=" + e.Exception);
				WebSocketManager.Instance.connectionClosed ();
			}
			
			private void websocket_Closed (object sender, EventArgs e)
			{
				Log.Info (TAG, "WebSocket connection closed");
				WebSocketManager.Instance.connectionClosed ();
			}
			
			private void websocket_MessageReceived (object sender, MessageReceivedEventArgs e)
			{
				// Forward the message to the Manager for correct dispatching
				WebSocketManager.Instance.dispatchReceivedMessage (e.Message);
			}
			
			public void open ()
			{
				websocket.Open ();
			}
			
			public void close ()
			{
				websocket.Close ();
			}
			
			public void sendMessage (WsMessage message)
			{
				if (WebSocketManager.Instance.isConnected == false) {
					Log.Warn (TAG, "The message could not be sent because the device is not connected to the Server");
					return;
				}
				
				if (message.qid <= 0
				    || string.IsNullOrEmpty (message.protocol) == true
				    || string.IsNullOrEmpty (message.from) == true
				    || string.IsNullOrEmpty (message.to) == true
				    || string.IsNullOrEmpty (message.type) == true) {
					Log.Warn (TAG, "The message could not be sent because one or more mandatory fields are null");
					return;
				}
				if (lastSentQid >= message.qid) {
					Log.Warn (TAG, "The message could not be sent because the Qid is inconsistent");
					return;
				}
				websocket.Send (message.ToString ());
				lastSentQid = message.qid;
			}
			
			public void sendMessage (long qid, WsMessageProtocol protocol, string from, string to, string type, string data)
			{
				if (WebSocketManager.Instance.isConnected == false) {
					Log.Warn (TAG, "The message could not be sent because the device is not connected to the Server");
					return;
				}
				
				if (protocol == null
				    || string.IsNullOrEmpty (from) == true
				    || string.IsNullOrEmpty (to) == true
				    || string.IsNullOrEmpty (type) == true) {
					Log.Warn (TAG, "The message could not be sent because one or more mandatory fields are null");
					return;
				}
				if (lastSentQid >= qid) {
					Log.Warn (TAG, "The message could not be sent because the Qid is inconsistent");
					return;
				}
				WsMessage message = new WsMessage (qid, protocol.GetValue (), from, to, 1, 1, type, data);
				websocket.Send (message.ToString ());
				lastSentQid = qid;
			}
			
			public void sendMessage (WsMessageProtocol protocol, string from, string to, string type, string data)
			{
				if (WebSocketManager.Instance.isConnected == false) {
					Log.Warn (TAG, "The message could not be sent because the device is not connected to the Server");
					return;
				}
				
				if (protocol == null
				    || string.IsNullOrEmpty (from) == true
				    || string.IsNullOrEmpty (to) == true
				    || string.IsNullOrEmpty (type) == true) {
					Log.Warn (TAG, "The message could not be sent because one or more mandatory fields are null");
					return;
				}
				lastSentQid++;
				WsMessage message = new WsMessage (lastSentQid, protocol.GetValue (), from, to, 1, 1, type, data);
				websocket.Send (message.ToString ());
			}
			
			public void sendHeartBeat (string deviceAddress)
			{
				if (WebSocketManager.Instance.isConnected == false) {
					Log.Warn (TAG, "The HeartBeat could not be sent because the device is not connected to the Server");
					return;
				}
				WsHeartBeatMessage hb = new WsHeartBeatMessage (deviceAddress);
				websocket.Send (hb.ToString ());
			}
			
			public long getNextQid ()
			{
				return lastSentQid + 1;
			}
		}
	}


