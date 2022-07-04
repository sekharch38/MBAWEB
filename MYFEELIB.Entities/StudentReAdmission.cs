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
    public class StudentReAdmission
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Batch")]
        public string Batch { get; set; }
        public IEnumerable<SelectListItem> Batchs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Student")]
        public string StudentId { get; set; }
        public IEnumerable<SelectListItem> StudentIds { get; set; }
        public string Name { get; set; }

        public string YearOfDetain { get; set; }
        public string PurposeofDetain { get; set; }
        public Nullable<System.Int32> DetainTransactionId { get; set; }

        public string FeeName { get; set; }

        public Decimal Actual { get; set; }

        public Decimal Received { get; set; }
        public Decimal Due { get; set; }

        public int  YearId { get; set; }
       
       // public string TypeOfReAdmission { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select ReAdmission")]
        public string TypeOfReAdmission { get; set; }
        public IEnumerable<SelectListItem> TypeOfReAdmissions { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select ReAdmissionBatch")]
        public string ReAdmissionBatch { get; set; }
        public IEnumerable<SelectListItem> ReAdmissionBatchs { get; set; }

       // public string ReAdmissionBatch { get; set; }
       // public string ReAdmissionYear { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select ReAdmissionYear")]
        public string ReAdmissionYear { get; set; }
        public IEnumerable<SelectListItem> ReAdmissionYears { get; set; }

       // public string Reason { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Reason")]
        public string Reason { get; set; }
        public IEnumerable<SelectListItem> Reasons { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "ReAdmissionDate")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ReAdmissionDate { get; set; }


        public string DetainId { get; set; }
        public string DetainName { get; set; }
        public string  Description { get; set; }
        public int Id { get; set; }
       // public int YearId { get; set; }

      
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> FromDate { get; set; }


       
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ToDate { get; set; }

        public string Status { get; set; }

        public string FD { get; set; }
        public string TD { get; set; }


    }
}
