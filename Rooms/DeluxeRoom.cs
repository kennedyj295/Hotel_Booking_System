using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Rooms
{
    public class DeluxeRoom : Room
    {
        public bool HasJacuzzi { get; set; }

        public DeluxeRoom(int roomId, int roomNumber, decimal price, bool smoking, bool hasJacuzzi) : base(roomId, roomNumber, price, "Deluxe", smoking) 
        {
            HasJacuzzi = hasJacuzzi;
        }
    }
}
