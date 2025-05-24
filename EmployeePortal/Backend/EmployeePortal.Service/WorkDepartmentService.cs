using EmployeePortal.Model;
using EmployeePortal.Repository.Common;
using EmployeePortal.Service.Common;

namespace WorkDepartmentPortal.Service
{
    public class WorkDepartmentService : IWorkDepartmentService
    {
        private IWorkDepartmentRepository _repository;

        public WorkDepartmentService(IWorkDepartmentRepository repository)
        { _repository = repository; }

        public async Task<List<WorkDepartment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}