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
        public async Task TryAcceptReturnsReservationIdInHappyPathScenario(
            [Frozen]Mock<IReservationsRepository> td,
            Reservation reservation,
            Reservation[] reservations,
            MaîtreD sut,
            int excessCapacity,
            int expected)
        {
            td
                .Setup(r => r.Create(reservation))
                .Returns(Task.FromResult(expected));
            var reservedSeats = reservations.Sum(r => r.Quantity);
            reservation.IsAccepted = false;
            sut = sut.WithCapacity(
                reservedSeats + reservation.Quantity + excessCapacity);

            var actual = await sut.TryAccept(reservations, reservation);

            Assert.Equal(new Maybe<int>(expected), actual);
            Assert.True(reservation.IsAccepted);
        }

        [Theory, BookingApiTestConventions]
        public async Task TryAcceptReturnsNullOnInsufficientCapacity(
            Reservation reservation,
            Reservation[] reservations,
            MaîtreD sut)
        {
            var reservedSeats = reservations.Sum(r => r.Quantity);
            reservation.IsAccepted = false;
            sut = sut.WithCapacity(reservedSeats + reservation.Quantity - 1);

            var actual = await sut.TryAccept(reservations, reservation);

            Assert.True(actual.IsNothing);
            Assert.False(reservation.IsAccepted);
        }
    }
}
