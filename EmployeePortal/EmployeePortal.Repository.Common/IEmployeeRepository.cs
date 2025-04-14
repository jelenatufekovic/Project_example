using EmployeePortal.Model;

namespace EmployeePortal.Repository.Common
{
    public interface IEmployeeRepository
    {
        bool UpdateEmployee(Guid id, Employee employee);

        bool DeleteEmployee(Guid id);

        List<Employee> GetAll();

        Employee GetById(Guid id);

        bool SaveEmployee(Employee employee);
    }
}