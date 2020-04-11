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
    public class HotelService : IHotelManager
    {
        private readonly AsyncInnDbContext _context;
        private readonly IHotelRoomManager _hotelRoom;

        public HotelService(AsyncInnDbContext context, IHotelRoomManager hotelRoom)
        {
            _context = context;
            _hotelRoom = hotelRoom;
        }

        public async Task CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotel(int id)
        {
            var toDelete = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(toDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<HotelDTO> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id); // get data for the hotel

            var hotelRoomDTOs = await GetHotelRooms(hotel.ID);

            HotelDTO hotelDTO = new HotelDTO
            {
                ID = hotel.ID,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                Rooms = hotelRoomDTOs
            };
            return hotelDTO;
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            List<HotelDTO> hotelDTOs = new List<HotelDTO>();
            foreach(var hotel in hotels)
            {
                var hotelDTO = await GetHotel(hotel.ID);
                hotelDTOs.Add(hotelDTO);
            }

            return hotelDTOs;
        }

        public async Task UpdateHotel(Hotel hotel, int id)
        {
            _context.Hotels.Update(hotel);
            
            await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _hotelRoom.GetHotelRoomsByHotelID(hotelId); // get data for the hotel rooms for the hotel

            List<HotelRoomDTO> hotelRoomDTOs = new List<HotelRoomDTO>(); // make a list so we can convert the hotel rooms to hotel room DTOs

            foreach (var hotelRoom in hotelRooms) // Convert each hotel room to a hotel room DTO
            {
                var roomDTO = await _hotelRoom.GetByRoomNumber(hotelRoom.HotelID, hotelRoom.RoomNumber); // Get room DTO data from our hotelroom
                hotelRoomDTOs.Add(new HotelRoomDTO
                {
                    HotelID = hotelRoom.HotelID,
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    PetFriendly = hotelRoom.PetFriendly,
                    RoomID = hotelRoom.RoomID,
                    Room = roomDTO.Room
                });
            }
            return hotelRoomDTOs;
        }
    }
}
