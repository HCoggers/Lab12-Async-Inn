using AsyncInn.Data;
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

        public async Task<Room> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task UpdateRoom(Room room, int id)
        {
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomAmenities>> GetRoomAmenities(int roomId)
        {
            var roomAmenities = await _context.RoomAmenities.Where(roomAmenity => roomAmenity.RoomID == roomId)
                .ToListAsync();
            foreach (var roomAmenity in roomAmenities)
            {
                // For each roomAmenity, set roomAmenity.Amenities roomAmenity.AmmenityID to the matching amenity from the amenities table.
                roomAmenity.Amenities = await _context.Amenities.FindAsync(roomAmenity.AmenitiesID);
            }
            return roomAmenities;
        }

    }
}
