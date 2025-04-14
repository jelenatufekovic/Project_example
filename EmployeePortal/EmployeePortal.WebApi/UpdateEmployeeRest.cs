namespace EmployeePortal.WebApi
{
    public class UpdateEmployeeRest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid? WorkDepartmentId { get; set; }
    }
}