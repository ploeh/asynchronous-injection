using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class MaîtreDTests
    {
        [Theory, BookingApiTestConventions]
        public void TryAcceptReturnsReservationIdInHappyPathScenario(
            Reservation reservation,
            Reservation[] reservations,
            MaîtreD sut,
            int excessCapacity)
        {
            var reservedSeats = reservations.Sum(r => r.Quantity);
            sut = sut.WithCapacity(
                reservedSeats + reservation.Quantity + excessCapacity);

            var actual = sut.TryAccept(reservations, reservation);

            Assert.Equal(new Maybe<Reservation>(reservation.Accept()), actual);
        }

        [Theory, BookingApiTestConventions]
        public void TryAcceptReturnsNullOnInsufficientCapacity(
            Reservation reservation,
            Reservation[] reservations,
            MaîtreD sut)
        {
            var reservedSeats = reservations.Sum(r => r.Quantity);
            sut = sut.WithCapacity(reservedSeats + reservation.Quantity - 1);

            var actual = sut.TryAccept(reservations, reservation);

            Assert.True(actual.IsNothing);
            Assert.False(reservation.IsAccepted);
        }
    }
}
