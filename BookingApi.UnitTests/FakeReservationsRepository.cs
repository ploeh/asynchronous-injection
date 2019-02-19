using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class FakeReservationsRepository : IReservationsRepository
    {
        private readonly List<Reservation> reservations;

        public FakeReservationsRepository()
        {
            reservations = new List<Reservation>();
        }

        public Task<int> Create(Reservation reservation)
        {
            reservations.Add(reservation);
            // Hardly a robust implementation, since indices will be reused,
            // but should be good enough for the purpose of a pair of
            // integration tests
            return Task.FromResult(reservations.IndexOf(reservation));
        }

        public Task<Reservation[]> ReadReservations(DateTimeOffset date)
        {
            var firstTick = date.Date;
            var lastTick = firstTick.AddDays(1).AddTicks(-1);
            var filteredReservations = reservations
                .Where(r => firstTick <= r.Date && r.Date <= lastTick)
                .ToArray();
            return Task.FromResult(filteredReservations);
        }

        public bool Contains(Reservation reservation)
        {
            return reservations.Contains(reservation);
        }

        public int GetId(Reservation reservation)
        {
            return reservations.IndexOf(reservation);
        }
    }
}