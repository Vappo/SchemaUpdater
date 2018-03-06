using System;
using System.Data;
using SchemaUpdater.Types;

namespace SchemaUpdater.Updates
{
    public abstract class BaseTableUpdate : IUpdate
    {
        public BaseTableUpdate(string tableName, Database database)
        {
            TableName = tableName;
            Database = database;
        }

        public int CommandTimeout { get; set; }

        public Database Database { get; }

        public bool IsLocked { get; protected set; }

        public string TableName { get; }

        public virtual void Lock()
        {
            IsLocked = true;
        }

        public virtual void Update(string connectionString)
        {
            using (IDbConnection connection = Database.CreateConnection())
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    Update(connection, transaction);
                    transaction.Commit();
                }
            }
        }

        public virtual void Update(IDbConnection connection, IDbTransaction transaction)
        {
        }

        protected virtual void CheckIfUpdateIsAlreadyLocked()
        {
            if (IsLocked)
            {
                throw new InvalidOperationException("The given update is already locked. You can't add additional changes.");
            }
        }

        protected string GetDbTypeFromFrameworkType<T>()
        {
            IDbTypeMap map = DbTypeMapFactory.CreateDbTypeMap(Database.Provider);
            Type type = typeof(T);
            return map.DbTypeMap[type];
        }
    }
}