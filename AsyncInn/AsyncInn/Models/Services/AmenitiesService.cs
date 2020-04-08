using AsyncInn.Data;
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

        public async Task<Amenities> GetAmenities(int id)
        {
            var amenities = await _context.Amenities.FindAsync(id);
            return amenities;
        }

        public async Task<List<Amenities>> GetAllAmenities()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task UpdateAmenities(Amenities amenities, int id)
        {
            _context.Amenities.Update(amenities);

            await _context.SaveChangesAsync();
        }
    }
}
