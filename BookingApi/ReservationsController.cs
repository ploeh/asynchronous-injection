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
            var reservations =
                await Repository.ReadReservations(reservation.Date);
            Maybe<Reservation> m =
                new MaîtreD(Capacity).TryAccept(reservations, reservation);
            return await m
                .Select(async r => await Repository.Create(r))
                .Match(
                    nothing: Task.FromResult(InternalServerError("Table unavailable")),
                    just: async id => Ok(await id));
        }
    }
}
