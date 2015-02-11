using System;

namespace TEGACore
{
	public class SQLiteQueryBuilder
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
		private string type = SQLiteQueryBuilder.SelectQueryType;
		private string tableName = null;
		private string part1 = null;
		private string part2 = null;
		
		public SQLiteQueryBuilder (string type, string tableName, string part1, string part2)
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
}

