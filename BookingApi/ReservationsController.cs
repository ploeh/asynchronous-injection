using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ploeh.Samples.BookingApi
{
    public class ReservationsController : ControllerBase
    {
        public ReservationsController(IMaîtreD maîtreD)
        {
            MaîtreD = maîtreD;
        }

        public IMaîtreD MaîtreD { get; }

        public async Task<IActionResult> Post(Reservation reservation)
        {
            int? id = await MaîtreD.TryAccept(reservation);
            if (id == null)
                return InternalServerError("Table unavailable");

            return Ok(id.Value);
        }
    }
}
