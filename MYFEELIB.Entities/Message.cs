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
    public class Message
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = " MessageId")]
        public string MessageId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Messages")]
        public string Messages { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select MessageType")]
        public string MessageType { get; set; }
        public IEnumerable<SelectListItem> MessageTypes { get; set; }
        public string IsAmount { get; set; }
        public string Status { get; set; }
    }
}
