using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.WebApi
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public Guid? WorkDepartmentId { get; set; }

        public WorkDepartment? WorkDepartment { get; set; }
    }
}