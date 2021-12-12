using ATMS_TestingSubject.Classes;
using ATMS_TestingSubject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATMS_TestingSubject.Controllers
{
    public class HeadController : Controller
    {
        // GET: Head
        // home dashboard
        private ATMS_Model db = new ATMS_Model();

        private ATMSEntities SP = new ATMSEntities();

        [OnlyHeadAccess]
        public ActionResult Index()
        {
            int id = int.Parse(Session["HeadId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            return View(myAcc);
        }
        // view for edit my account
        [HttpGet]
        [OnlyHeadAccess]
        public ActionResult EditMe()
        {

            int id = int.Parse(Session["HeadId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            myAcc.Passward = "";
            return View(myAcc);

        }
        // button for edit me
        [HttpPost]
        [OnlyHeadAccess]

        public ActionResult EditMe(UserInfo user)
        {
            if (ModelState.IsValid)
            {

                int id = int.Parse(Session["HeadId"].ToString());
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
                            Session["HeadName"] = myAcc.Name;
                            return RedirectToAction("Index", "Head");
                        }
                    }
                    else
                    {
                        myAcc.Name = user.Name;
                        myAcc.Gender = user.Gender;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Head");
                    }
                }
                else
                {
                    ViewBag.msg = "Password in Incorrect";
                }
            }
            return View();

        }
        //End of action

        [HttpGet]
        [OnlyHeadAccess]

        public ActionResult DetailsMe()
        {

            int id = int.Parse(Session["HeadId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            return View(myAcc);

        }
        //End of action

        [HttpGet]
        [OnlyHeadAccess]

        public ActionResult ChangePassword()
        {

            return View();

        }
        //End
        [HttpPost]
        [OnlyHeadAccess]

        public ActionResult ChangePassword(ChangePassword changepass)
        {

            if (ModelState.IsValid)
            {
                int id = int.Parse(Session["HeadId"].ToString());
                var myAcc = db.UserInfoes.Single(a => a.Id == id);
                if (myAcc.Passward == CryptPassword.Hash(changepass.OldPassword))
                {
                    myAcc.Passward = CryptPassword.Hash(changepass.NewPassword);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Head");
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
        //End
        [HttpGet]
        [OnlyHeadAccess]
        public ActionResult CurrentEmp()
        {
            int id = int.Parse(Session["HeadId"].ToString());
            var user = db.UserInfoes.Where(x => x.Id == id).FirstOrDefault();

            var CurrentEmployees = db.UserInfoes.Where(x => x.DepId == user.DepId);
            return View(CurrentEmployees);
        }
        [HttpGet]
        [OnlyHeadAccess]
        public ActionResult OnlineEmp()
        {
            int id = int.Parse(Session["HeadId"].ToString());
            var OnlineEmployees = db.UserInfoes.Where(x => x.DepId == id && x.Active == true);
            return View(OnlineEmployees);
        }
        /*[OnlyHeadAccess]
        [HttpGet]
        public ActionResult LeavingEmps()
        {
            int id = int.Parse(Session["HeadId"].ToString());
            var CurrentEmployees = db.UserInfoes.Where(x => x.Id == id).FirstOrDefault();

            /*var leavers = SP.Database.SqlQuery<Leaving>("leavingOfDep @id", new SqlParameter("id", CurrentEmployees.DepId));
            List<Leaving> leavers = new List<Leaving>();
            var ids = db.UserInfoes.Where(x => x.DepId == id).Select(x => x.Id);

            List<int> rolls = new List<int>();
                 foreach (var i in ids)
            {
                rolls.Add(db.Tickets.Single(x => x.Id == i).RollNo);
            }
            
            foreach (var r in rolls)
            {
                leavers.Add(db.Leavings.Single(x => x.RollNo == r));
            }
            ViewBag.leavers = leavers;

            return View() ;

        }
        */
    }
}