using Hotel_Booking_System.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Bookings
{
    public class Booking
    {

        public int BookingId { get; set; }
        public int BookedRoomId { get; set; }
        public Room? BookedRoom { get; set; }
        public decimal Rate { get; set; }
        public DateRange DateRange { get; set; }
        public string? GuestName { get; set; }
        public bool IsDeluxe { get; set; }

        public Booking(int bookingId, int bookedRoomId, Room bookedRoom, decimal rate, DateTime checkInDate, DateTime checkOutDate, string guestName, bool isDeluxe)
        {
            BookingId = bookingId;
            BookedRoomId = bookedRoomId;
            BookedRoom = bookedRoom;
            Rate = rate;
            DateRange = new DateRange(checkInDate, checkOutDate);
            GuestName = guestName;
            IsDeluxe = isDeluxe;
        }

        public Booking(int bookingId, int bookedRoomId, decimal rate, DateTime checkInDate, DateTime checkOutDate, string guestName, bool isDeluxe)
        {
            BookingId = bookingId;
            BookedRoomId = bookedRoomId;
            Rate = rate;
            DateRange = new DateRange(checkInDate, checkOutDate);
            GuestName = guestName;
            IsDeluxe = isDeluxe;
        }
    }
}
