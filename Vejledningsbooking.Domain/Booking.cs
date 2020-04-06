using System;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class Booking : Entity<BookingId>
    {
        public BookingId Id { get; private set; }
        public TimeslotId TimeslotId { get; private set; }
        public TimeRange TimeRange { get; private set; }
        

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

            //TODO: KBR - Hvordan skal en EnsureValidState sættes sammen?
            throw new NotImplementedException();
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.BookingOnTimeslotCreated e:
                    Id = new BookingId(e.BookingId);
                    TimeslotId = new TimeslotId(e.TimeslotId);
                    TimeRange = new TimeRange(e.BookingStartDateTime, e.BookingEndDateTime);
                    break;
                case Events.BookingStartTimeUpdated e:
                    TimeRange = TimeRange.UpdateStart(e.BookingStartDateTime, TimeRange);
                    break;
                case Events.BookingEndTimeUpdated e:
                    TimeRange = TimeRange.UpdateEnd(e.BookingEndDateTime, TimeRange);
                    break;
            }
        }
    }
}