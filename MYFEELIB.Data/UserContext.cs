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
    public class UserContext
    {
        static string connection = ConfigurationManager.ConnectionStrings["MyFeeConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connection);
        bool isValid = Common.ValidateConnection(connection);

        List<Registration> lstUsers = new List<Registration>();
        List<Modules> lstModule = new List<Modules>();
        List<SubModule> lstSubModule = new List<SubModule>();
        List<StudentAuth> lstStudent = new List<StudentAuth>();
        List<Program> lstProgram = new List<Program>();

        List<OtherFee> lstOtherFee = new List<OtherFee>();
        List<StudentQuotaCategory> lstStudentQuotaCategory = new List<StudentQuotaCategory>();
        List<Course> lstCourse = new List<Course>();
        List<StudentQuota> lstStudentQuota = new List<StudentQuota>();
        List<Quota> lstQuota = new List<Quota>();
        List<QuotaCategory> lstQuotacat = new List<QuotaCategory>();
        List<ListItem> list = new List<ListItem>();

        List<Certificate> lstCertificate = new List<Certificate>();
        List<CertificateMaster> lstCertificateMaster = new List<CertificateMaster>();
        List<Section> lstSection = new List<Section>();
        List<University> lstUniversity = new List<University>();
        List<FinancialBatches> lstFinancialBatch = new List<FinancialBatches>();
        List<Message> lstMessage = new List<Message>();
        List<MassMailingSMS> lstSMS = new List<MassMailingSMS>();

       
        public Registration UserSave(Registration data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_User_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", data.UserName);
                        cmd.Parameters.AddWithValue("@UserPwd", data.UserPwd);
                        cmd.Parameters.AddWithValue("@DisplayName", data.DisplayName);
                        cmd.Parameters.AddWithValue("@UserType", data.Role);
                        cmd.Parameters.AddWithValue("@MobileNo", data.MobileNo);
                        cmd.Parameters.AddWithValue("@Designation", data.Designation);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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


        public string MainModuleSave(Modules data)
        {
            string Status = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_MainModule_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", data.Id);
                        cmd.Parameters.AddWithValue("@Name", data.Name);
                        cmd.Parameters.AddWithValue("@WebPage", data.WebPage);
                        cmd.Parameters.AddWithValue("@F_Class", data.F_Class);



                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            Status = "Success";
                        }
                        else
                            Status = "Error";
                    }
                }
                else
                    Status = "Error";
            }
            catch (Exception ex)
            {
                con.Close();
                Status = "Error";
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return Status;
        }


        public string SubModuleSave(SubModule data)
        {
            string Status = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_SubModule_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Id", data.F_Id );
                        cmd.Parameters.AddWithValue("@Name", data.Name);
                        cmd.Parameters.AddWithValue("@WebPage", data.WebPage);
                        cmd.Parameters.AddWithValue("@F_Class", data.F_Class);
                        cmd.Parameters.AddWithValue("@cont", data.cont);
                        cmd.Parameters.AddWithValue("@Mod_Id", data.Id);



                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            Status = "Success";
                        }
                        else
                            Status = "Error";
                    }
                }
                else
                    Status = "Error";
            }
            catch (Exception ex)
            {
                con.Close();
                Status = "Error";
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return Status;
        }


        public string JV_SAVE(JournalVoucher data)
        {
            string Status = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_JV_SAVE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TransactionName", data.TId);
                        cmd.Parameters.AddWithValue("@DateOfTransaction", data.TransDate);
                        cmd.Parameters.AddWithValue("@NOS", data.NOS);
                        cmd.Parameters.AddWithValue("@Amount", data.Amount);
                        cmd.Parameters.AddWithValue("@TransType", data.TransactionType.Trim());
                        cmd.Parameters.AddWithValue("@UserName", data.UserName);
                        con.Open();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            Status = "Success";
                        }
                        else
                            Status = "Error";
                    }
                }
                else
                    Status = "Error";
            }
            catch (Exception ex)
            {
                con.Close();
                Status = "Error";
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return Status;
        }

        public Student StudentRegister(Student data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Student_SAVE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", data.RollNo);
                        cmd.Parameters.AddWithValue("@UserPwd", data.FirstName);
                        cmd.Parameters.AddWithValue("@Status", data.MiddleName);
                        cmd.Parameters.AddWithValue("@DisplayName", data.LastName);
                        cmd.Parameters.AddWithValue("@MobileNo", data.FullName);
                        cmd.Parameters.AddWithValue("@Designation", data.Program);
                        cmd.Parameters.AddWithValue("@Email", data.Course);
                        cmd.Parameters.AddWithValue("@UserType", data.Section);
                        cmd.Parameters.Add("@ID", SqlDbType.Int);
                        cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string ID = Convert.ToString(cmd.Parameters["@ID"].Value);
                        if (ID != "")
                        {
                            data.RollNo = ID;
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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

        public List<Registration> GetUserDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_USER_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Registration data = new Registration()
                                {
                                    RegId = Convert.ToInt32(dr["ID"]),
                                    MobileNo = dr["MobileNo"].ToString(),
                                    DisplayName = dr["DisplayName"].ToString(),
                                    Designation = dr["Designation"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    UserName = dr["Name"].ToString(),
                                    UserPwd = dr["UserPwd"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    UserType = Convert.ToInt32(dr["UserType"])
                                };
                                lstUsers.Add(data);
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

            return lstUsers;
        }

        List<Modules> lstMainModules = new List<Modules>();
        public List<Modules> GetMainModuleDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_MainModule_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Modules data = new Modules()
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Name = dr["Name"].ToString(),
                                    WebPage = dr["F_Page"].ToString(),
                                    F_Class = dr["F_Class"].ToString(),

                                };
                                lstMainModules.Add(data);
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

            return lstMainModules;
        }

        List<SubModule> lstSubModules = new List<SubModule>();
        public List<SubModule> GetSubModuleDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fee_Sub_Forms");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                SubModule data = new SubModule()
                                {
                                    F_Id = Convert.ToInt32(dr["F_Id"]),
                                    Name = dr["F_FormName"].ToString(),
                                    WebPage = dr["F_Page"].ToString(),
                                    F_Class = dr["F_Class"].ToString(),
                                    cont = dr["Controller"].ToString(),
                                    Id = Convert.ToInt32(dr["Id"]),

                                };
                                lstSubModules.Add(data);
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

            return lstSubModules;
        }

        List<JournalVoucher> lstJV = new List<JournalVoucher>();
        public List<JournalVoucher> GetJVDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[JVDetails]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                JournalVoucher data = new JournalVoucher()
                                {
                                    TId = dr["TransactionName"].ToString(),
                                    Select_Date = dr["Date"].ToString(),
                                    AmountCredit = Convert.ToDecimal(dr["AmountCredit"]),
                                    AmountDebit = Convert.ToDecimal(dr["AmountDebit"]),
                                    TransactionType = dr["Trans_Type"].ToString(),
                                    Balance = Convert.ToDecimal(dr["Balance"]),

                                };
                                lstJV.Add(data);
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

            return lstJV;
        }


        List<Custodian> lstCustodian = new List<Custodian>();
        public List<Custodian> CustodianDetails(string RN)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_GETCUST_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RN);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Custodian data = new Custodian()
                                {
                                    CustodianId = Convert.ToInt32(dr["cid"]),
                                    CustodianName = dr["cname"].ToString(),
                                };
                                lstCustodian.Add(data);
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

            return lstCustodian;
        }


        List<TobeReceived> lstTBR = new List<TobeReceived>();
        public List<TobeReceived> GetTBRDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_ToBeReceived");

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TobeReceived data = new TobeReceived()
                                {
                                    Program = dr["ProgramId"].ToString(),
                                    QuotaCategory = dr["QCId"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"]),
                                    Convenor = Convert.ToDecimal(dr["Convenor"]),
                                    APSMFC = Convert.ToDecimal(dr["Apminority"]),
                                    Student = Convert.ToDecimal(dr["Student"]),
                                    IsPercentage = dr["IsPercentage"].ToString(),
                                };
                                lstTBR.Add(data);
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
            return lstTBR;
        }


        //public List<Registration> GetRoles()
        //{
        //    try
        //    {
        //        if (isValid)
        //        {
        //            using (SqlCommand cmd = new SqlCommand("MF_USER_DETAILS", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                SqlDataReader dr = cmd.ExecuteReader();

        //                if (dr.HasRows)
        //                {
        //                    while (dr.Read())
        //                    {
        //                        Registration data = new Registration()
        //                        {
        //                            RegId = Convert.ToInt32(dr["ID"]),
        //                            MobileNo = dr["MobileNo"].ToString(),
        //                            DisplayName = dr["DisplayName"].ToString(),
        //                            Designation = dr["Designation"].ToString(),
        //                            Email = dr["Email"].ToString(),
        //                            UserName = dr["Name"].ToString(),
        //                            UserPwd = dr["UserPwd"].ToString(),
        //                            Status = dr["Status"].ToString(),
        //                            UserType = Convert.ToInt32(dr["UserType"])
        //                        };
        //                        lstUsers.Add(data);
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

        //    return lstUsers;
        //}



        public DataSet GetModules(int RId)
        {
            DataSet dataset = new DataSet();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("USP_MODULES_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", RId);
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


        public List<SubModule> GetSubModules(int RId, int Id)
        {

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_SUBMODULES_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@RId", RId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                SubModule dataModules = new SubModule()
                                {
                                    Id = Convert.ToInt32(dr["F_Id"]),
                                    Name = dr["F_FormName"].ToString(),
                                    WebPage = dr["WebPage"].ToString(),
                                    F_Class = dr["F_Class"].ToString(),
                                    cont = dr["Controller"].ToString()
                                };
                                lstSubModule.Add(dataModules);
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

            return lstSubModule;
        }


        public List<SubModule> GetSELECTROLEModules(int RId)
        {

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_SELECT_MODULES_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", RId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                SubModule dataModules = new SubModule()
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    F_Id = Convert.ToInt32(dr["F_Id"]),
                                };
                                lstSubModule.Add(dataModules);
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

            return lstSubModule;
        }


        public List<StudentAuth> GetStudent(string RId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_Student_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", RId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentAuth dataStudent = new StudentAuth() { RollNo = dr["RollNo"].ToString(), Status = dr["Status"].ToString() };
                                lstStudent.Add(dataStudent);
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

            return lstStudent;
        }


        public List<OtherFee> GetOtherFee(string PId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_Fee_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Program", PId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                OtherFee data = new OtherFee()
                                {
                                    FeeId = dr["FeeId"].ToString(),
                                    FeeName = dr["FeeName"].ToString(),
                                    Amount = Convert.ToDouble(dr["Amount"]),
                                };
                                lstOtherFee.Add(data);
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

            return lstOtherFee;
        }


        public List<ListItem> GetCategory()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentCategory");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT CATEGORY"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["CategoryId"].ToString(),
                                    Text = dr["CategoryName"].ToString()
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

        public List<ListItem> GetPrograms()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_Program");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT PROGRAM"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                           {
                               Value = dr["ProgramId"].ToString(),
                               Text = dr["ProgramName"].ToString()
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

        public List<ListItem> GetRoles()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_RoleMaster");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT ROLE"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["RoleId"].ToString(),
                                    Text = dr["RoleName"].ToString()
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

        public List<ListItem> GetModules_subModules()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fee_Sub_Modules");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT Modules"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["Id"].ToString(),
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

        public List<ListItem> GetJVTrans()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_JEMaster");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT JOURNAL VOUCHER ENTRIES"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["TranSactionId"].ToString(),
                                    Text = dr["TransactionName"].ToString()
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
        public List<ListItem> GetCourse(string PId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_Course");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "ProgramId");
                        cmd.Parameters.AddWithValue("@para_Name1", PId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT COURSE"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
     {
         Value = dr["CourseId"].ToString(),
         Text = dr["CourseName"].ToString()
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



        public List<ListItem> GetSection(string CId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_Section");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "CourseId");
                        cmd.Parameters.AddWithValue("@para_Name1", CId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT SECTION"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["SectionId"].ToString(),
                                    Text = dr["SectionName"].ToString()
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

        public List<ListItem> GetSemester(string YId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_Semester_master");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "YearId");
                        cmd.Parameters.AddWithValue("@para_Name1", YId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT SEMESTER"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["SemesterId"].ToString(),
                                    Text = dr["SemesterName"].ToString()
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
        public List<ListItem> GetQuota(string PId)
        {

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentQuota");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "ProgramId");
                        cmd.Parameters.AddWithValue("@para_Name1", PId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT QUOTA"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {

                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["QuotaId"].ToString(),
                                    Text = dr["QuotaName"].ToString()
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

        public List<ListItem> GetQuotaCategory(string QId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentQuotaCategory");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "QuotaId");
                        cmd.Parameters.AddWithValue("@para_Name1", QId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT QUOTA CATEGORY"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {

                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["QCId"].ToString(),
                                    Text = dr["QCName"].ToString()
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




        public List<ListItem> GetQuotaCategoryTBR(string Program)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_StudentQuotaCategory");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "ProgramId");
                        cmd.Parameters.AddWithValue("@para_Name1", Program);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT QUOTA CATEGORY"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {

                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["QCId"].ToString(),
                                    Text = dr["QCName"].ToString()
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


        public DataSet GetProgramDropDowns()
        {
            DataSet dataset = new DataSet();

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_GET_PROGCOURSEC_DETAILS]", con))
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





        public DataSet GetActiveStudents()
        {
            DataSet dataset = new DataSet();

            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_ACTIVE_STUDENTS]", con))
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



        public DataTable GetStudentDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Student_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
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
            return dt;
        }



        public DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_USER_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
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
            return dt;
        }



        public List<ListItem> GetState()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "State");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT STATE"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["StateCode"].ToString(),
                                    Text = dr["StateName"].ToString()
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



        public List<ListItem> GetDistrict(string STId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_ParameterSelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "District");
                        cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        cmd.Parameters.AddWithValue("@para_Name", "StateCode");
                        cmd.Parameters.AddWithValue("@para_Name1", STId);

                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT DISTRICT"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["DistrictCode"].ToString(),
                                    Text = dr["DistrictName"].ToString()
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


        public int FormAccess(User user)
        {
            int rescount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_USER_ACCESS]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", user.Username);
                cmd.Parameters.AddWithValue("@Access", user.Form_Access);

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


        public string TBR_SAVE(TobeReceived data)
        {
            string Status = null;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_TobeReceived_SAVE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Program", data.Program);
                        cmd.Parameters.AddWithValue("@Year", data.Year);
                        cmd.Parameters.AddWithValue("@QC", data.QuotaCategory);
                        cmd.Parameters.AddWithValue("@convenor", data.Convenor);
                        cmd.Parameters.AddWithValue("@APSMFC", data.APSMFC);
                        cmd.Parameters.AddWithValue("@Student", data.Student);
                        cmd.Parameters.AddWithValue("@IsPercentage", data.IsPercentage);

                        con.Open();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            Status = "Success";
                        }
                        else
                            Status = "Error";
                    }
                }
                else
                    Status = "Error";
            }
            catch (Exception ex)
            {
                con.Close();
                Status = "Error";
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return Status;
        }

        public int SaveCustodian(List<Custodian> cust)
        {
            int rescount = 0;
            try
            {
                DataTable dt = new DataTable();
                DataColumn dtColumn;

                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.Int32");
                dtColumn.ColumnName = "CustId";
                dtColumn.Caption = "CustId";
                dt.Columns.Add(dtColumn);



                string RN = null;

                foreach (var item in cust)
                {
                    DataRow dr = dt.NewRow();
                    RN = item.RollNo;
                    dr["CustId"] = item.CustodianId;
                    dt.Rows.Add(dr);
                }

                SqlCommand cmd = new SqlCommand("[MF_Custodian_save]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNo", RN);
                cmd.Parameters.AddWithValue("@CT", dt);


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



        public Course CourseSave(Course data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Course_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CourseId", data.CourseId);
                        cmd.Parameters.AddWithValue("@CourseName", data.CourseName);
                        cmd.Parameters.AddWithValue("@ProgramId", data.Program);
                        cmd.Parameters.AddWithValue("@Description", data.Description);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<Course> GetCourseDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_Course_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Course data = new Course()
                                {

                                    CourseId = dr["CourseId"].ToString(),
                                    CourseName = dr["CourseName"].ToString(),
                                    Program = dr["ProgramId"].ToString(),
                                    Description = dr["Description"].ToString()

                                };
                                lstCourse.Add(data);
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

            return lstCourse;
        }
        public Section SectionSave(Section data)
        {
            try
            {
                int NoOfStudents, LateralEntry, TransferEntry;
                if (data.NoOfStudents == null)
                {
                    NoOfStudents = 0;
                }
                else
                {
                    NoOfStudents =Convert.ToInt16(data.NoOfStudents);
                }
                if (data.LateralEntry == null)
                {
                    LateralEntry = 0;
                }
                else
                {
                    LateralEntry = Convert.ToInt16(data.LateralEntry);
                }
                if (data.TransferEntry == null)
                {
                    TransferEntry = 0;
                }
                else
                {
                    TransferEntry = Convert.ToInt16(data.TransferEntry);
                }
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Section_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SectionId", data.SectionId.Trim());
                        cmd.Parameters.AddWithValue("@SectionName", data.SectionName.Trim());
                        cmd.Parameters.AddWithValue("@ProgramId", data.Program.Trim());
                        cmd.Parameters.AddWithValue("@CourseId", data.Course.Trim());
                        cmd.Parameters.AddWithValue("@SectionDescription", data.Description.Trim());
                        cmd.Parameters.AddWithValue("@NoOfStudents", NoOfStudents);
                        cmd.Parameters.AddWithValue("@LateralEntry", LateralEntry);
                        cmd.Parameters.AddWithValue("@TransferEntry", TransferEntry);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<Section> GetSectionDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_Section_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Section data = new Section()
                                {

                                    SectionId = dr["SectionId"].ToString().Trim(),
                                    SectionName = dr["SectionName"].ToString().Trim(),
                                    Program = dr["ProgramId"].ToString().Trim(),
                                    Course = dr["CourseId"].ToString().Trim(),
                                    Description = dr["SectionDescription"].ToString().Trim(),
                                    NoOfStudents = dr["NoOfStudents"].ToString().Trim(),
                                    LateralEntry = dr["LateralEntry"].ToString().Trim(),
                                    TransferEntry = dr["TransferEntry"].ToString().Trim()
                                };
                                lstSection.Add(data);
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

            return lstSection;
        }


        public Program ProgramSave(Program data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Program_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProgramId", data.ProgramId.Trim());
                        cmd.Parameters.AddWithValue("@ProgramName", data.ProgramName.Trim());
                        cmd.Parameters.AddWithValue("@ProgramDescription", data.Description.Trim());
                        cmd.Parameters.AddWithValue("@Period", data.Period);
                        string IsDefault, IsFeeRestrict = "";
                        if (data.IsDefault == "Yes")
                        {
                            IsDefault = "Yes";
                        }
                        else
                        {
                            IsDefault = "No";
                        }
                        cmd.Parameters.AddWithValue("@IsDefault", IsDefault);
                        if (data.IsFeeRestrict == "Yes")
                        {
                            IsFeeRestrict = "Yes";
                        }
                        else
                        {
                            IsFeeRestrict = "No";
                        }
                        cmd.Parameters.AddWithValue("@IsFeeRestrict", IsFeeRestrict);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<Program> GetProgramDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_Program_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Program data = new Program()
                                {

                                    ProgramId = dr["ProgramId"].ToString().Trim(),
                                    ProgramName = dr["ProgramName"].ToString().Trim(),
                                    Description = dr["ProgramDescription"].ToString().Trim(),
                                    Period = Convert.ToInt32(dr["Period"]),
                                    IsDefault = dr["IsDefault"].ToString().Trim(),
                                    IsFeeRestrict = dr["IsFeeRestrict"].ToString().Trim()
                                };
                                lstProgram.Add(data);
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

            return lstProgram;
        }
        public StudentQuota StudentQuotaSave(StudentQuota data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentQuota_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@QuotaId", data.QuotaId.Trim());
                        cmd.Parameters.AddWithValue("@QuotaName", data.QuotaName.Trim());
                        cmd.Parameters.AddWithValue("@Description", data.Description.Trim());
                        cmd.Parameters.AddWithValue("@ProgramId", data.Program.Trim());

                        string IsScholarshipApplicable, OtherFeeRestrict = "";
                        if (data.ScholarshipApplicable == "Yes")
                        {
                            IsScholarshipApplicable = "Yes";
                        }
                        else
                        {
                            IsScholarshipApplicable = "No";
                        }
                        cmd.Parameters.AddWithValue("@ScholarshipApplicable", IsScholarshipApplicable);
                        if (data.IsOtherFeeRestrict == "Yes")
                        {
                            OtherFeeRestrict = "Yes";
                        }
                        else
                        {
                            OtherFeeRestrict = "No";
                        }
                        cmd.Parameters.AddWithValue("@IsOtherFeeRestrict", OtherFeeRestrict);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<StudentQuota> GetStudentQuotaDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_StudentQuota_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentQuota data = new StudentQuota()
                                {

                                    QuotaId = dr["QuotaId"].ToString().Trim(),
                                    QuotaName = dr["QuotaName"].ToString().Trim(),
                                    Description = dr["QuotaDescription"].ToString().Trim(),
                                    Program = dr["ProgramId"].ToString().Trim(),
                                    ScholarshipApplicable = dr["ScholarshipApplicable"].ToString().Trim(),
                                    IsOtherFeeRestrict = dr["IsOtherFeeRestrict"].ToString().Trim()
                                };
                                lstStudentQuota.Add(data);
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

            return lstStudentQuota;
        }
        public StudentQuotaCategory StudentQuotaCategorySave(StudentQuotaCategory data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_StudentQuotaCategory_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@QCId", data.QuotaCategoryId.Trim());
                        cmd.Parameters.AddWithValue("@QCName", data.QuotaCategoryName.Trim());
                        cmd.Parameters.AddWithValue("@QuotaId", data.Quota.Trim());
                        cmd.Parameters.AddWithValue("@Description", data.Description.Trim());
                        cmd.Parameters.AddWithValue("@IsReAdmit", data.IsReAdmit.Trim());
                        cmd.Parameters.AddWithValue("@ProgramId", data.Program.Trim());

                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<StudentQuotaCategory> GetStudentQuotaCategoryDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_StudentQuotaCategory_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                StudentQuotaCategory data = new StudentQuotaCategory()
                                {
                                    QuotaCategoryId = dr["QCId"].ToString().Trim(),
                                    QuotaCategoryName = dr["QCName"].ToString().Trim(),
                                    Quota = dr["QuotaId"].ToString().Trim(),
                                    Description = dr["Description"].ToString().Trim(),
                                    IsReAdmit = dr["IsReAdmit"].ToString().Trim(),
                                    Program = dr["ProgramId"].ToString().Trim()
                                };
                                lstStudentQuotaCategory.Add(data);
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

            return lstStudentQuotaCategory;
        }
        public CertificateMaster CertificateMasterSave(CertificateMaster data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Certificate_INSERT]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CertificateId", data.CertificateId.Trim());
                        cmd.Parameters.AddWithValue("@CertificateName", data.CertificateName.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public string CertificateSave(Certificate data)
        {
            int result = 0;
            string resultcount = "";
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Certificate_INSERT]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Rollno", data.RollNo.Trim());
                        cmd.Parameters.AddWithValue("@CertificateId", data.SelectCertificate.Trim());
                        //cmd.Parameters.AddWithValue("@StudentId", data.StudentId.Trim());
                      //  cmd.Parameters.AddWithValue("@Status", data.Status.Trim());
                        cmd.Parameters.AddWithValue("@DOL", data.DateOfLeaving);
                        cmd.Parameters.AddWithValue("@Course", data.Course.Trim());
                        cmd.Parameters.AddWithValue("@Dues", data.Dues.Trim());
                        cmd.Parameters.AddWithValue("@Conduct", data.Conduct.Trim());
                        cmd.Parameters.AddWithValue("@disciplinary", data.AnyDisciplinaryMeasurestakenagainsthim.Trim());
                        cmd.Parameters.AddWithValue("@Remarks", data.GeneralRemarks.Trim());
                        cmd.Parameters.AddWithValue("@SNo", data.SNo.Trim());
                        cmd.Parameters.AddWithValue("@Admission", data.AdmissiontotheCollege.Trim());
                        cmd.Parameters.AddWithValue("@Leaving", data.LeavingtheCollege.Trim());
                        cmd.Parameters.Add("@Result", SqlDbType.Char, 10);
                        cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                        con.Open();
                        // cmd.ExecuteNonQuery();

                        result = cmd.ExecuteNonQuery();

                        resultcount = cmd.Parameters["@Result"].Value.ToString();

                        // int ID = cmd.ExecuteNonQuery();
                        //if (ID > 0)
                        //{
                        //    data.Status = "Success";
                        //}
                        //else
                        //    data.Status = "Error";
                    }
                }
                //else
                //    data.Status = "Error";
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
            return resultcount;
        }
        public List<CertificateMaster> GetCertificateMasterDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_CertificateMaster_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                CertificateMaster data = new CertificateMaster()
                                {

                                    CertificateId = dr["CertificateId"].ToString().Trim(),
                                    CertificateName = dr["CertificateName"].ToString().Trim()
                                };
                                lstCertificateMaster.Add(data);
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

            return lstCertificateMaster;
        }

        public List<ListItem> GetBatchDetails(string PId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Batch_DETAILS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@Table_Name", "MF_Batch_DETAILS");
                        //cmd.Parameters.AddWithValue("@Colum_Name", "*");
                        //cmd.Parameters.AddWithValue("@para_Name", "ProgramId");
                        //cmd.Parameters.AddWithValue("@para_Name1", PId);
                        cmd.Parameters.AddWithValue("@ProgramId", PId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT Batch"
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
    
        public List<ListItem> GetCertificates()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[Dynamic_SelectSP]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_Name", "tbl_fees_CertificateMaster");
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT Certificate"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["CertificateId"].ToString(),
                                    Text = dr["CertificateName"].ToString()
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
        public List<Certificate> GetCertificateGridDetails(string rollno)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_Certification_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", rollno);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Certificate data = new Certificate()
                                {
                                    Batch = dr["BatchId"].ToString().Trim(),
                                    //YearId = dr["YearId"].ToString().Trim(),
                                    //StudentId = dr["StudentId"].ToString().Trim(),
                                    Name = dr["DisplayName"].ToString().Trim(),
                                    RollNo = dr["RollNo"].ToString().Trim(),
                                    Actual = dr["Actual"].ToString().Trim(),
                                    Paid = dr["Paid"].ToString().Trim(),
                                    Due = dr["Due"].ToString().Trim(),
                                    DateOfBirth = Convert.ToDateTime(dr["DOB"]),
                                    FatherName = dr["FatherName"].ToString().Trim()

                                };
                                lstCertificate.Add(data);
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

            return lstCertificate;
        }
        public University UniversitySave(University data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_University_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UniversityId", data.UniversityId.Trim());
                        cmd.Parameters.AddWithValue("@UniversityName", data.UniversityName.Trim());
                        cmd.Parameters.AddWithValue("@Description", data.UniversityName.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<University> GetUniversityDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_University_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                University data = new University()
                                {

                                    UniversityId = dr["UniversityId"].ToString().Trim(),
                                    UniversityName = dr["UniversityName"].ToString().Trim(),
                                    Description = dr["Description"].ToString().Trim()
                                };
                                lstUniversity.Add(data);
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

            return lstUniversity;
        }
        public List<Certificate> GetSelectCertificateDetails(string rollno)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_SelectedCertification_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RollNo", rollno);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Certificate data = new Certificate()
                                {
                                    EntryId = dr["EntryId"].ToString().Trim(),
                                    Batch = dr["BatchId"].ToString().Trim(),
                                    Description = dr["Description"].ToString().Trim(),
                                    Program = dr["ProgramName"].ToString().Trim(),
                                    Period = dr["Period"].ToString().Trim(),
                                    DateOfJoining = dr["DateOfJoining"].ToString().Trim()

                                };
                                lstCertificate.Add(data);
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

            return lstCertificate;
        }

        public List<Certificate> GetSelectCertificateSNODetails(string CertificateId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_SelectedCertificationSNo_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CertificateId", CertificateId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Certificate data = new Certificate()
                                {
                                    SNo = dr["SNo"].ToString().Trim()
                                   

                                };
                                lstCertificate.Add(data);
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

            return lstCertificate;
        }

        public List<ListItem> GetBatchIdDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_BatchId_DETAILS]", con))
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
                                Text = "SELECT Batch"
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
        public List<ListItem> GetYearCode()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_YearCode_DETAILS]", con))
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
                                Text = "SELECT YearCode"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["YearCode"].ToString(),
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

            return list;
        }

        public FinancialBatches FinancialBatchSave(FinancialBatches data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_FinancialBatches_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", data.Batch);
                        cmd.Parameters.AddWithValue("@YearId", data.Year);
                        cmd.Parameters.AddWithValue("@YearCode", data.YearCode);
                        cmd.Parameters.AddWithValue("@Academic", data.Academic);
                        cmd.Parameters.AddWithValue("@ProgramId", data.Program);
                        cmd.Parameters.AddWithValue("@FBNo", data.FBNo);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<FinancialBatches> GetFinancialBatchDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_FinancialBatches_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                FinancialBatches data = new FinancialBatches()
                                {

                                    FBNo =Convert.ToInt16(dr["FBNo"]),
                                    Batch = dr["BatchId"].ToString(),
                                    Program = dr["Programid"].ToString(),
                                    Year = dr["YearId"].ToString(),
                                    YearCode = dr["YearCode"].ToString(),
                                    MDate = dr["MFrom"].ToString(),
                                    TDate =dr["MTo"].ToString(),
                                    open = dr["Status"].ToString(),
                                    Academic = dr["Academic"].ToString()
                                  


                                };
                                lstFinancialBatch.Add(data);
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

            return lstFinancialBatch;
        }

        public Message MessageSave(Message data)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[MF_Message_INSERT_UPDATE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MessageId", data.MessageId);
                        cmd.Parameters.AddWithValue("@Message", data.Messages);
                        cmd.Parameters.AddWithValue("@IsAmount", data.IsAmount);
                        cmd.Parameters.AddWithValue("@MessageType", data.MessageType);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            data.Status = "Success";
                        }
                        else
                            data.Status = "Error";
                    }
                }
                else
                    data.Status = "Error";
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
        public List<Message> GetMessageDetails()
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("MF_Message_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Message data = new Message()
                                {
                                    MessageId = dr["MessageId"].ToString(),
                                    Messages = dr["Message"].ToString(),
                                    IsAmount = dr["IsAmount"].ToString(),
                                    MessageType = dr["MessageType"].ToString()

                                };
                                lstMessage.Add(data);
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

            return lstMessage;
        }

    
        public List<ListItem> GetQCName(string PId)
        {
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand("[USP_QCName_Details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProgramId", PId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            int index = 0;
                            list.Insert(index, new ListItem()
                            {
                                Value = null,
                                Text = "SELECT Qc"
                            });
                            index = index + 1;
                            while (dr.Read())
                            {
                                list.Insert(index, new ListItem()
                                {
                                    Value = dr["QCId"].ToString(),
                                    Text = dr["QCName"].ToString()
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


        public DataSet GetData(string sSQL, string sTableName)
        {
            DataSet dataset = new DataSet();
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand(sSQL, con))
                    {
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dataset, sTableName);
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
                            val = "0";
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

        public bool Save_Record(string sSql)
        {
            bool Status = false;
            try
            {
                if (isValid)
                {
                    using (SqlCommand cmd = new SqlCommand(sSql, con))
                    {

                        con.Open();
                        int ID = cmd.ExecuteNonQuery();
                        if (ID > 0)
                        {
                            Status = true;
                        }
                        else
                            Status = false;
                    }
                }
                else
                    Status = false;
            }
            catch (Exception ex)
            {
                con.Close();
                Status = false;
                ExceptionLog.ErrorLog(ex);
            }
            finally
            {
                con.Close();
            }
            return Status;
        }

    }
}
