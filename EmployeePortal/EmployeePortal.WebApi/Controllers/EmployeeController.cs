using EmployeePortal.Model;
using EmployeePortal.Service;
using EmployeePortal.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeePortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        //data annotation example for route
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await _service.GetAllAsync();
            if (employees == null)
            {
                return BadRequest();
            }
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null)
            {
                return BadRequest();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee newEmployee)
        {
            var isSuccessful = await _service.SaveEmployeeAsync(newEmployee);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, Employee updatedEmployee)
        {
            var isSuccessful = await _service.UpdateEmployeeAsync(id, updatedEmployee);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var isSuccessful = await _service.DeleteEmployeeAsync(id);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}