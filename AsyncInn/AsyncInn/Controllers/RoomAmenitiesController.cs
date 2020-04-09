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
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAmenitiesController : ControllerBase
    {
        private readonly AsyncInnDbContext _context;

        public RoomAmenitiesController(AsyncInnDbContext context)
        {
            _context = context;
        }

        // POST: api/RoomAmenities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RoomAmenities>> PostRoomAmenities(RoomAmenities roomAmenities)
        {
            _context.RoomAmenities.Add(roomAmenities);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoomAmenitiesExists(roomAmenities.AmenitiesID, roomAmenities.RoomID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRoomAmenities", new { id = roomAmenities.AmenitiesID }, roomAmenities);
        }

        // DELETE: api/RoomAmenities/5
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

        private bool RoomAmenitiesExists(int amenitiesId, int roomId)
        {
            return _context.RoomAmenities.Any(e => e.AmenitiesID == amenitiesId && e.RoomID == roomId);
        }
    }
}
