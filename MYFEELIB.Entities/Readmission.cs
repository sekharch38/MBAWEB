using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
     public class Readmission
    {
         public string FromBatch { get; set; }
         public string ToBatch { get; set; }
         public string FromYear { get; set; }
         public string ToYear { get; set; }
         public string Remarks { get; set; }
         public string DetainId { get; set; }
    }
}
