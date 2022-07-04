using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYFEELIB.Entities;
using MYFEELIB.Data;
using System.Data;
using System.Data.SqlClient;

namespace MYFEELIB.Domain
{
    public class AccountService
    {
        // Dim context As New UserContext()

        private List<Registration> lstUsers = new List<Registration>();
        private List<StudentAuth> lstStudent = new List<StudentAuth>();
        private List<Program> list = new List<Program>();
        private List<Course> listCourse = new List<Course>();
        private List<Modules> lstModules = new List<Modules>();
        private List<Quota> listQuota = new List<Quota>();
        private List<QuotaCategory> listQuotacat = new List<QuotaCategory>();
        private List<OtherFee> listOtherFee = new List<OtherFee>();
        List<ListItem> Item = new List<ListItem>();
        public User ValidateUser(User user)
        {
            try
            {
                GetRegistrationDetails();
                var data = lstUsers.Where(x => x.UserName == user.Username && x.UserPwd == user.Password).FirstOrDefault();

                if (data != null)
                {
                    user.isValid = true;
                    user.UserType = data.UserType;
                    user.Username = data.UserName;
                }
                else
                    user.Status = "Error";
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }

            return user;
        }

        public StudentAuth ValidateStudent(StudentAuth user)
        {
            try
            {
                GetStudentDetails(user.RollNo);
                var data = lstStudent.Where(x => x.RollNo == user.RollNo).FirstOrDefault();

                if (data != null)
                {
                    user.RollNo = data.RollNo;
                    user.Status = data.Status;
                }
                else
                    user.Status = "Error";
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }

            return user;
        }


        public List<StudentAuth> GetStudentDetails(string RNo)
        {
            try
            {
                UserContext context = new UserContext();
                lstStudent = context.GetStudent(RNo);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }

            return lstStudent;
        }


        public List<Registration> GetRegistrationDetails()
        {
            try
            {
                UserContext context = new UserContext();
                lstUsers = context.GetUserDetails();
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }

            return lstUsers;
        }
        //public List<Modules> GetModules(int RId)
        //{
        //    try
        //    {
        //        UserContext context = new UserContext();
        //        lstModules = context.GetModules(RId);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionLog.ErrorLog(ex);
        //    }

        //    return lstModules;
        //}

        public DataSet GetModules(int RId)
        {
            UserContext context = new UserContext();
            DataSet dataset = new DataSet();
            dataset = context.GetModules(RId);
            return dataset;
        }

        public List<StudentAuth> GetStudent(string RNo)
        {
            try
            {
                UserContext context = new UserContext();
                lstStudent = context.GetStudent(RNo);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }

            return lstStudent;
        }

        public List<ListItem> GetProgramDetails()
        {
            UserContext context = new UserContext();
            Item = context.GetPrograms();
            return Item;
        }

        public List<ListItem> GetRoles()
        {
            UserContext context = new UserContext();
            Item = context.GetRoles();
            return Item;
        }

        public List<ListItem> GetModules()
        {
            UserContext context = new UserContext();
            Item = context.GetModules_subModules();
            return Item;
        }

        public List<ListItem> GetTrans()
        {
            UserContext context = new UserContext();
            Item = context.GetJVTrans();
            return Item;
        }


        public List<ListItem> GetDetainDetails()
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetDetainTypes();
            return Item;
        }
        public List<ListItem> GetBatchDetails()
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetBatches();
            return Item;
        }

        public List<ListItem> GETYEARS(string Batch)
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetYear(Batch);
            return Item;
        }


        public List<ListItem> GetCategoryDetails()
        {
            UserContext context = new UserContext();
            Item = context.GetCategory();
            return Item;
        }


        public List<ListItem> GetStateDetails()
        {
            UserContext context = new UserContext();
            Item = context.GetState();
            return Item;
        }

        public List<ListItem> GetCourseDetails(string PId)
        {
            UserContext context = new UserContext();
            Item = context.GetCourse(PId);

            return Item;
        }
        public List<ListItem> GetBatch(string PId)
        {
            UserContext context = new UserContext();
            Item = context.GetBatchDetails(PId);

            return Item;
        }
        public List<ListItem> GetTransaction(DateTime Dt)
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetTransactions(Dt);

            return Item;
        }


        public List<ListItem> GetYearDetails(string Batch)
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetYears(Batch);

            return Item;
        }


        public List<ListItem> GetPayModeDetails()
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetPayModes();

            return Item;
        }


        public List<ListItem> GetProcessMonths()
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetProcessMonth();

            return Item;
        }

        public List<ListItem> GetWeeks()
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetWeeks();

            return Item;
        }


        public List<ListItem> GetClassTypes()
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetClassTypes();

            return Item;
        }



        public List<ListItem> GetDistrictDetails(string STId)
        {
            UserContext context = new UserContext();
            Item = context.GetDistrict(STId);

            return Item;
        }

        public List<ListItem> GetSectionDetails(string CId)
        {
            UserContext context = new UserContext();
            Item = context.GetSection(CId);
            return Item;
        }

        public List<ListItem> GetSemesterDetails(string YId)
        {
            UserContext context = new UserContext();
            Item = context.GetSemester(YId);
            return Item;
        }

        public List<ListItem> GetEmployees(string Dept)
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetEmployees(Dept);
            return Item;
        }

        public List<ListItem> Get_Attedance_subjects(string Sem, string Section, int WId)
        {
            AcademicContext context = new AcademicContext();
            Item = context.Get_Attend_Subject(Sem, Section, WId);
            return Item;
        }

        public List<ListItem> Get_Attedance_Peiod(int Sid, string Section, int WId, string course)
        {
            AcademicContext context = new AcademicContext();
            Item = context.Get_Attend_Period(Sid, Section, WId, course);
            return Item;
        }

        public List<ListItem> Get_Attedance_Employee(int Sid, string Section, int WId, string course,string Sem, int PId)
        {
            AcademicContext context = new AcademicContext();
            Item = context.Get_Attend_Employee(Sid, Section, WId, course,Sem ,PId);
            return Item;
        }

        public List<ListItem> Getsubjects(string Program, string Course, int Year, string Semester)
        {
            AcademicContext context = new AcademicContext();
            Item = context.GetSubject(Program, Course, Year, Semester);
            return Item;
        }

        public List<ListItem> GetQuotaDetails(string PId)
        {
            UserContext context = new UserContext();
            Item = context.GetQuota(PId);
            return Item;
        }

        public List<ListItem> GetQuotaCategoryDetails(string QId)
        {

            UserContext context = new UserContext();
            Item = context.GetQuotaCategory(QId);
            return Item;
        }

        public List<ListItem> GetQuotaCategoryTBR(string Program)
        {

            UserContext context = new UserContext();
            Item = context.GetQuotaCategoryTBR(Program);
            return Item;
        }

        public List<OtherFee> GetOtherFeeDetails(string PId)
        {

            UserContext context = new UserContext();
            listOtherFee = context.GetOtherFee(PId);
            return listOtherFee;
        }

        public DataSet GetProgramDropDowns()
        {
            DataSet dataset = new DataSet();
            UserContext context = new UserContext();
            dataset = context.GetProgramDropDowns();
            return dataset;
        }

        //private List<Challan> listChallanDetails = new List<Challan>();
        //public List<Challan> GetChallanDetails(string RollId, string Yearid)
        //{

        //    AcademicContext context = new AcademicContext();
        //    listChallanDetails = context.GetChallanDetails(RollId, Yearid);
        //    return listChallanDetails;
        //}

    }

}
