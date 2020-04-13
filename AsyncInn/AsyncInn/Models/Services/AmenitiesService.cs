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

        /// <summary>
        /// Amenities service Constructor
        /// </summary>
        /// <param name="context">injection of DBContext</param>
        public AmenitiesService(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create new amenities entry from DTO
        /// </summary>
        /// <param name="amenitiesDTO">amenity to be created</param>
        /// <returns>no content</returns>
        public async Task CreateAmenities(AmenitiesDTO amenitiesDTO)
        {
            Amenities amenities = await GetAmenitiesFromDTO(amenitiesDTO);
            _context.Amenities.Add(amenities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete an amenity by id
        /// </summary>
        /// <param name="id">id of amenity to be deleted</param>
        /// <returns>no content</returns>
        public async Task DeleteAmenities(int id)
        {
            var toDelete = await _context.Amenities.FindAsync(id);
            _context.Amenities.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get an amenity by its id
        /// </summary>
        /// <param name="id">id of amenity to be retrieved</param>
        /// <returns>amenity DTO</returns>
        public async Task<AmenitiesDTO> GetAmenities(int id)
        {
            var amenities = await _context.Amenities.FindAsync(id);
            var amenitiesDTO = GetDTOFromAmenities(amenities);
            return amenitiesDTO;
        }

        /// <summary>
        /// get a list of all amenities
        /// </summary>
        /// <returns>all amenities as DTOs</returns>
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

        /// <summary>
        /// update specific amenity
        /// </summary>
        /// <param name="amenitiesDTO">DTO of updated amenity</param>
        /// <param name="id">id of amenity to update</param>
        /// <returns>no content</returns>
        public async Task UpdateAmenities(AmenitiesDTO amenitiesDTO, int id)
        {
            Amenities amenities = await GetAmenitiesFromDTO(amenitiesDTO);
            _context.Amenities.Update(amenities);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// turn DTO into amenities class
        /// </summary>
        /// <param name="amenitiesDTO">DTO to be converted</param>
        /// <returns>converted amenity</returns>
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

        /// <summary>
        /// converts amenities class to DTO
        /// </summary>
        /// <param name="amenities">instance to be converted</param>
        /// <returns>DTO of amenity</returns>
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
