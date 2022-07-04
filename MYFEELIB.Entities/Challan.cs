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
    public class Challan
    {
        [Required(ErrorMessage = "RollNo is required.")]
        [Display(Name = "Roll Number")]
        public string RollNo { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = " Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EntryDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PaymentDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "Year")]
        public Nullable<System.Int32 > Year { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = " Challan No")]
        public string ChallanNo { get; set; }


        public string Remarks { get; set; }

        public string StudentId { get; set; }

        public string DisplayName { get; set; }

        public string FeeId { get; set; }

        public Decimal  TotalAmout { get; set; }

        public Decimal Due { get; set; }

        public string Status { get; set; }

        public string PayMode { get; set; }
        public string EnterBy { get; set; }

        public IEnumerable<SelectListItem> PayModes { get; set; }
        public int Id { get; set; }
        public int TransactionId { get; set; }

        public string PD { get; set; }
    }
}
