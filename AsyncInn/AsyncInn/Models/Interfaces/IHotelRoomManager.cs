﻿using AsyncInn.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoomManager
    {
        // CREATE
        public Task CreateHotelRoom(HotelRoom hotelRoom);

        // READ
        public Task<HotelRoomDTO> GetByRoomNumber(int hotelId, int roomNumber);
        public Task<List<HotelRoomDTO>> GetHotelRooms();

        // UPDATE
        public Task UpdateHotelRoom(HotelRoom hotelRoom);

        // DELETE
        public Task DeleteHotelRoom(int hotelId, int roomNumber);

        // GET HOTEL ROOMS
        public Task<List<HotelRoom>> GetHotelRoomsByHotelID(int hotelId);
    }
}
