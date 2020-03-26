using System;

namespace Vejledningsbooking.Domain
{
    public class BookingId : IEquatable<BookingId>
    {
        private Guid Value { get; }

        public BookingId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Booking Id cannot be empty");
            
            Value = value;
        }

        public static implicit operator Guid(BookingId self) => self.Value;
        
        public static implicit operator BookingId(string value) 
            => new BookingId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

        public bool Equals(BookingId other)
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
            return Equals((BookingId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}