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
    public class Course
    {

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Course Id")]
        public string CourseId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
