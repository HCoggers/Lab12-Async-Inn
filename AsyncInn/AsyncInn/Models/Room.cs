using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Room
    {
        // PRIMARY KEY
        public int ID { get; set; }

        public string Name { get; set; }
        public Layout Layout { get; set; }

        // Navigation Properties
        public List<RoomAmenities> RoomAmenities { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }
    }

    public enum Layout
    {
        studio = 0,
        OneBedroom = 1,
        TwoBedroom = 2
    }
}
