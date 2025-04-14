using EmployeePortal.Model;
using EmployeePortal.Repository;
using EmployeePortal.Repository.Common;
using EmployeePortal.Service.Common;

namespace EmployeePortal.Service
{
    public class EmployeeService : IEmployeeService
    {
        public bool DeleteEmployee(Guid id)
        {
            var repository = new EmployeeRepository();
            return repository.DeleteEmployee(id);
        }

        public List<Employee> GetAll()
        {
            var repository = new EmployeeRepository();
            return repository.GetAll();
        }

        public Employee GetById(Guid id)
        {
            var repository = new EmployeeRepository();
            return repository.GetById(id);
        }

        public bool SaveEmployee(Employee employee)
        {
            var repository = new EmployeeRepository();
            return repository.SaveEmployee(employee);
        }

        public bool UpdateEmployee(Guid id, Employee employee)
        {
            var repository = new EmployeeRepository();
            return repository.UpdateEmployee(id, employee);
        }
    }
}