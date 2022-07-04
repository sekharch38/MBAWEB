using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MYFEELIB.Entities
{
    public class SubModule
    {
        public int Id { get; set; }

        public IEnumerable<SelectListItem> Modules { get; set; }

        public Nullable<System.Int32> F_Id { get; set; }

        public string Name { get; set; }
        public string WebPage { get; set; }



        public string cont { get; set; }
        public string F_Class { get; set; }

        public int R_Id { get; set; }
        public string RoleName { get; set; }
    }
}
