using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MYFEELIB.Entities;
using System.Runtime.CompilerServices;

namespace MYFEELIB.Data
{
    public class RegistrationContext
    {
        static string connection = ConfigurationManager.ConnectionStrings["MyFeeConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connection);
        bool isValid = Common.ValidateConnection(connection);

        List<Registration> lstUsers = new List<Registration>();

        public Student StudentRegister(Student data)
        {
            try
            {

                string studentID, Batch, Gende = "";
                studentID = Convert.ToString(data.Batch).Substring(2) + "" + Convert.ToString(data.Program).Substring(0, 2).ToUpper() + "" + Convert.ToString(data.Course).Substring(0, 2).ToUpper();
                Batch = data.Batch + "-" + (Convert.ToInt32(data.Batch) + 1).ToString();
                SqlCommand cmdcount = new SqlCommand("[MF_Registration_StudentCount]", con);
                cmdcount.CommandType = CommandType.StoredProcedure;
                cmdcount.Parameters.AddWithValue("@StudentId", studentID);
                con.Open();
                object j = (object)cmdcount.ExecuteScalar();
                con.Close();

                SqlCommand cmd = new SqlCommand("[MF_RegistrationStudent_SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;


                if (j != null)
                {
                    int k;
                    k = Convert.ToInt32(j) + 1;
                    studentID = studentID + k;
                }
                else
                {

                    studentID = studentID + "1";
                }

                data.StudentId = studentID;
                cmd.Parameters.AddWithValue("@StudentId", studentID);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName.ToUpper());

                if (data.MiddleName != null)
                {
                    cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName.ToUpper());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName);
                }
                cmd.Parameters.AddWithValue("@LastName", data.LastName.ToUpper());
                cmd.Parameters.AddWithValue("@DisplayName", data.FullName.ToUpper());
                if (data.Gender == "True")
                {
                    Gende = "M";
                }
                else
                {
                    Gende = "F";
                }
                cmd.Parameters.AddWithValue("@Gender", Gende);
                cmd.Parameters.AddWithValue("@Enterby", data.FullName.ToUpper());

                cmd.Parameters.AddWithValue("@RollNo", data.RollNo);
                cmd.Parameters.AddWithValue("@ProgramId", data.Program);
                cmd.Parameters.AddWithValue("@CourseId", data.Course);
                cmd.Parameters.AddWithValue("@SectionId", data.Section);
                cmd.Parameters.AddWithValue("@QuotaId", data.Quota);
                cmd.Parameters.AddWithValue("@CategeoryId", data.Category);
                cmd.Parameters.AddWithValue("@RegistrationDate", data.RegistrationDate);
                cmd.Parameters.AddWithValue("@DateOfJoining", data.DateOfJoining);
                cmd.Parameters.AddWithValue("@EnterId", data.Entry);
                cmd.Parameters.AddWithValue("@YearId", data.Year);
                cmd.Parameters.AddWithValue("@Rank", data.EamcetRank);
                cmd.Parameters.AddWithValue("@BatchId", Batch);
                cmd.Parameters.AddWithValue("@QCId", data.QuotaCategory);
                cmd.Parameters.AddWithValue("@PhoneNo", data.S_MobileNo);
                cmd.Parameters.AddWithValue("@Mobile", data.P_MobileNo);

                cmd.Parameters.AddWithValue("@DOB", data.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", data.EMail);

                cmd.Parameters.AddWithValue("@Nationality", data.Nationality.ToUpper());
                cmd.Parameters.AddWithValue("@Religion", data.Religion);
                cmd.Parameters.AddWithValue("@FatherName", data.FatherName.ToUpper());
                cmd.Parameters.AddWithValue("@PresentAddress", data.PresentAddress.ToUpper());
                cmd.Parameters.AddWithValue("@PermanentAddress", data.PermenantAddress.ToUpper());
                cmd.Parameters.AddWithValue("@MotherName", data.MotherName.ToUpper());
                cmd.Parameters.AddWithValue("@AdhaarCardNo", data.AdhaarCardNo.ToUpper());

                cmd.Parameters.AddWithValue("@StateCode", data.State);
                cmd.Parameters.AddWithValue("@DistrictCode", data.District);
                cmd.Parameters.AddWithValue("@F_SpecialCategory", data.SpecialCategory);
                cmd.Parameters.AddWithValue("@ParentEMail", data.ParentEMail);
                cmd.Parameters.AddWithValue("@IdentificationMark1", data.IdentificationMark1.ToUpper());
                if (data.IdentificationMark2 != null)
                {
                    cmd.Parameters.AddWithValue("@IdentificationMark2", data.IdentificationMark2.ToUpper());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdentificationMark2", data.IdentificationMark2);
                }
                cmd.Parameters.AddWithValue("@UniversityOrderNo", data.UniversityOrderNo);
                cmd.Parameters.AddWithValue("@UniversityOrderIssuedDate", data.UniversityOrderIssuedDate);
                cmd.Parameters.AddWithValue("@UniversityLastQualifiedTCNo", data.UniversityLastQualifiedTCNo);
                cmd.Parameters.AddWithValue("@UniversityLastQualifiedTCIssuedDate", data.UniversityLastQualifiedTCIssuedDate);
                if (data.TypeOfSecondaryEducation != null)
                {
                    cmd.Parameters.AddWithValue("@TypeOfSecondaryEducation", data.TypeOfSecondaryEducation.ToUpper());

                }

                else
                {
                    cmd.Parameters.AddWithValue("@TypeOfSecondaryEducation", data.TypeOfSecondaryEducation);
                }
                // cmd.Parameters.AddWithValue("@StudentPhoto", bytes);





                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    data.StudentId = studentID;
                    data.Status = "Success";
                }
                else
                {

                    data.Status = "Error";
                }


            }
            catch (Exception ex)
            {
                con.Close();
                data.Status = "Error";
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return data;

        }


