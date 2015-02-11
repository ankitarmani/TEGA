using System;

namespace TEGACore
{
	public sealed class WsMessageProtocol
	{
		private readonly int internalValue;
		private readonly string value;
		private readonly string caption;
		public static readonly WsMessageProtocol Message = new WsMessageProtocol (1, "message", "Message");
		public static readonly WsMessageProtocol MessageError = new WsMessageProtocol (2, "message_error", "Message Error");
		public static readonly WsMessageProtocol Query = new WsMessageProtocol (3, "query", "Query");
		public static readonly WsMessageProtocol Result = new WsMessageProtocol (4, "result", "Result");
		public static readonly WsMessageProtocol QueryError = new WsMessageProtocol (5, "query_error", "Query Error");
		public static readonly WsMessageProtocol HeartBeat = new WsMessageProtocol (6, "beat", "HeartBeat");

		private WsMessageProtocol (int internalValue, string value, string caption)
		{
			this.internalValue = internalValue;
			this.value = value;
			this.caption = caption;
		}

		public int GetInternalValue ()
		{
			return internalValue;
		}

		public string GetValue ()
		{
			return value;
		}

		public override string ToString ()
		{
			return caption;
		}
	}
}

