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

        public async Task<int?> TryAccept(Reservation reservation)
        {
            var reservations =
                await Repository.ReadReservations(reservation.Date);
            int reservedSeats = reservations.Sum(r => r.Quantity);

            if (Capacity < reservedSeats + reservation.Quantity)
                return null;

            reservation.IsAccepted = true;
            return await Repository.Create(reservation);
        }
    }
}
