using System;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class Booking : Entity<BookingId>
    {
        public BookingId Id { get; private set; }
        public TimeslotId timeslotId { get; private set; }

        public DateTime BookingStartDateTime { get; private set; }
        public DateTime BookingEndDateTime { get; private set; }

        public bool Expired { get; private set; } = false;

        public Booking(BookingId id, TimeslotId timeslotId, DateTime BookingStartDateTime, DateTime BookingEndDateTime) =>
            Apply(new Events.BookingOnTimeslotCreated
            {
                BookingId = id,
                TimeslotId = timeslotId,
                BookingStartDateTime = BookingStartDateTime,
                BookingEndDateTime = BookingEndDateTime
            });

        public void UpdateBookingStartDateTime(BookingId id, DateTime datetime) =>
            Apply(new Events.TimeslotStartDateUpdated
            {
                TimeslotId = id,
                TimeslotStartDateTime = datetime
            });

        public void UpdateBookingEndDateTime(BookingId id, DateTime datetime) =>
            Apply(new Events.TimeslotEndDateUpdated
            {
                TimeslotId = id,
                TimeslotEndDateTime = datetime
            });
        protected override void EnsureValidState()
        {
            throw new NotImplementedException();
        }

        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}