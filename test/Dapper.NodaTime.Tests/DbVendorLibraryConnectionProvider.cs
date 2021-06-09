using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Dapper.NodaTime.Tests
{
    public sealed class DbVendorLibraryConnectionProvider
    {
        public IDbConnection Provide(DbVendorLibrary vendorLibrary, IConfiguration configuration)
        {
            var connectionString = new DbVendorLibraryConnectionStringProvider().Provide(configuration, vendorLibrary);
            return vendorLibrary switch
            {
                DbVendorLibrary.SystemSqlServer => new System.Data.SqlClient.SqlConnection(connectionString),
                DbVendorLibrary.MicrosoftSqlServer => new Microsoft.Data.SqlClient.SqlConnection(connectionString),
                _ => throw new ArgumentOutOfRangeException(nameof(vendorLibrary), "Vendor library unknown")
            };
        }
    }
}
