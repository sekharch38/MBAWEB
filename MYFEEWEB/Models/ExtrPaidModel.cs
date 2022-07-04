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
    public class ExtrPaidModel
    {
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string ChallanNo { get; set; }

        public Nullable<System.DateTime> ChallanDate { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public List<ExtraPaid > ExtraPaids { get; set; }

        public string PayMode { get; set; }
        public string EnterBy { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem> PayModes { get; set; }
    }
}