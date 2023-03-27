using Faculty_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Faculty_Management.Controllers
{
    public class HomeController : Controller
    {
        FacultyBassettiEntities _dbContext = new FacultyBassettiEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult ViewDepartments()
        {
            return View(_dbContext.Departments.ToList());
        }
        [HttpGet]
        public ActionResult ViewPublications()
        {

            return View(_dbContext.Publications.ToList());
        }
        [HttpGet]
        public ActionResult SearchByDept()
        {
            /*var facultyList = _dbContext.Faculties.ToList();
            return View(facultyList);*/
            return View();
        }
        [HttpPost]
        public ActionResult SearchByDept(string searchValue)
        {
            var facultyList = _dbContext.Faculties.ToList();

            if (searchValue != null)
            {
                facultyList = _dbContext.Faculties.Where(x => x.Department.DeptName == searchValue).ToList();
            }
            return View(facultyList);
        }

    }
}