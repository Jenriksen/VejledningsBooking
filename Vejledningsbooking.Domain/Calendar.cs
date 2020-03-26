using System;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class Calendar : Entity<CalendarId>
    {
        public CalendarDescription Description { get; private set; }

        public CalendarId Id { get; private set; }



        public Calendar(CalendarId id) =>
            Apply(new Events.CalendarCreated
            {
                CalendarId = id
            });

        public void UpdateCalendarDescription(CalendarId id, CalendarDescription text) =>
            Apply(new Events.CalendarDescriptionUpdated
            {
                CalendarId = id,
                Description = text
            });

        protected override void EnsureValidState()
        {
            throw new NotImplementedException();
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.CalendarDescriptionUpdated e:
                    Description = new CalendarDescription(e.Description);
                    break;
            }
        }
    }
}