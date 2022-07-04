using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MYFEELIB.Entities;

namespace MYFEEWEB.Models
{
    public class AcademicModel
    {
        [Required(ErrorMessage = "HallTicket is required.")]
        [Display(Name = "HallTicket")]
        public string HallTicket { get; set; }
        public string Department { get; set; }

        public Nullable<System.DateTime> PassedOut { get; set; }
        public int Max_Marks { get; set; }
        public int Secured_Marks { get; set; }

        public string Division { get; set; }
        public string Qualification { get; set; }

        public List<Academic> Academics{ get; set; }
    }
}