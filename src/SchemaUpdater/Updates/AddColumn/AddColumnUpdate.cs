using System;
using System.Collections.Generic;
using System.Data;

namespace SchemaUpdater.Updates.AddColumn
{
    public abstract class AddColumnUpdate : BaseTableUpdate
    {
        private List<Column> _columns = new List<Column>();

        public AddColumnUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        public virtual void AddColumn<T>(
            string columnName,
            string length = "0",
            bool isNullable = false,
            string defaultValue = "",
            string additionalInformation = "")
        {
            string type = GetDbTypeFromFrameworkType<T>();
            AddColumn(columnName, type, length, isNullable, defaultValue, additionalInformation);
        }

        public virtual void AddColumn(
            string columnName,
            string type,
            string length = "0",
            bool isNullable = false,
            string defaultValue = "",
            string additionalInformation = "")
        {
            var column = new Column(columnName);
            column.AdditionalInformation = additionalInformation;
            column.DefaultValue = defaultValue;
            column.IsNullable = isNullable;
            column.Length = length;
            column.Type = type;

            AddColumn(column);
        }

        public virtual void AddColumn(Column column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }

            CheckIfUpdateIsAlreadyLocked();

            _columns.Add(column);
        }

        public override void Update(IDbConnection connection, IDbTransaction transaction)
        {
            foreach (var column in _columns)
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandTimeout = CommandTimeout;
                    command.CommandText = CreateCommandTextForColumn(column);

                    if (!string.IsNullOrWhiteSpace(command.CommandText))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        protected abstract string CreateCommandTextForColumn(Column column);
    }
}