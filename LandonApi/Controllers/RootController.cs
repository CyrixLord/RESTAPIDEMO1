using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandonApi.Controllers
{
    [Route("/")] // route this as the defaut or root controller
    [ApiVersion("1.0")] // using version control from nuget versioning tool and after configing in startup.cs
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))] // this is good because if we change the name of the controller, we wo
        // this is where we tell the controller to handle this as a get request...
        public IActionResult GetRoot() // handle an HTTP Get request. we added 'Root' because you can be more descriptive if you want
        {
            // for now return a simple anonymous object with an href property that returns its own link
            var response = new
            {
                // you dont want to hard wire urls like 
                // href = "???"
                // best to let mvc help you

                href = Url.Link(nameof(GetRoot), null ), // where null is is where additional parameters are passed in the URL for now we dont have one
                // there is no semicolon here because this is an object being passed which is the link to the controller with no parameters
                rooms = new { href = Url.Link(nameof(RoomsController.GetRooms), null)} //we probably have to do this for all controllers to make
                                                                                       // it ion compatible that exposes all REST functions in teh rest
                                                                                       // response . get rooms is getting all rooms so there are no params
                                                                                       // <null>
                                                                                       // so yes, this adds rooms to the list of urls that can be called
                                                                                       // to the REST responses.
                
            };

            return Ok(response); // return HTTP OK 200
        }
    }
}
