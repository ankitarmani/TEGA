using System;
using System.Linq;
using System.Collections.Generic;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class PhoneDataService
	{
		private const string TAG = "PhoneDataService";
		private const string TableName = "Phone";
		private const string TableColumns = "phoneId INTEGER PRIMARY KEY, className TEXT, classPk INTEGER, typeId INTEGER, prefix TEXT, number TEXT, extension TEXT";
		private const string SelectWhereCondition = " WHERE phoneId={0}";
		private const string InsertColumnList = "phoneId, className, classPk, typeId, prefix, number, extension";
		private const string InsertValueList = "{0}, \"{1}\", {2}, {3}, \"{4}\", \"{5}\", \"{6}\"";
		private const string UpdateValueList = "phoneId={0}, className=\"{1}\", classPk={2}, typeId={3}, prefix=\"{4}\", number=\"{5}\", extension=\"{6}\"";
		private const string DeleteWhereCondition = " WHERE phoneId={0}";
		private static PhoneDataService instance = null;

		private PhoneDataService ()
		{
		}

		public static PhoneDataService Instance {
			get {
				if (instance == null) {
					instance = new PhoneDataService ();
				}
				return instance;
			}
		}

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

		public List<Phone> getPhones ()
		{
			List<Phone> phones = new List<Phone> ();
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, "", null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					Phone phone = new Phone ();
					phone.phoneId = reader.GetInt64 (0);
					phone.className = reader.GetString (1);
					phone.classPk = reader.GetInt64 (2);
					phone.typeId = reader.GetInt64 (3);
					phone.prefix = reader.GetString (4);
					phone.number = reader.GetString (5);
					phone.extension = reader.GetString (6);
					phones.Add (phone);
				}
			}
			return phones;
		}
		
		public Phone getPhone (long phoneId)
		{
			Phone phone = null;
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, string.Format (SelectWhereCondition, phoneId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					phone = new Phone ();
					phone.phoneId = reader.GetInt64 (0);
					phone.className = reader.GetString (1);
					phone.classPk = reader.GetInt64 (2);
					phone.typeId = reader.GetInt64 (3);
					phone.prefix = reader.GetString (4);
					phone.number = reader.GetString (5);
					phone.extension = reader.GetString (6);
				}
			}
			return phone;
		}
		
		public void addPhone (Phone phone)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.InsertQueryType, TableName, InsertColumnList, string.Format (InsertValueList, phone.phoneId, phone.className, phone.classPk, phone.typeId, phone.prefix, phone.number, phone.extension));
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void updatePhone (Phone phone)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.UpdateQueryType, TableName, string.Format (UpdateValueList, phone.phoneId, phone.className, phone.classPk, phone.typeId, phone.prefix, phone.number, phone.extension), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void deletePhone (long phoneId)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DeleteQueryType, TableName, string.Format (DeleteWhereCondition, phoneId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}

