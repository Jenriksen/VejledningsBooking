using System;
using System.Collections.Generic;
using System.Text;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class TimeRange : Value<TimeRange>
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        internal TimeRange(DateTime start, DateTime end)
        {
            int res = DateTime.Compare(start, end);

            if (res >= 0)
            {
                throw new ArgumentException(
                    nameof(start), "Start date must be set earlier than the end date.");
            }

            Start = start;
            End = end;
        }

        internal TimeRange UpdateStart(DateTime start, TimeRange timeRange)
        {
            return new TimeRange(start, timeRange.End);
        }

        internal TimeRange UpdateEnd(DateTime end, TimeRange timeRange)
        {
            return new TimeRange(timeRange.Start, end);
        }

    }
}
