using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkflowEvents.Models;

namespace WorkflowEvents.Controllers
{
    public class DashboardController : Controller
    {
        private Workflow_projectEntities db = new Workflow_projectEntities();
        // GET: Dashboard
        public ActionResult Home()
        {
            if (SessionHelper.CurrentUser.UserLoginID <= 0)
            {
                return RedirectToAction("Login", "User");
            }

            return View();

        }

        public ActionResult SignOut()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();


            #region Security for Browser Application
            HttpContext.Session.RemoveAll();
            HttpContext.Session.Abandon();

            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["__RequestVerificationToken"].Value = string.Empty;
            #endregion


            return RedirectToAction("Login", "User");
        }

        public new ActionResult Profile()
        {
            if (SessionHelper.CurrentUser.UserLoginID <= 0)
            {
                return RedirectToAction("Login", "User");
            }

            return View();

        }

        public ActionResult Task()
        {

            List<ClsTask> obj = new List<ClsTask>();
            //List<Task> objcollection = db.Tasks.ToList();

            var sam = (from r in db.Tasks
                       join ur in db.Status on r.StatusID_FK equals ur.StatusID
                       join udetails in db.tbl_Login on r.CreatedBY equals udetails.UserLoginID

                       select new { r.Task_ID_PK, r.TaskName, r.StatusID_FK, ur.StatusID_Name, r.CreatedDated,r.CreatedBY, udetails.FirstName, udetails.LastName }).ToList();


            if (SessionHelper.CurrentUser.RoleName != "Admin")
            {
                sam = sam.Where(x => x.CreatedBY == SessionHelper.CurrentUser.UserLoginID).ToList();
            }

            foreach (var item in sam)
            {
                obj.Add(new ClsTask()
                {
                    Task_ID_PK = item.Task_ID_PK,
                    TaskName = item.TaskName,
                    StatusID_FK = item.StatusID_FK,
                    StatusName = item.StatusID_Name,
                    CreatedDated = item.CreatedDated,
                    CreatedName = item.FirstName + " " + item.LastName
                });
            }

            return View(obj);


        }


        public ActionResult TaskCreate()
        {

            return View();


        }

        [HttpPost]
        public ActionResult TaskCreate([Bind(Include = "TaskName,TaskDesc")] ClsTask clsuser)
        {
            ViewBag.ErrorMsg = "Wrong Credentials";

            if (!string.IsNullOrWhiteSpace(clsuser.TaskName))
            {
                if (!string.IsNullOrWhiteSpace(clsuser.TaskDesc))
                {
                    int sid = (from r in db.Status
                               where (r.StatusID_Name == "Under Review")
                               select r.StatusID).FirstOrDefault();


                    if (ModelState.IsValid)
                    {
                        Task obj = new Task();
                        obj.TaskName = clsuser.TaskName;
                        obj.TaskDesc = clsuser.TaskDesc;
                        obj.CreatedBY = SessionHelper.CurrentUser.UserLoginID;
                        obj.CreatedDated = DateTime.Now;
                        obj.ModifedBy = SessionHelper.CurrentUser.UserLoginID;
                        obj.ModifedDate = DateTime.Now;
                        obj.StatusID_FK = sid;

                        db.Tasks.Add(obj);
                        db.SaveChangesAsync();
                        return RedirectToAction("Task");
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "task desc is empty";
                }

            }
            else
            {
                ViewBag.ErrorMsg = "task name is emty";
            }

            return View();



        }


    }
}