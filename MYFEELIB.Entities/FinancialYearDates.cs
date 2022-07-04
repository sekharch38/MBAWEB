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
    public class FinancialYearDates
    {
        [Display(Name = "From Date")]
        public Nullable<System.DateTime> FromDate { get; set; }

        [Display(Name = "To Date")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Status { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }

          [Display(Name = "Challan / DD Number")]
        public string ChallanNo { get; set; }

         [Display(Name = "Refund Date")]
        public Nullable<System.DateTime> Refund_Date { get; set; }
    }
}
