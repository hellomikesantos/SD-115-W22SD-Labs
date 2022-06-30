namespace SD_115_W22SD_Labs.Models
{
    public class Room
    {
        public int Number { get; set; }
        public int Capacity { get; set; }
        public bool Occupied { get; set; } = false;
        public string Rating { get; set; }
        public int CurrentOccupants { get; set; } = 0;
        public Reservation Reservation { get; set; }
        public Room(int number, int capacity, string rating)
        {
            Number = number;
            Capacity = capacity;
            Rating = rating;
        }
    }
}
