using DataAccounting.DBContext;
using DataAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        DBRepository db;


        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new DBRepository(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Main table and filtering
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();

            List<Department> departments = db.GetAllDepartments();
            List<Position> positions = db.GetAllPositions();

            ViewBag.Departments = departments.ToDictionary(d => d.DepartmentId, d => d.DepartmentName);
            ViewBag.Positions = positions.ToDictionary(p => p.PositionId, p => p.PositionName);

            employees = db.GetAllEmployees();

            return View(employees);
        }


        public IActionResult FilterEmployees(string searchTerm, string sortBy)
        {
            List<Employee> employees = db.GetFilteredEmployees(searchTerm, sortBy);

            List<Department> departments = db.GetAllDepartments();
            List<Position> positions = db.GetAllPositions();

            ViewBag.Departments = departments.ToDictionary(d => d.DepartmentId, d => d.DepartmentName);
            ViewBag.Positions = positions.ToDictionary(p => p.PositionId, p => p.PositionName);

            return View(nameof(Index), employees);
        }
        #endregion

        #region AddEmployee
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
        #endregion

        public IActionResult DeleteEmployee(int id)
        {
            if(db.DeleteEmployee(id)) return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }
    }
}
