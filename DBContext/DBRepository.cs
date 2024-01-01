using DataAccounting.Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccounting.DBContext
{
    public class DBRepository
    {
        SqlConnection _connection;
        string _connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataAccounting;Integrated Security=True";

        public DBRepository()
        {
            _connection = new SqlConnection(_connectionStr);
        }


        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (_connection)
            {
                if (String.IsNullOrEmpty(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionStr;
                _connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", _connection);

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
            using (_connection)
            {
                if (String.IsNullOrEmpty(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionStr;
                _connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Departments", _connection);

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

        public List<Position> GetAllPositions()
        {
            List<Position> positions = new List<Position>();
            using (_connection)
            {
                if (String.IsNullOrEmpty(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionStr;
                _connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Positions", _connection);

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

        public bool AddEmployee(Employee employee)
        {
            bool i = false;
            using(_connection)
            {
                if (String.IsNullOrEmpty(_connection.ConnectionString))
                    _connection.ConnectionString = _connectionStr;
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[Employees] ([EmployeeFullName], [Birthday], [EmploymentDate], [Salary], [DepartmentId], [PositionId]) VALUES (N'{employee.EmployeeFullName}', N'{employee.Birthday.ToString("yyyy-MM-dd")}', N'{employee.EmploymentDate.ToString("yyyy-MM-dd")}', CAST({employee.Salary} AS Money), {employee.DepartmentId}, {employee.PositionId})", _connection);

                i = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            return i;
        }
    }
}
