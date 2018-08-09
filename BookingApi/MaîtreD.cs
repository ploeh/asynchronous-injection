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

        public async Task<Maybe<int>> TryAccept(
            Reservation[] reservations,
            Reservation reservation)
        {
            int reservedSeats = reservations.Sum(r => r.Quantity);

            if (Capacity < reservedSeats + reservation.Quantity)
                return new Maybe<int>();

            reservation.IsAccepted = true;
            var id = await Repository.Create(reservation);
            return new Maybe<int>(id);
        }
    }
}
