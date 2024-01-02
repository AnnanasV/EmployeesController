using DataAccounting.Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccounting.DBContext
{
    public class DBRepository
    {
        private readonly string _connectionStr;

        public DBRepository(string connectionStr)
        {
            _connectionStr = connectionStr;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionStr);
        }


        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                foreach (DataRow reader in dt.Rows)
                {
                    employees.Add(new Employee()
                    {
                        EmployeeID = (int)reader["EmployeeId"],
                        EmployeeFullName = (string)reader["EmployeeFullName"],
                        Birthday = (DateTime)reader["Birthday"],
                        EmploymentDate = (DateTime)reader["EmploymentDate"],
                        Salary = (decimal)reader["Salary"],
                        DepartmentId = (int)reader["DepartmentId"],
                        PositionId = (int)reader["PositionId"]
                    });
                }
            }

            return employees;
        }

        public List<Employee> GetFilteredEmployees(string searchTerm, string sortBy)
        {
            List<Employee> employees = new List<Employee> ();
            using(SqlConnection connection = CreateConnection())
            {
                connection.Open();

                string queryStr = $"SELECT * FROM Employees WHERE EmployeeFullName LIKE '%{searchTerm}%'";

                var department = GetDepartmentByName(searchTerm);
                var position = GetPositionByName(searchTerm);

                if (department != null) queryStr += $" OR DepartmentId = {department.DepartmentId}";
                if (position != null) queryStr += $" OR PositionId = {position.PositionId}";

                queryStr += $" ORDER BY {sortBy}";

                SqlCommand cmd = new SqlCommand(queryStr, connection);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                foreach (DataRow reader in dt.Rows)
                {
                    employees.Add(new Employee()
                    {
                        EmployeeID = (int)reader["EmployeeId"],
                        EmployeeFullName = (string)reader["EmployeeFullName"],
                        Birthday = (DateTime)reader["Birthday"],
                        EmploymentDate = (DateTime)reader["EmploymentDate"],
                        Salary = (decimal)reader["Salary"],
                        DepartmentId = (int)reader["DepartmentId"],
                        PositionId = (int)reader["PositionId"]
                    });
                }
            }
            return employees;
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Departments", connection);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                foreach (DataRow reader in dt.Rows)
                {
                    departments.Add(new Department()
                    {
                        DepartmentId = (int)reader["DepartmentId"],
                        DepartmentName = (string)reader["DepartmentName"]
                    });
                }
            }
            return departments;
        }

        public Department GetDepartmentByName(string name)
        {
            Department department = new Department();
            using(SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Departments WHERE DepartmentName LIKE '%{name}%'", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        department = new Department()
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = (string)reader["DepartmentName"]
                        };
                    }
                }
            }
            return department;
        }

        public List<Position> GetAllPositions()
        {
            List<Position> positions = new List<Position>();
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Positions", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        positions.Add(new Position()
                        {
                            PositionId = (int)reader["PositionId"],
                            PositionName = (string)reader["PositionName"]
                        });
                    }
                }
            }
            return positions;
        }

        public Position GetPositionByName(string name)
        {
            Position position = new Position();
            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Positions WHERE PositionName LIKE '%{name}%'", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        position = new Position()
                        {
                            PositionId = (int)reader["PositionId"],
                            PositionName = (string)reader["PositionName"]
                        };
                    }
                }
            }
            return position;
        }

        public bool AddEmployee(Employee employee)
        {
            bool i = false;
            using(SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[Employees] ([EmployeeFullName], [Birthday], [EmploymentDate], [Salary], [DepartmentId], [PositionId]) VALUES (N'{employee.EmployeeFullName}', N'{employee.Birthday.ToString("yyyy-MM-dd")}', N'{employee.EmploymentDate.ToString("yyyy-MM-dd")}', CAST({employee.Salary} AS Money), {employee.DepartmentId}, {employee.PositionId})", connection);

                i = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            return i;
        }

        public bool DeleteEmployee(int id)
        {
            bool i = false;
            using( SqlConnection connection = CreateConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Employees WHERE EmployeeId = {id}", connection);
                i = Convert.ToBoolean(cmd.ExecuteNonQuery());

            }

            return i;
        }
    }
}
