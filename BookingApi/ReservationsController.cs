using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi
{
    public class ReservationsController : ControllerBase
    {
        public ReservationsController(
            int capacity,
            IReservationsRepository repository)
        {
            Capacity = capacity;
            Repository = repository;
        }

        public int Capacity { get; }
        public IReservationsRepository Repository { get; }

        public async Task<IActionResult> Post(Reservation reservation)
        {
            var maîtreD = new MaîtreD(Capacity);

            var reservations =
                await Repository.ReadReservations(reservation.Date);

            return await maîtreD
                .TryAccept(reservations, reservation)
                .Traverse(Repository.Create)
                .Match(
                    nothing: InternalServerError("Table unavailable"), 
                    just: id => Ok(id));
        }
    }
}