        public int StudentImport(List<Student> Students)
        {
            int rescount = 0;
            foreach (var data in Students)
            {
                try
                {

                    string studentID, Batch, Gende = "";
                    studentID = Convert.ToString(data.Batch).Substring(2) + "" + Convert.ToString(data.Program).Substring(0, 2).ToUpper() + "" + Convert.ToString(data.Course).Substring(0, 2).ToUpper();
                    Batch = data.Batch + "-" + (Convert.ToInt32(data.Batch) + 1).ToString();
                    SqlCommand cmdcount = new SqlCommand("[MF_Registration_StudentCount]", con);
                    cmdcount.CommandType = CommandType.StoredProcedure;
                    cmdcount.Parameters.AddWithValue("@StudentId", studentID);
                    con.Open();
                    object j = (object)cmdcount.ExecuteScalar();
                    con.Close();

                    SqlCommand cmd = new SqlCommand("[MF_RegistrationStudent_SAVE]", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    if (j != null)
                    {
                        int k;
                        k = Convert.ToInt32(j) + 1;
                        studentID = studentID + k;
                    }
                    else
                    {

                        studentID = studentID + "1";
                    }

                    data.StudentId = studentID;
                    cmd.Parameters.AddWithValue("@StudentId", studentID);
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName.ToUpper());

                    if (data.MiddleName != null)
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName.ToUpper());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName);
                    }
                    cmd.Parameters.AddWithValue("@LastName", data.LastName.ToUpper());
                    cmd.Parameters.AddWithValue("@DisplayName", data.FullName.ToUpper());
                    if (data.Gender == "MALE")
                    {
                        Gende = "M";
                    }
                    else
                    {
                        Gende = "F";
                    }
                    cmd.Parameters.AddWithValue("@Gender", Gende);
                    cmd.Parameters.AddWithValue("@Enterby", data.FullName.ToUpper());

