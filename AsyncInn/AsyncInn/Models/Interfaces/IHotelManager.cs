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
        public Task<Hotel> GetHotel(int id);
        public Task<List<Hotel>> GetHotels();

        // UPDATE
        public Task UpdateHotel(Hotel hotel, int id);

        // DELETE
        public Task DeleteHotel(int id);
    }
}
