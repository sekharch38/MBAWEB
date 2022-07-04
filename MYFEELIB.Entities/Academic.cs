using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MYFEELIB.Entities
{
    public  class Academic
    {
        [Required(ErrorMessage = "HallTicket is required.")]
        [Display(Name = "HallTicket")]
        public string HallTicket { get; set; }
        public string Department { get; set; }

        public Nullable<System.DateTime> PassedOut { get; set; }
        public int Max_Marks { get; set; }
        public int Secured_Marks { get; set; }

        public string Division { get; set; }
        public string Qualification { get; set; }

        public string StudentId { get; set; }

        //public Academic(string HallTicket, string Department, DateTime PassedOut, int Max_Marks, int Secured_Marks, string Division)
        //{
        //    this.HallTicket = HallTicket;
        //    this.Department = Department;
        //    this.PassedOut = PassedOut;
        //    this.Max_Marks = Max_Marks;
        //    this.Secured_Marks = Secured_Marks;
        //    this.Division = Division;
        //}
        //public static List<Academic> GetAcademics()
        //{
        //    return new List<Academic>
        //               {
        //                   new Academic( HallTicket : "" , Department: "", PassedOut: DateTime.Now, Max_Marks: 0,Secured_Marks : 0,Division:"")
                          
        //               };
        //}

    }
}
