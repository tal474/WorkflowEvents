using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowEvents.Models
{
    public class ClsTask
    {
        public int Task_ID_PK { get; set; }
        [Display(Name ="Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Task Desc")]
        public string TaskDesc { get; set; }
        public int CreatedBY { get; set; }
        public System.DateTime CreatedDated { get; set; }
        public Nullable<int> ModifedBy { get; set; }
        public Nullable<System.DateTime> ModifedDate { get; set; }
        public int StatusID_FK { get; set; }

        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedName { get; set; }
    }
}