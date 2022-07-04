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
    public class QuotaFee
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Year")]
        public string Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Quota")]
        public string Quota { get; set; }
        public IEnumerable<SelectListItem> Quotas { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Course")]
        public string Course { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select FeeType")]
        public string FeeType { get; set; }
        public IEnumerable<SelectListItem> FeeTypes { get; set; }
        public Nullable<System.Int32> Fee { get; set; }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value.Trim(); }
        }
        public string Status { get; set; }

        public string Sno { get; set; }
    }
}
