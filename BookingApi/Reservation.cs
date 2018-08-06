using System;
using System.Collections.Generic;
using System.Text;

namespace Ploeh.Samples.BookingApi
{
    public class Reservation
    {
        public DateTimeOffset Date { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsAccepted { get; set; }
    }
}
