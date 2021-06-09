using System.Linq;
using Microsoft.Extensions.Configuration;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [Collection("DBTests")]
    public class LocalDateTests
    {
        private IConfiguration _configuration;

        private class TestObject
        {
            public LocalDate? Value { get; set; }
        }

        public LocalDateTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            SqlMapper.AddTypeHandler(LocalDateHandler.Default);
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDate_Stored_As_Date(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject { Value = new LocalDate(1234, 12, 31) };

                const string sql = @"CREATE TABLE #T ([Value] date); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDate_Stored_As_DateTime(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject { Value = new LocalDate(1753, 12, 31) };

                const string sql = @"CREATE TABLE #T ([Value] datetime); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDate_Stored_As_DateTime2(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject { Value = new LocalDate(1234, 12, 31) };

                const string sql = @"CREATE TABLE #T ([Value] datetime2); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_LocalDate_With_Null_Value(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] date); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
