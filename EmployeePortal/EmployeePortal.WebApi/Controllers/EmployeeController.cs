using EmployeePortal.Model;
using EmployeePortal.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeePortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //data annotation example for route
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var service = new EmployeeService();
            var employees = service.GetAll();
            if (employees == null)
            {
                return BadRequest();
            }
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var service = new EmployeeService();
            var employee = service.GetById(id);
            if (employee == null)
            {
                return BadRequest();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee newEmployee)
        {
            var service = new EmployeeService();
            var isSuccessful = service.SaveEmployee(newEmployee);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Employee updatedEmployee)
        {
            var service = new EmployeeService();
            var isSuccessful = service.UpdateEmployee(id, updatedEmployee);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var service = new EmployeeService();
            var isSuccessful = service.DeleteEmployee(id);
            if (!isSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}