using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkflowEvents.Models;
using System.Threading.Tasks;


namespace WorkflowEvents.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        //[HttpPost]
        //public ActionResult LoginWithModel( UserLogin userLogin)            
        //{
        //    return View();
        //}
        Workflow_projectEntities dbobj = new Workflow_projectEntities();

        public ActionResult Registeration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registeration([Bind(Include = "LoginUserName,LoginPassword,UserEmail,FirstName,LastName")] UserLogin clsuser)
        {
            if (ModelState.IsValid)
            {
                tbl_Login obj = new tbl_Login();
                obj.UserName = clsuser.LoginUserName;
                obj.UserPassword = clsuser.LoginPassword;
                obj.UserEmail = clsuser.UserEmail;
                obj.FirstName = clsuser.FirstName;
                obj.LastName = clsuser.LastName;

                dbobj.tbl_Login.Add(obj);
                await dbobj.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {

            if (SessionHelper.CurrentUser.UserLoginID > 0)
            {
                return RedirectToAction("Home", "Dashboard", new { area = "" });
            }
            else
            {

                return View();
            }
        }

        //public ActionResult Login(string username = "", string password = "")
        //{
        //    UserLogin userLogin = new UserLogin();
        //    userLogin.LoginUserName = username;
        //    userLogin.LoginPassword = password;
        //    if (userLogin != null)
        //    {
        //        if (validatelogin(userLogin))
        //        {
        //            Session["username"] = userLogin.LoginUserName;
        //            return RedirectToAction("Home", "Dashboard", new { area = "" });
        //        }


        //        else
        //        {
        //            ViewBag.ErrorMsg = "Wrong Credentials";
        //            userLogin = null;
        //        }


        //    }
        //    return View(userLogin);


        //}

        [HttpPost]
        public ActionResult Login(UserLogin userLogin)
        {
            ViewBag.ErrorMsg = "Wrong Credentials";

            if (!string.IsNullOrWhiteSpace(userLogin.LoginUserName))
            {

                if (!string.IsNullOrWhiteSpace(userLogin.LoginPassword))
                {

                    #region Validatecredentials



                    if (validatelogin(userLogin))
                    {
                        return RedirectToAction("Home", "Dashboard", new { area = "" });
                    }
                    #endregion


                }
                else
                {
                    ViewBag.ErrorMsg = "not a valid password";
                }

            }
            else
            {
                ViewBag.ErrorMsg = "not a valid username";
            }
            return View();

        }

        public bool validatelogin(UserLogin userLogin)
        {
            //var sam = dbobj.tbl_Login.Select(x => (x.UserName == userLogin.LoginUserName || x.UserEmail == userLogin.LoginUserName) & x.UserPassword == userLogin.LoginPassword).FirstOrDefault();

            var sam = (from r in dbobj.tbl_Login
                      
                       where ((r.UserName == userLogin.LoginUserName || r.UserEmail == userLogin.LoginUserName) & r.UserPassword == userLogin.LoginPassword)
                       select new { r }).SingleOrDefault();
            if (string.IsNullOrWhiteSpace(Convert.ToString(sam.r.UserLoginID)))
                return false;
            else
            {
                var Rolename = (from r in dbobj.UserRoles
                           where (r.UserRole_ID_PK == sam.r.UserRole_ID_FK)
                           select r.UserRole_Name).FirstOrDefault();

                SessionHelper.CurrentUser.UserLoginID = sam.r.UserLoginID;
                SessionHelper.CurrentUser.UserName = sam.r.UserName;
                SessionHelper.CurrentUser.FullName = sam.r.FirstName + ' ' + sam.r.LastName;
                SessionHelper.CurrentUser.FirstName = sam.r.FirstName;

                SessionHelper.CurrentUser.RoleName = Rolename;

                SessionHelper.CurrentUser.ColorCode = (Rolename == "Admin" ? "#cdcdcd" : "#5f5f5f");

                return true;
            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbobj.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}