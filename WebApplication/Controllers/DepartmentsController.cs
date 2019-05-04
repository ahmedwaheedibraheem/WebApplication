using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.Entities;

namespace WebApplication.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Departments
        public ActionResult Index()
        {
            return View(context.Departments.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(department);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public async Task<ActionResult> Delete(int id)
        {
            Department result = context.Departments.FirstOrDefault(d => d.Id == id);
            if (result != null)
            {
                context.Departments.Remove(result);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}