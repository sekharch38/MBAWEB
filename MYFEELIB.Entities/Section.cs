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
   public class Section
    {

       [Required(ErrorMessage = "{0} is required")]
       [Display(Name = "Section Id")]
     
       public string SectionId { get; set; }
       
       [Required(ErrorMessage = "{0} is required")]
       [Display(Name = "Section Name")]
      
     
       public string SectionName { get; set; }
      

       [Required(ErrorMessage = "{0} is required")]
       [Display(Name = "Select Program")]
       public string Program { get; set; }
       public IEnumerable<SelectListItem> Programs { get; set; }
       [Required(ErrorMessage = "{0} is required")]
       [Display(Name = "Select Course")]
       public string Course { get; set; }
       public IEnumerable<SelectListItem> Courses { get; set; }

       public string NoOfStudents { get; set; }
      
       private string _Description;
       public string Description
       {
           get { return _Description; }
           set { _Description = value.Trim(); }
       }

          
       
       public string LateralEntry { get; set; }
       

       public string TransferEntry { get; set; }
     
       public string Status { get; set; }
    }
}
