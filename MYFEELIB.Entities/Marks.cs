using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
    public class Marks
    {
        public string RollNo { get; set; }
        public string SubjectName { get; set; }

        public string ExamMaxMrks { get; set; }

        public string ExamMinMrks { get; set; }

        public string ExamMarksSecured { get; set; }
        public string SessMaxMrks { get; set; }
        public string SessMinMrks { get; set; }

        public string SessMarksSecured1 { get; set; }
        public string SessMarksSecured2 { get; set; }    public string SessMarksSecured3 { get; set; }    public string SessMarksSecured { get; set; }    public string ID { get; set; }    public string SId { get; set; }
        public int Year { get; set; }
        public string Semester { get; set; }
        public string Course { get; set; }
        public string EnterBy { get; set; }

        public string Result { get; set; }
    }
}
