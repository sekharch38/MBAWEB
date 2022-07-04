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
    public class Program
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Program Id")]
        public string ProgramId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Program Name")]
        public string ProgramName { get; set; }
        public string Description { get; set; }
     
        [Required(ErrorMessage = "Period is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter only Digits!")]
        public Nullable<System.Int32> Period { get; set; }
      
        public string IsDefault { get; set; }
     
        public string IsFeeRestrict { get; set; }
        public string Status { get; set; }
    }
}
