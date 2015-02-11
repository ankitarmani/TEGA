using System;

namespace TEGACore
{
	public class WebSocketConnectionFactory
	{
		private const string TAG = "WebSocketConnectionFactory";
		private static WebSocketConnectionFactory instance = null;
		private WebSocketConnection activeConnection = null;

		private WebSocketConnectionFactory ()
		{
		}
		
		public static WebSocketConnectionFactory Instance {
			get {
				if (instance == null) {
					instance = new WebSocketConnectionFactory ();
				}
				return instance;
			}
		}

		public WebSocketConnection getActiveConnection ()
		{
			if (activeConnection == null) {
				activeConnection = new WebSocketConnection ();
				activeConnection.open ();
			}
			return activeConnection;
		}

		public void closeActiveConnection ()
		{
			if (activeConnection != null) {
				activeConnection.close ();
				activeConnection = null;
			}
		}
	}
}

