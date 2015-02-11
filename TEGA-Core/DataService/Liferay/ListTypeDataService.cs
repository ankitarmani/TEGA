using System;
using System.Linq;
using System.Collections.Generic;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class ListTypeDataService
	{
		private const string TAG = "ListTypeDataService";
		private const string TableName = "ListType";
		private const string TableColumns = "listTypeId INTEGER PRIMARY KEY, name TEXT, type TEXT";
		private const string SelectWhereCondition = " WHERE listTypeId={0}";
		private const string InsertColumnList = "listTypeId, name, type";
		private const string InsertValueList = "{0}, \"{1}\", \"{2}\"";
		private const string UpdateValueList = "listTypeId={0}, name=\"{1}\", type=\"{2}\"";
		private const string DeleteWhereCondition = " WHERE listTypeId={0}";
		private static ListTypeDataService instance = null;

		private ListTypeDataService ()
		{
		}

		public static ListTypeDataService Instance {
			get {
				if (instance == null) {
					instance = new ListTypeDataService ();
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

		public List<ListType> getListTypes ()
		{
			List<ListType> listTypes = new List<ListType> ();
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, "", null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					ListType listType = new ListType ();
					listType.listTypeId = reader.GetInt64 (0);
					listType.name = reader.GetString (1);
					listType.type = reader.GetString (2);
					listTypes.Add (listType);
				}
			}
			return listTypes;
		}
		
		public ListType getListType (long listTypeId)
		{
			ListType listType = null;
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, string.Format (SelectWhereCondition, listTypeId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					listType = new ListType ();
					listType.listTypeId = reader.GetInt64 (0);
					listType.name = reader.GetString (1);
					listType.type = reader.GetString (2);
				}
			}
			return listType;
		}
		
		public void addListType (ListType listType)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.InsertQueryType, TableName, InsertColumnList, string.Format (InsertValueList, listType.listTypeId, listType.name, listType.type));
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void updateListType (ListType listType)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.UpdateQueryType, TableName, string.Format (UpdateValueList, listType.listTypeId, listType.name, listType.type), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
		
		public void deleteListType (long listTypeId)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DeleteQueryType, TableName, string.Format (DeleteWhereCondition, listTypeId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}

