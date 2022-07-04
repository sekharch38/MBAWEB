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
    public class University
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "University Id")]
        public string UniversityId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "University Name")]
        public string UniversityName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
