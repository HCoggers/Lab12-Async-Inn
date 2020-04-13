using AsyncInn.Data;
using AsyncInn.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomService : IRoomManager
    {
        private readonly AsyncInnDbContext _context;
        private readonly IAmenitiesManager _amenities;

        /// <summary>
        /// room service constructor
        /// </summary>
        /// <param name="context">injects DBContext</param>
        /// <param name="amenities">injects amenities interface dependency</param>
        public RoomService(AsyncInnDbContext context, IAmenitiesManager amenities)
        {
            _context = context;
            _amenities = amenities;
        }

        /// <summary>
        /// create a new room type
        /// </summary>
        /// <param name="roomDTO">DTO version of new room</param>
        /// <returns>no content</returns>
        public async Task CreateRoom(RoomDTO roomDTO)
        {
            Room room = await GetRoomFromDTO(roomDTO);
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// delete specific room type
        /// </summary>
        /// <param name="id">id of room to be deleted</param>
        /// <returns>no content</returns>
        public async Task DeleteRoom(int id)
        {
            var toDelete = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// get a specific room by id
        /// </summary>
        /// <param name="id">id of room</param>
        /// <returns>room as a dTO</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            RoomDTO roomDTO = await GetDTOFromRoom(room);
            return roomDTO;
        }

        /// <summary>
        /// get all room types in database
        /// </summary>
        /// <returns>list of all rooms as DTOs</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach(var room in rooms)
            {
                RoomDTO roomDTO = await GetDTOFromRoom(room);
                roomDTOs.Add(roomDTO);
            }
            return roomDTOs;
        }

        /// <summary>
        /// update specific room type
        /// </summary>
        /// <param name="roomDTO">DTO of updated room</param>
        /// <param name="id">id of room to be updated</param>
        /// <returns>no content</returns>
        public async Task UpdateRoom(RoomDTO roomDTO, int id)
        {
            Room room = await GetRoomFromDTO(roomDTO);
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }

        // SATELLITE METHODS
        /// <summary>
        /// get all amenities for a particular room
        /// </summary>
        /// <param name="roomId">id of room</param>
        /// <returns>list of amenities</returns>
        private async Task<List<Amenities>> GetRoomAmenities(int roomId)
        {
            var roomAmenities = await _context.RoomAmenities.Where(roomAmenity => roomAmenity.RoomID == roomId)
                .Include(roomAmenity => roomAmenity.Amenities)
                .Select(amenity => amenity.Amenities)
                .ToListAsync();
            return roomAmenities;
        }

        /// <summary>
        /// convert DTO to room instance
        /// </summary>
        /// <param name="roomDTO">DTO to be converted</param>
        /// <returns>room instance</returns>
        private async Task<Room> GetRoomFromDTO(RoomDTO roomDTO) 
        {
            var roomAmenities = await _context.RoomAmenities.Where(roomAmenity => roomAmenity.RoomID == roomDTO.ID)
                .ToListAsync();
            var hotelRooms = await _context.HotelRooms.Where(hotelRoom => hotelRoom.RoomID == roomDTO.ID)
                .ToListAsync();

            Room room = new Room
            {
                ID = roomDTO.ID,
                Name = roomDTO.Name,
                Layout = Enum.Parse<Layout>(roomDTO.Layout),
                RoomAmenities = roomAmenities,
                HotelRooms = hotelRooms
            };

            return room;
        }

        /// <summary>
        /// convert room instance to a DTO
        /// </summary>
        /// <param name="room">instance to be converted</param>
        /// <returns>DTO of room</returns>
        private async Task<RoomDTO> GetDTOFromRoom(Room room)
        {
            var roomAmenities = await GetRoomAmenities(room.ID);
            List<AmenitiesDTO> amenityDTOs = new List<AmenitiesDTO>();
            if (roomAmenities != null)
            {
                foreach (var RoomAmenity in roomAmenities)
                {
                    var AmenityDTO = new AmenitiesDTO
                    {
                        ID = RoomAmenity.ID,
                        Name = RoomAmenity.Name
                    };
                    amenityDTOs.Add(AmenityDTO);
                }
            }

            RoomDTO roomDTO = new RoomDTO
            {
                ID = room.ID,
                Name = room.Name,
                Layout = room.Layout.ToString(),
                Amenities = amenityDTOs
            };

            return roomDTO;
        }
    }
}
