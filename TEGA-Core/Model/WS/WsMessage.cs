using System;

namespace TEGACore
{
	public class WsMessage
	{
		private long _qid = 0;
		private string _protocol = WsMessageProtocol.Message.GetValue ();
		private string _from = "";
		private string _to = "";
		private long _part = 1;
		private long _totalParts = 1;
		private string _type = "";
		private string _data = "";
		
		public WsMessage ()
		{
		}
		
		public WsMessage (long qid, string protocol, string from, string to, long part, long totalParts, string type, string data)
		{
			_qid = qid;
			_protocol = protocol;
			_from = from;
			_to = to;
			_part = part;
			_totalParts = totalParts;
			_type = type;
			_data = data;
		}
		
		public long qid {
			get {
				return _qid;
			}
			set {
				_qid = value;
			}
		}
		
		public string protocol {
			get {
				return _protocol;
			}
			set {
				_protocol = value;
			}
		}
		
		public string from {
			get {
				return _from;
			}
			set {
				_from = value;
			}
		}
		
		public string to {
			get {
				return _to;
			}
			set {
				_to = value;
			}
		}
		
		public long part {
			get {
				return _part;
			}
			set {
				_part = value;
			}
		}
		
		public long totalParts {
			get {
				return _totalParts;
			}
			set {
				_totalParts = value;
			}
		}
		
		public string type {
			get {
				return _type;
			}
			set {
				_type = value;
			}
		}
		
		public string data {
			get {
				return _data;
			}
			set {
				_data = value;
			}
		}
		
		public static string Serialize (WsMessage message)
		{
			return SimpleJson.SimpleJson.SerializeObject (message);
		}
		
		public static WsMessage Deserialize (string json)
		{
			return SimpleJson.SimpleJson.DeserializeObject<WsMessage> (json);
		}
		
		public override string ToString ()
		{
			return Serialize (this);
		}
	}
}