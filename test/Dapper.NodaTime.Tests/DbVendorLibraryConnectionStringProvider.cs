using System;
using Microsoft.Extensions.Configuration;

namespace Dapper.NodaTime.Tests
{
    public sealed class DbVendorLibraryConnectionStringProvider
    {
        public string Provide(IConfiguration configuration, DbVendorLibrary vendorLibrary) =>
            vendorLibrary switch
            {
                DbVendorLibrary.SystemSqlServer => configuration.GetConnectionString("TestDBSystemSqlServer"),
                DbVendorLibrary.MicrosoftSqlServer => configuration.GetConnectionString("TestDBMicrosoftSqlServer"),
                _ => throw new ArgumentOutOfRangeException(nameof(vendorLibrary), "Vendor library unknown")
            };
    }
}
