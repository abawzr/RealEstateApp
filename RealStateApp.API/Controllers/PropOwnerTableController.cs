using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropOwnerTableController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PropOwnerTableController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropOwnerTable>>> GetAllPropOwners()
        {
            return await _appDbContext.PropOwnerTables.ToListAsync();
        }

        [HttpGet("{ownerID}/{propertyID}")]
        public async Task<ActionResult<PropOwnerTable>> GetPropOwner(int ownerID, int propertyID)
        {
            var propOwner = await _appDbContext.PropOwnerTables.FindAsync(ownerID, propertyID);

            if (propOwner == null)
            {
                return NotFound();
            }

            return propOwner;
        }

        [HttpPost]
        public async Task<ActionResult<PropOwnerTable>> PostPropOwner(PropOwnerTable propOwner)
        {
            _appDbContext.PropOwnerTables.Add(propOwner);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetPropOwner", new { propOwner.OwnerID, propOwner.PropertyID }, propOwner);
        }

        [HttpPut("{ownerID}/{propertyID}")]
        public async Task<ActionResult> PutPropOwner(int ownerID, int propertyID, PropOwnerTable updatedPropOwner)
        {
            var propOwner = await _appDbContext.PropOwnerTables.FindAsync(ownerID, propertyID);

            if (propOwner == null)
            {
                return NotFound();
            }

            propOwner.OwnerID = updatedPropOwner.OwnerID;
            propOwner.PropertyID = updatedPropOwner.PropertyID;
            propOwner.PercentOwned = updatedPropOwner.PercentOwned;

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

        [HttpDelete("{ownerID}/{propertyID}")]
        public async Task<ActionResult> DeletePropOwner(int ownerID, int propertyID)
        {
            var propOwner = await _appDbContext.PropOwnerTables.FindAsync(ownerID, propertyID);
            if (propOwner == null)
            {
                return NotFound();
            }

            _appDbContext.PropOwnerTables.Remove(propOwner);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
