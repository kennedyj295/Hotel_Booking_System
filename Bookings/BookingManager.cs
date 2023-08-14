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

        public bool AddSingleBooking(Booking booking, DateTime checkInDate, DateTime checkOutDate)
        {
            return _sqlConnector.AddBookingToDB(booking.BookedRoomId, booking.Rate, checkInDate, checkOutDate, booking.GuestName, booking.IsDeluxe); 
        }

        public void AddMultipleBookings(List<Tuple<Booking, DateTime, DateTime>> bookingRequests)
        {
            List<Thread> threads = new List<Thread>();

            foreach (var request in bookingRequests)
            {
                int roomId = request.Item1.BookedRoomId;
                DateTime checkInDate = request.Item2;
                DateTime checkOutDate = request.Item3;
                string? guestName = request.Item1.GuestName;
                decimal rate = request.Item1.Rate;
                bool deluxe = request.Item1.IsDeluxe;

                Thread thread = new Thread(() => _sqlConnector.AddBookingToDB(roomId, rate, checkInDate, checkOutDate, guestName, deluxe));
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        public bool CancelBooking(int id)
        {
            return _sqlConnector.DeleteABooking(id);
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
