using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
    public class StudentAttendancePercentage
    {
        public int TOTAL_DAYS { get; set; }
        public int WORKING_DAYS { get; set; }

        public int PRESENT_DAYS { get; set; }
        public int ABSENT_DAYS { get; set; }
        public string Percentage { get; set; }
   
    }
}
