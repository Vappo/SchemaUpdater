using NUnit.Framework;
using SchemaUpdater.Exceptions;
using SchemaUpdater.Updates.NewTable;

namespace SchemaUpdater.Test.Updates
{
    [TestFixture]
    public class NewTableUpdateFactoryTest
    {
        [Test]
        public void TestCreateNewTableUpdate()
        {
            var database = new Database("connectionString", "System.Data.SqlClient");

            NewTableUpdate update = NewTableUpdateFactory.CreateNewTableUpdate("Table", database);

            Assert.IsNotNull(update);
        }

        [Test]
        public void TestCreateNewTableUpdateWithUnknownProvider()
        {
            var database = new Database("connectionString", "System.Data.None");

            Assert.Throws(typeof(SchemaUpdaterException), () =>
            {
                NewTableUpdateFactory.CreateNewTableUpdate("Table", database);
            });
        }
    }
}