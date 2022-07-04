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
    public class CertificateMaster
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Certificate Id")]
        public string CertificateId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Certificate Name")]
        public string CertificateName { get; set; }
        public string Status { get; set; }
    }
}
