using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;

namespace AsyncInn.Controllers
{
    [Route("api/Room/Amenities")]
    [ApiController]
    public class RoomAmenitiesController : ControllerBase
    {
        private readonly AsyncInnDbContext _context;

        /// <summary>
        /// Room amenities controller constructor
        /// </summary>
        /// <param name="context">database context injected</param>
        public RoomAmenitiesController(AsyncInnDbContext context)
        {
            _context = context;
        }

        // POST: api/Room/Amenities/5/2
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// create new room amenity connection
        /// </summary>
        /// <param name="roomId">id of room to add amenity to</param>
        /// <param name="amenitiesId">id of amenity to add to room</param>
        /// <returns></returns>
        [HttpPost("{roomId}/{amenitiesId}")]
        public async Task<ActionResult<RoomAmenities>> PostRoomAmenities(int roomId, int amenitiesId)
        {
            _context.RoomAmenities.Add(new RoomAmenities { RoomID = roomId, AmenitiesID = amenitiesId });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoomAmenitiesExists(amenitiesId, roomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRoomAmenities", new { AmenitiesID = amenitiesId, RoomID = roomId });
        }

        // DELETE: api/RoomAmenities/5
        /// <summary>
        /// delete a given room amenity connection
        /// </summary>
        /// <param name="id">id of entry to be removed</param>
        /// <returns>connection removed</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomAmenities>> DeleteRoomAmenities(int id)
        {
            var roomAmenities = await _context.RoomAmenities.FindAsync(id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            _context.RoomAmenities.Remove(roomAmenities);
            await _context.SaveChangesAsync();

            return roomAmenities;
        }

        /// <summary>
        /// check if room amenity connection exists
        /// </summary>
        /// <param name="amenitiesId">id of amenity to check</param>
        /// <param name="roomId">id of room to check</param>
        /// <returns>boolean</returns>
        private bool RoomAmenitiesExists(int amenitiesId, int roomId)
        {
            return _context.RoomAmenities.Any(e => e.AmenitiesID == amenitiesId && e.RoomID == roomId);
        }
    }
}
