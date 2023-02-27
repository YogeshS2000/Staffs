using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
           _fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeess = await _fullStackDbContext.employees.ToListAsync();
            return Ok(employeess);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeerequest)
        {
            employeerequest.Id= Guid.NewGuid();
            await _fullStackDbContext.employees.AddAsync(employeerequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeerequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployees([FromRoute] Guid id,Employee updateEmployeRequest)
        {
            var employee=await _fullStackDbContext.employees.FindAsync(id);

            if(employee==null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeRequest.Name;
            employee.Phone = updateEmployeRequest.Phone;
            employee.Email= updateEmployeRequest.Email;
            employee.Salary=    updateEmployeRequest.Salary;
            employee.Department= updateEmployeRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployees([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _fullStackDbContext.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}           
