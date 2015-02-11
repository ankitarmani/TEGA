using System;

namespace TEGACore
{
	public class WsHeartBeatMessage
	{
		private string _protocol = WsMessageProtocol.HeartBeat.GetValue ();
		private string _from = "";

		public WsHeartBeatMessage (string from)
		{
			_from = from;
		}

		public string protocol {
			get {
				return _protocol;
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

		public override string ToString ()
		{
			return SimpleJson.SimpleJson.SerializeObject (this);
		}
	}
}

