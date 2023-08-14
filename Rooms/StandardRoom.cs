using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Rooms
{
    public class StandardRoom : Room
    {
        public string? BedType { get; set; }

        public bool HasBalcony { get; set; }

        public StandardRoom(int roomId, int roomNumber, decimal price, bool smoking, string? bedType, bool hasBalcony) : base(roomId, roomNumber, price, "Standard", smoking)
        {
            BedType = bedType;
            HasBalcony = hasBalcony;
        }

        public StandardRoom(int roomId, int roomNumber, decimal price, string? roomType, bool smoking) : base(roomId, roomNumber, price, roomType, smoking)
        {
        }

        public StandardRoom(int roomNumber, decimal price, bool smoking, string? bedType, bool hasBalcony) : base(roomNumber, price, "Standard", smoking)
        {
            BedType = bedType;
            HasBalcony = hasBalcony;
        }

        public StandardRoom(int roomNumber, decimal price, string? roomType, bool smoking) : base(roomNumber, price, roomType, smoking)
        {
        }
    }
}
