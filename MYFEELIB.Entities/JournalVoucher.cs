using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MYFEELIB.Entities
{
    public class JournalVoucher
    {
        [Display(Name = "Transaction Name")]
        public string TId { get; set; }
        public IEnumerable<SelectListItem> TNames { get; set; }
        public string TransactionType { get; set; }
        public Nullable<System.Int32> NOS { get; set; }
        public DateTime TransDate { get; set; }

        public Nullable<System.Decimal> Amount { get; set; }

        public decimal AmountDebit { get; set; }
        public decimal AmountCredit { get; set; }
        public decimal Balance { get; set; }
        public string Select_Date { get; set; }
        public string UserName { get; set; }
    }
}
