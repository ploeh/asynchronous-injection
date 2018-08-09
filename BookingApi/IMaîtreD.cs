using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi
{
    public interface IMaîtreD
    {
        Task<Maybe<Reservation>> TryAccept(
            Reservation[] reservations,
            Reservation reservation);
    }
}
