using System.Text;

namespace SchemaUpdater.Updates.AddColumn
{
    public class SQLAddColumnUpdate : AddColumnUpdate
    {
        private StringBuilder _commandBuilder = new StringBuilder();

        public SQLAddColumnUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        protected override string CreateCommandTextForColumn(Column column)
        {
            _commandBuilder.Clear();

            AddColumnExistsCheckToBuilder(column);
            AddAlterTableCommandToBuilder(column);
            AddColumnIsNullableToBuilder(column);
            AddColumnDefaultValueToBuilder(column);
            AddColumnAdditionalInfoToBuilder(column);

            return _commandBuilder.ToString();
        }

        private void AddColumnExistsCheckToBuilder(Column column)
        {
            _commandBuilder.Append($" IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'{column.ColumnName}' AND Object_ID = Object_ID(N'{TableName}')) ");
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