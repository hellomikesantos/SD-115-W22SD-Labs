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

            Room room1 = new Room(101, 4, "Regular");
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
        //static void CreateNewRoom(string roomNumber, int capacity, string rating) // instantiates a new room and sets its properties
        //{
        //    Room room = new Room(roomNumber, capacity, rating, this);
        //    Room = room;
        //    Reservation = Room.Reservation;
        //    Room.Hotel = this;
        //}
        //static void CheckAllRooms() // displays all rooms in this hotel and their details: Room number, max occupants, rating
        //{
        //    Console.WriteLine("=========================== Check All Rooms ====================================");
        //    Console.WriteLine($"All {Rooms.Count} rooms have the following details:");
        //    foreach (Room room in Rooms)
        //    {
        //        Console.WriteLine($"Room: {room.Number} has {room.Occupants} guests/occupants ----- Rating: {room.Rating}, Number: {room.Number}");
        //        //Console.WriteLine($"Rating: {room.Rating}, Number: {room.Number}");
        //    }
        //}
        //static void CheckAllGuests() // displays all guests in this hotel and their details: their checked room, name, membership and how many they are checked-in
        //{
        //    Console.WriteLine($"There are a total of {Clients.Count} clients in {Name}");
        //    Console.WriteLine(Clients.Count);
        //    foreach (Client client in Clients)
        //    {
        //        try
        //        {
        //            Console.WriteLine("++++++++++++++++++++++++++++++ Check All Guests +++++++++++++++++++++++++++++++++++++++++++");
        //            Console.WriteLine($"Guest Name: {client.Name}");
        //            Console.WriteLine($"Guest Membership: {client.Membership}");
        //            Console.WriteLine($"Room No.: {client.Reservation.Room.Number}");
        //            Console.WriteLine($"No. of Occupants: {client.Reservation.Occupants}");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}
        //static void MakeReservation(Client client, string roomNumber, int occupants, DateTime date)
        //{
        //    // handle reservation
        //    foreach (Room r in Rooms) // find the room and check if available and/or capacity available
        //    {
        //        if (r.Number == roomNumber && !r.Occupied && r.Rating == client.Membership)
        //        {
        //            Reservation newReserve = new Reservation(r, this);
        //            if (newReserve.Room.Capacity >= occupants)
        //            {
        //                newReserve.Occupants = occupants;
        //                newReserve.Date = date;
        //                newReserve.IsCurrent = true;
        //                newReserve.Client = client;
        //                client.Reservation = newReserve;
        //                client.Hotel = this;
        //                Clients.Add(client);
        //                Reservations.Add(newReserve);
        //                newReserve.Room.Occupants = occupants;
        //                Console.WriteLine("***************************************************");
        //                Console.WriteLine($"Reservation created successfully:");
        //                Console.WriteLine($"Name: {newReserve.Client.Name}");
        //                Console.WriteLine($"Room: {newReserve.Room.Number}");
        //                Console.WriteLine($"No. of Occupants: {newReserve.Occupants}");
        //                Console.WriteLine($"Reservation Date: {newReserve.Date}");
        //                Console.WriteLine("################### END ################");
        //                break;
        //            }
        //        }
        //    }
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

        static List<Room> GetVacantRooms(List<Room> rooms)
        {
            List<Room> vacantRooms = rooms.Where(room => !room.Occupied).ToList();
            return vacantRooms;
        }

        static List<Client> TopThreeClients()
        {

            List<Client> topClients = Clients.OrderByDescending(client => client.Reservations.Count).ToList();
            List<Client> topThreeClients = topClients.Where(client => client.IdCounter <= 3).ToList();
            return topThreeClients;
        }

        static bool WithDuplicate(DateTime date)
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

        static void ReserveRoom(Client client, int roomNumber, DateTime startDate, int occupants)
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

        static Reservation AutomaticReservation(int clientId, int occupants)
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

        static void Checkin(string clientName)
        {
            Client client = Clients.First(client => client.Name == clientName);
            client.Reservation.Current = true;
            client.Reservation.Room.Occupied = true;
            client.Reservation.Room.CurrentOccupants = client.Reservation.Occupants;
        }
        static void CheckoutRoom(int roomNumber)
        {
            Room room = GetRoom(roomNumber);
            // check-out reservation
            room.Reservation.Current = false;
            room.Occupied = false;
            room.CurrentOccupants = room.Reservation.Occupants;
        }
        static void CheckOutRoom(string clientName)
        {
            Client client = Clients.First(client => client.Name == clientName);
            client.Reservation.Current = true;
            client.Reservation.Room.Occupied = true;
            client.Reservation.Room.CurrentOccupants = client.Reservation.Occupants;
        }

        static int TotalCapacityRemaining()
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

        static int OccupancyPercentage(Room room)
        {
            int capacity = room.Capacity;
            int currentOccupants = room.CurrentOccupants;

            int percentage = (currentOccupants / capacity) * 100;
            return percentage;

        }

        static int AverageOccupancyPercentage()
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

        static List<Reservation> FutureBookings()
        {
            List<Reservation> futureReservations = Reservations.Where(reservation => reservation.StartDate > DateTime.Today).ToList();
            return futureReservations;
        }
    }
}
