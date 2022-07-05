// Lab-2
using Microsoft.AspNetCore.Mvc;

namespace SD_115_W22SD_Labs.Models
{
    public static class Hotel
    {
        public static string Name { get; set; }
        public static string Address { get; set; }
        public static ICollection<Room> Rooms { get; set; }
        public static ICollection<Reservation> Reservations { get; set; }
        public static ICollection<Client> Clients { get; set; }
        static Hotel()
        {
            Name = "Brisk Holiday Palace";
            Address = "77 Evergreen Ave.";
            Rooms = new HashSet<Room>();
            Reservations = new HashSet<Reservation>();
            Clients = new HashSet<Client>();

            Room room1 = new Room(101, 2, "Regular");
            Room room2 = new Room(102, 4, "Regular");
            Room room3 = new Room(103, 2, "Premium");

            Rooms.Add(room1);
            Rooms.Add(room2);
            Rooms.Add(room3);

            Client client1 = new Client("Michael", 12345, "Regular");
            Client client2 = new Client("Oliver", 23433, "Premium");
            Client client3 = new Client("Rev", 23433, "Regular");

            Clients.Add(client1);
            Clients.Add(client2);
            Clients.Add(client3);

        }

        //internal static List<Room> GetVacantRooms(ICollection<Room> rooms)
        //{
        //    throw new NotImplementedException();
        //}

        static Client GetClient(int clientId)
        {
            Client client = Clients.First(client => client.IdCounter == clientId);
            return client;
        }

        static Reservation GetReservation(int id)
        {
            Reservation reservation = Reservations.First(reservation => reservation.IdCounter == id);
            return reservation;
        }
        
        static Room GetRoom(int roomNumber)
        {
            Room room = Rooms.First(room => room.Number == roomNumber);
            return room;
        }

        public static List<Room> GetVacantRooms(List<Room> rooms)
        {
            List<Room> vacantRooms = rooms.Where(room => !room.Occupied).ToList();
            return vacantRooms;
        }

        public static List<Client> TopThreeClients()
        {

            List<Client> topClients = Clients.OrderByDescending(client => client.Reservations.Count).ToList();
            List<Client> topThreeClients = topClients.Where(client => client.IdCounter <= 3).ToList();
            return topThreeClients;
        }

        public static bool WithDuplicate(DateTime date)
        {
            bool withDuplicate = false; ;
            foreach(Reservation reservation in Reservations)
            {
                if(reservation.StartDate == date)
                {
                    withDuplicate = true;
                    break;
                }
                else
                {
                    withDuplicate = false;
                }
            }
            return withDuplicate;
        }

        public static void ReserveRoom(Client client, int roomNumber, DateTime startDate, int occupants)
        {
            foreach (Room room in Rooms)
            {
                if (room.Number == roomNumber 
                    && !room.Occupied 
                    && room.Rating == client.Membership 
                    && !WithDuplicate(client.Reservation.StartDate))
                {
                    Reservation reservation = new Reservation(room, client);
                    if (reservation.Room.Capacity >= occupants)
                    {
                        Reservations.Add(reservation);
                        room.Reservation = reservation;
                        reservation.Client = client;
                        client.Reservation = reservation;
                        break;
                    }
                }
            }
        }

        public static Reservation AutomaticReservation(int clientId, int occupants)
        {
            
            List<Room> occupiableRooms = Rooms.Where(room => room.Capacity >= occupants).ToList();
            Room room = occupiableRooms.First(room => !room.Occupied);
            Client client = GetClient(clientId);

            if (!WithDuplicate(client.Reservation.StartDate))
            {
                // reserve the client
                Reservation reservation = new Reservation(room, client);
                Reservations.Add(reservation);
                room.Reservation = reservation;
                reservation.Client = client;
                client.Reservation = reservation;

                return reservation;
            }
            return null;
        }

        public static void Checkin(string clientName)
        {
            Client client = Clients.First(client => client.Name == clientName);
            client.Reservation.Current = true;
            client.Reservation.Room.Occupied = true;
            client.Reservation.Room.CurrentOccupants = client.Reservation.Occupants;
        }
        public static void CheckoutRoom(int roomNumber)
        {
            Room room = GetRoom(roomNumber);
            // check-out reservation
            room.Reservation.Current = false;
            room.Occupied = false;
            room.CurrentOccupants = room.Reservation.Occupants;
        }
        public static void CheckOutRoom(string clientName)
        {
            Client client = Clients.First(client => client.Name == clientName);
            client.Reservation.Current = true;
            client.Reservation.Room.Occupied = true;
            client.Reservation.Room.CurrentOccupants = client.Reservation.Occupants;
        }

        public static int TotalCapacityRemaining()
        {
            // get total capacity
            int TotalCapacity = 0;
            // get current occupants
            int currentOccupants = 0;
            foreach (Room room in Rooms)
            {
                TotalCapacity += room.Capacity;
                currentOccupants += room.CurrentOccupants;
            }
            int totalRemaining = TotalCapacity - currentOccupants;
            return totalRemaining;
        }

        public static int OccupancyPercentage(Room room)
        {
            int capacity = room.Capacity;
            int currentOccupants = room.CurrentOccupants;

            int percentage = (currentOccupants / capacity) * 100;
            return percentage;

        }

        public static int AverageOccupancyPercentage()
        {
            List<int> percentages = new List<int>();
            foreach (Room room in Rooms)
            {
                int roomOccupancyRate = OccupancyPercentage(room);
                percentages.Add(roomOccupancyRate);
            }
            int averageOccupancyRate = percentages.Sum() / Rooms.Count;
            return averageOccupancyRate;
        }

        public static List<Reservation> FutureBookings()
        {
            List<Reservation> futureReservations = Reservations.Where(reservation => reservation.StartDate > DateTime.Today).ToList();
            return futureReservations;
        }
    }
}
