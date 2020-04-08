using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class HotelRoomService : IHotelRoomManager
    {
        private readonly AsyncInnDbContext _context;
        public HotelRoomService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task CreateHotelRoom(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var toDelete = await _context.HotelRooms.FindAsync(roomNumber, hotelId);
            _context.HotelRooms.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoom> GetByRoomNumber(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(roomNumber, hotelId);
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            return await _context.HotelRooms.ToListAsync();
        }

        public async Task UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Update(hotelRoom);

            await _context.SaveChangesAsync();
        }
    }
}
