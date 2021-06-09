using System;
using System.Data;
using NodaTime;

namespace Dapper.NodaTime
{
    public class LocalDateHandler : SqlMapper.TypeHandler<LocalDate>
    {
        private LocalDateHandler()
        {
        }

        public static readonly LocalDateHandler Default = new LocalDateHandler();

        public override void SetValue(IDbDataParameter parameter, LocalDate value)
        {
            parameter.Value = value.AtMidnight().ToDateTimeUnspecified();

            if (parameter is Microsoft.Data.SqlClient.SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.Date;
            }
            else if (parameter is System.Data.SqlClient.SqlParameter sqlParameter2)
            {
                sqlParameter2.SqlDbType = SqlDbType.Date;
            }
        }

        public override LocalDate Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                return LocalDateTime.FromDateTime(dateTime).Date;
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.LocalDate");
        }
    }
}
