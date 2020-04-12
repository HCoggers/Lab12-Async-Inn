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
    public class HotelRoomService : IHotelRoomManager
    {
        private readonly AsyncInnDbContext _context;
        private readonly IRoomManager _room;
        public HotelRoomService(AsyncInnDbContext context, IRoomManager room)
        {
            _context = context;
            _room = room;
        }

        public async Task CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = await GetHotelRoomFromDTO(hotelRoomDTO);
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var toDelete = await _context.HotelRooms.FindAsync(roomNumber, hotelId);
            _context.HotelRooms.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<HotelRoomDTO> GetByRoomNumber(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(roomNumber, hotelId);

            HotelRoomDTO hotelRoomDTO = await GetDTOFromHotelRoom(hotelRoom);

            return hotelRoomDTO;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            var hotelRooms = await _context.HotelRooms.ToListAsync();

            List<HotelRoomDTO> hotelRoomDTOs = new List<HotelRoomDTO>();
            foreach (var hotelRoom in hotelRooms)
            {
                HotelRoomDTO hotelRoomDTO = await GetDTOFromHotelRoom(hotelRoom);
                hotelRoomDTOs.Add(hotelRoomDTO);
            }
            return hotelRoomDTOs;
        }

        public async Task UpdateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = await GetHotelRoomFromDTO(hotelRoomDTO);

            _context.HotelRooms.Update(hotelRoom);

            await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoom>> GetHotelRoomsByHotelID(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms.Where(hotelRoom => hotelRoom.HotelID == hotelId)
                .Include(hotelRoom => hotelRoom.Room)
                .ToListAsync();
            return hotelRooms;
        }

        // SATELLITE METHODS
        private async Task<HotelRoom> GetHotelRoomFromDTO(HotelRoomDTO hotelRoomDTO)
        {
            Hotel hotel = await _context.Hotels.FindAsync(hotelRoomDTO.HotelID);
            Room room = await _context.Rooms.FindAsync(hotelRoomDTO.RoomID);

            HotelRoom hotelRoom = new HotelRoom
            {
                HotelID = hotelRoomDTO.HotelID,
                RoomID = hotelRoomDTO.RoomID,
                RoomNumber = hotelRoomDTO.RoomNumber,
                Rate = hotelRoomDTO.Rate,
                PetFriendly = hotelRoomDTO.PetFriendly,
                Hotel = hotel,
                Room = room
            };

            return hotelRoom;
        }

        private async Task<HotelRoomDTO> GetDTOFromHotelRoom(HotelRoom hotelRoom)
        {
            var roomDTO = await _room.GetRoom(hotelRoom.RoomID);

            HotelRoomDTO hotelRoomDTO = new HotelRoomDTO
            {
                HotelID = hotelRoom.HotelID,
                RoomID = hotelRoom.RoomID,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                Room = roomDTO
            };

            return hotelRoomDTO;
        }
    }
}
