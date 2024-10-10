using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public OwnerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetAllOwners()
        {
            return await _appDbContext.Owners.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _appDbContext.Owners.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _appDbContext.Owners.Add(owner);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.OwnerID }, owner);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutOwner(int id, Owner updatedOwner)
        {
            var owner = await _appDbContext.Owners.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            owner.OwnerFirstName = updatedOwner.OwnerFirstName;
            owner.OwnerLastName = updatedOwner.OwnerLastName;

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
        public async Task<ActionResult> DeleteOwner(int id)
        {
            var owner = await _appDbContext.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _appDbContext.Owners.Remove(owner);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
