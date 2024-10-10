using RealStateApp.Data;
using RealStateApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmpID }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, Employee updatedEmployee)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.EmpFirstName = updatedEmployee.EmpFirstName;
            employee.EmpLastName = updatedEmployee.EmpLastName;
            employee.SalesOfficeID = updatedEmployee.SalesOfficeID;
            employee.DateOfBirth = updatedEmployee.DateOfBirth;
            employee.Age = updatedEmployee.Age;

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
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("ByOffice/{salesOfficeId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByOffice(int salesOfficeId)
        {
            var employees = await _appDbContext.Employees
                                 .Where(e => e.SalesOfficeID == salesOfficeId)
                                 .ToListAsync();

            return employees;
        }
    }
}
