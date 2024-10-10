using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PropertyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties()
        {
            return await _appDbContext.Properties.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
            var property = await _appDbContext.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return property;
        }

        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property property)
        {
            _appDbContext.Properties.Add(property);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetProperty", new { id = property.PropertyID }, property);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProperty(int id, Property updatedProperty)
        {
            var property = await _appDbContext.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            property.ListPrice = updatedProperty.ListPrice;
            property.Status = updatedProperty.Status;
            property.NoOfBedrooms = updatedProperty.NoOfBedrooms;
            property.NoOfBathrooms = updatedProperty.NoOfBathrooms;
            property.City = updatedProperty.City;
            property.SalesOfficeID = updatedProperty.SalesOfficeID;

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
        public async Task<ActionResult> DeleteProperty(int id)
        {
            var property = await _appDbContext.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            _appDbContext.Properties.Remove(property);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("ByOffice/{salesOfficeId}")]
        public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesByOffice(int salesOfficeId)
        {
            var properties = await _appDbContext.Properties
                                 .Where(p => p.SalesOfficeID == salesOfficeId)
                                 .ToListAsync();

            return properties;
        }

    }
}
