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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelManager _hotel;

        /// <summary>
        /// hotel constructor
        /// </summary>
        /// <param name="hotel">injection of hotel interface</param>
        public HotelsController(IHotelManager hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        /// <summary>
        /// Get all hotels in database
        /// </summary>
        /// <returns>List of all hotels</returns>
        [HttpGet]
        public async Task<IEnumerable<HotelDTO>> GetHotels()
        {
            return await _hotel.GetHotels();
        }

        // GET: api/Hotels/5
        /// <summary>
        /// Get single hotel by id
        /// </summary>
        /// <param name="id">ID of hotel to return</param>
        /// <returns>DTO version of Hotel entry</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotel.GetHotel(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// update particular hotel by id
        /// </summary>
        /// <param name="id">id of hotel to update</param>
        /// <param name="hotel">DTO version of updated hotel</param>
        /// <returns>no content</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.ID)
            {
                return BadRequest();
            }

            await _hotel.UpdateHotel(hotel, id);

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create new hotel data entry from DTO version
        /// </summary>
        /// <param name="hotel">new hotel to be created</param>
        /// <returns>success status code</returns>
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _hotel.CreateHotel(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.ID }, hotel);
        }

        // DELETE: api/Hotels/5
        /// <summary>
        /// delete a specific hotel entry by id
        /// </summary>
        /// <param name="id">ID of hotel to be deleted</param>
        /// <returns>no content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            await _hotel.DeleteHotel(id);

            return NoContent();
        }
    }
}
