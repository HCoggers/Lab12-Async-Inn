using AsyncInn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Specifies composite keys for our join table. so EF doesn't build automatic primary keys.
        /// </summary>
        /// <param name="modelBuilder">The model builder that is defining entity relationships</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //public int ID { get; set; }

            //public string Name { get; set; }
            //public string StreetAddress { get; set; }
            //public string City { get; set; }
            //public string State { get; set; }
            //public string Phone { get; set; }
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    ID = 1,
                    Name = "Pike Place Market Hotel",
                    StreetAddress = "1000 Pike St.",
                    City = "Seattle",
                    State = "WA",
                    Phone = "(777)777-7778",
                },
                new Hotel
                {
                    ID=2,
                    Name="Rockefeller Hotel",
                    StreetAddress="32 Rockefeller Center",
                    City="New York City",
                    State="NY",
                    Phone="(212)867-5309"
                },
                new Hotel {
                    ID = 3,
                    Name = "Ballard Inn",
                    StreetAddress = "123 Market St.",
                    City = "Seattle",
                    State = "WA",
                    Phone = "(206)555-4000"
                },
                new Hotel
                {
                    ID = 4,
                    Name = "L-hotel Asynchrone",
                    StreetAddress = "75 Paris Ave.",
                    City = "Paris",
                    State = "TX",
                    Phone = "(111)222-3333"
                },
                new Hotel
                {
                    ID=5,
                    Name = "Dongguan JiuDian",
                    StreetAddress = "8888 Bridge Street",
                    City = "Dongguan, China",
                    State = "Guangdong Province",
                    Phone = "86-888-8888"
                }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    ID = 1,
                    Name = "Rainier Room",
                    Layout = Layout.TwoBedroom
                },
                new Room
                {
                    ID = 2,
                    Name = "Salmon Cookie Room",
                    Layout = Layout.studio
                },
                new Room
                {
                    ID = 3,
                    Name = "Dog Room",
                    Layout = Layout.OneBedroom
                },
                new Room
                {
                    ID = 4,
                    Name = "Cat Room",
                    Layout = Layout.OneBedroom
                },
                new Room
                {
                    ID = 5,
                    Name = "French Room",
                    Layout=Layout.studio
                },
                new Room
                {
                    ID = 6,
                    Name = "Chinese Room",
                    Layout = Layout.TwoBedroom
                }
            );
            modelBuilder.Entity<Amenities>().HasData(
                new Amenities
                {
                    ID=1,
                    Name = "Running water"
                },
                new Amenities
                {
                    ID=2,
                    Name = "Pet spa"
                },
                new Amenities
                {
                    ID=3,
                    Name = "Puppy"
                },
                new Amenities
                {
                    ID=4,
                    Name = "Kitty"
                },
                new Amenities
                {
                    ID=5,
                    Name = "Fortune Teller (complimentary)"
                }
            );

            modelBuilder.Entity<HotelRoom>().HasKey(h => new { h.RoomNumber, h.HotelID });
            modelBuilder.Entity<RoomAmenities>().HasKey(a => new { a.AmenitiesID, a.RoomID });
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }
        public DbSet<Amenities> Amenities { get; set; }

    }
}
