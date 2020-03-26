using System;
using Vejledningsbooking.Framework;

namespace Vejledningsbooking.Domain
{
    public class CalendarDescription : Value<CalendarDescription>
    {
        public string Value { get; }

        internal CalendarDescription(string text) => Value = text;
        
        public static CalendarDescription FromString(string text) =>
            new CalendarDescription(text);
        
        public static implicit operator string(CalendarDescription text) =>
            text.Value;
    }
}
