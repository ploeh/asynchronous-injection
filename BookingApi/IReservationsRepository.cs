using System;

namespace Ploeh.Samples.BookingApi
{
    public interface IReservationsRepository
    {
        Reservation[] ReadReservations(DateTimeOffset date);

        int Create(Reservation reservation);
    }
}