using Microsoft.AspNetCore.Mvc;
using EX.Models;
using System.Collections.Generic;

namespace EX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // POST: api/Users/register
        [HttpPost("register")]
        public ActionResult<bool> Register([FromBody] User u)
        {
            bool success = EX.Models.User.Register(u);
            if (!success)
                return BadRequest("A user with this email already exists.");
            return Ok(true);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public ActionResult<bool> Login([FromBody] LoginRequest request)
        {
            bool success = EX.Models.User.Login(request.Email, request.Password);
            if (!success)
                return Unauthorized("Invalid email or password.");
            return Ok(true);
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Ok(EX.Models.User.Read());
        }

        // מבנה נתונים עבור login
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}

