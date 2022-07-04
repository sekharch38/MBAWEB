using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
    public class ExtraPaid
    {

        public string RollNo { get; set; }

        public string Name { get; set; }
        public string ChallanNo { get; set; }

        public Nullable<System.DateTime> ChallanDate { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public string PayMode { get; set; }

        public string Status { get; set; }

        public string EnterBy { get; set; }

        public int Sno { get; set; }
        public int Id { get; set; }
        public string result { get; set; }

        public string StudentId { get; set; }

    }
}
