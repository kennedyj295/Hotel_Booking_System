using Hotel_Booking_System.Bookings;
using Hotel_Booking_System.DataConnectivity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

namespace Hotel_Booking_System
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build())
                .AddTransient<SQLConnector>()
                .AddTransient<BookingManager>()
                .BuildServiceProvider();

            // Resolve BookingManager
            var bookingManager = serviceProvider.GetService<BookingManager>();

            while (true)
            {
                Console.WriteLine("Welcome to the Hotel Booking System");
                Console.WriteLine("1. View Available Rooms");
                Console.WriteLine("2. Make a Booking");
                Console.WriteLine("3. Cancel a Booking");
                Console.WriteLine("4. Exit");
                Console.Write("Please select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ConsoleHelper.ViewAvailableRooms(bookingManager);
                        break;
                    case "2":
                        ConsoleHelper.MakeABooking(bookingManager);
                        break;
                    case "3":
                        ConsoleHelper.CancelABooking(bookingManager);
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using the Hotel Booking System!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
          
        }
    }
}
