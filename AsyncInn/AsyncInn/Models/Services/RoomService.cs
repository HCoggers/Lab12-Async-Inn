using AsyncInn.Data;
using AsyncInn.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomService : IRoomManager
    {
        private readonly AsyncInnDbContext _context;
        private readonly IAmenitiesManager _amenities;

        public RoomService(AsyncInnDbContext context, IAmenitiesManager amenities)
        {
            _context = context;
            _amenities = amenities;
        }

        public async Task CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var toDelete = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            var roomAmenities = await GetRoomAmenities(room.ID);
            List<AmenitiesDTO> amenityDTOs = new List<AmenitiesDTO>();
            if (roomAmenities != null)
            {
                foreach (var RoomAmenity in roomAmenities)
                {
                    var AmenityDTO = new AmenitiesDTO
                    {
                        ID = RoomAmenity.ID,
                        Name = RoomAmenity.Name
                    };
                    amenityDTOs.Add(AmenityDTO);
                }
            }

            RoomDTO roomDTO = new RoomDTO
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout.ToString(),
                Amenities = amenityDTOs
            };
                
            return roomDTO;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach(var room in rooms)
            {
                List<AmenitiesDTO> amenityDTOs = new List<AmenitiesDTO>();
                if (room.RoomAmenities != null)
                {
                    foreach (var RoomAmenity in room.RoomAmenities)
                    {
                        var AmenityDTO = new AmenitiesDTO
                        {
                            ID = RoomAmenity.AmenitiesID,
                            Name = _context.Amenities.Find(RoomAmenity.AmenitiesID).Name
                        };
                        amenityDTOs.Add(AmenityDTO);
                    }
                }

                roomDTOs.Add(new RoomDTO
                {
                    ID = room.ID,
                    Name = room.Name,
                    Layout = room.Layout.ToString(),
                    Amenities = amenityDTOs
                });
            }
            return roomDTOs;
        }

        public async Task UpdateRoom(Room room, int id)
        {
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Amenities>> GetRoomAmenities(int roomId)
        {
            var roomAmenities = await _context.RoomAmenities.Where(roomAmenity => roomAmenity.RoomID == roomId)
                .Include(roomAmenity => roomAmenity.Amenities)
                .Select(amenity => amenity.Amenities)
                .ToListAsync();
            return roomAmenities;
        }

    }
}
