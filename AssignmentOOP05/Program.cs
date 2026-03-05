using System.Security.AccessControl;

namespace AssignmentOOP05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part1
            #region Q1
            //An interface in C# is a contract that defines a set of methods, properties..etc without implementing them. It specifies what a class must do
            //we use it to reduce dependency and to achieve abstraction and polymorphism in our code also Instead of depending on a specific class, we depend on a contract (interface).

            /*
              1- loose coupling : which makes the system easier to modify becuase classes depends on interfaces not on specific implementations
              2- Multiple Implementation : Different classes can implement the same interface in different ways.
              3- Better Maintainability : Changes to the implementation of an interface do not affect the classes that use it
            */
            #endregion

            #region Q2
            // both interfaces share the same implementation, which means the program cannot differentiate between English greeting and Arabic greeting.

            //We fix it using Explicit Interface Implementation so Each interface method is implemented separately.

            //No, you cannot call translator.Greet directly. Because when we use explicit interface implementation the methods are not accessible through the class itself.
            #endregion

            #region Q3  
            /*A shallow copy creates a new object but copies the references of reference  type fields instead of creating new objects 
             using : 1-Objects contain only value types 2-Shared references are acceptable

            */


            /*A deep copy creates a completely independent copy of the object and all objects it references which means A new object is created and All referenced objects are also duplicated
             * using: 1-Objects contain reference types that need to be duplicated 2-Shared references are not acceptable
            */
            #endregion

            #region Q4
            /*
                Dev - Testing
                QA - Testing 
              explaination: e1.Title remains "Dev" because it was copied separately. Dept.Name becomes "Testing" for both objects because Dept is a shared reference to shallow copy.
             
             */
            #endregion
            #endregion

            #region Part2

            Cinema cinema = new Cinema("Galaxy");

            cinema.OpenCinema();

            StandardTicket t1 = new StandardTicket("Inception", 80, "A5");
            VIPTicket t2 = new VIPTicket("Avengers", 150, true);
            IMAXTicket t3 = new IMAXTicket("Dune", 100, true);

            t1.Book();
            t2.Book();
            t3.Book();

            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);

            cinema.PrintAllTickets();

            Console.WriteLine("--- Clone Test ---");

            VIPTicket clone = (VIPTicket)t2.Clone();
            clone.MovieName = "Interstellar";

            Console.Write("Original : ");
            t2.Print();

            Console.Write("Clone    : ");
            clone.Print();

            Console.WriteLine();

            Console.WriteLine("--- After Cancellation ---");

            t1.Cancel();
            t1.Print();

            Console.WriteLine();

            IPrintable[] printableTickets =
            {
            t1,
            t2,
            t3
        };

            BookingHelper.PrintAll(printableTickets);

            cinema.CloseCinema();

            #endregion

        }

        #region Part2
        public interface IPrintable
        {
            void Print();
        }

        public interface IBookable
        {
            bool Book();
            bool Cancel();
        }

       

        #region Parent Class

        public abstract class Ticket : IPrintable, IBookable, ICloneable
        {
            private static int counter = 0;

            public int TicketId { get; private set; }
            public string MovieName { get; set; }

            private decimal price;

            public decimal Price
            {
                get => price;
                protected set
                {
                    if (value > 0)
                        price = value;
                }
            }

            public decimal PriceAfterTax => Price * 1.14m;

            public bool IsBooked { get; private set; }

            public Ticket(string movie, decimal price)
            {
                counter++;
                TicketId = counter;
                MovieName = movie;
                Price = price;
            }

            public bool Book()
            {
                if (IsBooked)
                    return false;

                IsBooked = true;
                return true;
            }

            public bool Cancel()
            {
                if (!IsBooked)
                    return false;

                IsBooked = false;
                return true;
            }

            public static int GetTotalTickets()
            {
                return counter;
            }

            public abstract void Print();

            public abstract object Clone();
        }

        #endregion

        #region Standerd Ticket

        public class StandardTicket : Ticket
        {
            public string SeatNumber { get; set; }

            public StandardTicket(string movie, decimal price, string seat)
                : base(movie, price)
            {
                SeatNumber = seat;
            }

            public override void Print()
            {
                Console.WriteLine(
                $"[Ticket #{TicketId}] {MovieName} | Standard | Seat: {SeatNumber} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {(IsBooked ? "Yes" : "No")}");
            }

            public override object Clone()
            {
                return new StandardTicket(MovieName, Price, SeatNumber);
            }
        }

        #endregion

        #region VIP Ticket

        public class VIPTicket : Ticket
        {
            public bool LoungeAccess { get; set; }
            public decimal ServiceFee { get; set; } = 50;

            public VIPTicket(string movie, decimal price, bool lounge)
                : base(movie, price + 50)
            {
                LoungeAccess = lounge;
            }

            public override void Print()
            {
                Console.WriteLine(
                $"[Ticket #{TicketId}] {MovieName} | VIP | Lounge: {(LoungeAccess ? "Yes" : "No")} | Fee: {ServiceFee} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {(IsBooked ? "Yes" : "No")}");
            }

            public override object Clone()
            {
                return new VIPTicket(MovieName, Price - ServiceFee, LoungeAccess);
            }
        }

        #endregion

        #region IMax Ticket

        public class IMAXTicket : Ticket
        {
            public bool Is3D { get; set; }

            public IMAXTicket(string movie, decimal price, bool is3D)
                : base(movie, is3D ? price + 30 : price)
            {
                Is3D = is3D;
            }

            public override void Print()
            {
                Console.WriteLine(
                $"[Ticket #{TicketId}] {MovieName} | IMAX | 3D: {(Is3D ? "Yes" : "No")} | Price: {Price} | After Tax: {PriceAfterTax} | Booked: {(IsBooked ? "Yes" : "No")}");
            }

            public override object Clone()
            {
                return new IMAXTicket(MovieName, Price, Is3D);
            }
        }

        #endregion

        #region Projector

        public class Projector
        {
            public void Start()
            {
                Console.WriteLine("Projector started.");
            }

            public void Stop()
            {
                Console.WriteLine("Projector stopped.");
            }
        }

        #endregion

        #region Cinema

        public class Cinema
        {
            public string CinemaName { get; set; }

            private Ticket[] tickets = new Ticket[20];

            private Projector projector = new Projector();

            public Cinema(string name)
            {
                CinemaName = name;
            }

            public void OpenCinema()
            {
                Console.WriteLine("=== Cinema Opened ===");
                projector.Start();
                Console.WriteLine();
            }

            public void CloseCinema()
            {
                Console.WriteLine();
                Console.WriteLine("=== Cinema Closed ===");
                projector.Stop();
            }

            public void AddTicket(Ticket t)
            {
                for (int i = 0; i < tickets.Length; i++)
                {
                    if (tickets[i] == null)
                    {
                        tickets[i] = t;
                        return;
                    }
                }
            }

            public void PrintAllTickets()
            {
                Console.WriteLine("--- All Tickets ---");

                foreach (var t in tickets)
                {
                    if (t != null)
                        t.Print();
                }

                Console.WriteLine();
            }
        }

        #endregion

        #region Booking Helper

        public static class BookingHelper
        {
            public static void PrintAll(IPrintable[] items)
            {
                Console.WriteLine("--- BookingHelper.PrintAll ---");

                foreach (var item in items)
                {
                    item.Print();
                }

                Console.WriteLine();
            }
        }

        #endregion 
        #endregion
    }
}
