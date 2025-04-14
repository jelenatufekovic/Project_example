using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeePortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>();

        //data anotation example for route
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            if (_employees == null)
            {
                return NotFound();
            }
            return Ok(_employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Employee? employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee newEmployee)
        {
            newEmployee.Id = Guid.NewGuid();
            _employees.Add(newEmployee);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Employee updatedEmployee)
        {
            Employee? currentEmployee = _employees.FirstOrDefault(e => e.Id == id);

            if (currentEmployee == null)
            {
                return BadRequest();
            }

            currentEmployee.FirstName = updatedEmployee.FirstName;
            currentEmployee.LastName = updatedEmployee.LastName;
            currentEmployee.WorkDepartmentId = updatedEmployee.WorkDepartmentId;
            currentEmployee.DateOfBirth = updatedEmployee.DateOfBirth;
            return Ok();
        }

        //example of sending complex object like query params and using rest models
        [HttpPut("{id}/updateFromQuery")]
        public IActionResult UpdateFromQuery(Guid id, [FromQuery] UpdateEmployeeRest updatedEmployee)
        {
            Employee? currentEmployee = _employees.FirstOrDefault(e => e.Id == id);

            if (currentEmployee == null)
            {
                return BadRequest();
            }

            currentEmployee.FirstName = updatedEmployee.FirstName;
            currentEmployee.LastName = updatedEmployee.LastName;
            currentEmployee.WorkDepartmentId = updatedEmployee.WorkDepartmentId;
            currentEmployee.DateOfBirth = updatedEmployee.DateOfBirth;
            return Ok(currentEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);

            if (employee is null)
            {
                return BadRequest();
            }

            _employees.Remove(employee);
            return Ok();
        }
    }
}