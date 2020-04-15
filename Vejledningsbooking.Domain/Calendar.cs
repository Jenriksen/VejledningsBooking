using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
                Timeslot = timeslot,
                CalendarId = this.Id,
                TimeslotStartDateTime = timeslot.TimeRange.Start,
                TimeslotEndDateTime = timeslot.TimeRange.End,
            });
        
        protected override void EnsureValidState()
        {
            Timeslots.ToList().ForEach(a => Timeslots.Any(b => b != a && Overlapped(a, b)));
            
            //foreach (var timeslot in Timeslots)
            //{
            //    foreach (var timeslotCheck in Timeslots)
            //    {
            //        if (!timeslot.Id.Equals(timeslotCheck.Id)) //do not compare against self
            //        {
            //            bool overlap = timeslot.TimeRange.Start < timeslotCheck.TimeRange.End &&
            //                           timeslotCheck.TimeRange.Start < timeslot.TimeRange.End;
            //            if (overlap)
            //            {
            //                throw new InvalidEntityStateException(this, "Timeslots may not overlap");
            //            }
            //        }
                    
            //    }
            //}
        }



        protected bool Overlapped(Timeslot a, Timeslot b)
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

            return false;
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
                    Timeslots.Add(e.Timeslot);
                    break;
            }
        }
    }
}