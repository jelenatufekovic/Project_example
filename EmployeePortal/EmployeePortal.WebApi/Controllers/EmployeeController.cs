using Microsoft.AspNetCore.Mvc;
using Npgsql;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeePortal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private const string connectionString = "Server=localhost;Port=5433;Userid=postgres;Password=postgres;Database=EmployeePortalDB";

        //data annotation example for route
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var employees = new List<Employee>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Employee\" e LEFT JOIN \"WorkDepartment\" wp ON wp.\"Id\"=e.\"WorkDepartmentId\";";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();

                using var reader = command.ExecuteReader();

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
                if (employees.Count == 0)
                {
                    return NotFound();
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var employee = new Employee();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Employee\" e LEFT JOIN \"WorkDepartment\" wp ON wp.\"Id\"=e.\"WorkDepartmentId\" WHERE e.\"Id\"= @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using var reader = command.ExecuteReader();

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
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee newEmployee)
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

                var numberOfCommits = command.ExecuteNonQuery();

                connection.Close();
                if (numberOfCommits == 0)
                {
                    return BadRequest();
                }
                return Ok("Successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Employee updatedEmployee)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var selectCommandText = "SELECT * FROM \"Employee\" WHERE \"Id\"= @id;";
                using var command = new NpgsqlCommand(selectCommandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using var reader = command.ExecuteReader();

                if (!reader.HasRows) { return BadRequest(); }

                reader.Close();
                var updateCommandText = "UPDATE \"Employee\" SET \"FirstName\"=@firstName, \"LastName\"=@lastName, \"DateOfBirth\"=@dob, \"WorkDepartmentId\"=@workDepartmentId WHERE \"Id\"=@id;";
                command.CommandText = updateCommandText;

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@firstName", updatedEmployee.FirstName);
                command.Parameters.AddWithValue("@lastName", updatedEmployee.LastName);
                command.Parameters.AddWithValue("@dob", updatedEmployee.DateOfBirth);
                command.Parameters.AddWithValue("@workDepartmentId", NpgsqlTypes.NpgsqlDbType.Uuid, updatedEmployee.WorkDepartmentId is null ? DBNull.Value : updatedEmployee.WorkDepartmentId.Value);

                var numberOfCommits = command.ExecuteNonQuery();

                if (numberOfCommits == 0)
                {
                    return NotFound();
                }
                return Ok("Successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var commandText = "DELETE FROM \"Employee\" WHERE \"Id\"= @id;";
                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    var numberOfCOmmits = command.ExecuteNonQuery();

                    connection.Close();

                    if (numberOfCOmmits == 0)
                    {
                        return NotFound();
                    }

                    return Ok("Deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}