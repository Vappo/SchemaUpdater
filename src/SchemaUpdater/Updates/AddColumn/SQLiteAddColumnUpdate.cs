using System.Data;
using System.Text;

namespace SchemaUpdater.Updates.AddColumn
{
    public class SQLiteAddColumnUpdate : AddColumnUpdate
    {
        private StringBuilder _commandBuilder = new StringBuilder();

        public SQLiteAddColumnUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        protected override string CreateCommandTextForColumn(Column column)
        {
            _commandBuilder.Clear();

            if (CheckIfColumnExists(column))
            {
                return string.Empty;
            }

            AddAlterTableCommandToBuilder(column);
            AddColumnIsNullableToBuilder(column);
            AddColumnDefaultValueToBuilder(column);
            AddColumnAdditionalInfoToBuilder(column);

            return _commandBuilder.ToString();
        }

        private bool CheckIfColumnExists(Column column)
        {
            using (IDbConnection connection = Database.CreateConnection())
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = $" PRAGMA table_info({TableName}) ";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Name"].Equals(column.ColumnName))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void AddAlterTableCommandToBuilder(Column column)
        {
            _commandBuilder.Append($" ALTER TABLE {TableName} ADD {column.ColumnName} {column.Length} {column.Type} ");
        }

        private void AddColumnIsNullableToBuilder(Column column)
        {
            if (column.IsNullable)
            {
                _commandBuilder.Append(" NULL ");
            }
            else
            {
                _commandBuilder.Append(" NOT NULL ");
            }
        }

        private void AddColumnDefaultValueToBuilder(Column column)
        {
            if (!string.IsNullOrWhiteSpace(column.DefaultValue))
            {
                _commandBuilder.Append($" DEFAULT {column.DefaultValue} ");
            }
        }

        private void AddColumnAdditionalInfoToBuilder(Column column)
        {
            if (!string.IsNullOrWhiteSpace(column.AdditionalInformation))
            {
                _commandBuilder.Append($" {column.AdditionalInformation} ");
            }
        }
    }
}