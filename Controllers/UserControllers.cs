using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // In-memory user list for demonstration
        private static readonly List<User> Users = new();
        private static int _nextId = 1;

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(Users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            user.Id = _nextId++;
            Users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            // Add other properties as needed

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            Users.Remove(user);
            return NoContent();
        }
    }
}