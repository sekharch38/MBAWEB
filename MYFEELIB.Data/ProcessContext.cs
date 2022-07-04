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
    public class ProcessContext
    {
        static string connection = ConfigurationManager.ConnectionStrings["MyFeeConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connection);
        bool isValid = Common.ValidateConnection(connection);
        List<RequireElements> listReq = new List<RequireElements>();
        public List<RequireElements> GetFYBatches(string Program)
        {
            try
            {
                if (isValid)
                {
                    DataSet dsbatches = new DataSet();
                    using (SqlCommand cmd = new SqlCommand("[MF_SELECT_FY_BATCHES]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Program", Program);
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dsbatches);
                        }

                    }
                    if (dsbatches != null)
                    {
                        if (dsbatches.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsbatches.Tables[0].Rows)
                            {
                                RequireElements data = new RequireElements()
                                {
                                    Batch = dr["BatchId"].ToString(),
                                    Year = Convert.ToInt32(dr["YearId"]),
                                };
                                listReq.Add(data);
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

            return listReq;
        }

        public int AcademicYearProcess(RequireElements Req)
        {
            int rescount = 0;
            try
            {
                if (isValid)
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
                    dtColumn.ColumnName = "BatchId";
                    dtColumn.Caption = "BatchId";
                    dt.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = Type.GetType("System.String");
                    dtColumn.ColumnName = "StudentId";
                    dtColumn.Caption = "StudentId";
                    dt.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = Type.GetType("System.String");
                    dtColumn.ColumnName = "RollNo";
                    dtColumn.Caption = "RollNo";
                    dt.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = Type.GetType("System.Int32");
                    dtColumn.ColumnName = "YearId";
                    dtColumn.Caption = "YearId";
                    dt.Columns.Add(dtColumn);

                    DataSet dsStudents = new DataSet();
                    using (SqlCommand cmd = new SqlCommand("[MF_SELECT_FY_BATCH_STUDENTS]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Batch", Req.Batch);
                        cmd.Parameters.AddWithValue("@Year", Req.Year);
                        cmd.Parameters.AddWithValue("@Program", Req.Program);
                        con.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dsStudents);
                        }
                        cmd.Connection.Close();
                    }
                    if (dsStudents != null)
                    {
                        if (dsStudents.Tables[0].Rows.Count > 0)
                        {
                            int id = 1;
                            foreach (DataRow dr in dsStudents.Tables[0].Rows)
                            {
                                DataRow drstudent = dt.NewRow();
                                drstudent["Id"] = id;
                                drstudent["BatchId"] = dr["BatchId"].ToString();
                                drstudent["StudentId"] = dr["StudentId"].ToString();
                                drstudent["RollNo"] = dr["RollNo"].ToString();
                                drstudent["YearId"] = dr["YearId"].ToString();

                                dt.Rows.Add(drstudent);
                                id += 1;
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("[MF_Year_Processing]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Batch", Req.Batch);
                        cmd.Parameters.AddWithValue("@Year", Req.Year);
                        cmd.Parameters.AddWithValue("@Program", Req.Program);
                        cmd.Parameters.AddWithValue("@UserName", Req.UserName);
                        cmd.Parameters.AddWithValue("@ClosingBatch", Req.BatchClose);
                        cmd.Parameters.AddWithValue("@YearProcess", dt);

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

        public string FinancialYearClose(string YearCode, string Username)
        {
            string result = "";
            int resultcount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_FINANCIAL_YEAR_CLOSE]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@YearCode", YearCode);
                cmd.Parameters.AddWithValue("@UserName", Username);
                cmd.Parameters.Add("@Result", SqlDbType.Char, 200);
                cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                con.Open();
                resultcount = cmd.ExecuteNonQuery();
                if (resultcount > 0)
                {
                    result = cmd.Parameters["@Result"].Value.ToString();
                }
                else
                {
                    result = null;
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
            return result;

        }

        public string FinancialYearOpen(FYDates Dates)
        {
            string result = "";
            int resultcount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("[MF_FY_OPEN]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@St_Date", Dates.StDate );
                cmd.Parameters.AddWithValue("@End_Date", Dates.EndDate );
                cmd.Parameters.Add("@Result", SqlDbType.Char, 200);
                cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
                con.Open();
                resultcount = cmd.ExecuteNonQuery();
                if (resultcount > 0)
                {
                    result = cmd.Parameters["@Result"].Value.ToString();
                }
                else
                {
                    result = null;
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
            return result;

        }
    }


}
