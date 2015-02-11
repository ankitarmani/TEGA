using System;
using System.Linq;
using System.Collections.Generic;
using Android.Util;
using Mono.Data.Sqlcipher;

namespace TEGACore
{
	public class OrganizationDataService
	{
		private const string TAG = "OrganizationDataService";
		private const string TableName = "Organization";
		private const string TableColumns = "organizationId INTEGER PRIMARY KEY, parentOrganizationId INTEGER, name TEXT, type TEXT, logoId INTEGER";
		private const string SelectWhereCondition = " WHERE organizationId={0}";
		private const string InsertColumnList = "organizationId, parentOrganizationId, name, type, logoId";
		private const string InsertValueList = "{0}, {1}, \"{2}\", \"{3}\", \"{4}\"";
		private const string UpdateValueList = "organizationId={0}, parentOrganizationId={1}, name=\"{2}\", type=\"{3}\", logoId=\"{4}\"";
		private const string DeleteWhereCondition = " WHERE organizationId={0}";
		private static OrganizationDataService instance = null;

		private OrganizationDataService ()
		{
		}

		public static OrganizationDataService Instance {
			get {
				if (instance == null) {
					instance = new OrganizationDataService ();
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

		public List<Organization> getOrganizations ()
		{
			List<Organization> orgs = new List<Organization> ();
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, "", null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					Organization org = new Organization ();
					org.organizationId = reader.GetInt64 (0);
					org.parentOrganizationId = reader.GetInt64 (1);
					org.name = reader.GetString (2);
					org.type = reader.GetString (3);
					org.logoId = reader.GetInt64 (4);
					orgs.Add (org);
				}
			}
			return orgs;
		}

		public Organization getOrganization (long organizationId)
		{
			Organization org = null;
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.SelectQueryType, TableName, string.Format (SelectWhereCondition, organizationId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				var reader = cmd.ExecuteReader ();
				while (reader.Read()) {
					org = new Organization ();
					org.organizationId = reader.GetInt64 (0);
					org.parentOrganizationId = reader.GetInt64 (1);
					org.name = reader.GetString (2);
					org.type = reader.GetString (3);
					org.logoId = reader.GetInt64 (4);
				}
			}
			return org;
		}

		public void addOrganization (Organization organization)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.InsertQueryType, TableName, InsertColumnList, string.Format (InsertValueList, organization.organizationId, organization.parentOrganizationId, organization.name, organization.type, organization.logoId));
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}

		public void updateOrganization (Organization organization)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.UpdateQueryType, TableName, string.Format (UpdateValueList, organization.organizationId, organization.parentOrganizationId, organization.name, organization.type, organization.logoId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}

		public void deleteOrganization (long organizationId)
		{
			using (var connection = SQLiteConnectionFactory.Instance.DatabaseConnection) {
				SqliteCommand cmd = connection.CreateCommand ();
				SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder (SQLiteQueryBuilder.DeleteQueryType, TableName, string.Format (DeleteWhereCondition, organizationId), null);
				cmd.CommandText = queryBuilder.buildQuery ();
				cmd.ExecuteNonQuery ();
			}
		}
	}
}

