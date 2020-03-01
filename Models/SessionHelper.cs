using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkflowEvents.Models
{
    public static class SessionHelper
    {
        public class CurrentUser
        {

            public static int UserLoginID
            {
                get
                {
                    return Convert.ToInt32(HttpContext.Current.Session["UserLoginID"]);
                }
                set
                {
                    HttpContext.Current.Session["UserLoginID"] = value;
                }
            }

            public static string UserName
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["UserName"]);
                }
                set
                {
                    HttpContext.Current.Session["UserName"] = value;
                }
            }

            public static string FullName
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["FullName"]);
                }
                set
                {
                    HttpContext.Current.Session["FullName"] = value;
                }
            }

            public static string FirstName
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["FirstName"]);
                }
                set
                {
                    HttpContext.Current.Session["FirstName"] = value;
                }
            }

            public static string LastName
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["LastName"]);
                }
                set
                {
                    HttpContext.Current.Session["LastName"] = value;
                }
            }

            public static string UserEmail
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["UserEmail"]);
                }
                set
                {
                    HttpContext.Current.Session["UserEmail"] = value;
                }
            }

            public static string RoleName
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["RoleName"]);
                }
                set
                {
                    HttpContext.Current.Session["RoleName"] = value;
                }
            }

            public static string ColorCode
            {
                get
                {
                    return Convert.ToString(HttpContext.Current.Session["ColorCode"]);
                }
                set
                {
                    var colcode = string.Empty;
                    if (string.IsNullOrWhiteSpace(RoleName))
                        HttpContext.Current.Session["ColorCode"] = "#222";
                    else
                        HttpContext.Current.Session["ColorCode"] = (RoleName == "Admin" ? "#0086dc" : "#5f5f5f");

                }
            }
        }
    }
}