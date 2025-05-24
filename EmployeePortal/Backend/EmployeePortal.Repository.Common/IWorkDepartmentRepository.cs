using EmployeePortal.Model;

namespace EmployeePortal.Repository.Common
{
    public interface IWorkDepartmentRepository
    {
        Task<List<WorkDepartment>> GetAllAsync();
    }
}