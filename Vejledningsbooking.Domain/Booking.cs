using System;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class Booking : Entity<BookingId>
    {
        public BookingId Id { get; private set; }
        public TimeslotId TimeslotId { get; private set; }

        public DateTime BookingStartDateTime { get; private set; } //refactor to value objects
        public DateTime BookingEndDateTime { get; private set; }

        public bool Expired { get; private set; } = false;

        public Booking(BookingId id, TimeslotId timeslotId, DateTime BookingStartDateTime, DateTime BookingEndDateTime) =>
            Apply(new Events.BookingOnTimeslotCreated
            {
                BookingId = id,
                TimeslotId = TimeslotId,
                BookingStartDateTime = BookingStartDateTime,
                BookingEndDateTime = BookingEndDateTime
            });

        public void UpdateBookingStartDateTime(BookingId id, DateTime datetime) =>
            Apply(new Events.BookingStartTimeUpdated
            {
                BookingId = id,
                BookingStartDateTime = datetime
            });

        public void UpdateBookingEndDateTime(BookingId id, DateTime datetime) =>
            Apply(new Events.BookingEndTimeUpdated
            {
                BookingId = id,
                BookingEndDateTime = datetime
            });
        protected override void EnsureValidState()
        {
            throw new NotImplementedException();
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.BookingOnTimeslotCreated e:
                    Id = new BookingId(e.BookingId);
                    TimeslotId = new TimeslotId(e.TimeslotId);
                    BookingStartDateTime = e.BookingStartDateTime;
                    BookingEndDateTime = e.BookingEndDateTime;
                    break;
                case Events.BookingStartTimeUpdated e:
                    BookingStartDateTime = e.BookingStartDateTime;
                    break;
                case Events.BookingEndTimeUpdated e:
                    BookingEndDateTime = e.BookingEndDateTime;
                    break;
            }
        }
    }
}