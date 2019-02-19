using System;

namespace Ploeh.Samples.BookingApi
{
    public sealed class Reservation
    {
        public Reservation(
            DateTimeOffset date,
            string email,
            string name,
            int quantity) : this(date, email, name, quantity, false)
        { }

        private Reservation(DateTimeOffset date,
            string email,
            string name,
            int quantity,
            bool isAccepted)
        {
            Date = date;
            Email = email;
            Name = name;
            Quantity = quantity;
            IsAccepted = isAccepted;
        }

        public DateTimeOffset Date { get; }
        public string Email { get; }
        public string Name { get; }
        public int Quantity { get; }
        public bool IsAccepted { get; }

        public Reservation Accept()
        {
            return new Reservation(
                Date,
                Email,
                Name,
                Quantity,
                true);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Reservation other))
                return false;

            return Equals(Date, other.Date)
                && Equals(Email, other.Email)
                && Equals(Name, other.Name)
                && Equals(Quantity, other.Quantity)
                && Equals(IsAccepted, other.IsAccepted);
        }

        public override int GetHashCode()
        {
            return
                Date.GetHashCode() ^
                Email.GetHashCode() ^
                Name.GetHashCode() ^
                Quantity.GetHashCode() ^
                IsAccepted.GetHashCode();
        }
    }
}
