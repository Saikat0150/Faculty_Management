using Faculty_Management.Data;
using Faculty_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Faculty_Management.Controllers
{
    public class UserLoginController : Controller
    {
        FacultyBassettiEntities _dbContext = new FacultyBassettiEntities();
        // GET: UserLogin
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = UserLogic.SignUpUser(model.UserId, model.Name, model.Email, model.Password, model.UserType);

                if (recordsCreated == 1)
                {
                    return RedirectToAction("SignIn");
                }
            }

            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult SignIn(User model)
        {

            var data = UserLogic.SignInUser(model.UserId, model.Name, model.Email, model.Password, model.UserType);
            if (data.Count == 0)
            {
                Faculty faculty = new Faculty();
                var fdata = _dbContext.Faculties.Where(x => x.Email == model.Email).ToList();
                if (fdata.Count != 0)
                {
                    TempData["AlertMessage"] = "Acount Blocked..... Please Contact with Admin";
                    return View();
                }
                else
                {
                    TempData["AlertMessage"] = "Invalid User...... Please Contact with Admin";
                    return View();
                }
                
            }
            else
            {
                List<User> obj = new List<User>();

                foreach (var row in data)
                {
                    obj.Add(new User
                    {
                        UserId = row.UserId,
                        Name = row.Name,
                        Email = row.Email,
                        Password = row.Password,
                        UserType = row.UserType
                    });
                }



                int response = obj[0].UserType;
                FormsAuthentication.SetAuthCookie(obj[0].Name, false);
                if (response == 1)
                {
                    Session["UserId"] = obj[0].UserId;
                    Session["Email"] = obj[0].Email;
                    return RedirectToAction("AdminLogin", "Administrator");
                }
                else if (response == 2)
                {
                    Session["UserId"] = obj[0].UserId;
                    Session["Email"] = obj[0].Email;

                    return RedirectToAction("FacultyLogin", "Faculty");
                }

            }


            return View();
        }

    }
}