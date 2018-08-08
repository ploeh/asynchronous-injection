using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi
{
    public interface IMaîtreD
    {
        Task<int?> TryAccept(
            Reservation[] reservations,
            Reservation reservation);
    }
}
