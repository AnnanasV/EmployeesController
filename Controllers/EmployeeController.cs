using DataAccounting.DBContext;
using DataAccounting.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            DBRepository db = new DBRepository();

            employees = db.GetAllEmployees();

            return View(employees);
        }
    }
}
