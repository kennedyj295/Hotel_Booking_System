using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel_Booking_System.Bookings;
using Hotel_Booking_System.Rooms;

namespace Hotel_Booking_System
{
    public class ConsoleHelper
    {
        public static void ViewAvailableRooms(BookingManager bookingManager)
        {
            List<Room> rooms = bookingManager.GetAvailableRooms();

            foreach (Room room in rooms)
            {
                if (room is DeluxeRoom deluxeRoom)
                {
                    Console.WriteLine($"Deluxe Room - Room Number: {deluxeRoom.RoomNumber}, Price: {deluxeRoom.Price}, Has Jacuzzi: {deluxeRoom.HasJacuzzi}");
                }
                else if (room is StandardRoom standardRoom)
                {
                    Console.WriteLine($"Standard Room - Room Number: {standardRoom.RoomNumber}, Price: {standardRoom.Price}, Bed Type: {standardRoom.BedType}, Has Balcony: {standardRoom.HasBalcony}");
                }
            }
            Console.ReadLine();
        }

        public static void MakeABooking(BookingManager bookingManager)
        {
            Console.WriteLine("Enter the Room Number:");
            int roomNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Check-in Date (YYYY-MM-DD):");
            DateTime checkInDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter Check-out Date (YYYY-MM-DD):");
            DateTime checkOutDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter the Guest Name:");
            string guestName = Console.ReadLine();

            Room roomToBeBooked = bookingManager.GetSingleRoomByRoomNumber(roomNumber);
            bool roomType = roomToBeBooked?.RoomType?.ToLower() == "deluxe" ? true : false;

            int numberOfNights = (checkOutDate - checkInDate).Days;
            decimal roomRate = roomToBeBooked.CalculateRoomRate(numberOfNights, 0);

            Booking booking = new Booking(roomToBeBooked.RoomId, roomRate, checkInDate, checkOutDate, guestName, roomType);
            bookingManager.AddSingleBooking(booking, checkInDate, checkOutDate);

            Console.WriteLine("Booking Successful!");
        }

        public static void CancelABooking(BookingManager bookingManager)
        {
            Console.WriteLine("Please enter your booking ID");
            int bookingId = int.Parse(Console.ReadLine());

            bookingManager.CancelBooking(bookingId);

            Console.WriteLine("Cancellation successful!");
        }

        public bool IsRoomAvailable(int roomNumber, BookingManager bookingManager)
        {
            return bookingManager.CheckSingleRoomAvailability(roomNumber);
        }

    }
}
