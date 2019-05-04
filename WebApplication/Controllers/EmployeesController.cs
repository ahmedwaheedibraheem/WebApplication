using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Entities;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        static ApplicationDbContext context = new ApplicationDbContext();
        // GET: Employees
        public ActionResult Index()
        {
            EmployeeViewModel employeeVM = new EmployeeViewModel();
            employeeVM.Employees = context.Employees.Include(e => e.Department).ToList();
            employeeVM.Departments = context.Departments.ToList();
            return View(employeeVM);
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return PartialView("_PartialEmployeeRaw", employee);
            }
            EmployeeViewModel employeeVM = new EmployeeViewModel();
            employeeVM.Departments = context.Departments.ToList();
            return View("AddEdit", employeeVM);
        }

        public ActionResult Edit(int id)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                ViewBag.Action = "Edit";
                EmployeeViewModel evm = new EmployeeViewModel() {
                    Departments = context.Departments.ToList(),
                    Employees = context.Employees.ToList(),
                    Employee = emp
                   
                };
               
                return View("AddEdit", evm);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var oldEmp = context.Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (oldEmp != null)
                {
                    oldEmp.Name = employee.Name;
                    oldEmp.Age = employee.Age;
                    oldEmp.Gender = employee.Gender;
                    oldEmp.Email = employee.Email;
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View("AddEdit", employee);
        }

        public ActionResult Details(int id)
        {
            Employee result = context.Employees.FirstOrDefault(e => e.Id == id);
            if (result != null)
            {
                return View(result);
            } else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<bool> Delete(int id)
        {
            Employee result = context.Employees.FirstOrDefault(e => e.Id == id);
            if (result != null)
            {
                context.Employees.Remove(result);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}