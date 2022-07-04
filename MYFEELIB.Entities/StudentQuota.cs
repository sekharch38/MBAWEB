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
    public class StudentQuota
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Enter QuotaId")]
        public string QuotaId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Enter QuotaName")]
        public string QuotaName { get; set; }

       
        public string Description { get; set; }
       
        public string ScholarshipApplicable { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        public string IsOtherFeeRestrict { get; set; }

        public string Status { get; set; }
    }
}
