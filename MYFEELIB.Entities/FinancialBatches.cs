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
   public class FinancialBatches
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Year Code")]
        public string YearCode { get; set; }
        public IEnumerable<SelectListItem> YearCodes { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Batch")]
        public string Batch { get; set; }
        public IEnumerable<SelectListItem> Batchs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Year")]
        public string Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Academic")]
        public string Academic { get; set; }
        public IEnumerable<SelectListItem> Academics { get; set; }

        public string Status { get; set; }

        public int FBNo { get; set; }

        public string open { get; set; }
        public string MDate { get; set; }
        public string TDate { get; set; }

    }
}
