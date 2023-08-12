using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Bookings
{
    public interface IBookable
    {
        bool BookRoom();

        bool CancelBooking();

        bool UpdateBooking();

        List<Booking> GetBookingList();

        Booking GetBookingById(int bookingId);
    }
}
