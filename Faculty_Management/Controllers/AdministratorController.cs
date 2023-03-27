using Faculty_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Faculty_Management.Controllers
{
    public class AdministratorController : Controller
    {
        FacultyBassettiEntities _dbContext = new FacultyBassettiEntities();
        // GET: Administrator
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateFaculty()
        {
            var departmentslist = _dbContext.Departments.ToList();
            ViewBag.DeptId = new SelectList(departmentslist, "DeptId", "DeptName");
            var designationlist = _dbContext.Designations.ToList();
            ViewBag.DesignationId = new SelectList(designationlist, "DesignationId", "DesignationName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateFaculty(Faculty model)
        {
            var departmentslist = _dbContext.Departments.ToList();
            ViewBag.DeptId = new SelectList(departmentslist, "DeptId", "DeptName");
            var designationlist = _dbContext.Designations.ToList();
            ViewBag.DesignationId = new SelectList(designationlist, "DesignationId", "DesignationName");
            
            _dbContext.Faculties.Add(model);
            User Umodel = new User();
            Umodel.UserId = model.FacultyId;
            Umodel.Email = model.Email;
            Umodel.Name = model.Name;
            //Umodel.Email = model.FirstName + "@Faculty.com";
            Umodel.Password = model.Name + "@Pass" + model.FacultyId.ToString();
            Umodel.UserType = 2;
            _dbContext.Users.Add(Umodel);
            _dbContext.SaveChanges();
            TempData["AlertMessage"] = "New Faculty is Registered successfully....!" + "LogIn Email is: " + Umodel.Email + " And Password is: " + Umodel.Password;
            //ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("AdminLogin");
        }


        // *********************Add Department***********************
        [HttpGet]
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(Department model)
        {
            _dbContext.Departments.Add(model);
            _dbContext.SaveChanges();
            TempData["AlertMessage"] = "Department Add Successfully";
            return RedirectToAction("AdminLogin");
        }

        // *********************Add Designation***********************
        [HttpGet]
        public ActionResult CreateDesignation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDesignation(Designation model)
        {
            _dbContext.Designations.Add(model);
            _dbContext.SaveChanges();
            TempData["AlertMessage"] = "Designation Add Successfully";
            return RedirectToAction("AdminLogin");
        }

        // ********************Delete Department***********************
        [HttpGet]
        public ActionResult DeleteDepartment(string id)
        {
            int _id = Convert.ToInt32(id);
            return View(_dbContext.Departments.Where(x => x.DeptId == _id).FirstOrDefault());
        }
        public ActionResult DeleteDepartment(string id, Department Model)
        {
            var data = _dbContext.Departments.Where(x => x.DeptId == Model.DeptId).FirstOrDefault();
            _dbContext.Departments.Remove(data);
            _dbContext.SaveChanges();
            TempData["AlertMessage"] = "Course Delete Successfully";
            return RedirectToAction("AdminLogin");
        }

        // ********************Delete Faculty***********************
        [HttpGet]
        public ActionResult DeleteFaculty(string id)
        {
            var departmentslist = _dbContext.Departments.ToList();
            ViewBag.DeptId = new SelectList(departmentslist, "DeptId", "DeptName");
            var designationlist = _dbContext.Designations.ToList();
            ViewBag.DesignationId = new SelectList(designationlist, "DesignationId", "DesignationName");


            int _id = Convert.ToInt32(id);
            var email = (string)Session["Email"];
            var data = _dbContext.Faculties.Where(x => x.FacultyId == _id).FirstOrDefault();
            return View(data);
        }
        public ActionResult DeleteFaculty(User users, Faculty model)
        {
            var departmentslist = _dbContext.Departments.ToList();
            ViewBag.DeptId = new SelectList(departmentslist, "DeptId", "DeptName");
            var designationlist = _dbContext.Designations.ToList();
            ViewBag.DesignationId = new SelectList(designationlist, "DesignationId", "DesignationName");


            var data = _dbContext.Faculties.Where(x => x.FacultyId == model.FacultyId).FirstOrDefault();
            int uId = model.FacultyId;
            var data1 = _dbContext.Users.Where(u => u.UserId == uId).FirstOrDefault();
            if (data != null)
            {
                data.LeaveDate = model.LeaveDate;
                data.Status = model.Status;

                Session["stat"] = model.Status;
                
                _dbContext.Users.Remove(data1);

                _dbContext.SaveChanges();
            }
                TempData["AlertMessage"] = "Faculty Delete Successfully";
            return RedirectToAction("AdminLogin");
        }

        [HttpGet]
        public ActionResult ViewDeletedFaculty(string id)
        {
            //int? stat = (int)Session["stat"];
            var data = _dbContext.Faculties.Where(x => x.Status == 0).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Search()
        {
            /*var facultyList = _dbContext.Faculties.ToList();
            return View(facultyList);*/
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchValue)
        {
            var facultyList = _dbContext.Faculties.ToList();
            if (searchValue != null)
            {
                facultyList = _dbContext.Faculties.Where(x => x.Name.Contains(searchValue)).ToList();
            }
            return View(facultyList);
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

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}