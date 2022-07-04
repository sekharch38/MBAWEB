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
    public class RequireElements
    {
        public string RollNo { get; set; }
        public string Name { get; set; }

        public string CourseName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Batch")]
        public string Batch { get; set; }
        public IEnumerable<SelectListItem> Batches { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Year")]

        public int Year { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Course")]

        public string Course { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EntryDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PaymentDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Voucher Type")]
        public string PayMode { get; set; }

        public IEnumerable<SelectListItem> PayModes { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Due From")]
        public string DueFrom { get; set; }

        public IEnumerable<SelectListItem> DuesFrom { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select   Status")]
        public string Status { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }

        public string YearCode { get; set; }

        public IEnumerable<SelectListItem> YearCodes { get; set; }


        public string ReportType { get; set; }
        public IEnumerable<SelectListItem> ReportTypes { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Challan / DD /Fund Transfer No")]
        public string ChallanNo { get; set; }

        public string UserName { get; set; }

        public string BatchClose { get; set; }

        [Display(Name = "Select Section")]
        public string Section { get; set; }

        public IEnumerable<SelectListItem> Sections { get; set; }

        [Display(Name = "Select Semester")]
        public string Semester { get; set; }

        public IEnumerable<SelectListItem> Semesters { get; set; }

        [Display(Name = "Select Quota")]


        public string Quota { get; set; }

        public IEnumerable<SelectListItem> Quotas { get; set; }

        [Display(Name = "Select Approve Type")]
        public string ApproveType { get; set; }

        public IEnumerable<SelectListItem> ApproveTypes { get; set; }

        [Display(Name = "Select Process Month")]
        public string ProcessMonth { get; set; }

        public IEnumerable<SelectListItem> ProcessMonths { get; set; }

        [Display(Name = "Select Week")]
        public string week { get; set; }


        public IEnumerable<SelectListItem> weeks { get; set; }

        [Display(Name = "Select Employee")]
        public string Faculty1 { get; set; }

        public IEnumerable<SelectListItem> Faculties1 { get; set; }



        public string Faculty2 { get; set; }

        [Display(Name = "Select Employee 2")]
        public IEnumerable<SelectListItem> Faculties2 { get; set; }

        [Display(Name = "Select Subject")]
        public string subject { get; set; }

        public IEnumerable<SelectListItem> subjects { get; set; }

        [RegularExpression(@"[0-9]{1,}", ErrorMessage = "Enter Number Of Hours!")]
        public Nullable<System.Int32> NoofHours { get; set; }

        public string TimeId { get; set; }

        [Display(Name = "Select Period")]
        public string Period { get; set; }

        public IEnumerable<SelectListItem> Periods { get; set; }

        public string ClassType { get; set; }

        public IEnumerable<SelectListItem> ClassTypes { get; set; }

    }
}
