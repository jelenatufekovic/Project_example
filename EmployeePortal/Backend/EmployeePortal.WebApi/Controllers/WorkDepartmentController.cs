using Autofac.Core;
using EmployeePortal.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDepartmentController : Controller
    {
        private IWorkDepartmentService _service;

        public WorkDepartmentController(IWorkDepartmentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var workDepartments = await _service.GetAllAsync();
            if (workDepartments == null)
            {
                return BadRequest();
            }
            return Ok(workDepartments);
        }
    }
}