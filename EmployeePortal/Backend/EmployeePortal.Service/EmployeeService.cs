using EmployeePortal.Model;
using EmployeePortal.Repository;
using EmployeePortal.Repository.Common;
using EmployeePortal.Service.Common;

namespace EmployeePortal.Service
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        { _repository = repository; }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            return await _repository.DeleteEmployeeAsync(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            return await _repository.SaveEmployeeAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee employee)
        {
            return await _repository.UpdateEmployeeAsync(id, employee);
        }
    }
}