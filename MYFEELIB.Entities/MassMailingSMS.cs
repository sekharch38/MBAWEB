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
    public class MassMailingSMS
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Course")]
        public string Course { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Batch")]
        public string Batch { get; set; }
        public IEnumerable<SelectListItem> Batchs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Year")]
        public string Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }

        public string QCId { get; set; }
        public string QCName { get; set; }
        public string Status { get; set; }
    }
}
