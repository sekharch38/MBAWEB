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
    public class ChallanConfirmation
    {
         [Display(Name = "Transaction Id")]
        public int TransactionId { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<SelectListItem> Transactions { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public string FeeId { get; set; }

        public Decimal TotalAmount { get; set; }

        public Decimal Amount { get; set; }

        public string Status { get; set; }

         [Display(Name = "Payment Mode")]

        public string PayMode { get; set; }
        public string AprrovedBy { get; set; }

          [Display(Name = "Payment Date")]
        public Nullable<System.DateTime> PaymentDate { get; set; }


         [Display(Name = "Challan / DD Number")]

        public string ChallanNo { get; set; }

    }
}
