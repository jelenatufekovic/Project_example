using EmployeePortal.Model;
using EmployeePortal.Repository;
using EmployeePortal.Repository.Common;
using EmployeePortal.Service.Common;

namespace EmployeePortal.Service
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var repository = new EmployeeRepository();
            return await repository.DeleteEmployeeAsync(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var repository = new EmployeeRepository();
            return await repository.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            var repository = new EmployeeRepository();
            return await repository.GetByIdAsync(id);
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var repository = new EmployeeRepository();
            return await repository.SaveEmployeeAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee employee)
        {
            var repository = new EmployeeRepository();
            return await repository.UpdateEmployeeAsync(id, employee);
        }
    }
}