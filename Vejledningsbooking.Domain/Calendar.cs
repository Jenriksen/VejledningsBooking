using System;
using System.Collections.Generic;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class Calendar : Entity<CalendarId>
    {
        public CalendarDescription Description { get; private set; }

        public CalendarId Id { get; private set; }

        public List<Timeslot> Timeslots { get; set; }

        public Calendar(CalendarId id) =>
            Apply(new Events.CalendarCreated
            {
                CalendarId = id,
            });

        public void UpdateCalendarDescription(CalendarId id, CalendarDescription text) =>
            Apply(new Events.CalendarDescriptionUpdated
            {
                CalendarId = id,
                Description = text
            });

        public void AddTimeSlotToCalendar(Timeslot timeslot) =>
            Apply(new Events.TimeslotAddedToCalendar
            {
                CalendarId = this.Id,
                TimeslotStartDateTime = timeslot.TimeRange.Start,
                TimeslotEndDateTime = timeslot.TimeRange.End,
            });
        
        protected override void EnsureValidState()
        {
            //TODO Implement validation for overlapping time slots
            foreach (var timeslot in Timeslots)
            {
                foreach (var timeslotCheck in Timeslots)
                {
                    if (!timeslot.Id.Equals(timeslotCheck.Id)) //do not compare against self
                    {
                        bool overlap = timeslot.TimeRange.Start < timeslotCheck.TimeRange.End &&
                                       timeslotCheck.TimeRange.Start < timeslot.TimeRange.End;
                        if (overlap)
                        {
                            throw new InvalidEntityStateException(this, "Timeslots may not overlap");
                        }
                    }
                    
                }
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.CalendarCreated e:
                    Id = new CalendarId(e.CalendarId);
                    Timeslots = new List<Timeslot>(); 
                    break;
                case Events.CalendarDescriptionUpdated e:
                    Description = new CalendarDescription(e.Description);
                    break;
                case Events.TimeslotAddedToCalendar e:
                    Timeslots.Add(new Timeslot(new TimeslotId(new Guid()), new CalendarId((e.CalendarId)), e.TimeslotStartDateTime, e.TimeslotEndDateTime));
                    break;
            }
        }
    }
}