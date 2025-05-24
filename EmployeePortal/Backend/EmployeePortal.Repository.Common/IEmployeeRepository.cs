using EmployeePortal.Model;

namespace EmployeePortal.Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<bool> UpdateEmployeeAsync(Guid id, Employee employee);

        Task<bool> DeleteEmployeeAsync(Guid id);

        Task<List<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(Guid id);

        Task<bool> SaveEmployeeAsync(Employee employee);
    }
}