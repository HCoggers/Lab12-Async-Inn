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
    public class HotelService : IHotelManager
    {
        private readonly AsyncInnDbContext _context;
        private readonly IHotelRoomManager _hotelRoom;

        /// <summary>
        /// hotel service constructor
        /// </summary>
        /// <param name="context">injects DBContext</param>
        /// <param name="hotelRoom">injects hotel room interface dependency</param>
        public HotelService(AsyncInnDbContext context, IHotelRoomManager hotelRoom)
        {
            _context = context;
            _hotelRoom = hotelRoom;
        }

        /// <summary>
        /// create new hotel
        /// </summary>
        /// <param name="hotel">hotel to be created</param>
        /// <returns>no content</returns>
        public async Task CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// remove hotel
        /// </summary>
        /// <param name="id">id of hotel to be deleted</param>
        /// <returns>no content</returns>
        public async Task DeleteHotel(int id)
        {
            var toDelete = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get specific hotel by id
        /// </summary>
        /// <param name="id">id of hotel to retrieve</param>
        /// <returns>hotel as a DTO</returns>
        public async Task<HotelDTO> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id); // get data for the hotel

            var hotelRoomDTOs = await GetHotelRooms(hotel.ID);

            HotelDTO hotelDTO = new HotelDTO
            {
                ID = hotel.ID,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotelRoomDTOs
            };
            return hotelDTO;
        }

        /// <summary>
        /// get a list of all hotels in database
        /// </summary>
        /// <returns>list of hotels as DTOs</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            List<HotelDTO> hotelDTOs = new List<HotelDTO>();
            foreach(var hotel in hotels)
            {
                var hotelDTO = await GetHotel(hotel.ID);
                hotelDTOs.Add(hotelDTO);
            }

            return hotelDTOs;
        }

        /// <summary>
        /// update hotel of a specific ID
        /// </summary>
        /// <param name="hotel">DTO of updated hotel</param>
        /// <param name="id">id of hotel to be updated</param>
        /// <returns>no content</returns>
        public async Task UpdateHotel(Hotel hotel, int id)
        {
            _context.Hotels.Update(hotel);
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieve hotel rooms of a specific hotel
        /// </summary>
        /// <param name="hotelId">id of hotel</param>
        /// <returns>list of hotel rooms as DTOs</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _hotelRoom.GetHotelRoomsByHotelID(hotelId); // get data for the hotel rooms for the hotel

            List<HotelRoomDTO> hotelRoomDTOs = new List<HotelRoomDTO>(); // make a list so we can convert the hotel rooms to hotel room DTOs

            foreach (var hotelRoom in hotelRooms) // Convert each hotel room to a hotel room DTO
            {
                var roomDTO = await _hotelRoom.GetByRoomNumber(hotelRoom.HotelID, hotelRoom.RoomNumber); // Get room DTO data from our hotelroom
                hotelRoomDTOs.Add(new HotelRoomDTO
                {
                    HotelID = hotelRoom.HotelID,
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    PetFriendly = hotelRoom.PetFriendly,
                    RoomID = hotelRoom.RoomID,
                    Room = roomDTO.Room
                });
            }
            return hotelRoomDTOs;
        }
    }
}
