using Connectitude.Database;
using Connectitude.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connectitude.Repository
{
    public class HomeRepository
    {
        private readonly ApiContext _context;
        public HomeRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<HomeModel[]> GetHomes()
        {
            return await _context.Homes.ToArrayAsync();
        }

        public async Task<RoomsModel> GetRooms()
        {
            var allRooms = await (from homes in _context.Homes
                                  join rooms in _context.Rooms on homes.Id equals rooms.HomeId

                                  select new RoomModel
                                  {
                                      Id = rooms.Id,
                                      Name = rooms.Name,
                                      Humidity = rooms.Humidity,
                                      Temperature = rooms.Temperature,
                                      HomeId = homes.Id,
                                      HomeName = homes.Name
                                  }).ToArrayAsync();


            RoomsModel savedRooms = new RoomsModel();
            savedRooms.Rooms = allRooms;

            return savedRooms;
        }

        public async Task<RoomsModel> GetHome(int id)
        {
            var allRooms = await (from homes in _context.Homes
                                  join rooms in _context.Rooms on homes.Id equals rooms.HomeId
                                  select new RoomModel
                                  {
                                      Id = rooms.Id,
                                      Name = rooms.Name,
                                      Humidity = rooms.Humidity,
                                      Temperature = rooms.Temperature,
                                      HomeId = homes.Id,
                                      HomeName = homes.Name
                                  })
                                  .Where(a => a.HomeId == id).ToArrayAsync();


            RoomsModel savedRooms = new RoomsModel();
            savedRooms.Rooms = allRooms;

            return savedRooms;
        }

        public int NewHome(string name)
        {
            try
            {
                HomeModel newFacility = new HomeModel
                {
                    Name = name
                };

                _context.Add(newFacility);
                 _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int NewRoom(RoomModel room)
        {
            try
            {

                RoomModel newRoom = new RoomModel
                {
                    Name = room.Name,
                    Temperature = room.Temperature,
                    Humidity = room.Humidity,
                    HomeId = room.HomeId
                };

                _context.Add(newRoom);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> DeleteRoom(int id)
        {
            try
            {
                var room = await _context.Rooms.Where(a => a.Id == id).FirstOrDefaultAsync();
                _context.Rooms.Remove(room);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<int> DeleteHome(int id)
        {
            try
            {
                var rooms = await _context.Rooms.Where(a => a.HomeId == id).ToArrayAsync();
                var home = await _context.Homes.Where(a => a.Id == id).FirstOrDefaultAsync();
                foreach(RoomModel room in rooms)
                {
                    _context.Rooms.Remove(room);
                }
                _context.Homes.Remove(home);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
