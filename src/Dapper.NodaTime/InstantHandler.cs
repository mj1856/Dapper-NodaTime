using System;
using System.Data;
using NodaTime;

namespace Dapper.NodaTime
{
    public class InstantHandler : SqlMapper.TypeHandler<Instant>
    {
        private InstantHandler()
        {
        }

        public static readonly InstantHandler Default = new InstantHandler();

        public override void SetValue(IDbDataParameter parameter, Instant value)
        {
            parameter.Value = value.ToDateTimeUtc();

            if (parameter is Microsoft.Data.SqlClient.SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime2;
            }
            else if (parameter is System.Data.SqlClient.SqlParameter sqlParameter2)
            {
                sqlParameter2.SqlDbType = SqlDbType.DateTime2;
            }
        }

        public override Instant Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                var dt = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                return Instant.FromDateTimeUtc(dt);
            }

            if (value is DateTimeOffset dateTimeOffset)
            {
                return Instant.FromDateTimeOffset(dateTimeOffset);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.Instant");
        }
    }
}
