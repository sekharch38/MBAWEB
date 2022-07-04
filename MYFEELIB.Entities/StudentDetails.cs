using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
    public class StudentDetails
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }

        public string CourseName { get; set; }
        public string CourseId { get; set; }
        public string ProgramId { get; set; }
    }
}
