using System;
using NUnit.Framework;
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
            
            Assert.Throws(typeof(ArgumentException), () =>
            {
                DbTypeMapFactory.CreateDbTypeMap(provider);
            });
        }
    }
}