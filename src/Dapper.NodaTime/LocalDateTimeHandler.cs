using System;
using System.Data;
using NodaTime;

namespace Dapper.NodaTime
{
    public class LocalDateTimeHandler : SqlMapper.TypeHandler<LocalDateTime>
    {
        private LocalDateTimeHandler()
        {
        }

        public static readonly LocalDateTimeHandler Default = new LocalDateTimeHandler();

        public override void SetValue(IDbDataParameter parameter, LocalDateTime value)
        {
            parameter.Value = value.ToDateTimeUnspecified();

            if (parameter is Microsoft.Data.SqlClient.SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTime2;
            }
            else if (parameter is System.Data.SqlClient.SqlParameter sqlParameter2)
            {
                sqlParameter2.SqlDbType = SqlDbType.DateTime2;
            }
        }

        public override LocalDateTime Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.LocalDateTime");
        }
    }
}
