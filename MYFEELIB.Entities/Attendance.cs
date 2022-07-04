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
    public class Attendance
    {
        public string TId { get; set; }
        public string SId { get; set; }

        public string ProgramId { get; set; }

        public string YearId { get; set; }

        public string SemesterName { get; set; }
        public string CourseId { get; set; }
        public string SectionId { get; set; }

        public string IsGroupBased { get; set; }
        public string GId { get; set; }    public string PName { get; set; }    public string SubjectCode { get; set; }    public string WeekName { get; set; }    public string NOH { get; set; }

        public string PId { get; set; }
        public string WId { get; set; }
        public string StudentId { get; set; }
        public string AR { get; set; }
        public DateTime todayDate { get; set; }
        public string EmployeeCode { get; set; }

        public string ClassType { get; set; }

       
    }
}
