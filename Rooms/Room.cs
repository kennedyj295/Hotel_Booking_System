using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Rooms
{
    public abstract class Room
    {
        protected int RoomId;
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }

        protected string? RoomType { get; set; } = null;

        protected bool Smoking { get; set; }

        protected Room(int roomId, int roomNumber, decimal price, string? roomType, bool smoking) 
        {
            RoomId = roomId;
            RoomNumber = roomNumber;
            Price = price;
            RoomType = roomType;
            Smoking = smoking;
        }

        protected Room(int roomNumber, decimal price, string? roomType, bool smoking)
        {
            RoomNumber = roomNumber;
            Price = price;
            RoomType = roomType;
            Smoking = smoking;
        }

        public decimal CalculateRoomRate(int numberOfNights, decimal discount)
        {
            return Price * discount * numberOfNights;
        }
    }
}
