using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MYFEELIB.Entities
{
    public class Registration
    {
        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "User Name Required !")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name Required !")]
        [Display(Name = "Employee Name")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Designation Required !")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

       
        public string Email { get; set; }
        public int UserType { get; set; }
        public string Status { get; set; }
        public string UserPwd { get; set; }
        public string Action { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Role")]
        public string Role { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public int RegId { get; set; }
      

      
    }

}
