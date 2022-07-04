using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web;


namespace MYFEELIB.Entities
{


    public class Student
    {

        public string StudentId { get; set; }

        [Required(ErrorMessage = "RollNo is required.")]
        [Display(Name = "Roll Number")]
        public string RollNo { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Batch")]
        public string Batch { get; set; }

        //[Required(ErrorMessage = "Eamcet Rank is required.")]
        //[RegularExpression(@"[0-9]{1,}", ErrorMessage = "Enter Eamcet Rank !")]
        [Display(Name = "EAMCET / ECET Rank")]
        public Nullable<System.Int32> EamcetRank { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

      
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Student Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Student Phone No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string S_MobileNo { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Parent Phone No")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string P_MobileNo { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Student Email")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Nationality")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter Alphabets Only !")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Religion")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter Alphabets Only !")]
        public string Religion { get; set; }

        public IEnumerable<SelectListItem> Religions { get; set; }


        
        public string Languages { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Permanent Address")]
        public string PermenantAddress { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Program")]
        public string Program { get; set; }
        public IEnumerable<SelectListItem> Programs { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Course")]

        public string Course { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Section")]

        public string Section { get; set; }

        public IEnumerable<SelectListItem> Sections { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Caste")]

        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Quota")]


        public string Quota { get; set; }

        public IEnumerable<SelectListItem> Quotas { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Quota category")]
        public string QuotaCategory { get; set; }

        public IEnumerable<SelectListItem> QuotaCategories { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Entry")]
        public string Entry { get; set; }


        public IEnumerable<SelectListItem> Entries { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Year")]
        public int Year { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> RegistrationDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfJoining { get; set; }

        [Required(ErrorMessage = "Your must provide a Adhar Card No")]
        [Display(Name = "Aadhar Card No")]
        [StringLength(12, ErrorMessage = "Please Enter 12 digit Aadhar Card No !", MinimumLength = 12)]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Not a valid Aadhar Card No !")]
       // [RegularExpression(@"^\d{4}\s\d{4}\s\d{4}$", ErrorMessage = "Not a valid Aadhar Card No !")]
        public string AdhaarCardNo { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select State")]
        public string State { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select District")]
        public string District { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Select Special Category")]
        public string SpecialCategory { get; set; }
        public IEnumerable<SelectListItem> SpecialCategories { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Identification Mark-I")]
        public string IdentificationMark1 { get; set; }
        [Display(Name = "Identification Mark-II")]
        public string IdentificationMark2 { get; set; }


        [Display(Name = "Parent Email")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string ParentEMail { get; set; }


        [Display(Name = "University OrderNo")]
        public string UniversityOrderNo { get; set; }

        [Display(Name = "Last Qualified TC No")]
        public string UniversityLastQualifiedTCNo { get; set; }

        [Display(Name = "Type Of Secondary Education")]
        public string TypeOfSecondaryEducation { get; set; }


        [Display(Name = "University OrderIssued Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> UniversityOrderIssuedDate { get; set; }

        [Display(Name = "Last Qualified TC Issued Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> UniversityLastQualifiedTCIssuedDate { get; set; }
        public string AR { get; set; }

    }


    public class Quota
    {
        [MustBeSelected(ErrorMessage = "Please Select QuotaId")]
        public string QuotaId { get; set; }
        public string QuotaName { get; set; }
        public string ProgramId { get; set; }
    }

    public class QuotaCategory
    {
        [MustBeSelected(ErrorMessage = "Please Select Quota Category")]
        public string QuotaCategoryId { get; set; }
        public string QuotaCategoryName { get; set; }
        public string QuotaId { get; set; }
    }
    public class MustBeSelected : ValidationAttribute, IClientValidatable // IClientValidatable for client side Validation
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else
                return true;
        }
        // Implement IClientValidatable for client side Validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage } };
        }
    }



}
