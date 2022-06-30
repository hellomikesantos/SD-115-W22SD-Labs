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

            Room room1 = new Room("101", 4, "Regular");
            Room room2 = new Room("102", 4, "Regular");
            Room room3 = new Room("103", 2, "Premium");

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
        
        static Room GetRoom(string roomNumber)
        {
            Room room = Rooms.First(room => room.Number == roomNumber);
            return room;
        }

        static List<Room> GetVacantRooms()
        {
            List<Room> vacantRooms = Rooms.Where(room => !room.Occupied).ToList();
            return vacantRooms;
        }

        static List<Client> TopThreeClients()
        {

            List<Client> topClients = Clients.OrderByDescending(client => client.Reservations.Count).ToList();
            List<Client> topThreeClients = topClients.Where(client => client.IdCounter <= 3).ToList();
            return topThreeClients;
        }

        static Reservation AutomaticReservation(int clientId, int occupants)
        {
            List<Room> occupiableRooms = Rooms.Where(room => room.Capacity >= occupants).ToList();
            Room room = occupiableRooms.First(room => !room.Occupied);

            Client client = Clients.First(client => client.IdCounter == clientId);

            Reservation reservation = new Reservation(room, client);
            return reservation;
        }

    }
}
