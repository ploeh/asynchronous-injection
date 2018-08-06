using System;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.BookingApi
{
    public class ReservationsController : ControllerBase
    {
        public ReservationsController(IMaîtreD maîtreD)
        {
            MaîtreD = maîtreD;
        }

        public IMaîtreD MaîtreD { get; }

        public IActionResult Post(Reservation reservation)
        {
            int? id = MaîtreD.TryAccept(reservation);
            if (id == null)
                return InternalServerError("Table unavailable");

            return Ok(id.Value);
        }
    }
}
