using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private ISqlUser _sqlUser;
        public UserController(ISqlUser sqlUser) 
        {
            _sqlUser = sqlUser;
        }

        [HttpPost("", Name = "Create")]
        public IActionResult Create(User user)
        {
            if (user == null) return BadRequest();
            else if (user.id == Guid.Empty) return BadRequest("Id can't be empty.");
            else if (user.email == null || user.email == string.Empty) return BadRequest("Email can't be empty");
            else if (user.displayname == null || user.displayname == string.Empty) return BadRequest("DisplayName can't be empty.");


            User createdUser = _sqlUser.AddUser(user);
            if (createdUser == null) return BadRequest("User already exists!");

            return Created("User database", createdUser);
        }

        [HttpGet("{email}", Name = "Read")]
        public IActionResult GetUser(string email)
        {
            if (email == null || email == string.Empty) return BadRequest("Email field can't be empty.");


            User foundUser = _sqlUser.GetUser(email);
            if (foundUser == null) return NotFound("User doesn't exists!");

            return Ok(foundUser);
        }

        [HttpGet("", Name = "ReadAll")]
        public IActionResult GetAll()
        {
            List<User> foundUser = _sqlUser.GetUsers();
            if (foundUser == null || foundUser.Count == 0) return NotFound("No users exist in the database.");

            return Ok(foundUser);
        }

        [HttpPatch("", Name = "Update")]
        public IActionResult Update()
        {
            return Ok("Accoutn updated");
        }

        [HttpDelete("", Name = "Delete")]
        public IActionResult DeleteUser(string email)
        {
            if (email == null || email == string.Empty) return BadRequest("Email field can't be empty.");
            bool success = _sqlUser.DeleteUser(email);

            if (!success) return Problem("Couldn't delete user", string.Empty, 500);
            return Ok("Account deleted!");
        }
    }
}
