using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _appDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(Guid id, User updatedUser)
        {
            var user = await _appDbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.UserEmail = updatedUser.UserEmail;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
