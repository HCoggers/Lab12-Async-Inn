using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenitiesManager
    {
        // CREATE
        public Task CreateAmenities(Amenities amenities);

        // READ
        public Task<Amenities> GetAmenities(int id);
        public Task<List<Amenities>> GetAllAmenities();

        // UPDATE
        public Task UpdateAmenities(Amenities amenities, int id);

        // DELETE
        public Task DeleteAmenities(int id);
    }
}
