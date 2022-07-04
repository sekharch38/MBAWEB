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
   public class Certificate
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Batch")]
        public string Batch { get; set; }
        public IEnumerable<SelectListItem> Batchs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "RollNo")]
        public string RollNo { get; set; }

         public string Name { get; set; }
      
        public string FatherName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Certificate")]
        public string SelectCertificate { get; set; }
        public IEnumerable<SelectListItem> SelectCertificates { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Course")]
        public string Course { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date Of Leaving")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfLeaving { get; set; }

        public string Description { get; set; }
        public string Period { get; set; }

        public string DateOfJoining { get; set; }
        public string Dues { get; set; }
        public string Conduct { get; set; }

        [Display(Name = "Admission to the College / Class")]
        public string AdmissiontotheCollege { get; set; }

        [Display(Name = "Leaving the College / Class")]
        public string LeavingtheCollege { get; set; }

        [Display(Name = "Any Disciplinary Measures taken against him")]
        public string AnyDisciplinaryMeasurestakenagainsthim { get; set; }

        [Display(Name = "General Remarks")]
        public string GeneralRemarks { get; set; }
       
        public string SNo { get; set; }
        public string Bonafied { get; set; }
        public string Status { get; set; }

        public string StudentId { get; set; }

        public string YearId { get; set; }

        public string Actual { get; set; }

        public string Paid { get; set; }

        public string Due { get; set; }
        public string EntryId { get; set; }
    }
}
