﻿using System.Text;

namespace SchemaUpdater.Updates.NewTable
{
    public class SQLNewTableUpdate : NewTableUpdate
    {
        private StringBuilder _commandBuilder = new StringBuilder();

        public SQLNewTableUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        protected override string CreateCommandText()
        {
            _commandBuilder.Clear();

            AddTableExistsCheckToBuilder();
            AddCreateTableCommandToBuilder();
            AddTableColumnsToBuilder();
            AddPrimaryKeyToBuilder();

            _commandBuilder.Append(")");

            return _commandBuilder.ToString();
        }

        private void AddTableExistsCheckToBuilder()
        {
            _commandBuilder.Append($" IF NOT EXISTS (SELECT * FROM sysobjects WHERE Name='{TableName}' and Xtype='U') ");
        }

        private void AddCreateTableCommandToBuilder()
        {
            _commandBuilder.Append($" CREATE TABLE {TableName} ( ");
        }

        private void AddTableColumnsToBuilder()
        {
            foreach (var column in Columns)
            {
                _commandBuilder.Append($"{column.ColumnName} {column.Type} {column.Length}");

                AddColumnIsNullableToBuilder(column);
                AddColumnDefaultValueToBuilder(column);
                AddColumnAdditionalInfoToBuilder(column);

                _commandBuilder.Append(",");
            }
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

        private void AddPrimaryKeyToBuilder()
        {
            string joinedColumns = string.Join(",", PrimaryKey.Columns);
            string clustered = PrimaryKey.Clustered ? "CLUSTERED" : "NONCLUSTERED";
            string primaryKey = $" PRIMARY KEY {clustered} ({joinedColumns}) ";

            if (!string.IsNullOrWhiteSpace(PrimaryKey.Name))
            {
                primaryKey = $"CONSTRAINT {PrimaryKey.Name} {primaryKey}";
            }

            _commandBuilder.Append(primaryKey);
        }
    }
}