using Faculty_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Faculty_Management.Controllers
{
    public class FacultyController : Controller
    {
        FacultyBassettiEntities _dbContext = new FacultyBassettiEntities();
        // GET: Faculty
        public ActionResult FacultyLogin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UpdateProfile(string id)
        {
            int _id = Convert.ToInt32(id);
            int cId = (int)Session["UserId"];
            var data = _dbContext.Users.Where(x => x.UserId == cId).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(User model)
        {
            int cId = (int)Session["UserId"];
            var data = _dbContext.Users.Where(x => x.UserId == cId).FirstOrDefault();
            if (data != null)
            {
                data.Email = model.Email;
                data.Password = model.Password;

                _dbContext.SaveChanges();
            }

            ViewBag.Message = "Data Updated Successfully";
            return RedirectToAction("FacultyLogin");
        }


        [HttpGet]
        public ActionResult UpdateFacultyInfo(string id)
        {
            var departmentslist = _dbContext.Departments.ToList();
            ViewBag.DeptId = new SelectList(departmentslist, "DeptId", "DeptName");
            var designationlist = _dbContext.Designations.ToList();
            ViewBag.DesignationId = new SelectList(designationlist, "DesignationId", "DesignationName");

            int _id = (int)Session["UserId"];
            var email = (string)Session["Email"];
            var data = _dbContext.Faculties.Where(x => x.FacultyId == _id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult UpdateFacultyInfo(Faculty model)
        {
            var departmentslist = _dbContext.Departments.ToList();
            ViewBag.DeptId = new SelectList(departmentslist, "DeptId", "DeptName");
            var designationlist = _dbContext.Designations.ToList();
            ViewBag.DesignationId = new SelectList(designationlist, "DesignationId", "DesignationName");

            var data = _dbContext.Faculties.Where(x => x.FacultyId == model.FacultyId).FirstOrDefault();
            if (data != null)
            {
                data.Name = model.Name;
                data.Email = model.Email;
                data.Gender = model.Gender;
                data.Address = model.Address;
                data.City = model.City;
                data.PinCode = model.PinCode;
                data.MobileNumber = model.MobileNumber;
                data.Experience = model.Experience;
                data.Degree = model.Degree;

                _dbContext.SaveChanges();
            }

            ViewBag.Message = "Data Updated Successfully";
            return RedirectToAction("FacultyLogin");
        }

        [HttpGet]
        public ActionResult CreatePublications()
        {
            var facultylist = _dbContext.Faculties.ToList();
            ViewBag.FacultyId = new SelectList(facultylist, "FacultyId", "Name");
            return View();
        }
        [HttpPost]

        public ActionResult CreatePublications(Publication model)
        {
            var facultylist = _dbContext.Faculties.ToList();
            ViewBag.FacultyId = new SelectList(facultylist, "FacultyId", "Name");

            int cId = (int)Session["UserId"];
            model.FacultyId = cId;
            _dbContext.Publications.Add(model);
            _dbContext.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return RedirectToAction("FacultyLogin");
        }

        [HttpGet]
        public ActionResult UpdatePublications(string id)
        {
            var facultieslist = _dbContext.Faculties.ToList();
            ViewBag.FacultyId = new SelectList(facultieslist, "FacultyId", "Name");


            int _id = Convert.ToInt32(id);
            int cId = (int)Session["UserId"];
            var data = _dbContext.Publications.Where(x => x.FacultyId == cId).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdatePublications(Publication Model)
        {
            var facultieslist = _dbContext.Faculties.ToList();
            ViewBag.FacultyId = new SelectList(facultieslist, "FacultyId", "Name");

            int cId = (int)Session["UserId"];
            var data = _dbContext.Publications.Where(x => x.FacultyId == cId).FirstOrDefault();
            if (data != null)
            {
                data.PublicationTiltle = Model.PublicationTiltle;
                data.ArticleName = Model.ArticleName;
                data.PublisherName = Model.PublisherName;
                data.PublicationLocation = Model.PublicationLocation;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("FacultyLogin");
        }

        [HttpGet]
        public ActionResult ViewPublication()
        {
            int id = (int)Session["UserId"];
            var data = _dbContext.Publications.Where(x => x.FacultyId == id).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}