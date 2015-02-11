
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;

namespace TEGACore
{
	public class SQLLiteQueryBuilder
	{
		// Query type
		public const string CreateTableQueryType = "CREATE_TABLE";
		public const string DropTableQueryType = "DROP_TABLE";
		public const string SelectQueryType = "SELECT";
		public const string InsertQueryType = "INSERT";
		public const string UpdateQueryType = "UPDATE";
		public const string DeleteQueryType = "DELETE";
		// Statements
		private const string CreateTableStatement = "CREATE TABLE IF NOT EXISTS {0} ({1});";
		private const string DropTableStatement = "DROP TABLE IF EXISTS {0}";
		private const string SelectStatement = "SELECT * FROM {0} {1};";
		private const string InsertStatement = "INSERT OR REPLACE INTO {0} ({1}) VALUES ({2});";
		private const string UpdateStatement = "UPDATE {0} SET {1};";
		private const string DeleteStatement = "DELETE FROM {0} {1};";
		// Attributes
		private string type = SQLLiteQueryBuilder.SelectQueryType;
		private string tableName = null;
		private string part1 = null;
		private string part2 = null;
		
		public SQLLiteQueryBuilder (string type, string tableName, string part1, string part2)
		{
			this.type = type;
			this.tableName = tableName;
			this.part1 = part1;
			this.part2 = part2;
		}
		
		public string buildQuery ()
		{
			if (type == CreateTableQueryType) {
				return string.Format (CreateTableStatement, tableName, part1);
			} else if (type == DropTableQueryType) {
				return string.Format (DropTableQueryType, tableName);
			} else if (type == SelectQueryType) {
				return string.Format (SelectStatement, tableName, part1);
			} else if (type == InsertQueryType) {
				return string.Format (InsertStatement, tableName, part1, part2);
			} else if (type == UpdateQueryType) {
				return string.Format (UpdateQueryType, tableName, part1);
			} else if (type == DeleteQueryType) {
				return string.Format (DeleteStatement, tableName, part1);
			} else {
				return "";
			}
		}
	}
	//	public class SQLLiteQueryBuilder
	//	{
	//		private SQLLiteDatabaseConnector connector = SQLLiteDatabaseConnector.Instance;
	//		private static SQLLiteQueryBuilder instance = null;
	//
	//	/*	public const string CreateTableQueryType = "CREATE_TABLE";
	//		public const string DropTableQueryType = "DROP_TABLE";
	//		public const string SelectQueryType = "SELECT";
	//		public const string InsertQueryType = "INSERT";
	//		public const string UpdateQueryType = "UPDATE";
	//		public const string DeleteQueryType = "DELETE";
	//		// Statements
	//		private const string CreateTableStatement = "CREATE TABLE IF NOT EXISTS {0} ({1});";
	//		private const string DropTableStatement = "DROP TABLE IF EXISTS {0}";
	//		private const string SelectStatement = "SELECT * FROM {0} {1};";
	//		private const string InsertStatement = "INSERT OR REPLACE INTO {0} ({1}) VALUES ({2});";
	//		private const string UpdateStatement = "UPDATE {0} SET {1};";
	//		private const string DeleteStatement = "DELETE FROM {0} {1};";
	//		// Attributes
	//		private string type = SQLLiteQueryBuilder.SelectQueryType;
	//		private string tableName = null;
	//		private string part1 = null;
	//		private string part2 = null;
	//		
	//		/*public SQLLiteQueryBuilder (string type, string tableName, string part1, string part2)
	//		{
	//			this.type = type;
	//			this.tableName = tableName;
	//			this.part1 = part1;
	//			this.part2 = part2;
	//		}*/
	//		/*
	//		public string buildQuery ()
	//		{
	//			if (type == CreateTableQueryType) {
	//				return string.Format (CreateTableStatement, tableName, part1);
	//			} else if (type == DropTableQueryType) {
	//				return string.Format (DropTableQueryType, tableName);
	//			} else if (type == SelectQueryType) {
	//				return string.Format (SelectStatement, tableName, part1);
	//			} else if (type == InsertQueryType) {
	//				return string.Format (InsertStatement, tableName, part1, part2);
	//			} else if (type == UpdateQueryType) {
	//				return string.Format (UpdateQueryType, tableName, part1);
	//			} else if (type == DeleteQueryType) {
	//				return string.Format (DeleteStatement, tableName, part1);
	//			} else {
	//				return "";
	//			}
	//		}
	//		*/
	//		public static SQLLiteQueryBuilder Instance {
	//			get {
	//				if (instance == null) {
	//					instance = new SQLLiteQueryBuilder ();
	//				}
	//				return instance;
	//			}
	//		}
	//
	//		/*public static void CreateDatabase (SqliteConnection connection)
	//		{
	//			var sql = "CREATE TABLE User (Id INTEGER PRIMARY KEY AUTOINCREMENT, Fullname varchar(32), Username varchar(32), Password varchar(32), Email varchar(32), Gender varchar(10), BirthDate date);";
	//			
	//			
	//			connection.Open ();
	//			
	//			using (var cmd = connection.CreateCommand ()) {
	//				cmd.CommandText = sql;
	//				cmd.ExecuteNonQuery ();
	//			}
	//			
	//			// Create a sample note to get the user started
	//			sql = "INSERT INTO User (FullName, Username, Password, Email, Gender, BirthDate) VALUES (@Fullname, @Username, @Password, @Email, @Gender, @BirthDate);";
	//			
	//			using (var cmd = connection.CreateCommand ()) {
	//				cmd.CommandText = sql;
	//				cmd.Parameters.AddWithValue ("@Fullname", "Ankit Ramani");
	//				cmd.Parameters.AddWithValue ("@Username", "ankit");
	//				cmd.Parameters.AddWithValue ("@Password", "ankit");
	//				cmd.Parameters.AddWithValue ("@Email", "a@gmail.com");
	//				cmd.Parameters.AddWithValue ("@Gender", "Male");
	//				cmd.Parameters.AddWithValue ("@BirthDate", "1988-09-17");
	//				
	//				cmd.ExecuteNonQuery ();
	//			}
	//			
	//			connection.Close ();
	//		}*/
	//
	//		public User GetUser (string username)
	//		{
	//			var sql = "SELECT Username FROM User";
	//		
	//			using (var conn = connector.GetConnection()) {
	//				conn.Open ();
	//
	//				using (var cmd = conn.CreateCommand ()) {
	//					cmd.CommandText = sql;
	//					
	//					using (var reader = cmd.ExecuteReader ()) {
	//						while(reader.Read())
	//						{
	//						var a = reader.GetString (1);
	//						Console.WriteLine ("a:{0}", a);
	//						}
	//						
	//						if (reader.Read ())
	//							return new User (reader.GetInt32 (0), reader.GetString (1), reader.GetString (2), reader.GetString (3), reader.GetString (4), reader.GetString (5), reader.GetDateTime (6)); 
	//						else
	//							return null;
	//					}
	//				}
	//			}
	//		}
	//	}
}

