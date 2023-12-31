using DataAccounting.Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccounting.DBContext
{
    public class DBRepository
    {
        SqlConnection _connection;
        public DBRepository()
        { 
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataAccounting;Integrated Security=True";
            _connection = new SqlConnection(connectionStr);
        }


        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (_connection)
            {
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
    }
}
