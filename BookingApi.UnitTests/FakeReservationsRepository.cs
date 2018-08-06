using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class FakeReservationsRepository : IReservationsRepository
    {
        private readonly List<Reservation> reservations;

        public FakeReservationsRepository()
        {
            reservations = new List<Reservation>();
        }

        public int Create(Reservation reservation)
        {
            reservations.Add(reservation);
            // Hardly a robut implementation, since indices will be reused,
            // but should be good enough for the purpose of a pair of
            // integration tests
            return reservations.IndexOf(reservation);
        }

        public Reservation[] ReadReservations(DateTimeOffset date)
        {
            var firstTick = date.Date;
            var lastTick = firstTick.AddDays(1).AddTicks(-1);
            return reservations
                .Where(r => firstTick <= r.Date && r.Date <= lastTick)
                .ToArray();
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
