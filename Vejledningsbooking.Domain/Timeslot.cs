using System;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class TimeSlot : Entity<TimeslotId>
    {
        public TimeslotId Id { get; private set; }
        public CalendarId CalendarId { get; private set; }

        public DateTime TimeslotStartDateTime { get; private set; }
        public DateTime TimeslotEndDateTime { get; private set; }
        public TimeslotState State { get; private set; }
        public bool Expired { get; private set; } = false;

        public TimeSlot(TimeslotId id, CalendarId calendarId, DateTime TimeslotStartDateTime, DateTime TimeslotEndDateTime) =>
            Apply(new Events.TimeslotCreated
            {
                TimeslotId = id,
                CalendarId = CalendarId,
                TimeslotEndDateTime = TimeslotEndDateTime,
                TimeslotStartDateTime = TimeslotStartDateTime
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
            // var valid =
            //     Id != null &&
            //     (State switch
                // {
                //     TimeslotState.Active =>
                //         // Insert datetime logic.
                //         TimeslotStartDateTime < DateTime.Now();
                //         Title != null
                //         && Text != null
                //         && Price?.Amount > 0
                //     _ => true
                // });

            // if (!valid)
            //     throw new InvalidEntityStateException(
            //         this, $"Post-checks failed in state {State}");

            throw new NotImplementedException();
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.TimeslotCreated e:
                    Id = new TimeslotId(e.TimeslotId);
                    CalendarId = new CalendarId(e.CalendarId);
                    // Timeslotstart and end will be "newed" up as well once implemented.
                    TimeslotStartDateTime = e.TimeslotStartDateTime;
                    TimeslotEndDateTime = e.TimeslotEndDateTime;
                    break;
                case Events.TimeslotStartDateUpdated e:
                    TimeslotStartDateTime = e.TimeslotStartDateTime;
                    break;
                case Events.TimeslotEndDateUpdated e:
                    TimeslotEndDateTime = e.TimeslotEndDateTime;
                    break;
            }
        }

        public enum TimeslotState
        {
            Active,
            Expired
        }
    }
}