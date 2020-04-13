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
        /// <summary>
        /// hotel room service constructor
        /// </summary>
        /// <param name="context">injects DBContext</param>
        /// <param name="room">injects room interface dependency</param>
        public HotelRoomService(AsyncInnDbContext context, IRoomManager room)
        {
            _context = context;
            _room = room;
        }

        /// <summary>
        /// create a new hotel room
        /// </summary>
        /// <param name="hotelRoomDTO">DTO of ne hotel instance</param>
        /// <returns>no content</returns>
        public async Task CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = await GetHotelRoomFromDTO(hotelRoomDTO);
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete hotel room entry by hotel id and room number
        /// </summary>
        /// <param name="hotelId">entry's hotel id</param>
        /// <param name="roomNumber">entry's room number</param>
        /// <returns>no content</returns>
        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            var toDelete = await _context.HotelRooms.FindAsync(roomNumber, hotelId);
            _context.HotelRooms.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get specific hotel room
        /// </summary>
        /// <param name="hotelId">id of hotel</param>
        /// <param name="roomNumber">room number</param>
        /// <returns>hotel room as a DTO</returns>
        public async Task<HotelRoomDTO> GetByRoomNumber(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(roomNumber, hotelId);

            HotelRoomDTO hotelRoomDTO = await GetDTOFromHotelRoom(hotelRoom);

            return hotelRoomDTO;
        }

        /// <summary>
        /// return all hotel rooms
        /// </summary>
        /// <returns>all hotel room entries as DTOs</returns>
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

        /// <summary>
        /// update hotel room
        /// </summary>
        /// <param name="hotelRoomDTO">DTO of hotel room to be updated</param>
        /// <returns>no content</returns>
        public async Task UpdateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = await GetHotelRoomFromDTO(hotelRoomDTO);

            _context.HotelRooms.Update(hotelRoom);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get all hotel rooms of a specific hotel
        /// </summary>
        /// <param name="hotelId">id of hotel</param>
        /// <returns>list of hotel rooms as DTOs</returns>
        public async Task<List<HotelRoom>> GetHotelRoomsByHotelID(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms.Where(hotelRoom => hotelRoom.HotelID == hotelId)
                .Include(hotelRoom => hotelRoom.Room)
                .ToListAsync();
            return hotelRooms;
        }

        // SATELLITE METHODS
        /// <summary>
        /// create hotel room entry from DTO
        /// </summary>
        /// <param name="hotelRoomDTO">DTO to be converted</param>
        /// <returns>hotel room instance</returns>
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

        /// <summary>
        /// converts hotel room instance to DTO
        /// </summary>
        /// <param name="hotelRoom">instance to be converted</param>
        /// <returns>DTO of hotel room</returns>
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
