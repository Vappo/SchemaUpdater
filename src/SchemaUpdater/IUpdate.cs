using System.Data;

namespace SchemaUpdater
{
    public interface IUpdate
    {
        int CommandTimeout { get; set; }
        
        Database Database { get; }

        bool IsLocked { get; }

        string TableName { get; }

        void Lock();

        void Update(string connectionString);

        void Update(IDbConnection connection, IDbTransaction transaction);
    }
}