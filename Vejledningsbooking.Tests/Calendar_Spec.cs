using System;
using Xunit;
using Vejledningsbooking.Domain;
using System.Linq;

namespace Vejledningsbooking.Tests
{
    public class Calendar_Spec
    {
        private readonly Calendar _calendar;

        public Calendar_Spec()
        {
            _calendar = new Calendar(new CalendarId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_Add_Valid_Timeslot_To_Calendar()
        {
            //start and end time created
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddHours(4);
            
            //Create new timeslot
            Timeslot timeslot_to_add = new Timeslot(new TimeslotId(Guid.NewGuid()), _calendar.Id, start, end);

            //Add Timeslot to Calendar
            _calendar.AddTimeSlotToCalendar(timeslot_to_add);

            //Assert
            //Assert.Contains<Timeslot>(timeslot_to_add, _calendar.Timeslots);

            Assert.True(_calendar.Timeslots.Any(ts => ts.Id == timeslot_to_add.Id));

            //myList.Any(item => item.UniqueProperty == wonderIfItsPresent.UniqueProperty);
        }

        [Fact]
        public void Cannot_Add_Overlapping_Timeslot_To_Calendar()
        {
            //start and end time created
            DateTime firstTimeslotStart = DateTime.Now;
            DateTime firstTimeslotEnd = DateTime.Now.AddHours(4);
            
             //start and end time created
            DateTime SecondTimeslotStart = DateTime.Now.AddHours(2);
            DateTime SecondTimeslotEnd = DateTime.Now.AddHours(4);

            //Create first new timeslot
            Timeslot first_timeslot_to_add = new Timeslot(new TimeslotId(Guid.NewGuid()), _calendar.Id, firstTimeslotStart, firstTimeslotEnd);

            //Create first new timeslot
            Timeslot second_timeslot_to_add = new Timeslot(new TimeslotId(Guid.NewGuid()), _calendar.Id, SecondTimeslotStart, SecondTimeslotEnd);

            //Add Timeslot to Calendar
            _calendar.AddTimeSlotToCalendar(first_timeslot_to_add);
            //Assert if method returns exception upon validation
            Assert.Throws<InvalidEntityStateException>( () => _calendar.AddTimeSlotToCalendar(second_timeslot_to_add));
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
