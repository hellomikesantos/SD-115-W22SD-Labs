﻿namespace SD_115_W22SD_Labs.Models
{
    public class Client
    {
        public int IdCounter { get; set; } = 1;
        public Room Room { get; set; }
        public string Name { get; set; }
        public long CreditCard { get; set; }
        public string Membership { get; set; }
        public List<Reservation> Reservations { get; set; }
        public Reservation Reservation { get; set; }
        public Client(string name, long creditCard, string membership)
        {
            Name = name;
            CreditCard = creditCard;
            Membership = membership;
            IdCounter++;
        }
    }
}
