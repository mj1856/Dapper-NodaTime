using System.Linq;
using Microsoft.Extensions.Configuration;
using NodaTime;
using Xunit;

namespace Dapper.NodaTime.Tests
{
    [Collection("DBTests")]
    public class OffsetDateTimeTests
    {
        private IConfiguration _configuration;

        public class TestObject
        {
            public OffsetDateTime? Value { get; set; }
        }

        public OffsetDateTimeTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            SqlMapper.AddTypeHandler(OffsetDateTimeHandler.Default);
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_OffsetDateTime_Stored_As_DateTimeOffset(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject
                {
                    Value = new OffsetDateTime(
                        new LocalDateTime(1234, 1, 2, 3, 4, 5, 6),
                        Offset.FromHoursAndMinutes(-5, -30))
                };

                const string sql = @"CREATE TABLE #T ([Value] datetimeoffset); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
            }
        }

        [Theory]
        [ClassData(typeof(DbVendorLibraryTestData))]
        public void Can_Write_And_Read_OffsetDateTime_With_Null_Value(DbVendorLibrary library)
        {
            using (var connection = new DbVendorLibraryConnectionProvider().Provide(library, _configuration))
            {
                var o = new TestObject();

                const string sql = @"CREATE TABLE #T ([Value] datetimeoffset); INSERT INTO #T VALUES (@Value); SELECT * FROM #T";
                var t = connection.Query<TestObject>(sql, o).First();

                Assert.Equal(o.Value, t.Value);
                Assert.Null(t.Value);
            }
        }
    }
}
