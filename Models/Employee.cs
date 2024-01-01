namespace DataAccounting.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime EmploymentDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
    }
}
