using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
   public class FeeWaiver
    {
       public string RollNo { get; set; }

       public string Name { get; set; }

       public decimal Fee_Waiver { get; set; }
       public decimal Current_Due { get; set; }
     
       public decimal Future_Amount { get; set; }

       public decimal Final_Collected { get; set; }


       public string UserName { get; set; }
       public string Remarks { get; set; }


    }
}
