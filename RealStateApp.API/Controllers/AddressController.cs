using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public AddressController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await _appDbContext.Addresses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _appDbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _appDbContext.Addresses.Add(address);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressID }, address);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAddress(int id, Address updatedAddress)
        {
            var address = await _appDbContext.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            address.AddressLine = updatedAddress.AddressLine;
            address.City = updatedAddress.City;
            address.State = updatedAddress.State;
            address.ZipCode = updatedAddress.ZipCode;

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
        public async Task<ActionResult> DeleteAddress(int id)
        {
            var address = await _appDbContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _appDbContext.Addresses.Remove(address);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
