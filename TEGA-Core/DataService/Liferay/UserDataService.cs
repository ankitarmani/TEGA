using System;
using System.Linq;
using System.Collections.Generic;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class UserDataService
	{
		private const string TAG = "UserDataService";
		private const string TableName = "User";
		private const string TableColumns = "userId INTEGER PRIMARY KEY, createDate INTEGER, screenName TEXT, email TEXT, country TEXT, region TEXT, firstName TEXT, middleName TEXT, lastName TEXT, jobTitle TEXT, lastLoginDate INTEGER, orgId INTEGER, contactId INTEGER";
		private const string SelectWhereCondition = " WHERE userId={0}";
		private const string InsertColumnList = "userId, createDate, screenName, email, country, region, firstName, middleName, lastName, jobTitle, lastLoginDate, orgId, contactId";
		private const string InsertValueList = "{0}, {1}, \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", {10}, \"{11}\", {12}";
		private const string UpdateValueList = "userId={0}, createDate={1}, screenName=\"{2}\", email=\"{3}\", country=\"{4}\", region=\"{5}\", firstName=\"{6}\", middleName=\"{7}\", lastName=\"{8}\", jobTitle=\"{9}\", lastLoginDate={10}, orgId=\"{11}\", contactId={12}";
		private const string DeleteWhereCondition = " WHERE userId={0}";
		private static UserDataService instance = null;

		private UserDataService ()
		{
		}

		public static UserDataService Instance {
			get {
				if (instance == null) {
					instance = new UserDataService ();
				}
				return instance;
			}
		}

		public long userId { get; set; }

		public void createTable ()
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.CreateTableQueryType, TableName, TableColumns, null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void dropTable ()
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DropTableQueryType, TableName, null, null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}

		public List<User> getUsers ()
		{
			List<User> users = new List<User> ();
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, "", null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					User user = new User ();
					user.userId = reader.GetInt64 (0);
					user.createDate = reader.GetInt64 (1);
					user.screenName = reader.GetString (2);
					user.email = reader.GetString (3);
					user.country = reader.GetString (4);
					user.region = reader.GetString (5);
					user.firstName = reader.GetString (6);
					user.middleName = reader.GetString (7);
					user.lastName = reader.GetString (8);
					user.jobTitle = reader.GetString (9);
					user.lastLoginDate = reader.GetInt64 (10);
					user.orgId = reader.GetInt64 (11);
					user.contactId = reader.GetInt64 (12);
					users.Add (user);
				}
			}
			return users;
		}
		
		public User getUser (long userId)
		{
			User user = null;
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, string.Format (SelectWhereCondition, userId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					user = new User ();
					user.userId = reader.GetInt64 (0);
					user.createDate = reader.GetInt64 (1);
					user.screenName = reader.GetString (2);
					user.email = reader.GetString (3);
					user.country = reader.GetString (4);
					user.region = reader.GetString (5);
					user.firstName = reader.GetString (6);
					user.middleName = reader.GetString (7);
					user.lastName = reader.GetString (8);
					user.jobTitle = reader.GetString (9);
					user.lastLoginDate = reader.GetInt64 (10);
					user.orgId = reader.GetInt64 (11);
					user.contactId = reader.GetInt64 (12);
				}
			}
			return user;
		}
		
		public void addUser (User user)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.InsertQueryType, TableName, InsertColumnList, string.Format (InsertValueList, user.userId, user.createDate, user.screenName, user.email, user.country, user.region, user.firstName, user.middleName, user.lastName, user.jobTitle, user.lastLoginDate, user.orgId, user.contactId));
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void updateUser (User user)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.UpdateQueryType, TableName, string.Format (UpdateValueList, user.userId, user.createDate, user.screenName, user.email, user.country, user.region, user.firstName, user.middleName, user.lastName, user.jobTitle, user.lastLoginDate, user.orgId, user.contactId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void deleteUser (long userId)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DeleteQueryType, TableName, string.Format (DeleteWhereCondition, userId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}

