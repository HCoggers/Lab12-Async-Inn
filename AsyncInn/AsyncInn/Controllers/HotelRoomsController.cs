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
    [Route("api/hotel/room")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoomManager _hotelroom;

        /// <summary>
        /// HotelRooms Constructoe
        /// </summary>
        /// <param name="hotelroom">Injection of hotelroom interface dependency</param>
        public HotelRoomsController(IHotelRoomManager hotelroom)
        {
            _hotelroom = hotelroom;
        }

        // GET: api/hotel/room
        /// <summary>
        /// Get all hotel rooms in the database
        /// </summary>
        /// <returns>List of all hotel rooms</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelroom.GetHotelRooms();
        }

        // GET: api/hotel/room/1/5
        /// <summary>
        /// Get a single hotel room by composite id, hotel id and room number.
        /// </summary>
        /// <param name="hotelId">Id of hotel with room</param>
        /// <param name="roomNumber">room's room number</param>
        /// <returns></returns>
        [HttpGet("{hotelId}/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetByRoomNumber(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelroom.GetByRoomNumber(hotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/hotel/room/5/4
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Update specific hotel room by hotel id and room number
        /// </summary>
        /// <param name="hotelId">ID of room's hotel</param>
        /// <param name="roomNumber">room's room number</param>
        /// <param name="hotelRoomDTO">updated DTO version of hotel room</param>
        /// <returns>no content</returns>
        [HttpPut("{hotelId}/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            if (hotelId != hotelRoomDTO.HotelID || roomNumber != hotelRoomDTO.RoomNumber)
            {
                return BadRequest();
            }

            await _hotelroom.UpdateHotelRoom(hotelRoomDTO);

            return NoContent();
        }

        // POST: api/hotel/room/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create a brand new hotel room entry from a DTO
        /// </summary>
        /// <param name="hotelId">ID of room's hotel</param>
        /// <param name="hotelRoomDTO">DTO version of new hotel room</param>
        /// <returns>success status code</returns>
        [HttpPost("{hotelId}")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO)
        {
            await _hotelroom.CreateHotelRoom(hotelRoomDTO);

            return CreatedAtAction("GetHotelRoom", new { id = hotelId });
        }

        // DELETE: api/hotel/room/1/3
        /// <summary>
        /// Delete specific room by hotel id and room number
        /// </summary>
        /// <param name="hotelId">Room's hotel id</param>
        /// <param name="roomNumber">room's room number</param>
        /// <returns>no content</returns>
        [HttpDelete("{hotelId}/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelroom.DeleteHotelRoom(hotelId, roomNumber);

            return NoContent();
        }
    }
}
