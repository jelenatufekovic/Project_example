using Microsoft.Extensions.Configuration;
using Npgsql;
using EmployeePortal.Repository.Common;
using EmployeePortal.Model;

namespace WorkDepartmentPortal.Repository
{
    public class WorkDepartmentRepository : IWorkDepartmentRepository
    {
        public WorkDepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        private string _connectionString;

        public async Task<List<WorkDepartment>> GetAllAsync()
        {
            try
            {
                var workDepartments = new List<WorkDepartment>();
                using var connection = new NpgsqlConnection(_connectionString);
                var commandText = "SELECT * FROM \"WorkDepartment\" ;";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var workDepartment = new WorkDepartment();
                        workDepartment.Id = Guid.Parse(reader[0].ToString());
                        workDepartment.Name = reader[1].ToString();

                        workDepartments.Add(workDepartment);
                    }
                }
                return workDepartments;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}