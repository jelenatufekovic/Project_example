using EmployeePortal.Model;

namespace EmployeePortal.Service.Common
{
    public interface IWorkDepartmentService
    {
        Task<List<WorkDepartment>> GetAllAsync();
    }
}