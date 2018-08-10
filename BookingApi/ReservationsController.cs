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
            maîtreD = new MaîtreD(capacity);
        }

        public int Capacity { get; }
        public IReservationsRepository Repository { get; }

        private readonly MaîtreD maîtreD;

        public async Task<IActionResult> Post(Reservation reservation)
        {
            return await Repository.ReadReservations(reservation.Date)
                .Select(rs => maîtreD.TryAccept(rs, reservation))
                .SelectMany(m => m.Traverse(Repository.Create))
                .Match(
                    nothing: InternalServerError("Table unavailable"),
                    just: id => Ok(id));
        }
    }
}
