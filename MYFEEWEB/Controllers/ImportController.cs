using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
//using LinqToExcel;
using System.Data.SqlClient;
using MYFEELIB.Entities;
using MYFEELIB.Data;

namespace MYFEEWEB.Controllers
{
    public class ImportController : Controller
    {
        DataSet Ds_Academic;
        //
        // GET: /Import/
        public ActionResult StudentImport()
        {
            ViewBag.Result = null;
            ViewBag.rescount = null;
            return View();
        }

        private void Grid_Dataset()
        {
            try
            {
                Ds_Academic = new DataSet();
                DataTable table = new DataTable("Student_Academic");

                table.Columns.Add("StudentId", System.Type.GetType("System.String"));
                table.Columns.Add("Qualification", System.Type.GetType("System.String"));
                table.Columns.Add("FromDate", System.Type.GetType("System.DateTime"));
                table.Columns.Add("ToDate", System.Type.GetType("System.DateTime"));
                table.Columns.Add("Percentage", System.Type.GetType("System.String"));
                table.Columns.Add("Remarks", System.Type.GetType("System.String"));
                table.Columns.Add("MaxMarks", System.Type.GetType("System.Int32"));
                table.Columns.Add("SecureMarks", System.Type.GetType("System.Int32"));
                table.Columns.Add("HallTicket", System.Type.GetType("System.String"));
                table.Columns.Add("Department", System.Type.GetType("System.String"));
                table.Columns.Add("Passedout", System.Type.GetType("System.DateTime"));
                table.Columns.Add("Max_Marks", System.Type.GetType("System.Int32"));
                table.Columns.Add("Sec_Marks", System.Type.GetType("System.Int32"));
                table.Columns.Add("Division", System.Type.GetType("System.String"));
                Ds_Academic.Tables.Add(table);

            }
            catch (Exception exception1)
            {

                Exception exception = exception1;

            }
        }

        public ActionResult StudentAcademicImport()
        {
            ViewBag.Result = null;
            ViewBag.rescount = null;
            return View();
        }

        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }


