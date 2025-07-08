using Microsoft.AspNetCore.Mvc;
using EX.Models;

namespace EX.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class UsersController : ControllerBase
        {
            // POST: api/Users
            [HttpPost]
            public ActionResult<bool> Post([FromBody] User u)
            {
                bool success = Models.User.Insert(u);
                if (!success)
                    return BadRequest("A user with the same Id already exists.");
                return Ok(true);
            }

            // GET: api/Users
            [HttpGet]
            public ActionResult<List<User>> Get()
            {
                return Ok(Models.User.Read());
            }
        }
   
}
