using AsyncInn.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoomManager
    {
        // CREATE
        public Task CreateRoom(Room Room);

        // READ
        public Task<RoomDTO> GetRoom(int id);
        public Task<List<RoomDTO>> GetRooms();

        // UPDATE
        public Task UpdateRoom(Room Room, int id);

        // DELETE
        public Task DeleteRoom(int id);

        // GET ROOM AMENITIES
        public Task<List<RoomAmenities>> GetRoomAmenities(int roomId);

    }
}
