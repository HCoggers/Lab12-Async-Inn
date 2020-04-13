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

        /// <summary>
        /// AmenitiesController constructor
        /// </summary>
        /// <param name="amenities">inject amenenities interface dependency</param>
        public AmenitiesController(IAmenitiesManager amenities)
        {
            _amenities = amenities;
        }

        // GET: api/Amenities
        /// <summary>
        /// Gets a list of all amenities
        /// </summary>
        /// <returns>List of amenity DTOs</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenitiesDTO>>> GetAmenities()
        {
            return await _amenities.GetAllAmenities();
        }

        // GET: api/Amenities/5
        /// <summary>
        /// Get a single amenity by id
        /// </summary>
        /// <param name="id">Id of amenity to return</param>
        /// <returns>Amenity that matches given ID</returns>
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
        /// <summary>
        /// Update an amenity of a given id
        /// </summary>
        /// <param name="id">Id of amenity to update</param>
        /// <param name="amenitiesDTO">Updated DTO version of amenity</param>
        /// <returns>no content</returns>
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
        /// <summary>
        /// Create a brand new amenity from a given DTO
        /// </summary>
        /// <param name="amenitiesDTO">Amenity to be created</param>
        /// <returns>Success status code</returns>
        [HttpPost]
        public async Task<ActionResult<Amenities>> PostAmenities(AmenitiesDTO amenitiesDTO)
        {
            await _amenities.CreateAmenities(amenitiesDTO);

            return CreatedAtAction("GetAmenities", new { id = amenitiesDTO.ID }, amenitiesDTO);
        }

        // DELETE: api/Amenities/5
        /// <summary>
        /// Delete amenity of a given ID
        /// </summary>
        /// <param name="id">ID of amenity to delete</param>
        /// <returns>no content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenities>> DeleteAmenities(int id)
        {
            await _amenities.DeleteAmenities(id);

            return NoContent();
        }
    }
}
