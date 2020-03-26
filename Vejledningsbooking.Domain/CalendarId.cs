using System;

namespace Vejledningsbooking.Domain
{
    public class CalendarId : IEquatable<CalendarId>
    {
        private Guid Value { get; }

        public CalendarId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Calendar Id cannot be empty");
            
            Value = value;
        }

        public static implicit operator Guid(CalendarId self) => self.Value;
        
        public static implicit operator CalendarId(string value) 
            => new CalendarId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

        public bool Equals(CalendarId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CalendarId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}