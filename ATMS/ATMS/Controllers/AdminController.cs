using ATMS_TestingSubject.Classes;
using ATMS_TestingSubject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ATMS_TestingSubject.Controllers
{
    public class AdminController : Controller
    {
        // you can use 'db' object to database connection
        private ATMS_Model db = new ATMS_Model();
        // home dashboard 
        [OnlyAdminAccess]
        public ActionResult Index()
        {
            return View("Index");

        }
        // Details Me 
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult DetailsMe()
        {

            int id = int.Parse(Session["AdminId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            return View("DetailsMe",myAcc);

        }
        // view for edit my account
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult EditMe()
        {

            int id = int.Parse(Session["AdminId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            myAcc.Passward = "";
            return View("EditMe",myAcc);

        }
        // button for edit me
        [HttpPost]
        [OnlyAdminAccess]
        public ActionResult EditMe(UserInfo user)
        {
            
            if (ModelState.IsValid)
            {

                int id = int.Parse(Session["AdminId"].ToString());
                var myAcc = db.UserInfoes.Single(a => a.Id == id);
                string passwordCome = CryptPassword.Hash(user.Passward);
                if (myAcc.Passward == passwordCome)
                {
                    if (myAcc.Email != user.Email)
                    {
                        if (db.UserInfoes.Any(e => e.Email == user.Email))
                        {
                            ViewBag.msg = "This Email is already in use";

                        }
                        else
                        {
                            myAcc.Name = user.Name;
                            myAcc.Gender = user.Gender;
                            myAcc.Email = user.Email;
                            db.SaveChanges();
                            Session["AdminName"] = myAcc.Name;
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        myAcc.Name = user.Name;
                        myAcc.Gender = user.Gender;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ViewBag.msg = "Password in Incorrect";
                }
            }
            return View("EditMe");

        }
        // view for edit my Password
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult ChangePassword()
        {
            return View();

        }
        // button for edit my password
        [HttpPost]
        [OnlyAdminAccess]
        public ActionResult ChangePassword(ChangePassword changepass)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(Session["AdminId"].ToString());
                var myAcc = db.UserInfoes.Single(a => a.Id == id);
                if (myAcc.Passward == CryptPassword.Hash(changepass.OldPassword))
                {
                    myAcc.Passward = CryptPassword.Hash(changepass.NewPassword);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.msg = "Old Passoed is InCorrect";
                }
            }
            else
            {
                return View();
            }
            return View();

        }
        // Depatments
        [OnlyAdminAccess]
        public ActionResult Departments()
        {
            return View(db.Departments.ToList());

        }
        // add department view
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult AddDepartment()
        {

            ViewBag.Heads = new SelectList(db.UserInfoes.Where(a => a.Type == "Head" && a.DepId == null), "Id", "Name");
            return View();

        }
        // button add department 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnlyAdminAccess]
        public ActionResult AddDepartment(Department dept)
        {

            if (ModelState.IsValid)
            {

                if (db.Departments.Any(a => a.DepName == dept.DepName))
                {
                    ViewBag.msg = "This Name is here, enter other name";
                }
                else
                {
                    Department d = new Department();
                    d.DepName = dept.DepName;
                    db.Departments.Add(d);
                    db.SaveChanges();
                    return RedirectToAction("Departments", "Admin");
                }
            }
            else
            {
                return View();
            }

            return View();
        }
        // detils for department 
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult DetailsDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);

        }
        // Edit for department  view
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);

        }
        // button edit department 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnlyAdminAccess]
        public ActionResult EditDepartment(Department dept)
        {

            if (ModelState.IsValid)
            {

                if (db.Departments.Any(a => a.DepName == dept.DepName))
                {
                    ViewBag.msg = "This Name is here, enter other name";
                }
                else
                {
                    db.Entry(dept).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Departments", "Admin");
                }
            }
            else
            {
                return View();
            }

            return View();
        }
        // detils for detele department 
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult DeleteDepartment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);

        }
        // button delete department 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnlyAdminAccess]
        public ActionResult DeleteDepartment(Department dept)
        {

            db.Entry(dept).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Departments", "Admin");

        }
        // requests for emp
        [OnlyAdminAccess]
        public ActionResult Requests()
        {

            return View(db.UserInfoes.Where(a =>  a.Accepted == false));

        }
        // accepted for emp
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult Approve(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo user = db.UserInfoes.Where(a => a.Accepted == false && a.Id == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        // button approve emp  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnlyAdminAccess]
        public ActionResult Approve(UserInfo user)
        {
            var us = db.UserInfoes.Single(a => a.Id == user.Id);
            us.Accepted = true;
            db.SaveChanges();
            return RedirectToAction("Requests", "Admin");

        }
        // reject for emp
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult Reject(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo user = db.UserInfoes.Where(a => a.Type == "Employee" && a.Accepted == false && a.Id == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnlyAdminAccess]
        public ActionResult Reject(UserInfo user)
        {

            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Requests", "Admin");

        }

        // Employees
        [OnlyAdminAccess]
        public ActionResult Users(string Type)
        {

            int id = int.Parse(Session["AdminId"].ToString());
            if (Type == "Head")
            {
                return View(db.UserInfoes.Where(a => a.Type.Equals("Head") && a.Accepted == null).ToList());
            }
            else if (Type == "Employee")
            {
                return View(db.UserInfoes.Where(a => a.Type.Equals("Employee") && a.Accepted == true).ToList());
            }
            else if (Type == "Admin")
            {
                return View(db.UserInfoes.Where(a => a.Type.Equals("Admin") && a.Accepted == null && a.Id != id).ToList());
            }
            return View(db.UserInfoes.Where(a => a.Accepted == true && a.Accepted == null).ToList());


        }
        // view for add emp
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult AddEmp()
        {
            ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
            return View();

        }
        // button for add emp
        [HttpPost]
        [OnlyAdminAccess]
        public ActionResult AddEmp(UserRegister userInfo)
        {

            if (db.UserInfoes.Any(e => e.Email == userInfo.Email))
            {
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
                ui.Accepted = true;
                ui.AbsenceHours = 0;
                db.UserInfoes.Add(ui);
                db.SaveChangesAsync();
                return RedirectToAction("Users", "Admin");
            }
            ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
            return View(userInfo);

        }

        // view for add head
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult AddHead()
        {

            ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
            return View();

        }
        // button for add head
        [HttpPost]
        [OnlyAdminAccess]
        public ActionResult AddHead(UserRegister userInfo)
        {

            if (db.UserInfoes.Any(e => e.Email == userInfo.Email))
            {
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
                ui.Accepted = true;
                ui.AbsenceHours = 0;
                db.UserInfoes.Add(ui);
                db.SaveChangesAsync();
                return RedirectToAction("Users", "Admin");
            }
            ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
            return View(userInfo);

        }
        // view for add admin
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult AddAdmin()
        {

            return View();

        }
        // button for add Admin
        [HttpPost]
        [OnlyAdminAccess]
        public ActionResult AddAdmin(UserRegister userInfo)
        {

            if (db.UserInfoes.Any(e => e.Email == userInfo.Email))
            {
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
                ui.DepId = null;
                ui.Active = null;
                ui.Accepted = null;
                ui.AbsenceHours = null;
                db.UserInfoes.Add(ui);
                db.SaveChangesAsync();
                return RedirectToAction("Users", "Admin");
            }
            return View(userInfo);

        }
        // details for head & admin & emp
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult DetailsUser(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo usre = db.UserInfoes.Find(id);
            if (usre == null)
            {
                return HttpNotFound();
            }
            return View(usre);

        }
        // detils for detele head & admin & emp 
        [HttpGet]
        [OnlyAdminAccess]
        public ActionResult DeleteUser(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo user = db.UserInfoes.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        // button delete users 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OnlyAdminAccess]
        public ActionResult DeleteUser(UserInfo user)
        {

            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Users", "Admin");

        }
        // view for online emp
        [OnlyAdminAccess]
        public ActionResult OnlineUsers()
        {
            return View(db.UserInfoes.Where(a => a.Type == "Employee" && a.Accepted == true && a.Active == true));

        }
        // view for online emp
        [OnlyAdminAccess]
        public ActionResult LeavingRequest()
        {

            return View(db.Leavings.Where(x=> x.LeaveState==States.Pending.ToString()));

        }
        [OnlyAdminAccess]

        public ActionResult Accept(int id)
        {
            db.Leavings.Single(x => x.LeaveId == id).LeaveState = States.Aprroved.ToString();
            db.SaveChanges();
            return RedirectToAction("LeavingRequest");
        }
        [OnlyAdminAccess]

        public ActionResult Refuse(int id)
        {
            db.Leavings.Single(x => x.LeaveId == id).LeaveState = States.NotApproved.ToString();
            db.SaveChanges();
            return RedirectToAction("LeavingRequest");
        }

    }
}