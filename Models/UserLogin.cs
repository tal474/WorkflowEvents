using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowEvents.Models
{
    public class UserLogin
    {
        public int UserLoginID { get; set; }

        [Display(Name = "User Name")]
        public string LoginUserName { get; set; }
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "User Role")]
        public int UserRole_ID_FK { get; set; }

        [Display(Name = "Role Name")]
        public string UserRole_Name { get; set; }



    }
}