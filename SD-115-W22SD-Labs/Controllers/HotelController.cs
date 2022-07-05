using Microsoft.AspNetCore.Mvc;
using SD_115_W22SD_Labs.Models;

namespace SD_115_W22SD_Labs.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllRooms()
        {
            List<Room> allRooms = Hotel.Rooms.ToList();
            return View(allRooms);
        }

        public IActionResult TotalCapacity()
        {
            List<Room> allRooms = Hotel.Rooms.ToList();
            foreach (Room room in allRooms)
            {
                room.RemainingCapacity = room.Capacity - room.CurrentOccupants;
            }

            ViewBag.Message = "No capacity remaining.";
            return View(allRooms);
        }

        public IActionResult RoomRemaining()
        {
            List<Room> allRooms = Hotel.Rooms.ToList();
            foreach(Room room in allRooms)
            {
                room.RemainingCapacity = room.Capacity - room.CurrentOccupants;
            }
            return View(allRooms);
        }

        public IActionResult AvailableRoom(int occupants)
        {
            try
            {
                List<Room> rooms = Hotel.Rooms.Where(room => room.Capacity >= occupants).ToList();
                Room room = rooms[0];
                if (rooms.Count < 1)
                {
                    return RedirectToAction("Error");
                }

                for (int i = 0; i < rooms.Count - 1; i++)
                {
                    for (int j = 1; j < rooms.Count; j++)
                    {
                        if (rooms[i].Number < rooms[j].Number)
                        {
                            room = rooms[i];
                        }
                        else if (rooms[j].Number < rooms[i].Number)
                        {
                            room = rooms[j];
                            break;
                        }
                    }
                }
                return View(room);
            }
            catch
            {
                return View();
            }
            
        }
    }
}
