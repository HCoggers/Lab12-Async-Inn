using AsyncInn.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenitiesManager
    {
        // CREATE
        public Task CreateAmenities(AmenitiesDTO amenitiesDTO);

        // READ
        public Task<AmenitiesDTO> GetAmenities(int id);
        public Task<List<AmenitiesDTO>> GetAllAmenities();

        // UPDATE
        public Task UpdateAmenities(AmenitiesDTO amenitiesDTO, int id);

        // DELETE
        public Task DeleteAmenities(int id);
    }
}
