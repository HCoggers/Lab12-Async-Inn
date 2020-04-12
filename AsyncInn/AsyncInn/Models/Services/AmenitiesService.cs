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

        public async Task CreateAmenities(AmenitiesDTO amenitiesDTO)
        {
            Amenities amenities = await GetAmenitiesFromDTO(amenitiesDTO);
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
            var amenitiesDTO = GetDTOFromAmenities(amenities);
            return amenitiesDTO;
        }

        public async Task<List<AmenitiesDTO>> GetAllAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            List<AmenitiesDTO> amenitiesDTOs = new List<AmenitiesDTO>();

            foreach (var amenity in amenities)
            {
                var amenitiesDTO = GetDTOFromAmenities(amenity);
                amenitiesDTOs.Add(amenitiesDTO);
            }
            return amenitiesDTOs;
        }

        public async Task UpdateAmenities(AmenitiesDTO amenitiesDTO, int id)
        {
            Amenities amenities = await GetAmenitiesFromDTO(amenitiesDTO);
            _context.Amenities.Update(amenities);

            await _context.SaveChangesAsync();
        }

        private async Task<Amenities> GetAmenitiesFromDTO(AmenitiesDTO amenitiesDTO)
        {
            List<RoomAmenities> roomAmenities = await _context.RoomAmenities.Where(roomAmenities => roomAmenities.AmenitiesID == amenitiesDTO.ID)
                .ToListAsync();

            Amenities amenities = new Amenities
            {
                ID = amenitiesDTO.ID,
                Name = amenitiesDTO.Name,
                RoomAmenities = roomAmenities
            };

            return amenities;
        }

        private AmenitiesDTO GetDTOFromAmenities(Amenities amenities)
        {
            AmenitiesDTO amenitiesDTO = new AmenitiesDTO
            {
                ID = amenities.ID,
                Name = amenities.Name
            };

            return amenitiesDTO;
        }
    }
}
