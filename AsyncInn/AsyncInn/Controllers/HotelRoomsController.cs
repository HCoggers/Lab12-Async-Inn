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

        public HotelRoomsController(IHotelRoomManager hotelroom)
        {
            _hotelroom = hotelroom;
        }

        // GET: api/hotel/room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelroom.GetHotelRooms();
        }

        // GET: api/hotel/room/1/5
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
        [HttpPost("{hotelId}")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO)
        {
            await _hotelroom.CreateHotelRoom(hotelRoomDTO);

            return CreatedAtAction("GetHotelRoom", new { id = hotelId });
        }

        // DELETE: api/hotel/room/1/3
        [HttpDelete("{hotelId}/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelroom.DeleteHotelRoom(hotelId, roomNumber);

            return NoContent();
        }
    }
}