        [HttpPost]
        public ActionResult UploadExcel(Student Students, HttpPostedFileBase FileUpload)
        {
            string Restult = "";
            List<string> data = new List<string>();
            RegistrationContext sdb = new RegistrationContext();
            int rescount = 0;
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [EMPDATA$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];

                    int row = 1;
                    foreach (DataRow drData in dtable.Rows)
                    {
                        try
                        {
                            row = row + 1;
                            if (drData["RollNo"].ToString() == "" || drData["DisplayName"].ToString() == "" || drData["FatherName"].ToString() == "" || drData["MotherName"].ToString() == "" || drData["Gender"].ToString() == "" || drData["Rank"].ToString() == "" || drData["DOB"].ToString() == "" || drData["DOJ"].ToString() == "" || drData["RegistrationDate"].ToString() == "" || drData["StudentEmail"].ToString() == "" || drData["StudentPhone"].ToString() == "" || drData["ParentPhone"].ToString() == "" || drData["Nationality"].ToString() == "" || drData["Religion"].ToString() == "" || drData["PermanentAddress"].ToString() == "" || drData["PresentAddress"].ToString() == "" || drData["Aadhar_No"].ToString() == "" || drData["ID1"].ToString() == "")
                            {
                                Restult = "In Excel Row Number : " + row + " RollNo,DisplayName,FatherName,MotherName,Gender,Rank,DOB,DOJ,RegistrationDate,StudentEmail,StudentPhone,ParentPhone,Nationality,Religion,PermanentAddress,PresentAddress,Aadhar_No,ID1 are not Empty !";
                                ViewBag.Result = Restult;
                                ViewBag.rescount = 0;
                                return View("StudentImport");
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            ViewBag.Result = ex.Message.ToString();
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }



                    DataTable uniqueProgram = dtable.DefaultView.ToTable(true, "Program");
                    foreach (DataRow drProgram in uniqueProgram.Rows)
                    {
                        string value;
                        value = sdb.GetValue("tbl_fees_Program", "ProgramId", "ProgramId", drProgram["Program"].ToString());
                        if (value == null)
                        {
                            Restult = "Program : " + drProgram["Program"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }

                    DataTable uniqueCourse = dtable.DefaultView.ToTable(true, "Course");
                    foreach (DataRow drCourse in uniqueCourse.Rows)
                    {
                        string value;
                        value = sdb.GetValue("tbl_fees_Course", "CourseId", "CourseId", drCourse["Course"].ToString());
                        if (value == null)
                        {
                            Restult = "Course : " + drCourse["Course"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }

                    DataTable uniqueSection = dtable.DefaultView.ToTable(true, "Section");
                    foreach (DataRow drSection in uniqueSection.Rows)
                    {
                        string value;
                        value = sdb.GetValue("tbl_fees_Section", "SectionId", "SectionId", drSection["Section"].ToString());
                        if (value == null)
                        {
                            Restult = "Section : " + drSection["Section"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }

                    DataTable uniqueState = dtable.DefaultView.ToTable(true, "State");
                    foreach (DataRow drState in uniqueState.Rows)
                    {
                        string value;
                        value = sdb.GetValue("State", "StateCode", "StateCode", drState["State"].ToString());
                        if (value == null)
                        {
                            Restult = "State : " + drState["State"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }

                    DataTable uniqueDistrict = dtable.DefaultView.ToTable(true, "District");
                    foreach (DataRow drDistrict in uniqueDistrict.Rows)
                    {
                        string value;
                        value = sdb.GetValue("District", "DistrictCode", "DistrictCode", drDistrict["District"].ToString());
                        if (value == null)
                        {
                            Restult = "District : " + drDistrict["District"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }

                    DataTable uniqueQuota = dtable.DefaultView.ToTable(true, "Quota");
                    foreach (DataRow drQuota in uniqueQuota.Rows)
                    {
                        string value;
                        value = sdb.GetValue("tbl_fees_StudentQuota", "QuotaId", "QuotaId", drQuota["Quota"].ToString());
                        if (value == null)
                        {
                            Restult = "Quota : " + drQuota["Quota"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }


                    DataTable uniqueQuotaCategory = dtable.DefaultView.ToTable(true, "QuotaCategory");
                    foreach (DataRow drQuotaCategory in uniqueQuotaCategory.Rows)
                    {
                        string value;
                        value = sdb.GetValue("tbl_fees_StudentQuotaCategory", "QCId", "QCId", drQuotaCategory["QuotaCategory"].ToString());
                        if (value == null)
                        {
                            Restult = "QuotaCategory : " + drQuotaCategory["QuotaCategory"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }

                    DataTable uniqueCaste = dtable.DefaultView.ToTable(true, "Caste");
                    foreach (DataRow drCaste in uniqueCaste.Rows)
                    {
                        string value;
                        value = sdb.GetValue("tbl_fees_StudentCategory", "CategoryId", "CategoryId", drCaste["Caste"].ToString());
                        if (value == null)
                        {
                            Restult = "Caste : " + drCaste["Caste"].ToString() + " Not Available !";
                            ViewBag.Result = Restult;
                            ViewBag.rescount = 0;
                            return View("StudentImport");
                        }
                    }




                    List<Student> ListStudents = new List<Student>();
                    foreach (DataRow dr in dtable.Rows)
                    {
                        if (dr["Entry"].ToString() == "1")
                        {
                            Student ST = new Student()
                            {
                                RollNo = dr["RollNo"].ToString(),
                                FirstName = dr["FirstName"].ToString(),
                                MiddleName = dr["MiddleName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                FullName = dr["DisplayName"].ToString(),
                                Gender = dr["Gender"].ToString(),

                                DateOfBirth = Convert.ToDateTime(dr["DOB"]),
                                DateOfJoining = Convert.ToDateTime(dr["DOJ"]),
                                RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"]),

                                FatherName = dr["FatherName"].ToString(),
                                MotherName = dr["MotherName"].ToString(),
                                EamcetRank = Convert.ToInt32(dr["Rank"]),
                                S_MobileNo = dr["StudentPhone"].ToString(),
                                P_MobileNo = dr["ParentPhone"].ToString(),
                                EMail = dr["StudentEmail"].ToString(),
                                ParentEMail = dr["ParentEmail"].ToString(),
                                Nationality = dr["Nationality"].ToString(),
                                Religion = dr["Religion"].ToString(),
                                Quota = dr["Quota"].ToString(),
                                QuotaCategory = dr["QuotaCategory"].ToString(),
                                Entry = dr["Entry"].ToString(),
                                Batch = dr["Batch"].ToString(),
                                Year = Convert.ToInt32(dr["Year"]),
                                Program = dr["Program"].ToString(),
                                Course = dr["Course"].ToString(),
                                Section = dr["Section"].ToString(),
                                Category = dr["Caste"].ToString(),
                                SpecialCategory = dr["SpecialCategory"].ToString(),
                                AdhaarCardNo = dr["Aadhar_No"].ToString(),
                                State = dr["State"].ToString(),
                                District = dr["District"].ToString(),
                                IdentificationMark1 = dr["ID1"].ToString(),
                                IdentificationMark2 = dr["ID2"].ToString(),
                                PresentAddress = dr["PresentAddress"].ToString(),
                                PermenantAddress = dr["PermanentAddress"].ToString()

                            };
                            ListStudents.Add(ST);

                        }
                        else
                        {
                            Student ST = new Student()
                           {
                               RollNo = dr["RollNo"].ToString(),
                               FirstName = dr["FirstName"].ToString(),
                               MiddleName = dr["MiddleName"].ToString(),
                               LastName = dr["LastName"].ToString(),
                               FullName = dr["DisplayName"].ToString(),
                               Gender = dr["Gender"].ToString(),

                               DateOfBirth = Convert.ToDateTime(dr["DOB"]),
                               DateOfJoining = Convert.ToDateTime(dr["DOJ"]),
                               RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"]),


                               FatherName = dr["FatherName"].ToString(),
                               MotherName = dr["MotherName"].ToString(),
                               EamcetRank = Convert.ToInt32(dr["Rank"]),
                               S_MobileNo = dr["StudentPhone"].ToString(),
                               P_MobileNo = dr["ParentPhone"].ToString(),
                               EMail = dr["StudentEmail"].ToString(),
                               ParentEMail = dr["ParentEmail"].ToString(),
                               Nationality = dr["Nationality"].ToString(),
                               Religion = dr["Religion"].ToString(),
                               Quota = dr["Quota"].ToString(),
                               QuotaCategory = dr["QuotaCategory"].ToString(),
                               Entry = dr["Entry"].ToString(),
                               Batch = dr["Batch"].ToString(),
                               Year = Convert.ToInt32(dr["Year"]),
                               Program = dr["Program"].ToString(),
                               Course = dr["Course"].ToString(),
                               Section = dr["Section"].ToString(),
                               Category = dr["Caste"].ToString(),
                               SpecialCategory = dr["SpecialCategory"].ToString(),
                               AdhaarCardNo = dr["Aadhar_No"].ToString(),
                               State = dr["State"].ToString(),
                               District = dr["District"].ToString(),
                               IdentificationMark1 = dr["ID1"].ToString(),
                               IdentificationMark2 = dr["ID2"].ToString(),
                               PresentAddress = dr["PresentAddress"].ToString(),
                               PermenantAddress = dr["PermanentAddress"].ToString(),
                               UniversityOrderNo = dr["UniversityOrderNo"].ToString(),
                               UniversityLastQualifiedTCNo = dr["LastQualifiedTCNo"].ToString(),
                               TypeOfSecondaryEducation = dr["TypeOfSecondaryEducation"].ToString(),
                               UniversityOrderIssuedDate = Convert.ToDateTime(dr["UniversityOrderIssuedDate"]),
                               UniversityLastQualifiedTCIssuedDate = Convert.ToDateTime(dr["LastQualifiedTCIssuedDate"])
                           };
                            ListStudents.Add(ST);
                        }
                    }

                    rescount = sdb.StudentImport(ListStudents);
                    int insertedRecords = rescount;
                    Restult = insertedRecords.ToString() + " Students Imported .";
                    ViewBag.Result = Restult;
                    ViewBag.rescount = rescount;
                    return View("StudentImport");

                }
                else
                {

                    Restult = "Only Excel file format is allowed.";
                    ViewBag.Result = Restult;
                    ViewBag.rescount = 0;
                    return View("StudentImport");
                }
            }
            else
            {

                Restult = "Please choose Excel file.";
                ViewBag.Result = Restult;
                ViewBag.rescount = 0;
                return View("StudentImport");
            }
        }

        [HttpPost]
        public ActionResult UploadAcademic(Student Students, HttpPostedFileBase FileUpload)
        {
            string Restult = "";
            List<string> data = new List<string>();
            RegistrationContext sdb = new RegistrationContext();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [DATA$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];

                    int row = 1;
                    foreach (DataRow drData in dtable.Rows)
                    {
                        try
                        {
                            row = row + 1;
                            if (drData["RollNo"].ToString() == "" || drData["Qualification"].ToString() == "" || drData["Passedout"].ToString() == "" || drData["HallTicket"].ToString() == "" || drData["Department"].ToString() == "" || drData["MaxMarks"].ToString() == "" || drData["SecureMarks"].ToString() == "" || drData["Division"].ToString() == "")
                            {
                                Restult = "In Excel Row Number : " + row + " RollNo,Qualification,Passedout,HallTicket,Department,MaxMarks,SecureMarks,Division are not Empty !";
                                ViewBag.Result = Restult;
                                ViewBag.rescount = 0;
                                return View("StudentImport");
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            ViewBag.Result = ex.Message.ToString();
                            ViewBag.rescount = 0;
                            return View("StudentAcademicImport");
                        }
                    }

                    Grid_Dataset();

                    foreach (DataRow dr in dtable.Rows)
                    {
                        DataRow acrow = Ds_Academic.Tables["Student_Academic"].NewRow();

                        string StudId = sdb.GetValue("tbl_fees_StudentEnrollment", "StudentId", "RollNo", dr["RollNo"].ToString());

                        acrow["StudentId"] = StudId;
                        acrow["Qualification"] = dr["Qualification"].ToString();
                        acrow["FromDate"] = Convert.ToDateTime(dr["Passedout"]);
                        acrow["ToDate"] = Convert.ToDateTime(dr["Passedout"]);
                        acrow["Percentage"] = 0;
                        acrow["Remarks"] = "";
                        acrow["MaxMarks"] = Convert.ToInt32(dr["MaxMarks"]);
                        acrow["SecureMarks"] = Convert.ToInt32(dr["SecureMarks"]);
                        acrow["HallTicket"] = dr["HallTicket"].ToString();
                        acrow["Department"] = dr["Department"].ToString();
                        acrow["Passedout"] = Convert.ToDateTime(dr["Passedout"]);
                        acrow["Max_Marks"] = Convert.ToInt32(dr["MaxMarks"]);
                        acrow["Sec_Marks"] = Convert.ToInt32(dr["SecureMarks"]);
                        acrow["Division"] = dr["Division"].ToString();
                        Ds_Academic.Tables["Student_Academic"].Rows.Add(acrow);
                    }
                    int num = sdb.BulkRecordInsert(Ds_Academic.Tables["Student_Academic"], "tbl_fees_StudentAcademic");
                    int insertedRecords = num;
                    Restult = insertedRecords.ToString() + " Students Academic Details Imported .";
                    ViewBag.Result = Restult;
                    ViewBag.rescount = insertedRecords;
                    return View("StudentAcademicImport");
                }
                else
                {

                    Restult = "Only Excel file format is allowed.";
                    ViewBag.Result = Restult;
                    ViewBag.rescount = 0;
                    return View("StudentImport");
                }
            }
            else
            {

                Restult = "Please choose Excel file.";
                ViewBag.Result = Restult;
                ViewBag.rescount = 0;
                return View("StudentImport");
            }
        }
    }
}