using System;

namespace Vejledningsbooking.Domain
{
    public class TimeslotId : IEquatable<TimeslotId>
    {
        public Guid Value { get; private set; }

        public TimeslotId(Guid value)
        {
            // if (value == default)
            //    throw new ArgumentNullException(nameof(value), value + " cannot be empty ");

            Value = value;
        }

        public static implicit operator Guid(TimeslotId self) => self.Value;

        public static implicit operator TimeslotId(string value)
            => new TimeslotId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

        public bool Equals(TimeslotId other)
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
            return Equals((TimeslotId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}