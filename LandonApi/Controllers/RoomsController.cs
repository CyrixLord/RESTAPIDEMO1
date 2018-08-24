using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LandonApi.Controllers
{
    [Route("/[controller]")] // this route handles localhost/Rooms route . now go to rootcontroller file
    public class RoomsController : Controller
    {
        // GET: /<controller>/
        [HttpGet(Name = nameof(GetRooms))] // name route as name of method
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        }
    }
}
