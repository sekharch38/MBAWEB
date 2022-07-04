using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace MYFEELIB.Entities
{
    public class Student_Detain_Discontinue
    {
        [Required(ErrorMessage = "RollNo is required.")]
        [Display(Name = "Roll Number")]
        public string RollNo { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Detain Type")]
        public string DetainId { get; set; }
        public IEnumerable<SelectListItem> DetainTypes { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Enter Year")]
        public Nullable<System.Int32> Year { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Detain / Discontinue Date")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DetainDate { get; set; }
        public string Remarks { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Status")]
        public string Status { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }

        public string EnterBy { get; set; }

        public string DDate { get; set; }
        public string DISDate { get; set; }

        public int ID { get; set; }

        public Nullable<System.DateTime> Discontinue_Date { get; set; }

    }
}
