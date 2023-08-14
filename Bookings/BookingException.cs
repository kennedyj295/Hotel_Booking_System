using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Bookings
{
    internal class BookingException : Exception
    {
        public BookingException() { }

        public BookingException(string message) : base(message) { }
    }
}
