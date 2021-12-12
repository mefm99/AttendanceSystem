using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATMS_TestingSubject.Models;
using ATMS_TestingSubject.Classes;

namespace ATMS_TestingSubject.Controllers
{
    public class UserInfoController : Controller
    {
        //#region Database Connection
        //// you can use 'db' object 
        //private ATMS_Model db = new ATMS_Model();
        //#endregion
        //// View 
        //[HttpGet]
        //public ActionResult Login()
        //{  
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(UserLogin user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var UserCheck = db.UserInfoes.Where(x => x.Type == user.Type && x.Email == user.Email && x.Passward == user.Passward).FirstOrDefault();
        //        if (UserCheck != null)
        //        {
        //            if (user.Type == "Admin")
        //            {
        //                return RedirectToAction("AdminDashboard", "UserInfo");
        //            }
        //            else if (user.Type =="Head")
        //            {
        //                if (UserCheck.Accepted == true)
        //                {
        //                    return RedirectToAction("HeadDashboard", "UserInfo");
        //                }
        //                else
        //                {
        //                    ViewBag.msgApproved = "Not Approved";
        //                }
        //            }
        //            else if (user.Type == "Employee")
        //            {
        //                if (UserCheck.Accepted == true)
        //                {
        //                    return RedirectToAction("EmployeeDashboard", "UserInfo");
        //                }
        //                else
        //                {
        //                    ViewBag.msg = "Not Approved";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.msg = "Email or Password is Incorrect";
        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Register(UserRegister userInfo)
        //{
        //    if(db.UserInfoes.Any(e=>e.Email == userInfo.Email))
        //    {
        //        ModelState.AddModelError("Email", "Email is already in use");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        UserInfo ui = new UserInfo();
        //        ui.Type = userInfo.Type;
        //        ui.Name = userInfo.Name;
        //        ui.Passward = userInfo.Passward;
        //        ui.Email = userInfo.Email;
        //        ui.Gender = userInfo.Gender;
        //        ui.DepId = userInfo.DepId;
        //        ui.Active = false;
        //        ui.Accepted = false;
        //        ui.AbsenceHours = 0;
        //        db.UserInfoes.Add(ui);
        //        db.SaveChangesAsync();
        //        return RedirectToAction("Login");
        //    }
        //    ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName");
        //    return View(userInfo);
        //}
        //[NoDirectAccess]
        //public async Task<ActionResult> AdminDashboard()
        //{
        //    var userInfoes = db.UserInfoes.Include(u => u.Department);
        //    return View(await userInfoes.ToListAsync());
        //}

        //[NoDirectAccess]
        //public async Task<ActionResult> Details(int? id)
        //{
        //    TempData["CurrentUser"] = id;
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserInfo userInfo = await db.UserInfoes.FindAsync(id);
        //    if (userInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userInfo);
        //}
        //[NoDirectAccess]
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserInfo userInfo = await db.UserInfoes.FindAsync(id);
        //    if (userInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName", userInfo.DepId);
        //    return View(userInfo);
        //}

        //// POST: UserInfo/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Type,Name,Passward,Email,Gender,DepId,Active,Accepted,AbsenceHours")] UserInfo userInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(userInfo).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Details", new { id = userInfo.Id });
        //    }
        //    ViewBag.DepId = new SelectList(db.Departments, "DepId", "DepName", userInfo.DepId);
        //    return View(userInfo);
        //}

        //// GET: UserInfo/Delete/5
        //[NoDirectAccess]
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserInfo userInfo = await db.UserInfoes.FindAsync(id);
        //    if (userInfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userInfo);
        //}

        //// POST: UserInfo/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    UserInfo userInfo = await db.UserInfoes.FindAsync(id);
        //    Ticket ticket = db.Tickets.Single(x => x.Id == id);
        //    db.Tickets.Remove(ticket);
        //    db.UserInfoes.Remove(userInfo);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("AdminDashboard");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        public ViewResult Login(UserInfo user)
        {
            throw new NotImplementedException();
        }
    }
}
