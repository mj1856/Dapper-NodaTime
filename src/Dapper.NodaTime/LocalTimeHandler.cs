using System;
using System.Data;
using NodaTime;

namespace Dapper.NodaTime
{
    public class LocalTimeHandler : SqlMapper.TypeHandler<LocalTime>
    {
        private LocalTimeHandler()
        {
        }

        public static readonly LocalTimeHandler Default = new LocalTimeHandler();

        public override void SetValue(IDbDataParameter parameter, LocalTime value)
        {
            parameter.Value = TimeSpan.FromTicks(value.TickOfDay);

            if (parameter is Microsoft.Data.SqlClient.SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.Time;
            }
            else if (parameter is System.Data.SqlClient.SqlParameter sqlParameter2)
            {
                sqlParameter2.SqlDbType = SqlDbType.Time;
            }
        }

        public override LocalTime Parse(object value)
        {
            if (value is TimeSpan timeSpan)
            {
                return LocalTime.FromTicksSinceMidnight(timeSpan.Ticks);
            }

            if (value is DateTime dateTime)
            {
                return LocalTime.FromTicksSinceMidnight(dateTime.TimeOfDay.Ticks);
            }

            throw new DataException("Cannot convert " + value.GetType() + " to NodaTime.LocalTime");
        }
    }
}
