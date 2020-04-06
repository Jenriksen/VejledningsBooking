using System;
using System.Collections.Generic;
using System.Text;
using Vejledningsbooking.Domain;
using Xunit;

namespace Vejledningsbooking.Tests
{
    public class Timeslot_Spec
    {
        private readonly Timeslot _timeslot;

        public Timeslot_Spec()
        {
            _timeslot = new Timeslot(new TimeslotId(new Guid()), new CalendarId(new Guid()), DateTime.Now, DateTime.Now.AddDays(1));
        }

        [Fact]
        public void Can_Update_Start_Using_Valid_DateTime()
        {

        }

        [Fact]
        public void Can_Update_End_Using_Valid_DateTime()
        {

        }
    }
}
