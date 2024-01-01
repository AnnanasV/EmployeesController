using DataAccounting.DBContext;
using DataAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        DBRepository db = new DBRepository();

        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            employees = db.GetAllEmployees();

            return View(employees);
        }

        public IActionResult AddEmployee()
        {
            List<Department> departments = db.GetAllDepartments();
            List<Position> positions = db.GetAllPositions();

            ViewBag.DepartmentList = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.PositionList = new SelectList(positions, "PositionId", "PositionName");

            return View();
        }

        public IActionResult AddNewEmployee(Employee employee)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(db.AddEmployee(employee))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return RedirectToAction(nameof(AddEmployee));
            }
            return RedirectToAction(nameof(AddEmployee));
        }

    }
}
