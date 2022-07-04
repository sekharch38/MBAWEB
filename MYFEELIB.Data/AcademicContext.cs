using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MYFEELIB.Entities;
namespace MYFEELIB.Data
{
    public class AcademicContext
    {
        static string connection = ConfigurationManager.ConnectionStrings["MyFeeConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connection);
        bool isValid = Common.ValidateConnection(connection);
        List<ListItem> list = new List<ListItem>();
        List<FillDue> ListDue = new List<FillDue>();
        List<StudentDetails> listSD = new List<StudentDetails>();
        //List<SubModule> lstSubModule = new List<SubModule>();
        public List<ListItem> GetBatches()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Batch]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT BATCH"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["BatchId"].ToString(),
                                    Text = dr["BatchId"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetEmployees(string Dept)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Employees]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Dept", Dept);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT FACULTY"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["EmployeeCode"].ToString(),
                                    Text = dr["DisplayName"].ToString()
                                });
                                index = index + 1;
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
            list.RemoveAt(0);
            return list;
        }

        public List<ListItem> Get_Attend_Subject(string Sem, String Section, int WId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Attend_Subjects]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Sem", Sem);
                        cmd.Parameters.AddWithValue("@Sec", Section);
                        cmd.Parameters.AddWithValue("@Wid", WId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT SUBJECT"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["TId"].ToString(),
                                    Text = dr["SubjectCode"].ToString()
                                });
                                index = index + 1;
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
           // list.RemoveAt(0);
            return list;
        }

        public List<ListItem> Get_Attend_Period(int Sid, String Section, int WId, string course)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Attend_Period]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Sid", Sid);
                        cmd.Parameters.AddWithValue("@Sec", Section);
                        cmd.Parameters.AddWithValue("@Wid", WId);
                        cmd.Parameters.AddWithValue("@course", course);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT PERIOD"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["PId"].ToString(),
                                    Text = dr["PName"].ToString()
                                });
                                index = index + 1;
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
           // list.RemoveAt(0);
            return list;
        }

        public List<ListItem> Get_Attend_Employee(int Sid, String Section, int WId, string course,string sem,int PId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Attend_Employee]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Sid", Sid);
                        cmd.Parameters.AddWithValue("@Sec", Section);
                        cmd.Parameters.AddWithValue("@Wid", WId);
                        cmd.Parameters.AddWithValue("@course", course);
                        cmd.Parameters.AddWithValue("@Sem", sem);
                        cmd.Parameters.AddWithValue("@PId", PId);


                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT EMPLOYEE"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["EmployeeCode"].ToString(),
                                    Text = dr["DisplayName"].ToString()
                                });
                                index = index + 1;
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
           // list.RemoveAt(0);
            return list;
        }

        public List<ListItem> GetSubject(string Program, string course, int year, string Semester)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Subjects]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Program", Program);
                        cmd.Parameters.AddWithValue("@Course", course);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Semester", Semester);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT SUBJECT"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["SId"].ToString(),
                                    Text = dr["SubjectName"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetDetainTypes()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentDetain");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT DETAIN TYPE"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["DetainId"].ToString(),
                                    Text = dr["DetainName"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }
        public List<ListItem> GetTransactions(DateTime dt)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ChallanConfirmation_DateSelect]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ChallanDate", dt);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT TRANSACTION"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["TransactionId"].ToString(),
                                    Text = dr["DisplayName"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetYears(string Batch)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Years]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", Batch);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT YEAR"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["YearId"].ToString(),
                                    Text = dr["YearId"].ToString() + " Year"
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetWeeks()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_WeekMaster");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            //list.Insert(index, new ListItem()
                            //{
                            //    Value = null,
                            //    Text = "SELECT PAY MODE"
                            //});
                            //index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["Id"].ToString(),
                                    Text = dr["WeekName"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetClassTypes()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_ClassType");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            //list.Insert(index, new ListItem()
                            //{
                            //    Value = null,
                            //    Text = "SELECT PAY MODE"
                            //});
                            //index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["CId"].ToString(),
                                    Text = dr["CType"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }


        public List<ListItem> GetPayModes()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_PaymentMode");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            //list.Insert(index, new ListItem()
                            //{
                            //    Value = null,
                            //    Text = "SELECT PAY MODE"
                            //});
                            //index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["PId"].ToString(),
                                    Text = dr["PaymentName"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetProcessMonth()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_ProcessMonths]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["Id"].ToString(),
                                    Text = dr["PMonth"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        //public List<FillDue> GetFillDue(RequireElements REQ)
        //{

        //    try
        //    {
        //        if (isValid)
        //        {
        //            using (SqlCommand cmd = new SqlCommand("[MF_GetMassUpdateFee]", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@BatchId", REQ.Batch);
        //                cmd.Parameters.AddWithValue("@YearId", REQ.Year);
        //                cmd.Parameters.AddWithValue("@ProgramId", REQ.Program);
        //                cmd.Parameters.AddWithValue("@CourseId", REQ.Course);
        //                cmd.Parameters.AddWithValue("@Status", REQ.Status);
        //                cmd.Parameters.AddWithValue("@QuotaId", REQ.DueFrom);
        //                con.Open();
        //                SqlDataReader dr = cmd.ExecuteReader();

        //                if (dr.HasRows)
        //                {
        //                    while (dr.Read())
        //                    {

        //                        FillDue FD = new FillDue()
        //                        {
        //                            RollNo = dr["RollNo"].ToString(),
        //                            DisplayName = dr["DisplayName"].ToString(),
        //                            FatherName = dr["FatherName"].ToString(),
        //                            TuitionFee = Convert.ToDecimal(dr["Due"]),
        //                            ScholarshipId = dr["ScholarshipId"].ToString(),
        //                            StudentId = dr["StudentId"].ToString()

        //                        };
        //                        ListDue.Add(FD);
        //                    }
        //                }
        //            }

        //        }
        //    }





        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        ExceptionLog.ErrorLog(ex);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //    return ListDue;
        //}






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


        //public string GetPending(string RN, int Year)
        //{
        //    //Pending For Approval
        //    string Pending_Amount = "";

        //    using (SqlCommand cmdpending = new SqlCommand("[MF_PendingForApproval]", con))
        //    {
        //        cmdpending.CommandType = CommandType.StoredProcedure;
        //        cmdpending.Parameters.AddWithValue("@RollNo", RN);
        //        cmdpending.Parameters.AddWithValue("@YearId", Year);
        //        Pending_Amount = cmdpending.ExecuteScalar().ToString();
        //    }

        //    return Pending_Amount;
        //}



        public DataSet GetFinancialPeriod()
        {
            DataSet dataset = new DataSet();

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[FY_Period]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataset);
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


        public DataSet GetFinancialDates(string YearCode)
        {
            DataSet dataset = new DataSet();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[FY_Period_Dates]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@YearCode", YearCode);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataset);
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
        public List<ListItem> GetFinancialYears()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_financial_Years]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT FINANCIAL YEAR"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["SNo"].ToString(),
                                    Text = dr["YearCode"].ToString()
                                });
                                index = index + 1;
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

            list.RemoveAt(0);
            return list;
        }

        public DataSet GetMonthDates(int Id)
        {
            DataSet dataset = new DataSet();

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_Month_Dates]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataset);
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

        public int StudentMassUpdate(List<FillDue> DueUpdate)
        {
            int rescount = 0;
            foreach (var item in DueUpdate)
            {
                SqlCommand cmd = new SqlCommand("[MF_StudentMassUpdae_SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", item.StudentId);
                cmd.Parameters.AddWithValue("@YearId", item.Year);
                cmd.Parameters.AddWithValue("@TuitionFee", item.TuitionFee);
                cmd.Parameters.AddWithValue("@OUFee", item.OUFee);
                cmd.Parameters.AddWithValue("@ChallanNo", item.ChallanNo);
                cmd.Parameters.AddWithValue("@ChallanDate", item.ChallanDate);
                cmd.Parameters.AddWithValue("@EntryDate", item.EntryDate);
                cmd.Parameters.AddWithValue("@ReceivedFrom", item.ReceivedFrom);
                cmd.Parameters.AddWithValue("@PaymentId", item.PaymentId);
                cmd.Parameters.AddWithValue("@Status", item.Status);
                cmd.Parameters.AddWithValue("@EnterBy", item.EnterBy);
                cmd.Parameters.AddWithValue("@ProgramId", item.ProgramId);


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

        public string Select_Values(string Ssql)
        {
            string val = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand(Ssql, con))
                    {
                        con.Open();
                        string value = Convert.ToString(cmd.ExecuteScalar());
                        if (value != "")
                        {
                            val = value;
                        }
                        else
                            val = value;
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
            return val;
        }


        public int Update_QR_BAR(byte[] datab, byte[] dataQR, string SId)
        {
            int rescount = 0;
            SqlCommand cmdStudent = new SqlCommand("[MF_QR_BAR_SAVE]", con);
            cmdStudent.CommandType = CommandType.StoredProcedure;
            cmdStudent.Parameters.AddWithValue("@StudentId", SId);
            cmdStudent.Parameters.AddWithValue("@QR", dataQR);
            cmdStudent.Parameters.AddWithValue("@Bar", datab);
            con.Open();
            int i = cmdStudent.ExecuteNonQuery();
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
            return rescount;
        }


        public int ExtrPaid(List<ExtraPaid> Ac)
        {
            int rescount = 0;

            foreach (var item in Ac)
            {

                SqlCommand cmdStudent = new SqlCommand("[Dynamic_StudentId]", con);
                cmdStudent.CommandType = CommandType.StoredProcedure;
                cmdStudent.Parameters.AddWithValue("@RollNo", item.RollNo);
                con.Open();
                string StudentId = cmdStudent.ExecuteScalar().ToString();
                con.Close();


                string Tran_Status = "";
                if (item.Status == "Refund")
                {
                    Tran_Status = "Refund To be Confirm";
                }
                else
                {
                    Tran_Status = "Not Approved";
                }


                SqlCommand cmd = new SqlCommand("[MF_ExtraPaid_SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@YearId", item.Year);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                cmd.Parameters.AddWithValue("@DateOfConfirmation", item.ChallanDate);
                cmd.Parameters.AddWithValue("@VoucherNo", item.ChallanNo);
                cmd.Parameters.AddWithValue("@Status", Tran_Status);
                cmd.Parameters.AddWithValue("@EnterBy", item.EnterBy);
                cmd.Parameters.AddWithValue("@PId", item.PayMode);


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



        public List<FillDue> GetFillDue(RequireElements REQ)
        {

            try
            {
                if (isValid)
                {
                    DataSet dataset = new DataSet();
                    using (SqlCommand cmd = new SqlCommand("[MF_GetMassUpdateFee]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", REQ.Batch);
                        cmd.Parameters.AddWithValue("@YearId", REQ.Year);
                        cmd.Parameters.AddWithValue("@ProgramId", REQ.Program);
                        cmd.Parameters.AddWithValue("@CourseId", REQ.Course);
                        cmd.Parameters.AddWithValue("@Status", REQ.Status);
                        cmd.Parameters.AddWithValue("@QuotaId", REQ.DueFrom);
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dataset);
                        }
                        cmd.Connection.Close();


                    }
                    foreach (DataRow dr in dataset.Tables[0].Rows)
                    {
                        DataSet datasetPending = new DataSet();
                        using (SqlCommand cmd = new SqlCommand("[MF_PendingForApproval]", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RollNo", dr["RollNo"].ToString());
                            cmd.Parameters.AddWithValue("@YearId", REQ.Year);
                            con.Open();


                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(datasetPending);
                            }
                            cmd.Connection.Close();

                        }

                        if (datasetPending.Tables[0].Rows.Count == 0)
                        {
                            FillDue FD = new FillDue()
                            {
                                RollNo = dr["RollNo"].ToString(),
                                DisplayName = dr["DisplayName"].ToString(),
                                FatherName = dr["FatherName"].ToString(),
                                TuitionFee = Convert.ToDecimal(dr["Due"]),
                                ScholarshipId = dr["ScholarshipId"].ToString(),
                                StudentId = dr["StudentId"].ToString()

                            };
                            ListDue.Add(FD);

                        }
                        else
                        {
                            if (datasetPending.Tables[0].Rows.Count > 0)
                            {
                                decimal Rem_Fee = Convert.ToDecimal(dr["Due"]) - Convert.ToDecimal(datasetPending.Tables[0].Rows[0][0]);
                                if (Rem_Fee > 0)
                                {
                                    FillDue FD = new FillDue()
                                    {
                                        RollNo = dr["RollNo"].ToString(),
                                        DisplayName = dr["DisplayName"].ToString(),
                                        FatherName = dr["FatherName"].ToString(),
                                        TuitionFee = Rem_Fee,
                                        ScholarshipId = dr["ScholarshipId"].ToString(),
                                        StudentId = dr["StudentId"].ToString()

                                    };
                                    ListDue.Add(FD);
                                }
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

            return ListDue;
        }


        List<Challan> lstChallanDetails = new List<Challan>();
        public List<Challan> GetChallanDetails(Challan REQ)
        {
            try
            {
                if (isValid)
                {
                    DataSet datasetAC = new DataSet();
                    using (SqlCommand cmd = new SqlCommand("[MF_AcadamicChallan_Fill]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", REQ.RollNo);
                        cmd.Parameters.AddWithValue("@YearId", REQ.Year);
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(datasetAC);
                        }
                        cmd.Connection.Close();
                    }


                    foreach (DataRow dr in datasetAC.Tables[0].Rows)
                    {
                        DataSet datasetPending = new DataSet();

                        using (SqlCommand cmd = new SqlCommand("[MF_Academic_ChallanPending]", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RollNo", REQ.RollNo);
                            cmd.Parameters.AddWithValue("@YearId", REQ.Year);
                            cmd.Parameters.AddWithValue("@FeeId", dr["FeeId"].ToString());

                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(datasetPending);
                            }
                            cmd.Connection.Close();

                        }

                        if (datasetPending.Tables[0].Rows.Count == 0)
                        {
                            Challan data = new Challan()
                            {
                                StudentId = dr["StudentId"].ToString(),
                                DisplayName = dr["DisplayName"].ToString(),
                                FeeId = dr["FeeId"].ToString(),
                                Due = Convert.ToDecimal(dr["Due"]),
                            };
                            lstChallanDetails.Add(data);

                        }
                        else
                        {
                            if (datasetPending.Tables[0].Rows.Count > 0)
                            {
                                decimal Rem_Fee = Convert.ToDecimal(dr["Due"]) - Convert.ToDecimal(datasetPending.Tables[0].Rows[0][0]);
                                if (Rem_Fee > 0)
                                {
                                    Challan FD = new Challan()
                                    {
                                        StudentId = dr["StudentId"].ToString(),
                                        DisplayName = dr["DisplayName"].ToString(),
                                        FeeId = dr["FeeId"].ToString(),
                                        Due = Rem_Fee,
                                    };
                                    lstChallanDetails.Add(FD);
                                }
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

            return lstChallanDetails;
        }





        List<ChallanConfirmation> lstChallanConfirm = new List<ChallanConfirmation>();
        public List<ChallanConfirmation> GetTransactionDetails(string TranId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ChallanConfirmation_StudentSelect]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TransactionId", TranId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ChallanConfirmation data = new ChallanConfirmation()
                                {
                                    StudentId = dr["StudentId"].ToString(),
                                    DisplayName = dr["DisplayName"].ToString(),
                                    FeeId = dr["FeeId"].ToString(),
                                    Amount = Convert.ToDecimal(dr["Amount"]),
                                    TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                                    Year = Convert.ToInt32(dr["YearId"]),
                                    ChallanNo = dr["ChallanNo"].ToString(),
                                    PayMode = dr["PId"].ToString()

                                };
                                lstChallanConfirm.Add(data);
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

            return lstChallanConfirm;
        }

        public int AcadimicChallanSave(IEnumerable<Challan> DueUpdate)
        {
            int rescount = 0;
            try
            {
                int StTransaction;
                SqlCommand cmdcount = new SqlCommand("[MF_AcadamicChallan_TransactionCount]", con);
                cmdcount.CommandType = CommandType.StoredProcedure;
                con.Open();
                object j = (object)cmdcount.ExecuteScalar();
                con.Close();

                if (j != null)
                {
                    StTransaction = Convert.ToInt32(j) + 1;
                }
                else
                {
                    StTransaction = 100001;
                }

                DataTable dt = new DataTable();
                DataColumn dtColumn;
                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "TransactionId";
                dtColumn.Caption = "TransactionId";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.String");
                dtColumn.ColumnName = "FeeId";
                dtColumn.Caption = "FeeId";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Decimal");
                dtColumn.ColumnName = "Amount";
                dtColumn.Caption = "Amount";
                dt.Columns.Add(dtColumn);



                string StudentId = "";
                int year = 0;
                string ChallanNo = "";
                DateTime ChallanDate = DateTime.Now;
                decimal TotalAmount = 0;
                DateTime EntryDate = DateTime.Now;
                string Remarks = "";
                string EnterBy = "";
                int PId = 0;

                foreach (var item in DueUpdate)
                {

                    StudentId = item.StudentId;
                    year = Convert.ToInt32(item.Year);
                    ChallanNo = item.ChallanNo;
                    ChallanDate = Convert.ToDateTime(item.PaymentDate);
                    TotalAmount = item.TotalAmout;
                    EntryDate = Convert.ToDateTime(item.EntryDate);
                    Remarks = item.Remarks;
                    EnterBy = item.EnterBy;
                    PId = Convert.ToInt32(item.PayMode);

                    DataRow dr = dt.NewRow();
                    dr["TransactionId"] = StTransaction;
                    dr["FeeId"] = item.FeeId;
                    dr["Amount"] = item.Due;
                    dt.Rows.Add(dr);

                }


                SqlCommand cmd = new SqlCommand("[MF_AcadamicChallan_SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransactionId", StTransaction);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@YearId", year);
                cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);
                cmd.Parameters.AddWithValue("@ChallanDate", ChallanDate);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@EntryDate", EntryDate);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@EnterBy", EnterBy);
                cmd.Parameters.AddWithValue("@PId", PId);
                cmd.Parameters.AddWithValue("@feeChallan", dt);


                con.Open();
                int i = cmd.ExecuteNonQuery();


                if (i > 0)
                {
                    rescount = 1;
                    con.Close();
                }
                else
                {
                    rescount = 0;
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
            return rescount;

        }

        public int AcadimicChallanConfirmation(IEnumerable<Challan> DueUpdate)
        {
            int rescount = 0;
            try
            {
                DataTable dt = new DataTable();
                DataColumn dtColumn;
                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "Id";
                dtColumn.Caption = "Id";
                dt.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.String");
                dtColumn.ColumnName = "FeeId";
                dtColumn.Caption = "FeeId";
                dt.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Decimal");
                dtColumn.ColumnName = "Amount";
                dtColumn.Caption = "Amount";
                dt.Columns.Add(dtColumn);





                string StudentId = "";
                int year = 0;
                string ChallanNo = "";
                DateTime ChallanDate = DateTime.Now;
                decimal TotalAmount = 0;
                DateTime EntryDate = DateTime.Now;
                string Remarks = "";
                string EnterBy = "";
                int PId = 0;
                int StTransaction = 0;
                foreach (var item in DueUpdate)
                {
                    StTransaction = item.TransactionId;
                    StudentId = item.StudentId;
                    year = Convert.ToInt32(item.Year);
                    ChallanNo = item.ChallanNo;
                    ChallanDate = Convert.ToDateTime(item.PaymentDate);
                    TotalAmount = item.TotalAmout;
                    EntryDate = Convert.ToDateTime(item.EntryDate);
                    Remarks = item.Remarks;
                    EnterBy = item.EnterBy;
                    PId = Convert.ToInt32(item.PayMode);

                    DataRow dr = dt.NewRow();

                    dr["Id"] = item.Id;
                    dr["FeeId"] = item.FeeId;
                    dr["Amount"] = item.Due;
                    dt.Rows.Add(dr);

                }


                SqlCommand cmd = new SqlCommand("[MF_ChallanConfirmation_save]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TransactionId", StTransaction);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@YearId", year);
                cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);
                cmd.Parameters.AddWithValue("@DateOfConfirmation", ChallanDate);
                cmd.Parameters.AddWithValue("@Amount", TotalAmount);
                cmd.Parameters.AddWithValue("@ReceivedFrom", "Student");
                cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ApprovedBY", EnterBy);
                cmd.Parameters.AddWithValue("@PId", PId);
                cmd.Parameters.AddWithValue("@ChallanTransaction", dt);


                con.Open();
                int i = cmd.ExecuteNonQuery();


                if (i > 0)
                {
                    rescount = 1;
                    con.Close();
                }
                else
                {
                    rescount = 0;
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
            return rescount;

        }

        List<ExtraPaid> lstEP = new List<ExtraPaid>();

        //DateTime Dt
        public List<ExtraPaid> GetExtraPaidDetails(DateTime Dt)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_ExtrPaid]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DateOfConfirmation", Dt);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ExtraPaid data = new ExtraPaid()
                                {
                                    Sno = Convert.ToInt32(dr["Sno"].ToString()),
                                    RollNo = dr["RollNo"].ToString(),
                                    Name = dr["DisplayName"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"].ToString()),
                                    Amount = Convert.ToDecimal(dr["Amount"]),
                                    ChallanDate = Convert.ToDateTime(dr["DateOfConfirmation"]),
                                    Status = dr["Status"].ToString()
                                };
                                lstEP.Add(data);
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

            return lstEP;
        }


        public int ExtrapaidConfirm(DataTable DT, string ApprovedBy)
        {
            SqlCommand cmd = new SqlCommand("[MF_ExtraPaidConfirm]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ApprovedBy", ApprovedBy);
            cmd.Parameters.AddWithValue("@ExtraPaid", DT);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return i;
            }
            else
            {
                return 0;
            }
        }

        public int Challan_Update(int Sno, string ChallanNo, DateTime Dt, string Status, string Res, string SuspenseId)
        {
            int rescount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_UPDATE_CHALLAN]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TranId", Sno);
                cmd.Parameters.AddWithValue("@ChallanNo", ChallanNo);
                cmd.Parameters.AddWithValue("@PaymentDate", Dt);
                cmd.Parameters.AddWithValue("@Result", Res);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@SuspenseId", SuspenseId);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    rescount = 1;

                }
                else
                {
                    rescount = 0;
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
            return rescount;
        }


        public int Challan_Delete(int Sno, string Status, string Res)
        {
            int rescount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_DELETE_CHALLAN]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TranId", Sno);
                cmd.Parameters.AddWithValue("@Result", Res);
                cmd.Parameters.AddWithValue("@Status", Status);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    rescount = 1;

                }
                else
                {
                    rescount = 0;
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
            return rescount;
        }

        public int Challan_Delete_Approved(int Sno, string Status, string Res)
        {
            int rescount = 0;
            try
            {

                DataSet datasetFeeId = new DataSet();
                using (SqlCommand cmd = new SqlCommand("[MF_GETFEE_DELETE]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TranId", Sno);

                    con.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(datasetFeeId);
                    }
                    cmd.Connection.Close();
                }

                DataTable dt = new DataTable();
                DataColumn dtColumn;
                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "Id";
                dtColumn.Caption = "Id";
                dt.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.String");
                dtColumn.ColumnName = "FeeId";
                dtColumn.Caption = "FeeId";
                dt.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Decimal");
                dtColumn.ColumnName = "Amount";
                dtColumn.Caption = "Amount";
                dt.Columns.Add(dtColumn);

                int rowcount = 1;
                foreach (DataRow drfee in datasetFeeId.Tables[0].Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = rowcount;
                    dr["FeeId"] = drfee["FeeId"].ToString();
                    dr["Amount"] = Convert.ToDecimal(drfee["Amount"]);
                    dt.Rows.Add(dr);
                    rowcount += 1;
                }


                using (SqlCommand cmd = new SqlCommand("[MF_DELETEAPPROVED_CHALLAN]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TranId", Sno);
                    cmd.Parameters.AddWithValue("@ChallanTransaction", dt);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i >= 1)
                    {
                        rescount = 1;

                    }
                    else
                    {
                        rescount = 0;
                    }
                }
            }

            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
                return 0;
            }
            finally
            {
                con.Close();
            }
            return rescount;
        }

        public List<ExtraPaid> GetChallanUpdate(string Result, string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_SELECT_CHALLAN]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Result", Result);
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);


                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ExtraPaid data = new ExtraPaid()
                                {
                                    Sno = Convert.ToInt32(dr["TransactionId"].ToString()),
                                    RollNo = dr["RollNo"].ToString(),
                                    Name = dr["DisplayName"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"].ToString()),
                                    Amount = Convert.ToDecimal(dr["TotalAmount"]),
                                    ChallanNo = dr["ChallanNo"].ToString(),
                                    ChallanDate = Convert.ToDateTime(dr["ChallanDate"]),
                                    Status = dr["Status"].ToString(),
                                    PayMode = dr["PId"].ToString(),
                                    result = Result
                                };
                                lstEP.Add(data);
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

            return lstEP;
        }



        public List<ExtraPaid> GetFillRefund(FinancialYearDates REQ)
        {

            try
            {
                if (isValid)
                {
                    DataSet dataset = new DataSet();
                    using (SqlCommand cmd = new SqlCommand("[MF_FeeRefund_Fill]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@strdt", REQ.FromDate);
                        cmd.Parameters.AddWithValue("@strEndt", REQ.ToDate);
                        cmd.Parameters.AddWithValue("@Status", REQ.Status);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ExtraPaid data = new ExtraPaid()
                                {
                                    StudentId = dr["StudentId"].ToString(),
                                    RollNo = dr["RollNo"].ToString(),
                                    Name = dr["DisplayName"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"].ToString()),
                                    Amount = Convert.ToDecimal(dr["Amount"]),

                                };
                                lstEP.Add(data);
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

            return lstEP;
        }

        public int StudentRefund(List<ExtraPaid> Refund)
        {
            int rescount = 0;
            try
            {
                DataTable dt = new DataTable();
                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "Id";
                dtColumn.Caption = "Id";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.String");
                dtColumn.ColumnName = "StudentId";
                dtColumn.Caption = "StudentId";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "YearId";
                dtColumn.Caption = "YearId";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.String");
                dtColumn.ColumnName = "VoucherNo";
                dtColumn.Caption = "VoucherNo";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.DateTime");
                dtColumn.ColumnName = "DateofRefund";
                dtColumn.Caption = "DateofRefund";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Decimal");
                dtColumn.ColumnName = "Amount";
                dtColumn.Caption = "Amount";
                dt.Columns.Add(dtColumn);

                int id = 1;

                foreach (var item in Refund)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = id;
                    dr["StudentId"] = item.StudentId;
                    dr["YearId"] = item.Year;
                    dr["VoucherNo"] = item.ChallanNo;
                    dr["DateofRefund"] = item.ChallanDate;
                    dr["Amount"] = item.Amount;
                    dt.Rows.Add(dr);
                    id += 1;
                }

                SqlCommand cmd = new SqlCommand("[MF_Refund_save]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RT", dt);
                con.Open();


                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {

                    rescount = dt.Rows.Count;

                }
                else
                {

                    rescount = 0;

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
            return rescount;
        }

        public List<StudentDetails> CheckRollNo(string RollNo)
        {

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Get_RollNo]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                int index = 0;
                                listSD.Insert(index, new StudentDetails()
                                {
                                    Year = Convert.ToInt32(dr["YearId"]),
                                    Name = dr["DisplayName"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    CourseName = dr["CourseName"].ToString(),
                                    CourseId = dr["CourseId"].ToString(),
                                    ProgramId = dr["ProgramId"].ToString()
                                });
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

            return listSD;
        }

        public int StudentDetain_Discontinue(Student_Detain_Discontinue Detain)
        {
            int rescount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_Discontinue]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNo", Detain.RollNo);
                cmd.Parameters.AddWithValue("@YearId", Detain.Year);
                cmd.Parameters.AddWithValue("@DetainId", Detain.DetainId);
                cmd.Parameters.AddWithValue("@DetainType", Detain.Status);
                cmd.Parameters.AddWithValue("@FromDate", Detain.DetainDate);
                cmd.Parameters.AddWithValue("@ToDate", Detain.DetainDate);
                cmd.Parameters.AddWithValue("@Remarks", Detain.Remarks);
                cmd.Parameters.AddWithValue("@EnterBy", Detain.EnterBy);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    rescount = 1;
                }
                else
                {
                    rescount = 0;
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
            return rescount;
        }

        List<StudentFeeMaster> lstStudentFee = new List<StudentFeeMaster>();
        public List<StudentFeeMaster> GetStudentFeeDetails(string RollNo, string FeeDetail)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentFee_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        cmd.Parameters.AddWithValue("@SelectFee", FeeDetail);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentFeeMaster data = new StudentFeeMaster()
                                {
                                    StudentId = dr["StudentId"].ToString(),
                                    Name = dr["Name"].ToString(),
                                    FeeName = dr["FeeName"].ToString(),
                                    FeeId = dr["FeeId"].ToString(),
                                    Actual = Convert.ToDecimal(dr["Actual"]),
                                    Received = Convert.ToDecimal(dr["Received"]),
                                    Due = Convert.ToDecimal(dr["Due"]),
                                    YearId = Convert.ToInt32(dr["YearId"]),
                                    SFMId = Convert.ToInt32(dr["Id"])

                                };
                                lstStudentFee.Add(data);
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

            return lstStudentFee;
        }


        public List<StudentFeeMaster> GetStudentSearchFee(string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentSearch_Fee_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentFeeMaster data = new StudentFeeMaster()
                                {
                                    Actual = Convert.ToDecimal(dr["Actual"]),
                                    Received = Convert.ToDecimal(dr["Paid"]),
                                    Due = Convert.ToDecimal(dr["Due"]),
                                    YearId = Convert.ToInt32(dr["YearId"]),
                                };
                                lstStudentFee.Add(data);
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

            return lstStudentFee;
        }

        List<StudentAttendancePercentage> lstAttend = new List<StudentAttendancePercentage>();
        public List<StudentAttendancePercentage> GetStudentAttendance(string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Attendance_Month]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentAttendancePercentage data = new StudentAttendancePercentage()
                                {
                                    TOTAL_DAYS = Convert.ToInt32(dr["TOTAL_DAYS"]),
                                    WORKING_DAYS = Convert.ToInt32(dr["WORKING_DAYS"]),
                                    PRESENT_DAYS = Convert.ToInt32(dr["PRESENT_DAYS"]),
                                    ABSENT_DAYS = Convert.ToInt32(dr["ABSENT_DAYS"]),
                                    Percentage = dr["Percentage"].ToString(),
                                };
                                lstAttend.Add(data);
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

            return lstAttend;
        }


        List<Challan> lstChallan = new List<Challan>();
        public List<Challan> GetStudentSearchChallanFee(string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentSearch_ChallanFee_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Challan data = new Challan()
                                {
                                    TotalAmout = Convert.ToDecimal(dr["TotalAmount"]),
                                    ChallanNo = dr["ChallanNo"].ToString(),
                                    PD = dr["ChallanDate"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"]),
                                    Status = dr["Status"].ToString(),
                                };
                                lstChallan.Add(data);
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

            return lstChallan;
        }

        List<Challan> lstExtra = new List<Challan>();
        public List<Challan> GetStudentSearchExtraFee(string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentSearch_ExtraFee_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Challan data = new Challan()
                                {
                                    TotalAmout = Convert.ToDecimal(dr["Amount"]),
                                    ChallanNo = dr["VoucherNo"].ToString(),
                                    PD = dr["DateOfConfirmation"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"]),
                                    Status = dr["Status"].ToString()
                                };
                                lstExtra.Add(data);
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

            return lstExtra;
        }

        List<Student_Detain_Discontinue> lstDetain = new List<Student_Detain_Discontinue>();
        public List<Student_Detain_Discontinue> GetStudentSearchDetain(string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentSearch_DETAIN_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Student_Detain_Discontinue data = new Student_Detain_Discontinue()
                                {
                                    DetainId = dr["DetainId"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    DDate = dr["ToDate"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"]),
                                    DISDate = dr["Discontinue_Date"].ToString(),
                                    ID = Convert.ToInt32(dr["Id"]),

                                };
                                lstDetain.Add(data);
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

            return lstDetain;
        }

        public Student_Detain_Discontinue GetStudentDetain(string ID)
        {
            Student_Detain_Discontinue data = new Student_Detain_Discontinue();
            try
            {

                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Student_DETAIN_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                data.DetainId = dr["DetainId"].ToString();
                                if (dr["Status"].ToString() == "Not ReAdmitted")
                                {
                                    data.Status = "Detain";

                                }
                                else
                                {
                                    data.Status = dr["Status"].ToString();

                                }
                                if (dr["ToDate"] != DBNull.Value)
                                {
                                    data.DetainDate = Convert.ToDateTime(dr["ToDate"]);
                                }

                                if (dr["Discontinue_Date"] != DBNull.Value)
                                {
                                    data.Discontinue_Date = Convert.ToDateTime(dr["Discontinue_Date"]);
                                }

                                data.ID = Convert.ToInt32(dr["Id"]);
                                //lstDetain.Add(data);
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

            return data;
        }

        public int DetainUpdate(Student_Detain_Discontinue Upd)
        {
            int rescount = 0;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Detain_Discontinue_Update]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Upd.ID);
                        cmd.Parameters.AddWithValue("@DetainId", Upd.DetainId);
                        //cmd.Parameters.AddWithValue("@DetainDate", Upd.DetainDate);
                        cmd.Parameters.AddWithValue("@DisContinueDate", Upd.Discontinue_Date);
                        cmd.Parameters.AddWithValue("@Status", Upd.Status);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i >= 1)
                        {
                            rescount = 1;

                        }
                        else
                        {
                            rescount = 0;
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

            return rescount;
        }

        List<Readmission> lstRA = new List<Readmission>();
        public List<Readmission> GetStudentSearchReadmission(string RollNo)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentSearch_Readmission_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Readmission data = new Readmission()
                                {
                                    DetainId = dr["DetainId"].ToString(),
                                    Remarks = dr["Remarks"].ToString(),
                                    FromBatch = dr["FromBatch"].ToString(),
                                    FromYear = dr["FromYear"].ToString(),
                                    ToBatch = dr["ToBatch"].ToString(),
                                    ToYear = dr["ToYear"].ToString(),
                                };
                                lstRA.Add(data);
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

            return lstRA;
        }

        public Decimal GetFutureAmount(string RollNo)
        {
            decimal Future_amount = 0;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_FutureFee_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Future_amount = Convert.ToDecimal(dr["FA"]);
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

            return Future_amount;
        }

        public string GetTCDetails(string RollNo)
        {
            string TC = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentSearch_TC_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        //cmd.Parameters.Add("@Result", SqlDbType.Char, 200);
                        //cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TC = dr["Result"].ToString();
                                //TC = cmd.Parameters["@Result"].Value.ToString();
                            }
                        }

                        //con.Open();
                        //int i = cmd.ExecuteNonQuery ();



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

            return TC;
        }
        public string GetFeeWaiverStatus(string RollNo)
        {
            string WaiverStatus = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_FeeWaiver_Status]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (dr["Fee_Waiver_Status"] != DBNull.Value)
                                {
                                    WaiverStatus = dr["Fee_Waiver_Status"].ToString();
                                }
                                else
                                {
                                    WaiverStatus = "Fee Waiver Not Done";
                                }


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

            return WaiverStatus;

        }


        public int Discontinue_FeeWiver(FeeWaiver FW)
        {
            int rescount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_DISCONTINUE_FEEWAIVER]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNo", FW.RollNo);
                cmd.Parameters.AddWithValue("@Fee_Waiver", FW.Fee_Waiver);
                cmd.Parameters.AddWithValue("@TotCurrent_Due", FW.Current_Due);
                cmd.Parameters.AddWithValue("@Final_collected", FW.Final_Collected);
                cmd.Parameters.AddWithValue("@Future_Amount_Collected", FW.Future_Amount);
                cmd.Parameters.AddWithValue("@UserName", FW.UserName);
                cmd.Parameters.AddWithValue("@Reason", FW.Remarks);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    rescount = 1;
                }
                else
                {
                    rescount = 0;
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
            return rescount;
        }


        public int RegularFeeWaiverSave(IEnumerable<StudentFeeMaster> data)
        {
            int rescount = 0;
            try
            {


                DataTable dt = new DataTable();
                DataColumn dtColumn;
                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "Id";
                dtColumn.Caption = "Id";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "SFMId";
                dtColumn.Caption = "SFMId";
                dt.Columns.Add(dtColumn);

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Decimal");
                dtColumn.ColumnName = "Amount";
                dtColumn.Caption = "Amount";
                dt.Columns.Add(dtColumn);


                string RollNo = "";
                string Reason = "";
                decimal Fee_Waiver = 0;
                string UserName = "";
                if (data != null)
                {
                    foreach (var item in data)
                    {

                        RollNo = item.RollNo;
                        Reason = item.Reason;
                        Fee_Waiver = item.Fee_Waiver;
                        UserName = item.UserName;

                        DataRow dr = dt.NewRow();
                        dr["Id"] = item.Id;
                        dr["SFMId"] = item.SFMId;
                        dr["Amount"] = item.Actual;
                        dt.Rows.Add(dr);

                    }


                    SqlCommand cmd = new SqlCommand("[MF_REGULAR_FEEWAIVER]", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RollNo", RollNo);
                    cmd.Parameters.AddWithValue("@Reason", Reason);
                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    cmd.Parameters.AddWithValue("@RF", dt);


                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i > 0)
                    {
                        rescount = 1;
                        con.Close();
                    }
                    else
                    {
                        rescount = 0;
                        con.Close();
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
            return rescount;

        }



        //public DataSet GetStudentSearch(string RollNo)
        //{
        //    DataSet dataset = new DataSet();

        //    try
        //    {
        //        if (isValid)
        //        {
        //            using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@Table_Name", "Vw_StudentSearch");
        //                cmd.Parameters.AddWithValue("@Colum_Name", "*");
        //                cmd.Parameters.AddWithValue("@para_Name", "RollNo");
        //                cmd.Parameters.AddWithValue("@para_Name1", RollNo);
        //                con.Open();
        //                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                da.Fill(dataset);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        con.Close();
        //        ExceptionLog.ErrorLog(ex);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return dataset;
        //}

        public StudentSearch GetStudentSearch(string RollNo)
        {
            StudentSearch data = new StudentSearch();
            try
            {
                if (isValid)
                {

                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "Vw_StudentSearch");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "RollNo");
                        cmd.Parameters.AddWithValue("@para_Name1", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                data.RollNo = dr["RollNo"].ToString();
                                data.FirstName = dr["FirstName"].ToString();
                                data.MiddleName = dr["MiddleName"].ToString();
                                data.LastName = dr["LastName"].ToString();
                                data.FullName = dr["DisplayName"].ToString();
                                data.FatherName = dr["FatherName"].ToString();
                                data.MotherName = dr["MotherName"].ToString();
                                data.DateOfBirth = dr["DOB"].ToString();
                                data.DateOfJoining = dr["DateOfJoining"].ToString();
                                data.RegistrationDate = dr["RegistrationDate"].ToString();
                                data.Entry = dr["EntryId"].ToString();
                                data.Year = Convert.ToInt32(dr["YearId"]);
                                data.Program = dr["ProgramName"].ToString();
                                data.Course = dr["CourseName"].ToString();
                                data.Section = dr["SectionName"].ToString();
                                data.Quota = dr["QuotaName"].ToString();
                                data.QuotaCategory = dr["QCId"].ToString();
                                data.AdhaarCardNo = dr["AdhaarCardNo"].ToString();
                                data.Batch = dr["BatchId"].ToString();
                                data.S_MobileNo = dr["PhoneNo"].ToString();
                                data.P_MobileNo = dr["Mobile"].ToString();
                                data.EamcetRank = dr["Rank"].ToString();

                                data.EMail = dr["EMail"].ToString();
                                data.Religion = dr["Religion"].ToString();
                                data.Languages = dr["Languages"].ToString();
                                data.Nationality = dr["Nationality"].ToString();
                                data.PresentAddress = dr["PresentAddress"].ToString();
                                data.PermenantAddress = dr["PermanentAddress"].ToString();
                                data.Category = dr["CategoryName"].ToString();
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
                                data.Status = dr["StudentStatus"].ToString();
                                data.Content = "Success";


                            }
                        }
                        else
                        {
                            data.Content = "Error";
                        }

                        cmd.Connection.Close();
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

            return data;
        }

        public byte[] GetStudentPhoto(string RollNo)
        {
            byte[] imageData = null;
            try
            {
                if (isValid)
                {

                    using (SqlCommand cmd = new SqlCommand("[MF_GETPHOTO_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RollNo);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {

                                imageData = (byte[])dr["StudentPhoto"];

                            }
                        }


                        cmd.Connection.Close();
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

            return imageData;
        }



        List<StudentReAdmission> lstStudentReAdmissionConfirm = new List<StudentReAdmission>();
        public List<StudentReAdmission> GetStudentReAdmissionDetails(string StudentId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_Student]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", StudentId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentReAdmission data = new StudentReAdmission()
                                {
                                    StudentId = dr["StudentId"].ToString(),
                                    Name = dr["Name"].ToString(),
                                    FeeName = dr["FeeName"].ToString(),
                                    Actual = Convert.ToDecimal(dr["Actual"]),
                                    Received = Convert.ToDecimal(dr["Received"]),
                                    Due = Convert.ToDecimal(dr["Due"]),
                                    YearId = Convert.ToInt32(dr["YearId"])

                                };
                                lstStudentReAdmissionConfirm.Add(data);
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

            return lstStudentReAdmissionConfirm;
        }

        List<StudentReAdmission> lstStudentReAdmissionDetails = new List<StudentReAdmission>();
        public List<StudentReAdmission> GetStudentReAdmissionList(string StudentId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_StudentDetail]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", StudentId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentReAdmission data = new StudentReAdmission()
                                {

                                    DetainId = dr["Id"].ToString(),
                                    DetainName = dr["DetainName"].ToString(),
                                    Description = dr["Description"].ToString(),
                                    YearId = Convert.ToInt32(dr["YearId"]),
                                    FromDate = Convert.ToDateTime(dr["FromDate"]),
                                    ToDate = Convert.ToDateTime(dr["ToDate"]),
                                    FD = dr["FromDate"].ToString(),
                                    TD = dr["ToDate"].ToString(),
                                    Status = dr["Status"].ToString()

                                };
                                lstStudentReAdmissionDetails.Add(data);
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

            return lstStudentReAdmissionDetails;
        }

        public List<ListItem> GetReAdmissionSameBatch(string Readmission)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_SameBatchReadmission]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", Readmission);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT READMISSION BATCH"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["BatchId"].ToString(),
                                    Text = dr["BatchId"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }

        public List<ListItem> GetReAdmissionOtherBatch(string year)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_OtherBatchReadmission]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@yearId", year);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT READMISSION BATCH"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["BatchId"].ToString(),
                                    Text = dr["BatchId"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }








        public List<ListItem> GetStudent(string BatchId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_BatchSelect]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", BatchId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT STUDENT"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["StudentId"].ToString(),
                                    Text = dr["Name"].ToString()
                                });
                                index = index + 1;
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

            return list;
        }



        public List<ListItem> GetYear(string BatchId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_Year]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", BatchId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT READMISSION YEAR"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["YearId"].ToString(),
                                    Text = dr["YearId"].ToString() + " Year"
                                });
                                index = index + 1;
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

            return list;
        }

        public DataSet GetDate(string student, string batch)
        {
            DataSet dataset = new DataSet();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ReAdmission_Date]", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", student);
                        cmd.Parameters.AddWithValue("@Batch", batch);
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
        public int StudentReadmissionSave(IEnumerable<StudentReAdmission> DueUpdate)
        {
            int result = 0;
            int resultcount = 0;
            try
            {
                string StudentId = "";
                string FromBatch = "";
                string FromYear = "";
                string ToBatch = "";
                string ToYear = "";
                int detainid = 0;
                string Remarks = "";
                DateTime date = DateTime.Now;

                foreach (var item in DueUpdate)
                {
                    StudentId = item.StudentId;
                    FromBatch = item.Batch;
                    FromYear = item.YearOfDetain;
                    ToBatch = item.ReAdmissionBatch;
                    ToYear = item.ReAdmissionYear;
                    detainid = Convert.ToInt32(item.DetainTransactionId);
                    Remarks = item.Reason;
                    date = Convert.ToDateTime(item.ReAdmissionDate);

                }


                SqlCommand cmd = new SqlCommand("[MF_ReAdmission__SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                cmd.Parameters.AddWithValue("@FromBatch", FromBatch);
                cmd.Parameters.AddWithValue("@FromYear", FromYear);
                cmd.Parameters.AddWithValue("@ToBatch", ToBatch);
                cmd.Parameters.AddWithValue("@ToYear", ToYear);
                cmd.Parameters.AddWithValue("@DetainId", detainid);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@id", detainid);
                cmd.Parameters.AddWithValue("@ToDate", date);
                cmd.Parameters.Add("@Result", SqlDbType.Char, 10);
                cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                con.Open();
                result = cmd.ExecuteNonQuery();

                resultcount = Convert.ToInt32(cmd.Parameters["@Result"].Value);
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
            return resultcount;

        }

        public string Role_Save(DataTable Sub_Mod_dt, DataTable Main_dt, int RId, string RoleName)
        {
            string result = null;
            try
            {

                DataSet datasetFeeId = new DataSet();
                using (SqlCommand cmd = new SqlCommand("[MF_ROLE_INSERT_UPDATE]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RId", RId);
                    cmd.Parameters.AddWithValue("@RoleName", RoleName);
                    cmd.Parameters.AddWithValue("@ModuleAccess", Main_dt);
                    cmd.Parameters.AddWithValue("@FormAccess", Sub_Mod_dt);
                    cmd.Parameters.Add("@Result", SqlDbType.Char, 250);
                    cmd.Parameters["@Result"].Direction = ParameterDirection.Output;


                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    if (i >= 1)
                    {
                        result = cmd.Parameters["@Result"].Value.ToString();

                    }
                    else
                    {
                        result = null;
                    }
                }
            }

            catch (Exception ex)
            {
                con.Close();
                ExceptionLog.ErrorLog(ex);
                result = null;
            }
            finally
            {
                con.Close();
            }
            return result;
        }


        public int Generate_BC(string RollNo)
        {
            int rescount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_Certificate_SAVE]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CertificateId", "BC");
                cmd.Parameters.AddWithValue("@RollNo", RollNo);
                cmd.Parameters.AddWithValue("@Status", "Prepared");
                cmd.Parameters.AddWithValue("@DOL", DateTime.Now);
                cmd.Parameters.AddWithValue("@Course", "Not Completed");
                cmd.Parameters.AddWithValue("@Dues", "NIL");
                cmd.Parameters.AddWithValue("@Conduct", "Good");
                cmd.Parameters.AddWithValue("@disciplinary", "NIL");
                cmd.Parameters.AddWithValue("@Remarks", "No");
                cmd.Parameters.AddWithValue("@Copy", "ORIGINAL");
                cmd.Parameters.AddWithValue("@TransactionStatus", "Open");
                cmd.Parameters.AddWithValue("@Admission", "");
                cmd.Parameters.AddWithValue("@Leaving", "");



                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    rescount = 1;
                }
                else
                {
                    rescount = 0;
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
            return rescount;
        }
    }
}
