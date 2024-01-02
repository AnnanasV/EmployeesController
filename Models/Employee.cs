using System.ComponentModel;

namespace DataAccounting.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        
        [DisplayName("ПІБ")]
        public string EmployeeFullName { get; set; }

        [DisplayName("Дата народження")]
        public DateTime Birthday { get; set; }

        [DisplayName("Дата прийняття на роботу")]
        public DateTime EmploymentDate { get; set; }

        [DisplayName("Заробітна плата")]
        public decimal Salary { get; set; }

        [DisplayName("Відділ")]
        public int DepartmentId { get; set; }

        [DisplayName("Посада")]
        public int PositionId { get; set; }
    }
}
