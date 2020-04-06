using System;
using Xunit;
using Vejledningsbooking.Domain;

namespace Vejledningsbooking.Tests
{
    public class Calendar_Spec
    {
        private readonly Calendar _calendar;

        public Calendar_Spec()
        {
            _calendar = new Calendar(new CalendarId(new Guid()));
        }

        [Fact]
        public void Can_Add_Valid_Timeslot_To_Calendar()
        {
            // start and end time created
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddHours(4);
            
            // Create new timeslot
            Timeslot timeslot_to_add = new Timeslot(new TimeslotId(new Guid()), _calendar.Id, start, end);

            //Add Timeslot to Calendar
            _calendar.AddTimeSlotToCalendar(timeslot_to_add);
            Assert.Equal()
        }
        [Fact]
        public void Cannot_Add_Overlapping_Timeslot_To_Calendar()
        {
            // start and end time created
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddHours(4);
            
            // Create new timeslot
            Timeslot timeslot_to_add = new Timeslot(new TimeslotId(new Guid()), _calendar.Id, start, end);

            //Add Timeslot to Calendar
            _calendar.AddTimeSlotToCalendar(timeslot_to_add);
            Assert.Equal()
        }
        [Fact]
        public void Can_Update_Description_From_String_To_CalendarDescription()
        {
            //Create new description
            string newDescription = "Updated description";
            //Update calendar with new description
            _calendar.UpdateCalendarDescription(_calendar.Id, CalendarDescription.FromString(newDescription));
            //Assert if new description was added to calendar
            Assert.Same(newDescription, _calendar.Description.ToString());
        }
    }
}
