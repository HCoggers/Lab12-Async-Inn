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

        public RoomService(AsyncInnDbContext context)
        {
            _context = context;
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
    }
}
