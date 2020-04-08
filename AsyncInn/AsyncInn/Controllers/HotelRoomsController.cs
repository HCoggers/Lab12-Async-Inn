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

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoomManager _hotelroom;

        public HotelRoomsController(IHotelRoomManager hotelroom)
        {
            _hotelroom = hotelroom;
        }

        // GET: api/HotelRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
            return await _hotelroom.GetHotelRooms();
        }

        // GET: api/HotelRooms/5
        [HttpGet("{hotelId}/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetByRoomNumber(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelroom.GetByRoomNumber(hotelId, roomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/HotelRooms/5/4
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{hotelId}/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hotelRoom)
        {
            if (hotelId != hotelRoom.HotelID || roomNumber != hotelRoom.RoomNumber)
            {
                return BadRequest();
            }

            await _hotelroom.UpdateHotelRoom(hotelRoom);

            return NoContent();
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            await _hotelroom.CreateHotelRoom(hotelRoom);

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.RoomNumber }, hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{hotelId}/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelroom.DeleteHotelRoom(hotelId, roomNumber);

            return NoContent();
        }
    }
}
