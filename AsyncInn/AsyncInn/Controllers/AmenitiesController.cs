using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.DTOs;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenitiesManager _amenities;

        public AmenitiesController(IAmenitiesManager amenities)
        {
            _amenities = amenities;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenitiesDTO>>> GetAmenities()
        {
            return await _amenities.GetAllAmenities();
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenitiesDTO>> GetAmenities(int id)
        {
            var amenities = await _amenities.GetAmenities(id);

            if (amenities == null)
            {
                return NotFound();
            }

            return amenities;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, AmenitiesDTO amenitiesDTO)
        {
            if (id != amenitiesDTO.ID)
            {
                return BadRequest();
            }

            await _amenities.UpdateAmenities(amenitiesDTO, id);

            return NoContent();
        }

        // POST: api/Amenities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Amenities>> PostAmenities(AmenitiesDTO amenitiesDTO)
        {
            await _amenities.CreateAmenities(amenitiesDTO);

            return CreatedAtAction("GetAmenities", new { id = amenitiesDTO.ID }, amenitiesDTO);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenities>> DeleteAmenities(int id)
        {
            await _amenities.DeleteAmenities(id);

            return NoContent();
        }
    }
}
