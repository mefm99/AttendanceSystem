using ATMS_TestingSubject.Classes;
using ATMS_TestingSubject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;

namespace ATMS_TestingSubject.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        // home dashboed
        private static int TicketNum;
        private ATMS_Model db = new ATMS_Model();

        [HttpGet]
        [ChildActionOnly]
        [ActionName("RandomCheck")]
        public PartialViewResult RandomCheck_get()
        {
            int timeInSec = 10;
            int id = int.Parse(Session["EmpId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            if (ScheduleJob.n >= timeInSec)
            {
                myAcc.Active = false;
                myAcc.AbsenceHours ++;
                db.SaveChanges();
                return PartialView("_Question");

            }
            return PartialView(myAcc);

        }
        [HttpPost]
        [ChildActionOnly]
        [ActionName("RandomCheck")]

        public ActionResult RandomCheck_post()
        {
            int id = int.Parse(Session["EmpId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            myAcc.Active = true;
            db.SaveChanges();
            ScheduleJob.n = 0;
            return PartialView(myAcc);

        }


       
        [OnlyEmployeeAccess]
        public ActionResult Index()
        {

            int id = int.Parse(Session["EmpId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            return View(myAcc);



        }
        // view for edit my account
        [HttpGet]
        [OnlyEmployeeAccess]
        public ActionResult EditMe()
        {

            int id = int.Parse(Session["EmpId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            myAcc.Passward = "";
            return View(myAcc);

        }
        // button for edit me
        [HttpPost]
        [OnlyEmployeeAccess]

        public ActionResult EditMe(UserInfo user)
        {
            if (ModelState.IsValid)
            {

                int id = int.Parse(Session["EmpId"].ToString());
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
                            Session["EmpName"] = myAcc.Name;
                            return RedirectToAction("Index", "Employee");
                        }
                    }
                    else
                    {
                        myAcc.Name = user.Name;
                        myAcc.Gender = user.Gender;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Employee");
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
        [OnlyEmployeeAccess]

        public ActionResult DetailsMe()
        {

            int id = int.Parse(Session["EmpId"].ToString());
            var myAcc = db.UserInfoes.Single(a => a.Id == id);
            return View(myAcc);

        }
        //End of action

        [HttpGet]
        [OnlyEmployeeAccess]

        public ActionResult ChangePassword()
        {

            return View();

        }
        //End
        [HttpPost]
        [OnlyEmployeeAccess]

        public ActionResult ChangePassword(ChangePassword changepass)
        {

            if (ModelState.IsValid)
            {
                int id = int.Parse(Session["EmpId"].ToString());
                var myAcc = db.UserInfoes.Single(a => a.Id == id);
                if (myAcc.Passward == CryptPassword.Hash(changepass.OldPassword))
                {
                    myAcc.Passward = CryptPassword.Hash(changepass.NewPassword);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Employee");
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

        //End of action
        // GET: Attendances/Create
        [HttpGet]
        [OnlyEmployeeAccess]
        public ActionResult TakeAttendance()
        {
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [OnlyEmployeeAccess]

        public ActionResult TakeAttendance([Bind(Include = "RollNo")] Attendance attendance)
        {

            int id = int.Parse(Session["EmpId"].ToString());

            var LastTickets = db.Tickets.Where(x => x.Id == id).ToArray();
            if (LastTickets.Length > 0)
            {
                Ticket LastTicket = db.Tickets.Where(x => x.Id == id).ToArray().Last();
                if (LastTicket.RollNo == attendance.RollNo)
                {

                    attendance.State = true;
                    attendance.Date = DateTime.Now.Date;
                    attendance.Time = DateTime.Now.TimeOfDay;
                    db.Attendances.Add(attendance);
                    db.UserInfoes.Single(x => x.Id == id).Active = true;
                    db.SaveChangesAsync();
                    return RedirectToAction("Index", "Employee", null);

                }
                else
                {
                    Response.Write("wrong barcode");
                    return View();
                }

            }
            Response.Write("No barcode found! 404");
            return View();
        }
        //End
        [OnlyEmployeeAccess]
        public ActionResult GetTicket()
        {
            int Id = int.Parse(Session["EmpId"].ToString());

            var alltikects = db.Tickets.ToArray();
            TimeSpan TicketAvailable;
            TimeSpan? TimePassed;
            TimeSpan.TryParse("24:00:00", out TicketAvailable); //time needed to get another code
            Ticket Newticket = new Ticket();
            if (alltikects.Length > 0)
            {
                TicketNum = alltikects.Last().RollNo;
            }
            var OldTickets = alltikects.Where(x => x.Id == Id).ToArray();
            if (OldTickets.Length > 0)
            {
                TimePassed = DateTime.Now.TimeOfDay - OldTickets.Last().time;

                if (TimePassed < TicketAvailable)
                {
                    Response.Write("wait " + (TicketAvailable - TimePassed) + " to get a second barcode");
                    Response.Write("your last barcode is");

                    return View(OldTickets.Last());
                }

            }
            Newticket.RollNo = ++TicketNum;
            Newticket.time = DateTime.Now.TimeOfDay;
            Newticket.date = DateTime.Now.ToLocalTime();
            Newticket.Id = Id;
            db.Tickets.Add(Newticket);
            db.SaveChanges();
            Response.Write("your ticket number is " + TicketNum);
            return View(Newticket);
        }
        //End
        [HttpGet]
        [OnlyEmployeeAccess]
        public ActionResult LeavingRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LeavingRequest(Leaving leave)
        {
            Leaving leaving = new Leaving();
            leaving.LeaveRequest = leave.LeaveRequest;
            leaving.RollNo = leave.RollNo;
            leaving.Time = DateTime.Now.TimeOfDay;
            leaving.Date = DateTime.Now.ToLocalTime();
            leaving.LeaveState = Enum.GetName(typeof(States), 0);
            db.Leavings.Add(leaving);
            db.SaveChanges();
            return RedirectToAction("Index", "Employee", null);
        }
    }
}