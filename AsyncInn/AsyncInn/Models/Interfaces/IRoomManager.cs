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
        public Task CreateRoom(RoomDTO RoomDTO);

        // READ
        public Task<RoomDTO> GetRoom(int id);
        public Task<List<RoomDTO>> GetRooms();

        // UPDATE
        public Task UpdateRoom(RoomDTO RoomDTO, int id);

        // DELETE
        public Task DeleteRoom(int id);

    }
}
