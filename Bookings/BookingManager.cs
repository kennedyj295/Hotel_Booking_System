using Hotel_Booking_System.DataConnectivity;
using Hotel_Booking_System.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Bookings
{
    public class BookingManager : IBookable
    {
        private List<Booking> Bookings;
        private readonly SQLConnector _sqlConnector;

        public BookingManager(SQLConnector sqlConnector, IEnumerable<Booking>? bookings = null)
        {
            _sqlConnector = sqlConnector;
            Bookings = bookings != null ? new List<Booking>(bookings) : new List<Booking>();
        }

        public bool AddBooking(Booking booking, DateTime checkInDate, DateTime checkOutDate)
        {
            return _sqlConnector.AddBookingToDB(booking.BookedRoomId, booking.Rate, checkInDate, checkOutDate, booking.GuestName, booking.IsDeluxe); 
        }

        public bool CancelBooking()
        {
            bool t = false;
            return t;
        }

        public List<Booking> GetBookingList()
        {
            return _sqlConnector.GetAllBookings();
        }

        public List<Room> GetAvailableRooms() 
        {
            return _sqlConnector.GetAvailableRoomsFromDb();
        }

        public bool CheckSingleRoomAvailability(int roomNumber)
        {
            return _sqlConnector.checkRoomAvailability(roomNumber);
        }

        public Room GetSingleRoomByRoomNumber(int roomNumber)
        {
            return _sqlConnector.GetRoomByRoomNumber(roomNumber);
        }

        public bool BookRoom()
        {
            throw new NotImplementedException();
        }

        public bool UpdateBooking()
        {
            throw new NotImplementedException();
        }

        public Booking GetBookingById(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
