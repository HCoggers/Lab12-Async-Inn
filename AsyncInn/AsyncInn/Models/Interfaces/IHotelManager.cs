using AsyncInn.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelManager
    {
        // CREATE
        public Task CreateHotel(Hotel hotel);

        // READ
        public Task<HotelDTO> GetHotel(int id);
        public Task<List<HotelDTO>> GetHotels();

        // UPDATE
        public Task UpdateHotel(Hotel hotel, int id);

        // DELETE
        public Task DeleteHotel(int id);

        // GET HOTEL ROOMS
        public Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);
    }
}
