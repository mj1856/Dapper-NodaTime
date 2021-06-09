using System;
using System.Data;
using NodaTime;

namespace Dapper.NodaTime
{
    public class OffsetDateTimeHandler : SqlMapper.TypeHandler<OffsetDateTime>
    {
        private OffsetDateTimeHandler()
        {
        }

        public static readonly OffsetDateTimeHandler Default = new OffsetDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, OffsetDateTime value)
        {
            parameter.Value = value.ToDateTimeOffset();

            if (parameter is Microsoft.Data.SqlClient.SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTimeOffset;
            }
            else if (parameter is System.Data.SqlClient.SqlParameter sqlParameter2)
            {
                sqlParameter2.SqlDbType = SqlDbType.DateTimeOffset;
            }
        }

        public override OffsetDateTime Parse(object value)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return OffsetDateTime.FromDateTimeOffset(dateTimeOffset);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.OffsetDateTime");
        }
    }
}
