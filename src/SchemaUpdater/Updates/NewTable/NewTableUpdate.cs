using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchemaUpdater.Updates.NewTable
{
    public abstract class NewTableUpdate : BaseTableUpdate
    {
        private List<Column> _columns = new List<Column>();
        private List<string> _primaryKeyColumns = new List<string>();

        public NewTableUpdate(string tableName, Database database) : base(tableName, database)
        {
        }

        protected List<Column> Columns
        {
            get { return _columns; }
        }

        protected List<string> PrimaryKeyColumns
        {
            get { return _primaryKeyColumns; }
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

        public void AddPrimaryKey(params string[] primaryKey)
        {
            if (primaryKey == null || primaryKey.Length == 0)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }

            CheckIfUpdateIsAlreadyLocked();

            _primaryKeyColumns = primaryKey.ToList();
        }

        public override void Update(IDbConnection connection, IDbTransaction transaction)
        {
            CheckIfColumnsAreAdded();
            CheckIfPrimaryKeyIsAdded();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandTimeout = CommandTimeout;
                command.CommandText = CreateCommandText();

                if (!string.IsNullOrWhiteSpace(command.CommandText))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CheckIfColumnsAreAdded()
        {
            if (_columns == null || !_columns.Any())
            {
                throw new InvalidOperationException("No columns are added to the table.");
            }
        }

        private void CheckIfPrimaryKeyIsAdded()
        {
            if (_primaryKeyColumns == null || !_primaryKeyColumns.Any())
            {
                throw new InvalidOperationException("Primary Key is missing.");
            }
        }

        protected abstract string CreateCommandText();
    }
}