                    cmd.Parameters.AddWithValue("@RollNo", data.RollNo);
                    cmd.Parameters.AddWithValue("@ProgramId", data.Program);
                    cmd.Parameters.AddWithValue("@CourseId", data.Course);
                    cmd.Parameters.AddWithValue("@SectionId", data.Section);
                    cmd.Parameters.AddWithValue("@QuotaId", data.Quota);
                    cmd.Parameters.AddWithValue("@CategeoryId", data.Category);
                    cmd.Parameters.AddWithValue("@RegistrationDate", data.RegistrationDate);
                    cmd.Parameters.AddWithValue("@DateOfJoining", data.DateOfJoining);
                    cmd.Parameters.AddWithValue("@EnterId", data.Entry);
                    cmd.Parameters.AddWithValue("@YearId", data.Year);
                    cmd.Parameters.AddWithValue("@Rank", data.EamcetRank);
                    cmd.Parameters.AddWithValue("@BatchId", Batch);
                    cmd.Parameters.AddWithValue("@QCId", data.QuotaCategory);
                    cmd.Parameters.AddWithValue("@PhoneNo", data.S_MobileNo);
                    cmd.Parameters.AddWithValue("@Mobile", data.P_MobileNo);

                    cmd.Parameters.AddWithValue("@DOB", data.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", data.EMail);

                    cmd.Parameters.AddWithValue("@Nationality", data.Nationality.ToUpper());
                    cmd.Parameters.AddWithValue("@Religion", data.Religion);
                    cmd.Parameters.AddWithValue("@FatherName", data.FatherName.ToUpper());
                    cmd.Parameters.AddWithValue("@PresentAddress", data.PresentAddress.ToUpper());
                    cmd.Parameters.AddWithValue("@PermanentAddress", data.PermenantAddress.ToUpper());
                    cmd.Parameters.AddWithValue("@MotherName", data.MotherName.ToUpper());
                    cmd.Parameters.AddWithValue("@AdhaarCardNo", data.AdhaarCardNo.ToUpper());

                    cmd.Parameters.AddWithValue("@StateCode", data.State);
                    cmd.Parameters.AddWithValue("@DistrictCode", data.District);
                    cmd.Parameters.AddWithValue("@F_SpecialCategory", data.SpecialCategory);
                    cmd.Parameters.AddWithValue("@ParentEMail", data.ParentEMail);
                    cmd.Parameters.AddWithValue("@IdentificationMark1", data.IdentificationMark1.ToUpper());
                    if (data.IdentificationMark2 != null)
                    {
                        cmd.Parameters.AddWithValue("@IdentificationMark2", data.IdentificationMark2.ToUpper());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IdentificationMark2", data.IdentificationMark2);
                    }
                    cmd.Parameters.AddWithValue("@UniversityOrderNo", data.UniversityOrderNo);
                    cmd.Parameters.AddWithValue("@UniversityOrderIssuedDate", data.UniversityOrderIssuedDate);
                    cmd.Parameters.AddWithValue("@UniversityLastQualifiedTCNo", data.UniversityLastQualifiedTCNo);
                    cmd.Parameters.AddWithValue("@UniversityLastQualifiedTCIssuedDate", data.UniversityLastQualifiedTCIssuedDate);
                    if (data.TypeOfSecondaryEducation != null)
                    {
                        cmd.Parameters.AddWithValue("@TypeOfSecondaryEducation", data.TypeOfSecondaryEducation.ToUpper());

                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@TypeOfSecondaryEducation", data.TypeOfSecondaryEducation);
                    }
                 
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        rescount = rescount + 1;
                        con.Close();
                    }
                    else
                    {
                        rescount = rescount + 0;
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    ExceptionLog.ErrorLog(ex);
                }
                finally
                {
                    con.Close();
                }
            }
            return rescount;
        }

        public Student UpdateStudentRegister(Student data)
        {
            try
            {
                string Gender = "";

                SqlCommand cmd = new SqlCommand("[MF_RegistrationStudent_Update]", con);
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.AddWithValue("@StudentId", data.StudentId);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName.ToUpper());
                if (data.MiddleName != null)
                {
                    cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName.ToUpper());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName);
                }
                cmd.Parameters.AddWithValue("@LastName", data.LastName.ToUpper());
                cmd.Parameters.AddWithValue("@DisplayName", data.FullName.ToUpper());
                if (data.Gender == "Male")
                {
                    Gender = "M";
                }
                else
                {
                    Gender = "F";
                }
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Enterby", data.FullName.ToUpper());
                cmd.Parameters.AddWithValue("@RollNo", data.RollNo);
                cmd.Parameters.AddWithValue("@ProgramId", data.Program);
                cmd.Parameters.AddWithValue("@CourseId", data.Course);
                cmd.Parameters.AddWithValue("@SectionId", data.Section);
                cmd.Parameters.AddWithValue("@QuotaId", data.Quota);
                cmd.Parameters.AddWithValue("@CategeoryId", data.Category);
                cmd.Parameters.AddWithValue("@RegistrationDate", data.RegistrationDate);
                cmd.Parameters.AddWithValue("@DateOfJoining", data.DateOfJoining);
                cmd.Parameters.AddWithValue("@EnterId", data.Entry);
                cmd.Parameters.AddWithValue("@YearId", data.Year);
                cmd.Parameters.AddWithValue("@Rank", data.EamcetRank);
                cmd.Parameters.AddWithValue("@BatchId", data.Batch);
                cmd.Parameters.AddWithValue("@QCId", data.QuotaCategory);
                cmd.Parameters.AddWithValue("@PhoneNo", data.S_MobileNo);
                cmd.Parameters.AddWithValue("@Mobile", data.P_MobileNo);

                cmd.Parameters.AddWithValue("@DOB", data.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", data.EMail);
                cmd.Parameters.AddWithValue("@Nationality", data.Nationality.ToUpper());
                cmd.Parameters.AddWithValue("@Religion", data.Religion);
                cmd.Parameters.AddWithValue("@FatherName", data.FatherName.ToUpper());
                cmd.Parameters.AddWithValue("@PresentAddress", data.PresentAddress.ToUpper());
                cmd.Parameters.AddWithValue("@PermanentAddress", data.PermenantAddress.ToUpper());
                cmd.Parameters.AddWithValue("@MotherName", data.MotherName.ToUpper());
                cmd.Parameters.AddWithValue("@AdhaarCardNo", data.AdhaarCardNo);



                cmd.Parameters.AddWithValue("@StateCode", data.State);
                cmd.Parameters.AddWithValue("@DistrictCode", data.District);
                cmd.Parameters.AddWithValue("@F_SpecialCategory", data.SpecialCategory);
                cmd.Parameters.AddWithValue("@ParentEMail", data.ParentEMail);
                cmd.Parameters.AddWithValue("@IdentificationMark1", data.IdentificationMark1.ToUpper());
                if (data.IdentificationMark2 != null)
                {
                    cmd.Parameters.AddWithValue("@IdentificationMark2", data.IdentificationMark2.ToUpper());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdentificationMark2", data.IdentificationMark2);
                }

                cmd.Parameters.AddWithValue("@UniversityOrderNo", data.UniversityOrderNo);
                cmd.Parameters.AddWithValue("@UniversityOrderIssuedDate", data.UniversityOrderIssuedDate);
                cmd.Parameters.AddWithValue("@UniversityLastQualifiedTCNo", data.UniversityLastQualifiedTCNo);
                cmd.Parameters.AddWithValue("@UniversityLastQualifiedTCIssuedDate", data.UniversityLastQualifiedTCIssuedDate);
                if (data.TypeOfSecondaryEducation != null)
                {
                    cmd.Parameters.AddWithValue("@TypeOfSecondaryEducation", data.TypeOfSecondaryEducation.ToUpper());

                }

                else
                {
                    cmd.Parameters.AddWithValue("@TypeOfSecondaryEducation", data.TypeOfSecondaryEducation);
                }

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {

                    data.Status = "Success";
                }
                else
                {

                    data.Status = "Error";
                }


            }
            catch (Exception ex)
            {
                con.Close();
                data.Status = "Error";
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return data;

        }


        public Student GetStudentData(string SId)
        {
            Student data = new Student();
            try
            {
                if (isValid)
                {

                    using (SqlCommand cmd = new SqlCommand("[MF_RegistrationStudent_Fill]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", SId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                data.RollNo = dr["RollNo"].ToString();
                                data.StudentId = dr["StudentId"].ToString();
                                data.FirstName = dr["FirstName"].ToString();
                                data.MiddleName = dr["MiddleName"].ToString();
                                data.LastName = dr["LastName"].ToString();
                                data.FullName = dr["DisplayName"].ToString();
                                data.FatherName = dr["FatherName"].ToString();
                                data.MotherName = dr["MotherName"].ToString();
                                data.DateOfBirth = Convert.ToDateTime(dr["DOB"]);
                                data.DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]);
                                data.RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"]);
                                data.Entry = dr["EntryId"].ToString();
                                data.Year = Convert.ToInt32(dr["YearId"]);
                                data.Program = dr["ProgramId"].ToString();
                                data.Course = dr["CourseId"].ToString();
                                data.Section = dr["SectionId"].ToString();
                                data.Quota = dr["QuotaId"].ToString();
                                data.QuotaCategory = dr["QCId"].ToString();
                                data.AdhaarCardNo = dr["AdhaarCardNo"].ToString();
                                data.Batch = dr["BatchId"].ToString();
                                data.S_MobileNo = dr["PhoneNo"].ToString();
                                data.P_MobileNo = dr["Mobile"].ToString();
                                if (dr["Rank"] != DBNull.Value)
                                {
                                    data.EamcetRank = Convert.ToInt32(dr["Rank"]);
                                }

                                data.EMail = dr["EMail"].ToString();
                                data.Religion = dr["Religion"].ToString();
                                data.Languages = dr["Languages"].ToString();
                                data.Nationality = dr["Nationality"].ToString();
                                data.PresentAddress = dr["PresentAddress"].ToString();
                                data.PermenantAddress = dr["PermanentAddress"].ToString();
                                data.Category = dr["CategeoryId"].ToString();
                                data.State = dr["StateCode"].ToString();
                                data.District = dr["DistrictCode"].ToString();
                                data.SpecialCategory = dr["F_SpecialCategory"].ToString();


                                data.ParentEMail = dr["ParentEMail"].ToString();
                                data.IdentificationMark1 = dr["IdentificationMark1"].ToString();
                                data.IdentificationMark2 = dr["IdentificationMark2"].ToString();

                                data.UniversityOrderNo = dr["L_UniversityOrderNo"].ToString();

                                if (dr["L_UniversityOrderIssuedDate"] != DBNull.Value)
                                {
                                    data.UniversityOrderIssuedDate = Convert.ToDateTime(dr["L_UniversityOrderIssuedDate"]);
                                }
                                data.UniversityLastQualifiedTCNo = dr["L_UniversityLastQulifiedTCNO"].ToString();
                                if (dr["L_UniversityLastQulifiedTCIssuedDate"] != DBNull.Value)
                                {
                                    data.UniversityLastQualifiedTCIssuedDate = Convert.ToDateTime(dr["L_UniversityLastQulifiedTCIssuedDate"]);

                                }
                                data.TypeOfSecondaryEducation = dr["L_TypeOfSecondaryEducation"].ToString();


                                if (dr["Gender"].ToString() == "M")
                                {
                                    data.Gender = "Male";
                                }
                                else
                                {
                                    data.Gender = "Female";
                                }


                            }
                        }

                        cmd.Connection.Close();
                    }

                    //using (SqlCommand cmd = new SqlCommand("[MF_GetTutionFee]", con))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("@StudentId", SId);
                    //    cmd.Parameters.AddWithValue("@YearId", data.Year);
                    //    con.Open();
                    //    SqlDataReader dr = cmd.ExecuteReader();
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            data.F_Fees = Convert.ToDecimal(dr["Amount"]);
                    //        }
                    //    }
                    //    cmd.Connection.Close();
                    //}

                    //using (SqlCommand cmd = new SqlCommand("[MF_ConversionFee]", con))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("@StudentId", SId);
                    //    cmd.Parameters.AddWithValue("@YearId", data.Year);
                    //    con.Open();
                    //    SqlDataReader dr = cmd.ExecuteReader();
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            data.F_Convenor = Convert.ToDecimal(dr["Convinor"]);
                    //            data.F_APMinority = Convert.ToDecimal(dr["APMinority"]);
                    //            data.F_Student = Convert.ToDecimal(dr["Student"]);

                    //        }
                    //    }

                    //    cmd.Connection.Close();
                    //}



                    //using (SqlCommand cmd = new SqlCommand("[MF_Percentages1]", con))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("@StudentId", SId);
                    //    cmd.Parameters.AddWithValue("@Qualifacation", "SSC");
                    //    con.Open();
                    //    SqlDataReader dr = cmd.ExecuteReader();
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            data.F_SSC_Percentage = Convert.ToDecimal(dr["Percentage"]);


                    //        }
                    //    }

                    //    cmd.Connection.Close();
                    //}

                    //using (SqlCommand cmd = new SqlCommand("[MF_Percentages1]", con))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("@StudentId", SId);
                    //    cmd.Parameters.AddWithValue("@Qualifacation", "Inter");
                    //    con.Open();
                    //    SqlDataReader dr = cmd.ExecuteReader();
                    //    if (dr.HasRows)
                    //    {
                    //        while (dr.Read())
                    //        {
                    //            data.F_Inter_Percentage = Convert.ToDecimal(dr["Percentage"]);


                    //        }
                    //    }

                    //    cmd.Connection.Close();
                    //}


                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return data;
        }



        public bool StudentOtherFee(string StudentId, string FeeId, int YearId, decimal Amount)
        {
            SqlCommand cmd = new SqlCommand("[MF_StudentOtherFee_SAVE]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@FeeId", FeeId);
            cmd.Parameters.AddWithValue("@YearId", YearId);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public int StudentAcademic(List<Academic> Ac)
        {
            int rescount = 0;

            foreach (var item in Ac)
            {
                SqlCommand cmd = new SqlCommand("[MF_Academic_SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", item.StudentId);
                cmd.Parameters.AddWithValue("@Qualification", item.Qualification);
                cmd.Parameters.AddWithValue("@Passedout", item.PassedOut);
                cmd.Parameters.AddWithValue("@HallTicket", item.HallTicket);
                cmd.Parameters.AddWithValue("@Department", item.Department);
                cmd.Parameters.AddWithValue("@Max_Marks", item.Max_Marks);
                cmd.Parameters.AddWithValue("@Sec_Marks", item.Secured_Marks);
                cmd.Parameters.AddWithValue("@Division", item.Division);
                con.Open();


                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {

                    rescount = rescount + 1;
                    con.Close();
                }
                else
                {

                    rescount = rescount + 0;
                    con.Close();
                }



            }
            return rescount;


        }


        public bool StudentPhoto(string StudentId, byte[] bytes)
        {
            SqlCommand cmd = new SqlCommand("[MF_Photo_SAVE]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            cmd.Parameters.AddWithValue("@StudentPhoto", bytes);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int BulkRecordInsert(DataTable Dt_Record, string TableName)
        {
            int num;
            try
            {
                num = this.ExecuteBulkInsert(Dt_Record, TableName);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                throw new Exception(exception.Message, exception.InnerException);
            }
            return num;
        }

        public int ExecuteBulkInsert(DataTable Dt_Record, string TableName)
        {
            int num;
            try
            {
                num = this.BlukInsert(ref Dt_Record, TableName);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                throw new Exception(exception.Message, exception.InnerException);
            }
            return num;
        }
        public int BlukInsert(ref DataTable oDataTable, string TableName)
        {
            int num = 0;
            try
            {
                // this.OpenConnection(strDbtype);
                con.Open();
               
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + TableName + ";", con)
                    {
                        CommandTimeout = 0xe10
                    };
                    long num3 = Convert.ToInt32(RuntimeHelpers.GetObjectValue(command.ExecuteScalar()));
                    using (SqlBulkCopy copy = new SqlBulkCopy(con))
                    {
                        copy.DestinationTableName = TableName;
                        try
                        {
                            copy.BulkCopyTimeout = 0xe10;
                            copy.WriteToServer(oDataTable);
                        }
                        catch (Exception exception1)
                        {
                            Exception exception = exception1;
                        }
                    }
                    long num2 = Convert.ToInt32(RuntimeHelpers.GetObjectValue(command.ExecuteScalar()));
                    num = (int)(num2 - num3);
             
                con.Close();
                return num;
            }
            catch (Exception exception5)
            {
                Exception exception3 = exception5;
                throw new Exception(exception3.Message, exception3.InnerException);
            }

        }

        public decimal GetFeeAmount(string FId)
        {
            decimal Amount = 0;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_FeesMaster");
                        cmd.Parameters.AddWithValue("@Colum_Name", "Amount");
                        cmd.Parameters.AddWithValue("@para_Name", "FeeId");
                        cmd.Parameters.AddWithValue("@para_Name1", FId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Amount = Convert.ToDecimal(dr["Amount"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return Amount;
        }


        public string GetValue(string Var_Table_Name,string Var_Col_Name,string Parm_col,string Parm_Val)
        {
            string Value = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", Var_Table_Name);
                        cmd.Parameters.AddWithValue("@Colum_Name", Var_Col_Name);
                        cmd.Parameters.AddWithValue("@para_Name", Parm_col);
                        cmd.Parameters.AddWithValue("@para_Name1", Parm_Val);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                Value = dr[Var_Col_Name].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return Value;
        }



        public string GetStudentId(string RollNo)
        {
            string SId = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentEnrollment");
                        cmd.Parameters.AddWithValue("@Colum_Name", "StudentId");
                        cmd.Parameters.AddWithValue("@para_Name", "RollNo");
                        cmd.Parameters.AddWithValue("@para_Name1", RollNo);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                SId = dr["StudentId"].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return SId;
        }


        public string GetPhotoStudent(string Sid)
        {
            string SId = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentPhoto");
                        cmd.Parameters.AddWithValue("@Colum_Name", "StudentId");
                        cmd.Parameters.AddWithValue("@para_Name", "StudentId");
                        cmd.Parameters.AddWithValue("@para_Name1", Sid);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                SId = dr["StudentId"].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return SId;
        }


        public decimal GetTutionFee(int YearId, string QuotaId, string ProgramId, string CourseId)
        {
            decimal Amount = 0;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_Get_Fee]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@YearId", YearId);
                        cmd.Parameters.AddWithValue("@QuotaId", QuotaId);
                        cmd.Parameters.AddWithValue("@ProgramId", ProgramId);
                        cmd.Parameters.AddWithValue("@CourseId", CourseId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                Amount = Convert.ToDecimal(dr["Fees"]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return Amount;
        }




        public DataSet GetTutionBifurcationFee(int YearId, string QuotaCatagoryId)
        {
            DataSet dataset = new DataSet();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_Get_BifurcationFee]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@YearId", YearId);
                        cmd.Parameters.AddWithValue("@QuotaCatagoryId", QuotaCatagoryId);
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dataset);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }

            return dataset;
        }


    }
}
