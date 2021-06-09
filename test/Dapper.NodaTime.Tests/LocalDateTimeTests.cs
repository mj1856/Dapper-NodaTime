using System.Linq;
using Microsoft.Extensions.Configuration;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [Collection("DBTests")]
    public class LocalDateTimeTests
    {
        private IConfiguration _configuration;

        private class TestObject
        {
            public LocalDateTime? Value { get; set; }
        }

        public LocalDateTimeTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            SqlMapper.AddTypeHandler(LocalDateTimeHandler.Default);
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDateTime_Stored_As_DateTime2(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject { Value = new LocalDateTime(1234, 12, 31, 1, 2, 3, 4) };

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDateTime_Stored_As_DateTime(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject { Value = new LocalDateTime(1753, 12, 31, 1, 2, 3, 333) };

                const string sql = @"CREATE TABLE #T ([Value] datetime); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDateTime_With_Null_Value(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
