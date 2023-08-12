using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Booking_System.Bookings
{
    public struct DateRange
    {
        public DateTime StartDate {  get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date must be after start date");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public int NumberOfDays => (EndDate - StartDate).Days;
    }
}
