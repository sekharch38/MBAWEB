using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web;

namespace MYFEELIB.Entities
{
    public class TobeReceived
    {
         [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

          [Display(Name = "Select Quota Category")]
        public string QuotaCategory { get; set; }

        public IEnumerable<SelectListItem> QuotaCategories { get; set; }

          [Display(Name = "Select Year")]

        public int Year { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }


        [Display(Name = "Convenor")]
        public Nullable<System.Decimal>  Convenor { get; set; }

        [Display(Name = "TSMFC")]
        public Nullable<System.Decimal> APSMFC { get; set; }

        [Display(Name = "Student")]
        public Nullable<System.Decimal> Student { get; set; }

        [Display(Name = "Is Percentage")]
        public string IsPercentage { get; set; }

    }
}
