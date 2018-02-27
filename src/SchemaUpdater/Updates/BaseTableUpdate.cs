using System.Data;

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
        }

        public virtual void Update(IDbConnection connection, IDbTransaction transaction)
        {
        }
    }
}