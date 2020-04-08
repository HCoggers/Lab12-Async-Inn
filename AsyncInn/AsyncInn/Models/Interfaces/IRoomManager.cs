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
        public Task<Room> GetRoom(int id);
        public Task<List<Room>> GetRooms();

        // UPDATE
        public Task UpdateRoom(Room Room, int id);

        // DELETE
        public Task DeleteRoom(int id);

    }
}
