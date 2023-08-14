using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Bookings
{
    public interface IBookable
    {
        bool CancelBooking(int id);
        bool AddSingleBooking(Booking booking, DateTime checkInDate, DateTime checkOutDate);
        bool UpdateBooking();

        List<Booking> GetBookingList();

        Booking GetBookingById(int bookingId);

        public void AddMultipleBookings(List<Tuple<Booking, DateTime, DateTime>> bookingRequests);
    }
}
