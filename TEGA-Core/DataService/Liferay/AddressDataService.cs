using System;
using System.Linq;
using System.Collections.Generic;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class AddressDataService
	{
		private const string TAG = "AddressDataService";
		private const string TableName = "Address";
		private const string TableColumns = "addressId INTEGER PRIMARY KEY, className TEXT, classPk INTEGER, typeId INTEGER, street1 TEXT, street2 TEXT, street3 TEXT, zip TEXT, city TEXT, region TEXT, country TEXT";
		private const string SelectWhereCondition = " WHERE addressId={0}";
		private const string InsertColumnList = "addressId, className, classPk, typeId, street1, street2, street3, zip, city, region, country";
		private const string InsertValueList = "{0}, \"{1}\", {2}, {3}, \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", \"{10}\"";
		private const string UpdateValueList = "addressId={0}, className=\"{1}\", classPk={2}, typeId={3}, street1=\"{4}\", street2=\"{5}\", street3=\"{6}\", zip=\"{7}\", city=\"{8}\", region=\"{9}\", country=\"{10}\"";
		private const string DeleteWhereCondition = " WHERE addressId={0}";
		private static AddressDataService instance = null;

		private AddressDataService ()
		{
		}

		public static AddressDataService Instance {
			get {
				if (instance == null) {
					instance = new AddressDataService ();
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

		public List<Address> getAddresses ()
		{
			List<Address> addresses = new List<Address> ();
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, "", null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					Address address = new Address ();
					address.addressId = reader.GetInt64 (0);
					address.className = reader.GetString (1);
					address.classPk = reader.GetInt64 (2);
					address.typeId = reader.GetInt64 (3);
					address.street1 = reader.GetString (4);
					address.street2 = reader.GetString (5);
					address.street3 = reader.GetString (6);
					address.zip = reader.GetString (7);
					address.city = reader.GetString (8);
					address.region = reader.GetString (9);
					address.country = reader.GetString (10);
					addresses.Add (address);
				}
			}
			return addresses;
		}

		public Address getAddress (long addressId)
		{
			Address address = null;
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, string.Format (SelectWhereCondition, addressId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					address = new Address ();
					address.addressId = reader.GetInt64 (0);
					address.className = reader.GetString (1);
					address.classPk = reader.GetInt64 (2);
					address.street1 = reader.GetString (3);
					address.street2 = reader.GetString (4);
					address.street3 = reader.GetString (5);
					address.zip = reader.GetString (6);
					address.city = reader.GetString (7);
					address.region = reader.GetString (8);
					address.country = reader.GetString (9);
				}
			}
			return address;
		}

		public void addAddress (Address address)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.InsertQueryType, TableName, InsertColumnList, string.Format (InsertValueList, address.addressId, address.className, address.classPk, address.street1, address.street2, address.street3, address.zip, address.city, address.region, address.country));
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}

		public void updateAddress (Address address)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.UpdateQueryType, TableName, string.Format (UpdateValueList, address.addressId, address.className, address.classPk, address.street1, address.street2, address.street3, address.zip, address.city, address.region, address.country), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}

		public void deleteAddress (long addressId)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DeleteQueryType, TableName, string.Format (DeleteWhereCondition, addressId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}

