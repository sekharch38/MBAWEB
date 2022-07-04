using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYFEELIB.Entities
{
    public class StudentSearch
    {

        public string RollNo { get; set; }
        public string Batch { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string EamcetRank { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string S_MobileNo { get; set; }
        public string P_MobileNo { get; set; }
        public string EMail { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Languages { get; set; }
        public string PresentAddress { get; set; }
        public string PermenantAddress { get; set; }
        public int Year { get; set; }
        public string Entry { get; set; }

        public string Program { get; set; }
        public string Course { get; set; }
        public string Section { get; set; }
        public string Category { get; set; }
        public string Quota { get; set; }
        public string QuotaCategory { get; set; }
        public string DateOfBirth { get; set; }
        public string RegistrationDate { get; set; }
        public string DateOfJoining { get; set; }
        public string AdhaarCardNo { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string SpecialCategory { get; set; }
        public string IdentificationMark1 { get; set; }
        public string IdentificationMark2 { get; set; }
        public string ParentEMail { get; set; }
        public string UniversityOrderNo { get; set; }
        public string UniversityLastQualifiedTCNo { get; set; }
        public string TypeOfSecondaryEducation { get; set; }
        public Nullable<System.DateTime> UniversityOrderIssuedDate { get; set; }
        public Nullable<System.DateTime> UniversityLastQualifiedTCIssuedDate { get; set; }

        public string Content { get; set; }
        public byte[] studentPhoto { get; set; }
    }
}
