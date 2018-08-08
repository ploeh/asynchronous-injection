using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class ReservationsIntegrationTests
    {
        [Fact]
        public async Task ReservationSucceeds()
        {
            var repo = new FakeReservationsRepository();
            var sut = new ReservationsController(new MaîtreD(10, repo), repo);

            var reservation = new Reservation
            {
                Date = new DateTimeOffset(2018, 8, 13, 16, 53, 0, TimeSpan.FromHours(2)),
                Email = "mark@example.com",
                Name = "Mark Seemann",
                Quantity = 4
            };
            var actual = await sut.Post(reservation);

            Assert.True(reservation.IsAccepted);
            Assert.True(repo.Contains(reservation));
            var expectedId = repo.GetId(reservation);
            var ok = Assert.IsAssignableFrom<OkActionResult>(actual);
            Assert.Equal(expectedId, ok.Value);
        }

        [Fact]
        public async Task ReservationFails()
        {
            var repo = new FakeReservationsRepository();
            var sut = new ReservationsController(new MaîtreD(10, repo), repo);

            var reservation = new Reservation
            {
                Date = new DateTimeOffset(2018, 8, 13, 16, 53, 0, TimeSpan.FromHours(2)),
                Email = "mark@example.com",
                Name = "Mark Seemann",
                Quantity = 11
            };
            var actual = await sut.Post(reservation);

            Assert.False(reservation.IsAccepted);
            Assert.False(repo.Contains(reservation));
            Assert.IsAssignableFrom<InternalServerErrorActionResult>(actual);
        }
    }
}
