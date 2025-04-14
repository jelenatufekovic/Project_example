using EmployeePortal.Model;
using EmployeePortal.Repository.Common;
using Npgsql;

namespace EmployeePortal.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string connectionString = "Server=localhost;Port=5433;Userid=postgres;Password=postgres;Database=EmployeePortalDB";

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "DELETE FROM \"Employee\" WHERE \"Id\"= @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    var numberOfCommits = await command.ExecuteNonQueryAsync();

                    connection.Close();

                    return numberOfCommits > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            try
            {
                var employees = new List<Employee>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Employee\" e LEFT JOIN \"WorkDepartment\" wp ON wp.\"Id\"=e.\"WorkDepartmentId\";";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new Employee();
                        employee.Id = Guid.Parse(reader[0].ToString());
                        employee.FirstName = reader[1].ToString();
                        employee.LastName = reader["LastName"].ToString();
                        employee.DateOfBirth = reader.GetFieldValue<DateOnly>(3);
                        employee.WorkDepartmentId = Guid.TryParse(reader[4].ToString(), out var result) ? result : null; ;
                        if (employee.WorkDepartmentId != null)
                        {
                            var workDepartment = new WorkDepartment();
                            workDepartment.Id = employee.WorkDepartmentId.Value;
                            workDepartment.Name = reader[6].ToString();
                            employee.WorkDepartment = workDepartment;
                        }

                        employees.Add(employee);
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            try
            {
                var employee = new Employee();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Employee\" e LEFT JOIN \"WorkDepartment\" wp ON wp.\"Id\"=e.\"WorkDepartmentId\" WHERE e.\"Id\"= @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();

                    employee.Id = Guid.Parse(reader[0].ToString());
                    employee.FirstName = reader[1].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.DateOfBirth = reader.GetFieldValue<DateOnly>(3);
                    employee.WorkDepartmentId = Guid.TryParse(reader[4].ToString(), out var result) ? result : null;
                    if (employee.WorkDepartmentId != null)
                    {
                        var workDepartment = new WorkDepartment();
                        workDepartment.Id = employee.WorkDepartmentId.Value;
                        workDepartment.Name = reader[6].ToString();
                        employee.WorkDepartment = workDepartment;
                    }
                }
                return employee;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveEmployeeAsync(Employee newEmployee)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = $"INSERT INTO \"Employee\" VALUES( @id, @firstName, @lastName, @dob, @workDepartmentId);";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@firstName", newEmployee.FirstName);
                command.Parameters.AddWithValue("@lastName", newEmployee.LastName);
                command.Parameters.AddWithValue("@dob", newEmployee.DateOfBirth);
                command.Parameters.AddWithValue("@workDepartmentId", NpgsqlTypes.NpgsqlDbType.Uuid, newEmployee.WorkDepartmentId is null ? DBNull.Value : newEmployee.WorkDepartmentId.Value);

                connection.Open();

                var numberOfCommits = await command.ExecuteNonQueryAsync();

                connection.Close();

                return numberOfCommits > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Guid id, Employee updatedEmployee)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var selectCommandText = "SELECT * FROM \"Employee\" WHERE \"Id\"= @id;";
                using var command = new NpgsqlCommand(selectCommandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows) { return false; }

                reader.Close();
                var updateCommandText = "UPDATE \"Employee\" SET \"FirstName\"=@firstName, \"LastName\"=@lastName, \"DateOfBirth\"=@dob, \"WorkDepartmentId\"=@workDepartmentId WHERE \"Id\"=@id;";
                command.CommandText = updateCommandText;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", updatedEmployee.FirstName);
                command.Parameters.AddWithValue("@lastName", updatedEmployee.LastName);
                command.Parameters.AddWithValue("@dob", updatedEmployee.DateOfBirth);
                command.Parameters.AddWithValue("@workDepartmentId", NpgsqlTypes.NpgsqlDbType.Uuid, updatedEmployee.WorkDepartmentId is null ? DBNull.Value : updatedEmployee.WorkDepartmentId.Value);

                var numberOfCommits = await command.ExecuteNonQueryAsync();

                return numberOfCommits > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}