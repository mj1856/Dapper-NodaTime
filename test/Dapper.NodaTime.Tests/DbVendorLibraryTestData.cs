using System.Collections;
using System.Collections.Generic;

namespace Dapper.NodaTime.Tests
{
    public sealed class DbVendorLibraryTestData
        : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
#if NET461_OR_GREATER
            yield return new object[] { DbVendorLibrary.SystemSqlServer };
#endif            
            yield return new object[] { DbVendorLibrary.MicrosoftSqlServer};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
