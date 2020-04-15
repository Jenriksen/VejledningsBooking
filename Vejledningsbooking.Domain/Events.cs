using System;

namespace Vejledningsbooking.Domain
{
    public static class Events 
    {
        public class CalendarCreated
        {
            public Guid CalendarId {get; set;}
        }

        // Skal CalendarUpdated være med?
        // public class CalendarUpdated
        // {
        //     public Guid CalendarId { get; set; }

        // }

        public class CalendarDescriptionUpdated
        {
            public Guid CalendarId { get; set; }
            public string Description { get; set; }
        }

        public class CalendarRemoved
        {
            public Guid CalendarId { get; set; }
        }

        public class TimeslotAddedToCalendar
        {
            public Timeslot Timeslot { get; set; }
            public Guid CalendarId { get; set; }
            public DateTime TimeslotStartDateTime { get; set; }
            public DateTime TimeslotEndDateTime { get; set; }
        }

        public class TimeslotCreated
        {
            public Guid TimeslotId { get; set; }
            public Guid CalendarId { get; set; }
            public DateTime TimeslotStartDateTime { get; set; }
            public DateTime TimeslotEndDateTime { get; set; }
        }
        
        public class TimeslotStartDateUpdated 
        {
            public Guid TimeslotId { get; set; }
            public DateTime TimeslotStartDateTime { get; set; }
        }
        
        public class TimeslotEndDateUpdated
        {
            public Guid TimeslotId { get; set; }
            public DateTime TimeslotEndDateTime { get; set; }
        }


        //TODO: Sp�rge Kaj hvordan denne kan implementeres
        // public class TimeslotExpirationUpdated
        // {
        //     // skal implementeres som exception under "EnsureValidState"
        // }

        public class BookingOnTimeslotCreated
        {
            // Hvordan skal StudentId forbindelsen laves?
            public Guid BookingId { get; set; }
            public Guid TimeslotId { get; set; }
            //public Guid StudentId { get; set; }
            public DateTime BookingStartDateTime { get; set; }
            public DateTime BookingEndDateTime { get; set; }

        }

        public class BookingAddedToTimeslot
        {
            public Guid BookingId { get; set; }
            public Guid TimeslotId { get; set; }
            public DateTime BookingStartDateTime { get; set; }
            public DateTime BookingEndDateTime { get; set; }

        }
        
        public class BookingFromTimeSlotRemoved
        {
            public Guid BookingId { get; set; }
        }
        
        public class BookingStartTimeUpdated
        {
            public Guid BookingId { get; set; }
            public DateTime BookingStartDateTime { get; set; }
        }

        public class BookingEndTimeUpdated
        {
            public Guid BookingId { get; set; }
            public DateTime BookingEndDateTime { get; set; }
        }

        //TODO: Sp�rge Kaj hvordan denne kan implementeres
        // public class BookingExpirationUpdated
        // {
        //     // skal implementeres som exception under "EnsureValidState"
        // }

    }
}

