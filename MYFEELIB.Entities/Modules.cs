using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MYFEELIB.Entities
{
    public class Modules
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Module Name Required !")]
        [Display(Name = "Module Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Web Page Required !")]
        [Display(Name = "Web Page")]
        public string WebPage { get; set; }


        public int RId { get; set; }


        [Required(ErrorMessage = "Css Class Required !")]
        [Display(Name = "Css Class")]
        public string F_Class { get; set; }

        public string Status { get; set; }
        public IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
                selectList.Add(new SelectListItem()
                {
                    Value = element,
                    Text = element
                });

            return selectList;
        }
    }

}
