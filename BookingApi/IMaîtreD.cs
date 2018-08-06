using System;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.BookingApi
{
    public interface IMaîtreD
    {
        int? TryAccept(Reservation reservation);
    }
}
