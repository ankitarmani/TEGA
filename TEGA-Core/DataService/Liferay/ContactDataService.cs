using System;
using System.Linq;
using System.Collections.Generic;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class ContactDataService
	{
		private const string TAG = "ContactDataService";
		private const string TableName = "Contact";
		private const string TableColumns = "contactId INTEGER PRIMARY KEY, firstName TEXT, middleName TEXT, lastName TEXT, facebookId TEXT";
		private const string SelectWhereCondition = " WHERE contactId={0}";
		private const string InsertColumnList = "contactId, firstName, middleName, lastName, facebookId";
		private const string InsertValueList = "{0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\"";
		private const string UpdateValueList = "contactId={0}, firstName=\"{1}\", middleName=\"{2}\", lastName=\"{3}\", facebookId=\"{4}\"";
		private const string DeleteWhereCondition = " WHERE contactId={0}";
		private static ContactDataService instance = null;

		private ContactDataService ()
		{
		}

		public static ContactDataService Instance {
			get {
				if (instance == null) {
					instance = new ContactDataService ();
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

		public List<Contact> getContacts ()
		{
			List<Contact> contacts = new List<Contact> ();
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, "", null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					Contact contact = new Contact ();
					contact.contactId = reader.GetInt64 (0);
					contact.firstName = reader.GetString (1);
					contact.middleName = reader.GetString (2);
					contact.lastName = reader.GetString (3);
					contact.facebookId = reader.GetString (4);
					contacts.Add (contact);
				}
			}
			return contacts;
		}
		
		public Contact getContact (long contactId)
		{
			Contact contact = null;
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, string.Format (SelectWhereCondition, contactId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					contact = new Contact ();
					contact.contactId = reader.GetInt64 (0);
					contact.firstName = reader.GetString (1);
					contact.middleName = reader.GetString (2);
					contact.lastName = reader.GetString (3);
					contact.facebookId = reader.GetString (4);
				}
			}
			return contact;
		}
		
		public void addContact (Contact contact)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.InsertQueryType, TableName, InsertColumnList, string.Format (InsertValueList, contact.contactId, contact.firstName, contact.middleName, contact.lastName, contact.facebookId));
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void updateContact (Contact contact)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.UpdateQueryType, TableName, string.Format (UpdateValueList, contact.contactId, contact.firstName, contact.middleName, contact.lastName, contact.facebookId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void deleteContact (long contactId)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DeleteQueryType, TableName, string.Format (DeleteWhereCondition, contactId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}

