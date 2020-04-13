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
    public class RoomsController : ControllerBase
    {
        private readonly IRoomManager _room;

        /// <summary>
        /// rooms controller constructoe
        /// </summary>
        /// <param name="room">injection of room interface dependency</param>
        public RoomsController(IRoomManager room)
        {
            _room = room;
        }

        // GET: api/Rooms
        /// <summary>
        /// get all room types in database
        /// </summary>
        /// <returns>a list of all room types</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        /// <summary>
        /// get particular room type by id
        /// </summary>
        /// <param name="id">id of room type to recieve</param>
        /// <returns>DTO version of room</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            var room = await _room.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// update particular room type by id
        /// </summary>
        /// <param name="id">id of room to be updated</param>
        /// <param name="roomDTO">DTO version of updated room</param>
        /// <returns>no content</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO roomDTO)
        {
            if (id != roomDTO.ID)
            {
                return BadRequest();
            }

            await _room.UpdateRoom(roomDTO, id);

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// create a brand new room type
        /// </summary>
        /// <param name="roomDTO">DTO version of new room type</param>
        /// <returns>success status code</returns>
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomDTO roomDTO)
        {
            await _room.CreateRoom(roomDTO);

            return CreatedAtAction("GetRoom", new { id = roomDTO.ID }, roomDTO);
        }

        // DELETE: api/Rooms/5
        /// <summary>
        /// delete a particular room type by id
        /// </summary>
        /// <param name="id">id of room type to be deleted</param>
        /// <returns>no content</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            await _room.DeleteRoom(id);

            return NoContent();
        }
    }
}
