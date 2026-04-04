using System;
using System.Collections.Generic;

namespace LinkedListProblems
{
    
    public class TicketNode
    {
        public int TicketID { get; set; }
        public string CustomerName { get; set; }
        public string MovieName { get; set; }
        public string SeatNumber { get; set; }
        public DateTime BookingTime { get; set; }
        public TicketNode Next { get; set; }

        public TicketNode(int ticketID, string customerName, string movieName, string seatNumber, DateTime bookingTime)
        {
            TicketID = ticketID;
            CustomerName = customerName;
            MovieName = movieName;
            SeatNumber = seatNumber;
            BookingTime = bookingTime;
            Next = null;
        }

        public override string ToString()
        {
            return $"[ID: {TicketID}, Customer: {CustomerName}, Movie: {MovieName}, Seat: {SeatNumber}, Time: {BookingTime:g}]";
        }
    }

    public class OnlineTicketReservationSystem
    {
        private TicketNode head;

        public OnlineTicketReservationSystem()
        {
            head = null;
        }

        /// <summary>
        /// Add a new ticket reservation at the end of the circular list
        /// </summary>
        public void AddTicketReservation(int ticketID, string customerName, string movieName, string seatNumber)
        {
            TicketNode newTicket = new TicketNode(ticketID, customerName, movieName, seatNumber, DateTime.Now);

            if (head == null)
            {
                head = newTicket;
                head.Next = head; // Make it circular
            }
            else
            {
                TicketNode last = head;
                while (last.Next != head)
                {
                    last = last.Next;
                }

                newTicket.Next = head;
                last.Next = newTicket;
            }

            Console.WriteLine($"Ticket {ticketID} reserved for {customerName} - Movie: {movieName}, Seat: {seatNumber}");
        }

        /// <summary>
        /// Remove a ticket by Ticket ID
        /// </summary>
        public void RemoveTicketByID(int ticketID)
        {
            if (head == null)
            {
                Console.WriteLine("No reservations available");
                return;
            }

            if (head.TicketID == ticketID)
            {
                if (head.Next == head)
                {
                    head = null;
                }
                else
                {
                    TicketNode last = head;
                    while (last.Next != head)
                    {
                        last = last.Next;
                    }
                    last.Next = head.Next;
                    head = head.Next;
                }
                Console.WriteLine($"Ticket {ticketID} cancelled");
                return;
            }

            TicketNode node = head;
            do
            {
                if (node.Next.TicketID == ticketID)
                {
                    node.Next = node.Next.Next;
                    Console.WriteLine($"Ticket {ticketID} cancelled");
                    return;
                }
                node = node.Next;
            } while (node != head);

            Console.WriteLine($"Ticket {ticketID} not found");
        }

        /// <summary>
        /// Display the current tickets in the list
        /// </summary>
        public void DisplayAllTickets()
        {
            if (head == null)
            {
                Console.WriteLine("No reservations available");
                return;
            }

            Console.WriteLine("\n--- Current Reservations ---");
            TicketNode node = head;
            int count = 1;

            do
            {
                Console.WriteLine($"{count}. {node}");
                node = node.Next;
                count++;
            } while (node != head);

            Console.WriteLine();
        }

        /// <summary>
        /// Search for a ticket by Customer Name
        /// </summary>
        public List<TicketNode> SearchByCustomerName(string customerName)
        {
            List<TicketNode> results = new List<TicketNode>();

            if (head == null)
                return results;

            TicketNode node = head;
            do
            {
                if (node.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(node);
                }
                node = node.Next;
            } while (node != head);

            return results;
        }

        /// <summary>
        /// Search for a ticket by Movie Name
        /// </summary>
        public List<TicketNode> SearchByMovieName(string movieName)
        {
            List<TicketNode> results = new List<TicketNode>();

            if (head == null)
                return results;

            TicketNode node = head;
            do
            {
                if (node.MovieName.Equals(movieName, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(node);
                }
                node = node.Next;
            } while (node != head);

            return results;
        }

        /// <summary>
        /// Calculate the total number of booked tickets
        /// </summary>
        public int CalculateTotalBookedTickets()
        {
            if (head == null)
                return 0;

            int count = 1;
            TicketNode node = head.Next;

            while (node != head)
            {
                count++;
                node = node.Next;
            }

            return count;
        }

        /// <summary>
        /// Get total revenue from all bookings
        /// </summary>
        public double GetTotalRevenue(double ticketPrice)
        {
            return CalculateTotalBookedTickets() * ticketPrice;
        }

        /// <summary>
        /// Find ticket by Ticket ID
        /// </summary>
        public TicketNode FindTicketByID(int ticketID)
        {
            if (head == null)
                return null;

            TicketNode node = head;
            do
            {
                if (node.TicketID == ticketID)
                {
                    return node;
                }
                node = node.Next;
            } while (node != head);

            return null;
        }

        /// <summary>
        /// Get movie statistics
        /// </summary>
        public void DisplayMovieStatistics()
        {
            if (head == null)
            {
                Console.WriteLine("No reservations available");
                return;
            }

            Dictionary<string, int> movieCounts = new Dictionary<string, int>();
            TicketNode node = head;

            do
            {
                if (movieCounts.ContainsKey(node.MovieName))
                {
                    movieCounts[node.MovieName]++;
                }
                else
                {
                    movieCounts[node.MovieName] = 1;
                }
                node = node.Next;
            } while (node != head);

            Console.WriteLine("\n--- Movie Statistics ---");
            foreach (var movie in movieCounts)
            {
                Console.WriteLine($"{movie.Key}: {movie.Value} tickets");
            }
            Console.WriteLine();
        }
    }

    // Example Usage
    public class OnlineTicketReservationSystemExample
    {
        public static void Main()
        {
            OnlineTicketReservationSystem system = new OnlineTicketReservationSystem();

            // Add ticket reservations
            system.AddTicketReservation(1, "Alice Johnson", "Avatar", "A1");
            system.AddTicketReservation(2, "Bob Smith", "Avatar", "A2");
            system.AddTicketReservation(3, "Charlie Brown", "Inception", "B1");
            system.AddTicketReservation(4, "Diana Prince", "Inception", "B2");
            system.AddTicketReservation(5, "Eve Wilson", "Avatar", "A3");
            system.AddTicketReservation(6, "Frank Castle", "Titanic", "C1");

            system.DisplayAllTickets();

            // Search by customer name
            Console.WriteLine("Tickets for Alice Johnson:");
            var aliceTickets = system.SearchByCustomerName("Alice Johnson");
            foreach (var ticket in aliceTickets)
            {
                Console.WriteLine(ticket);
            }

            Console.WriteLine("\n");

            // Search by movie name
            Console.WriteLine("Tickets for Avatar:");
            var avatarTickets = system.SearchByMovieName("Avatar");
            foreach (var ticket in avatarTickets)
            {
                Console.WriteLine(ticket);
            }

            Console.WriteLine("\n");

            // Display statistics
            system.DisplayMovieStatistics();

            // Calculate totals
            int totalTickets = system.CalculateTotalBookedTickets();
            double totalRevenue = system.GetTotalRevenue(150.0); // $150 per ticket

            Console.WriteLine($"Total Booked Tickets: {totalTickets}");
            Console.WriteLine($"Total Revenue (at $150/ticket): ${totalRevenue:F2}");

            Console.WriteLine("\n");

            // Cancel a ticket
            system.RemoveTicketByID(3);

            system.DisplayAllTickets();

            Console.WriteLine($"Total Booked Tickets after cancellation: {system.CalculateTotalBookedTickets()}");
        }
    }
}
