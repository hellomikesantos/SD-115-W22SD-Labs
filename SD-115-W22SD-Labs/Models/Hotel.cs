// Lab-1
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
