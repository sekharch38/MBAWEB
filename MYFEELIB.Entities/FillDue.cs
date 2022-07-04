using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
   public  class FillDue
    {

          public string StudentId { get; set; }
     
        public string RollNo { get; set; }

        public string DisplayName { get; set; }

        public string FatherName { get; set; }
        public decimal TuitionFee { get; set; }
        public decimal OUFee { get; set; }

        public string  ScholarshipId { get; set; }

        public int Year { get; set; }

        public string ChallanNo { get; set; }
        public Nullable<System.DateTime> ChallanDate { get; set; }
       // public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public Nullable<System.DateTime> EntryDate { get; set; }

        public string ReceivedFrom { get; set; }

        public string EnterBy { get; set; }


        public int PaymentId { get; set; }

        public string ProgramId { get; set; }



      
    }
}
