using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Hotel_Booking_System.Bookings;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Hotel_Booking_System.Rooms;
using System.Data;

namespace Hotel_Booking_System.DataConnectivity
{
    public class SQLConnector
    {
        private string _connectionString;

        public SQLConnector(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT b.BookingID, b.RoomID, r.RoomNumber, r.Price, r.RoomType, r.Smoking, s.BedType, s.HasBalcony, d.HasJacuzzi, b.Rate, b.CheckInDate, b.CheckOutDate, b.GuestName, b.IsDeluxe FROM Bookings b JOIN Rooms r ON b.RoomID = r.RoomID LEFT JOIN StandardRooms s ON r.RoomID = s.RoomID LEFT JOIN DeluxeRooms d ON r.RoomID = d.RoomID;";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Room room;

                    if (reader.GetBoolean(reader.GetOrdinal("IsDeluxe")))
                    {
                        room = new DeluxeRoom
                        (
                            reader.GetInt32(reader.GetOrdinal("RoomId")),
                            reader.GetInt32(reader.GetOrdinal("RoomNumber")),
                            reader.GetDecimal(reader.GetOrdinal("Price")),
                            reader.GetBoolean(reader.GetOrdinal("Smoking")),
                            reader.GetBoolean(reader.GetOrdinal("HasJacuzzi"))
                        );
                    }
                    else
                    {
                        room = new StandardRoom
                        (
                            reader.GetInt32(reader.GetOrdinal("RoomId")),
                            reader.GetInt32(reader.GetOrdinal("RoomNumber")),
                            reader.GetDecimal(reader.GetOrdinal("Price")),
                            reader.GetBoolean(reader.GetOrdinal("Smoking")),
                            reader.GetString(reader.GetOrdinal("BedType")),
                            reader.GetBoolean(reader.GetOrdinal("HasBalcony"))
                        );
                    }


                    Booking booking = new Booking
                    (
                        reader.GetInt32(reader.GetOrdinal("BookingID")),
                        reader.GetInt32(reader.GetOrdinal("RoomID")),
                        room,
                        reader.GetDecimal(reader.GetOrdinal("Rate")),
                        reader.GetDateTime(reader.GetOrdinal("CheckInDate")),
                        reader.GetDateTime(reader.GetOrdinal("CheckOutDate")),
                        reader.GetString(reader.GetOrdinal("GuestName")),
                        reader.GetBoolean(reader.GetOrdinal("IsDeluxe"))
                    );

                    bookings.Add(booking);
                }
            }
            return bookings;
        }

        public List<Room> GetAvailableRoomsFromDb()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT r.RoomID, r.RoomNumber, r.Price, r.RoomType, r.Smoking, s.BedType, s.HasBalcony, d.HasJacuzzi FROM Rooms r LEFT JOIN StandardRooms s ON r.RoomID = s.RoomID LEFT JOIN DeluxeRooms d ON r.RoomID = d.RoomID WHERE r.Availability = 1;";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Room room;

                    if (reader.GetString(reader.GetOrdinal("RoomType")) == "Deluxe")
                    {
                        room = new DeluxeRoom
                        (
                            reader.GetInt32(reader.GetOrdinal("RoomId")),
                            reader.GetInt32(reader.GetOrdinal("RoomNumber")),
                            reader.GetDecimal(reader.GetOrdinal("Price")),
                            reader.GetBoolean(reader.GetOrdinal("Smoking")),
                            reader.GetBoolean(reader.GetOrdinal("HasJacuzzi"))
                        );
                    }
                    else
                    {
                        room = new StandardRoom
                        (
                            reader.GetInt32(reader.GetOrdinal("RoomId")),
                            reader.GetInt32(reader.GetOrdinal("RoomNumber")),
                            reader.GetDecimal(reader.GetOrdinal("Price")),
                            reader.GetBoolean(reader.GetOrdinal("Smoking")),
                            reader.GetString(reader.GetOrdinal("BedType")),
                            reader.GetBoolean(reader.GetOrdinal("HasBalcony"))
                        );
                    }

                    rooms.Add(room);
                }
            }
            return rooms;
        }

        public bool AddBookingToDB(int roomID, decimal rate, DateTime checkInDate, DateTime checkOutDate, string guestName, bool isDeluxe) 
        {
            string query = "INSERT INTO Bookings (RoomID, Rate, CheckInDate, CheckOutDate, GuestName, IsDeluxe) VALUES (@RoomID, @Rate, @CheckInDate, @CheckOutDate, @GuestName, @IsDeluxe)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomID", roomID);
                command.Parameters.AddWithValue("@Rate", rate);
                command.Parameters.AddWithValue("@CheckInDate", checkInDate);
                command.Parameters.AddWithValue("@CheckOutDate", checkOutDate);
                command.Parameters.AddWithValue("@GuestName", guestName);
                command.Parameters.AddWithValue("@IsDeluxe", isDeluxe);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected == 1;
            }
        }

        public Room GetRoomByRoomNumber(int roomNumber)
        {
            Room room = null;

            string query = "SELECT * FROM Rooms WHERE RoomNumber = @RoomNumber";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    room = new StandardRoom
                    (
                        reader.GetInt32(reader.GetOrdinal("RoomNumber")),
                        reader.GetDecimal(reader.GetOrdinal("Price")),
                        reader.GetString(reader.GetOrdinal("RoomType")),
                        reader.GetBoolean(reader.GetOrdinal("Smoking"))
                    );
                }
            }

            return room;
        }

        public bool checkRoomAvailability(int roomNumber)
        {
            bool isAvailable = false;
            string query = "SELECT Availability FROM Rooms WHERE RoomNumber = @RoomNumber";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isAvailable = reader.GetBoolean(reader.GetOrdinal("Availability"));
                }
            }

            return isAvailable;
        }

    }
}
