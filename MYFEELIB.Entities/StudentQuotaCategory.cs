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
    public class StudentQuotaCategory
    {
        //StudentQuotaCategory

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Enter QuotaCategoryId")]
        public string QuotaCategoryId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Enter QuotaCategoryName")]
        public string QuotaCategoryName  { get; set; }
       
      
        public string Description  { get; set; }
      

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Quota")]
        public string Quota { get; set; }
        public IEnumerable<SelectListItem> Quotas { get; set; }
        public string IsReAdmit { get; set; }
        public string Status { get; set; }
    }
}
