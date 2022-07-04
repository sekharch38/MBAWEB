using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
    public class StudentFeeMaster
    {
        public string StudentId { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public string FeeName { get; set; }

        public int Id { get; set; }
        public int SFMId { get; set; }
        public string FeeId { get; set; }
        public decimal Actual { get; set; }
        public decimal Received { get; set; }
        public decimal Due { get; set; }
        public int YearId { get; set; }
        public string Reason { get; set; }

        public string UserName { get; set; }
        public decimal Fee_Waiver { get; set; }
    }
}
