using System;
using System.Collections.Generic;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class Timeslot : Entity<TimeslotId>
    {
        public TimeslotId Id { get; private set; }
        public CalendarId CalendarId { get; private set; }
        public TimeRange TimeRange { get; private set; }
        public TimeslotState State { get; private set; }

        public List<Booking> Bookings { get; set; }

        public Timeslot(TimeslotId id, CalendarId calendarId, DateTime timeslotStartDateTime, DateTime timeslotEndDateTime) =>
            Apply(new Events.TimeslotCreated
            {
                TimeslotId = id,
                CalendarId = calendarId,
                TimeslotStartDateTime = timeslotStartDateTime,
                TimeslotEndDateTime = timeslotEndDateTime
            });

        public void UpdateTimeslotStartDate(TimeslotId id, DateTime datetime) =>
            Apply(new Events.TimeslotStartDateUpdated
            {
                TimeslotId = id,
                TimeslotStartDateTime = datetime
            });

        public void UpdateTimeslotEndDate(TimeslotId id, DateTime datetime) =>
            Apply(new Events.TimeslotEndDateUpdated
            {
                TimeslotId = id,
                TimeslotEndDateTime = datetime
            });

        protected override void EnsureValidState()
        {
            // TODO: KBR - Hvordan skal denne strikkes sammen? lige pt. ser det ud til at vi manuelt skal tjekke om et timeslot-End eller -start er i fortiden.
            

            //var valid =
            //    Id != null &&
            //    (State switch
            //    {
            //        TimeslotState.Active =>
            //             // Insert datetime logic.
            //             TimeslotStartDateTime < DateTime.Now();
            //         Title != null
            //        && Text != null
            //        && Price?.Amount > 0

            //    _ => true
            //    });

            //if (!valid)
            //    throw new InvalidEntityStateException(
            //        this, $"Post-checks failed in state {State}");

            //throw new NotImplementedException();

            bool valid = Id != null;

            switch (State)
            {
                case TimeslotState.Active:
                    valid = valid
                        && CalendarId != null
                        && TimeRange.End > DateTime.Now;
                    break;
                case TimeslotState.Expired:
                    valid = valid && TimeRange.End < DateTime.Now;
                    break;

            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.TimeslotCreated e:
                    Id = new TimeslotId(e.TimeslotId);
                    CalendarId = new CalendarId(e.CalendarId); 
                    // Timeslotstart and end will be "newed" up as well once implemented.
                    TimeRange = new TimeRange(e.TimeslotStartDateTime, e.TimeslotEndDateTime);
                    State = TimeslotState.Active;
                    Bookings = new List<Booking>();
                    break;
                case Events.TimeslotStartDateUpdated e:
                    TimeRange = TimeRange.UpdateStart(e.TimeslotStartDateTime, TimeRange);
                    break;
                case Events.TimeslotEndDateUpdated e:
                    TimeRange = TimeRange.UpdateEnd(e.TimeslotEndDateTime, TimeRange);
                    break;
            }
        }

        public enum TimeslotState
        {
            Active,
            Expired
        }
    }


    public class CalernderStub : Calendar
    {
        public CalernderStub(CalendarId id) : base(id)
        {
        }

        public new bool Overlapped(Timeslot a, Timeslot b)
        {
            return base.Overlapped(a, b);

        }
    }
}