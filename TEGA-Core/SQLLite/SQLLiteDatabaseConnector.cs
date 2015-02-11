
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;	
using Android.Util;
using Mono.Data.Sqlite;

namespace TEGACore
{
	public class SQLLiteDatabaseConnector
	{
		private static SQLLiteDatabaseConnector instance = null;
		private SqliteConnection connection = null;
		private static string TEGA = "masoud.db3";
		
		private SQLLiteDatabaseConnector ()
		{
		}

		public SqliteConnection GetConnection ()
		{
			var dbPath = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), TEGA);
			bool exists = File.Exists (dbPath);
			
			if (!exists)
				SqliteConnection.CreateFile (dbPath);
			
			var connection = new SqliteConnection ("Data Source=" + dbPath);
			
			if (!exists)
				CreateDatabase (connection);
			
			return connection;
		}

		public void CreateDatabase (SqliteConnection connection)
		{
			var sql = "CREATE TABLE User (Id INTEGER PRIMARY KEY AUTOINCREMENT, Fullname ntext, Username ntext, Password ntext, Email ntext, Gender ntext, BirthDate date);";


			connection.Open ();
			
			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery ();
			}
			
			// Create a sample note to get the user started
			sql = "INSERT INTO User (FullName, Username, Password, Email, Gender, BirthDate) VALUES (@Fullname, @Username, @Password, @Email, @Gender, @BirthDate);";
			
			using (var cmd = connection.CreateCommand ()) {
				cmd.CommandText = sql;
				cmd.Parameters.AddWithValue ("@Fullname", "Ankit Ramani");
				cmd.Parameters.AddWithValue ("@Username", "ankit");
				cmd.Parameters.AddWithValue ("@Password", "ankit");
				cmd.Parameters.AddWithValue ("@Email", "a@gmail.com");
				cmd.Parameters.AddWithValue ("@Gender", "Male");
				cmd.Parameters.AddWithValue ("@BirthDate", "1988-09-17");
				
				cmd.ExecuteNonQuery ();
				using (var reader = cmd.ExecuteReader ()){
					while(reader.Read())
					{
						var a = reader.GetString (1);
						Console.WriteLine ("a:{0}", a);
					}
				}

			}


			connection.Close ();
		}

		/*public static string DatabaseFilePath {
			get {
				return Path.Combine (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, CoreConstants.FolderNameLogiAssist, CoreConstants.DatabaseName);
			}
		}*/
		
		public static SQLLiteDatabaseConnector Instance {
			get {
				if (instance == null) {
					instance = new SQLLiteDatabaseConnector ();
				}
				return instance;
			}
		}
		
		/*public SqliteConnection DatabaseConnection {
			get {
				if (connection == null) {
					connection = new SqliteConnection(string.Format ("Data Source={0};Password={1};DateTimeFormat=Ticks;", DatabaseFilePath, CoreConstants.DatabasePwd));
					connection.Open ();
				}
				return connection;
			}
		}*/

		public void closeConnection ()
		{
			if (connection != null) {
				connection.Close ();
			}
		}
	}
}

