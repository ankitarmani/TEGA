using System;
using System.IO;
using Android.Content;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class SQLiteConnectionFactory
	{
		private static SQLiteConnectionFactory instance = null;
		private SqliteConnection connection = null;
		
		private SQLiteConnectionFactory ()
		{
		}
		
		public static string DatabaseFilePath {
			get {
				return Path.Combine (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, CoreConstants.FolderNameLogiAssist, CoreConstants.DatabaseName);
			}
		}
		
		public static SQLiteConnectionFactory Instance {
			get {
				if (instance == null) {
					instance = new SQLiteConnectionFactory ();
				}
				return instance;
			}
		}
		
		public SqliteConnection DatabaseConnection {
			get {
				if (connection == null) {
					connection = new SqliteConnection (string.Format ("Data Source={0};Password={1};DateTimeFormat=Ticks;", DatabaseFilePath, CoreConstants.DatabasePwd));
				}
				connection.Open ();
				return connection;
			}
		}
		
		public void closeConnection ()
		{
			if (connection != null) {
				connection.Close ();
			}
		}
	}
}