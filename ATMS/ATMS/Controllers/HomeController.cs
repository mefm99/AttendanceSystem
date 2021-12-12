using ATMS_TestingSubject.Classes;
using ATMS_TestingSubject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATMS_TestingSubject.Controllers
{
    public class HomeController : Controller
    {
        // you can use 'db' object to database connection
        private ATMS_Model db = new ATMS_Model();
        // home page view 
        [HttpGet]
        public ActionResult Index()
        {
            Logout();
            return View();
        }
       
        //  about page view
        public ActionResult About()
        {
            return View();
        }
        // contact page view
        public ActionResult Contact()
        {
            return View();
        }
        // login page view
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        // button for login 
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            // validation
            if (ModelState.IsValid)
            {
                string pass = CryptPassword.Hash(user.Passward);
                var UserCheck = db.UserInfoes.Where(x => x.Type == user.Type && x.Email == user.Email && x.Passward == pass).FirstOrDefault();
                if (UserCheck != null)
                {   // if user type => admin => redirect it to 'AdminController'
                    if (user.Type == "Admin")
                    {
                        Session["AdminId"] = UserCheck.Id;
                        Session["AdminName"] = UserCheck.Name;
                        return RedirectToAction("Index", "Admin");
                    }
                    // if user type => head =>if  he accepted redirect it to 'HeadController' 
                    else if (user.Type == "Head")
                    {
                        if (UserCheck.Accepted == true)
                        {
                            Session["HeadId"] = UserCheck.Id;
                            Session["HeadName"] = UserCheck.Name;
                            return RedirectToAction("Index","Head");
                        }
                        else
                        {
                            ViewBag.msgApproved = "Not Approved";
                        }
                    }
                    // if user type => head =>if  he accepted redirect it to 'EmployeeController'

                    else if (user.Type == "Employee")
                    {
                        if (UserCheck.Accepted == true)
                        {
                            Session["EmpId"] = UserCheck.Id;
                            Session["EmpName"] = UserCheck.Name;
                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            ViewBag.msg = "Not Approved";
                        }
                    }
                }
                else
                {
                    ViewBag.msg = "Email or Password is Incorrect";
                }
                return View("Login");
            }
            else
            {
                return View("Login");
            }
        }
        //  register page view
        [HttpGet]
        public ActionResult Register()
        {
            //  dropdownlist elments
            ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName","1");
            return View("Register");
        }
        // button for register
        [HttpPost]
        public ActionResult Register(UserRegister userInfo)
        {
            // check if Email is already in use or no
            if (db.UserInfoes.Any(e => e.Email == userInfo.Email))
            {
                // if Email is already in use => add model error
                ModelState.AddModelError("Email", "Email is already in use");
            }
            if (ModelState.IsValid)
            {
                UserInfo ui = new UserInfo();
                ui.Type = userInfo.Type;
                ui.Name = userInfo.Name;
                ui.Passward = CryptPassword.Hash(userInfo.Passward);
                ui.Email = userInfo.Email;
                ui.Gender = userInfo.Gender;
                ui.DepId = userInfo.DepId;
                ui.Active = false;
                ui.Accepted = false;
                ui.AbsenceHours = 0;
                db.UserInfoes.Add(ui);
                db.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
            return View(userInfo);
        }
        // logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");

        }
    }
}