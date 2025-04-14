using EmployeePortal.Model;

namespace EmployeePortal.Service.Common
{
    public interface IEmployeeService
    {
        bool UpdateEmployee(Guid id, Employee employee);

        bool DeleteEmployee(Guid id);

        List<Employee> GetAll();

        Employee GetById(Guid id);

        bool SaveEmployee(Employee employee);
    }
}