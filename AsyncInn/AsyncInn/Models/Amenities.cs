using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Amenities
    {
        // PRIMARY KEY
        public int ID { get; set; }

        public string Name { get; set; }

        // Navigation Properties
        public List<RoomAmenities> RoomAmenities {get; set;}
    }
}
