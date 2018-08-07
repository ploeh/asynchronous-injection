using System;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi
{
    public interface IReservationsRepository
    {
        Task<Reservation[]> ReadReservations(DateTimeOffset date);

        Task<int> Create(Reservation reservation);
    }
}