using System;
using Zora.Payments.Domain.Exceptions;
using Zora.Shared.Domain.Models;

namespace Zora.Payments.Domain.Models
{
    public class DateTimeRange : ValueObject
    {
        public DateTimeRange(DateTime start, DateTime end)
        {
            Validate(start, end);

            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }//current date

        public DateTime End { get; private set; }

        public DateTimeRange NewStart(DateTime newStart)
          => new DateTimeRange(newStart, this.End);

        public DateTimeRange NewEnd(DateTime newEnd)
            => new DateTimeRange(this.Start, newEnd);

        private void Validate(DateTime startDate, DateTime endDate)
         => Guard.AgainstInvalidDateRange<InvalidDateRangeException>(startDate, endDate, nameof(this.End));

    }

}
