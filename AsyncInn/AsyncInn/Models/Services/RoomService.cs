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

        public RoomService(AsyncInnDbContext context, IAmenitiesManager amenities)
        {
            _context = context;
            _amenities = amenities;
        }

        public async Task CreateRoom(RoomDTO roomDTO)
        {
            Room room = await GetRoomFromDTO(roomDTO);
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var toDelete = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            RoomDTO roomDTO = await GetDTOFromRoom(room);
            return roomDTO;
        }

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

        public async Task UpdateRoom(RoomDTO roomDTO, int id)
        {
            Room room = await GetRoomFromDTO(roomDTO);
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }

        // SATELLITE METHODS
        private async Task<List<Amenities>> GetRoomAmenities(int roomId)
        {
            var roomAmenities = await _context.RoomAmenities.Where(roomAmenity => roomAmenity.RoomID == roomId)
                .Include(roomAmenity => roomAmenity.Amenities)
                .Select(amenity => amenity.Amenities)
                .ToListAsync();
            return roomAmenities;
        }

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
