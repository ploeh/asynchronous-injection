using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.BookingApi
{
    public class MaîtreD : IMaîtreD
    {
        public MaîtreD(int capacity, IReservationsRepository repository)
        {
            Capacity = capacity;
            Repository = repository;
        }

        public int Capacity { get; }
        public IReservationsRepository Repository { get; }

        public int? TryAccept(Reservation reservation)
        {
            var reservations = Repository.ReadReservations(reservation.Date);
            int reservedSeats = reservations.Sum(r => r.Quantity);

            if (Capacity < reservedSeats + reservation.Quantity)
                return null;

            reservation.IsAccepted = true;
            return Repository.Create(reservation);
        }
    }
}
