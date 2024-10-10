using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOfficeController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public SalesOfficeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOffice>>> GetAllSalesOffices()
        {
            return await _appDbContext.SalesOffices.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOffice>> GetSalesOffice(int id)
        {
            var salesOffice = await _appDbContext.SalesOffices.FindAsync(id);

            if (salesOffice == null)
            {
                return NotFound();
            }

            return salesOffice;
        }

        [HttpPost]
        public async Task<ActionResult<SalesOffice>> PostSalesOffice(SalesOffice salesOffice)
        {
            _appDbContext.SalesOffices.Add(salesOffice);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction("GetSalesOffice", new { id = salesOffice.OfficeID }, salesOffice);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutSalesOffice(int id, SalesOffice updatedSalesOffice)
        {
            var salesOffice = await _appDbContext.SalesOffices.FindAsync(id);

            if (salesOffice == null)
            {
                return NotFound();
            }

            salesOffice.OfficeName = updatedSalesOffice.OfficeName;
            salesOffice.AddressID = updatedSalesOffice.AddressID;
            salesOffice.ManagedByEmployeeID = updatedSalesOffice.ManagedByEmployeeID;

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
        public async Task<ActionResult> DeleteSalesOffice(int id)
        {
            var salesOffice = await _appDbContext.SalesOffices.FindAsync(id);
            if (salesOffice == null)
            {
                return NotFound();
            }

            _appDbContext.SalesOffices.Remove(salesOffice);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
