using AutoFixture.Xunit2;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class MaîtreDTests
    {
        [Theory, BookingApiTestConventions]
        public void TryAcceptReturnsReservationIdInHappyPathScenario(
            [Frozen]Mock<IReservationsRepository> td,
            Reservation reservation,
            Reservation[] reservations,
            MaîtreD sut,
            int excessCapacity,
            int expected)
        {
            td
                .Setup(r => r.ReadReservations(reservation.Date))
                .Returns(reservations);
            td.Setup(r => r.Create(reservation)).Returns(expected);
            var reservedSeats = reservations.Sum(r => r.Quantity);
            reservation.IsAccepted = false;
            sut = sut.WithCapacity(
                reservedSeats + reservation.Quantity + excessCapacity);

            var actual = sut.TryAccept(reservation);

            Assert.Equal(expected, actual);
            Assert.True(reservation.IsAccepted);
        }

        [Theory, BookingApiTestConventions]
        public void TryAcceptReturnsNullOnInsufficientCapacity(
            [Frozen]Mock<IReservationsRepository> td,
            Reservation reservation,
            Reservation[] reservations,
            MaîtreD sut)
        {
            td
                .Setup(r => r.ReadReservations(reservation.Date))
                .Returns(reservations);
            var reservedSeats = reservations.Sum(r => r.Quantity);
            reservation.IsAccepted = false;
            sut = sut.WithCapacity(reservedSeats + reservation.Quantity - 1);

            var actual = sut.TryAccept(reservation);

            Assert.Null(actual);
            Assert.False(reservation.IsAccepted);
        }
    }
}
