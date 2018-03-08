using NUnit.Framework;
using SchemaUpdater.Exceptions;
using SchemaUpdater.Types;

namespace SchemaUpdater.Test.Types
{
    [TestFixture]
    public class DbTypeMapFactoryTest
    {
        [Test]
        public void TestCreateDbTypeMap()
        {
            var provider = DatabaseProvider.MSSQL;

            IDbTypeMap map = DbTypeMapFactory.CreateDbTypeMap(provider);

            Assert.IsNotNull(map);
        }

        [Test]
        public void TestCreateDbTypeMapWithUnknownProvider()
        {
            var provider = DatabaseProvider.None;

            Assert.Throws(typeof(SchemaUpdaterException), () =>
            {
                DbTypeMapFactory.CreateDbTypeMap(provider);
            });
        }
    }
}