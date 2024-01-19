using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Data;
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

        [HttpPost("", Name = "Create"), Authorize(Roles = "administrator")]
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

        [HttpGet("{userId}", Name = "Read")]
        public IActionResult GetUser(string userId)
        {
            if (userId == null || userId == string.Empty) return BadRequest("UserId field can't be empty.");

            User foundUser = _sqlUser.GetUser(userId);
            if (foundUser == null) return NotFound("User doesn't exists!");
            if (User.Claims.Contains(new Claim(ClaimTypes.Name, foundUser.id.ToString())) || User.IsInRole("administrator"))
                return Ok(foundUser);
            else return Unauthorized();
        }

        [HttpGet("All", Name = "ReadAll"), Authorize(Roles = "administrator")]
        public IActionResult GetAll()
        {
            List<User> foundUser = _sqlUser.GetUsers();
            if (foundUser == null || foundUser.Count == 0) return NotFound("No users exist in the database.");

            return Ok(foundUser);
        }

        [HttpPatch("", Name = "Update")]
        public IActionResult Update(User user)
        {
            if (user.id.ToString() == null || user.id.ToString() == string.Empty) return BadRequest("UserId field can't be empty.");
            if (User.Claims.Contains(new Claim(ClaimTypes.Name, user.id.ToString())) || User.IsInRole("administrator"))
            {
                User existing = _sqlUser.GetUser(user.id.ToString());
                if (existing == null) return NotFound("Couldn't find account.");

                existing.email = user.email;
                existing.displayname = user.displayname;
                User success = _sqlUser.UpdateUser(existing);

                if (success == null) return Problem("Couldn' t update account", string.Empty, 500);
                return Ok("Account updated");
            }
            else return Unauthorized();
        }

        [HttpDelete("{userId}", Name = "Delete"), Authorize(Roles = "administrator")]
        public IActionResult DeleteUser(string userId)
        {
            if (userId == null || userId == string.Empty) return BadRequest("UserId field can't be empty.");
            bool success = _sqlUser.DeleteUser(userId);

            if (!success) return Problem("Couldn't delete user", string.Empty, 500);
            return Ok("Account deleted!");
        }
    }
}
