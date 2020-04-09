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
    public class AmenitiesService : IAmenitiesManager
    {
        private readonly AsyncInnDbContext _context;

        public AmenitiesService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task CreateAmenities(Amenities amenities)
        {
            _context.Amenities.Add(amenities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenities(int id)
        {
            var toDelete = await _context.Amenities.FindAsync(id);
            _context.Amenities.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<AmenitiesDTO> GetAmenities(int id)
        {
            var amenities = await _context.Amenities.FindAsync(id);

            AmenitiesDTO amenitiesDTO = new AmenitiesDTO
            {
                ID = amenities.ID,
                Name = amenities.Name
            };

            return amenitiesDTO;
        }

        public async Task<List<AmenitiesDTO>> GetAllAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            List<AmenitiesDTO> amenitiesDTOs = new List<AmenitiesDTO>();

            foreach(var amenity in amenities)
                amenitiesDTOs.Add(new AmenitiesDTO
                {
                    ID = amenity.ID,
                    Name = amenity.Name
                });
            return amenitiesDTOs;
        }

        public async Task UpdateAmenities(Amenities amenities, int id)
        {
            _context.Amenities.Update(amenities);

            await _context.SaveChangesAsync();
        }
    }
}
