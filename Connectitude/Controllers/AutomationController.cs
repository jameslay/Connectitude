using System;
using System.Threading.Tasks;
using Connectitude.Models;
using Connectitude.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Connectitude.Controllers
{
    [Route("api/homes")]
    [ApiController]
    public class AutomationController : Controller
    {
        HomeRepository _homeRepo;

        public AutomationController(HomeRepository homeRepo)
        {
            _homeRepo = homeRepo;
        }

        [HttpGet]
        [Route("names")]
        public async Task<IActionResult> AllHomeNames()
        {
            try
            {
                return Ok(await _homeRepo.GetHomes());
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("data")]
        public async Task<IActionResult> AllRooms()
        {
            try
            {
                return Ok(await _homeRepo.GetRooms());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("{id}/data")]
        public async Task<IActionResult> Home(int id)
        {
            try
            {
                return Ok(await _homeRepo.GetHome(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("{name}/newHome")]
        public IActionResult NewHome(string name)
        {
            try
            {
                return Ok(_homeRepo.NewHome(name));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("newRoom")]
        public IActionResult NewRoom(RoomModel room)
        {
            try
            {
                return Ok(_homeRepo.NewRoom(room));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("{id}/deleteRoom")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                return Ok(await _homeRepo.DeleteRoom(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("{id}/deleteHome")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            try
            {
                return Ok(await _homeRepo.DeleteHome(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
