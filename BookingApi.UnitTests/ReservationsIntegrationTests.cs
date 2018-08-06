using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class ReservationsIntegrationTests
    {
        [Fact]
        public void ReservationSucceeds()
        {
            var repo = new FakeReservationsRepository();
            var sut = new ReservationsController(new MaîtreD(10, repo));

            var reservation = new Reservation
            {
                Date = new DateTimeOffset(2018, 8, 13, 16, 53, 0, TimeSpan.FromHours(2)),
                Email = "mark@example.com",
                Name = "Mark Seemann",
                Quantity = 4
            };
            var actual = sut.Post(reservation);

            Assert.True(reservation.IsAccepted);
            Assert.True(repo.Contains(reservation));
            var expectedId = repo.GetId(reservation);
            var ok = Assert.IsAssignableFrom<OkActionResult>(actual);
            Assert.Equal(expectedId, ok.Value);
        }
    }
}
