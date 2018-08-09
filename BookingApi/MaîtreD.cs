using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

#pragma warning disable 1998
        public async Task<Maybe<Reservation>> TryAccept(
            Reservation[] reservations,
            Reservation reservation)
        {
            int reservedSeats = reservations.Sum(r => r.Quantity);

            if (Capacity < reservedSeats + reservation.Quantity)
                return new Maybe<Reservation>();

            reservation.IsAccepted = true;
            return new Maybe<Reservation>(reservation);
        }
#pragma warning restore 1998
    }
}
