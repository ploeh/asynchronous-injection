using System;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.BookingApi
{
    public class MaîtreD : IMaîtreD
    {
        public int? TryAccept(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
