using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYFEELIB.Entities;
using MYFEELIB.Domain;
using MYFEELIB.Data;
using MYFEEWEB.Models;
using System.Data;
using System.Data.SqlClient;
using WebApplication.Filters;
using System.Text.RegularExpressions;
using System.Globalization;

namespace MYFEEWEB.Controllers
{
    [SessionTimeout]
    public class AcademicController : Controller
    {
        string sSql;
        UserContext udb = new UserContext();
        AccountService service = new AccountService();
        RequireElements req = new RequireElements();
        AcademicContext Ac = new AcademicContext();
        Challan Chall = new Challan();
        public DataSet Ds_GridRec;
        //
        // GET: /Academic/
        [HttpGet]

        public ActionResult FeeRefund()
        {
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }
            var model = new FinancialYearDates();
            var status = GetRefundStatus();
            model.Statuses = GetSelectListItems(status);

            return View(model);
        }

        [HttpGet]
        public ActionResult AcademicChallan()
        {

            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["FD"] = ds.Tables[0].Rows[0][1];
                    Session["ED"] = ds.Tables[0].Rows[0][2];
                }
            }

            var model = new Challan();
            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Year = GetYear("");
            model.Years = GetSelectListItems(Year);
            var Paymode = service.GetPayModeDetails();
            model.PayModes = GetSelectListItems(Paymode);

            return View(model);


        }


        public JsonResult InsertAcademicChallan(IEnumerable<Challan> StudentDue)
        {
            List<Challan> listChallan = new List<Challan>();
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (StudentDue != null)
            {

                rescount = sdb.AcadimicChallanSave(StudentDue);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }




        public JsonResult StudentChallanDetails(IEnumerable<Challan> Params)
        {

            AcademicContext sdb = new AcademicContext();
            Challan req = new Challan();
            foreach (var Acd in Params)
            {
                req.RollNo = Acd.RollNo;
                req.Year = Acd.Year;

            }

            var FillDue = sdb.GetChallanDetails(req);
            //Session["username"] = null;
            if (FillDue.Count == 0)
            {
                FillDue = null;
                return Json(FillDue, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillDue.ToArray(), JsonRequestBehavior.AllowGet);

        }




        public List<ListItem> GetYear(string PId)
        {
            List<ListItem> listYear = new List<ListItem>();

            if (PId == "ET")
            {
                listYear.Insert(0, new ListItem()
                {
                    Value = null,
                    Text = "SELECT YEAR"
                });
                listYear.Insert(1, new ListItem()
                {
                    Value = "1",
                    Text = "1 Year "
                });

                listYear.Insert(2, new ListItem()
                {
                    Value = "2",
                    Text = "2 Year"
                });

                listYear.Insert(3, new ListItem()
                {
                    Value = "3",
                    Text = "3 Year"
                });

                listYear.Insert(4, new ListItem()
                {
                    Value = "4",
                    Text = "4 Year"
                });
            }
            else if (PId == "ME")
            {
                listYear.Insert(0, new ListItem()
                {
                    Value = null,
                    Text = "SELECT YEAR"
                });
                listYear.Insert(1, new ListItem()
                {
                    Value = "1",
                    Text = "1 Year "
                });

                listYear.Insert(2, new ListItem()
                {
                    Value = "2",
                    Text = "2 Year"
                });


            }
            else
            {

                //listYear.Insert(0, new ListItem()
                //{
                //    Value = null,
                //    Text = "SELECT YEAR"
                //});
            }



            return listYear;
        }

        public JsonResult YearList(string PId)
        {
            try
            {
                var yr = GetYear(PId);
                Chall.Years = GetSelectListItems(yr);
                return Json(new SelectList(yr.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public JsonResult YearsList(string PId)
        {
            try
            {
                var yr = GetYears(PId);
                Chall.Years = GetSelectListItems(yr);
                return Json(new SelectList(yr.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }






        [HttpGet]
        public ActionResult MassUpdate()
        {

            Session["Header"] = Request.QueryString["UT"];

            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }

            var model = new RequireElements();
            var mainBatch = GetBatches();
            model.Batches = GetSelectListItems(mainBatch);
            var mainProgram = GetPrograms();

            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            model.Programs = GetSelectListItems(mainProgram);
            var Course = service.GetCourseDetails("");
            model.Courses = GetSelectListItems(Course);


            var status = GetStatus();
            model.Statuses = GetSelectListItems(status);

            var Due = GetDueFrom();
            model.DuesFrom = GetSelectListItems(Due);

            var Paymode = service.GetPayModeDetails();
            model.PayModes = GetSelectListItems(Paymode);




            return View(model);

        }

        public ActionResult SessionalMarks()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new RequireElements();



            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            var Semester = service.GetSemesterDetails("");
            model.Semesters = GetSelectListItems(Semester);




            return View(model);
        }

        public ActionResult ExternalMarks()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new RequireElements();



            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            var Semester = service.GetSemesterDetails("");
            model.Semesters = GetSelectListItems(Semester);




            return View(model);
        }

        //public JsonResult SaveExternalMarks(IEnumerable<Marks> StudentMarks)
        //{
        //    DataSet Arr;
        //    string markssec;
        //    List<Student_Detain_Discontinue> Rec = new List<Student_Detain_Discontinue>();
        //    UserContext sdb = new UserContext();

        //    int rescount = 0;
        //    int insertedRecords = 0;
        //    if (StudentMarks != null)
        //    {
        //        foreach (var Acd in StudentMarks)
        //        {
        //            if (Acd.SessMarksSecured == "")
        //            {
        //                markssec = "ABS";
        //            }
        //            else
        //            {
        //                markssec = Acd.SessMarksSecured;
        //            }
        //            sSql = "Select [MaxMarks],[MinMarks],[Sess_MaxMarks],[Sess_MinMarks],[SId] from [tbl_fees_SubjectMasterDetails] where YearId='" + Acd.Year + "' and Semester='" + Acd.Semester + "' and CourseId='" + Acd.Course + "' and SId=" + Acd.SId + "";
        //            Arr = sdb.GetData(sSql, "MarksValue");
        //            string Var_SId;
        //            string SId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + Acd.RollNo + "'");
        //            sSql = "Select id from tbl_fees_StudentMarks where StudentId='" + SId + "' and YearId=" + Acd.Year + " and SemId='" + Acd.Semester + "' and SId=" + Acd.SId + "";
        //            Var_SId = sdb.Select_Values(sSql);
        //            if (Var_SId.ToString() == "0")
        //            {
        //                sSql = "Insert into [tbl_fees_StudentMarks]([Studentid],[SubjectName] ,[ExamMaxMrks],[ExamMinMrks],[SessMaxMrks] ,[SessMinMrks],[SemId],[ExamMarksSecured],[SessMarksSecured] ,[Result],[YearId],[EntryBy],[EntryDate],[SId])values( '" + SId + "','" + Acd.SubjectName + "','" + Arr.Tables["MarksValue"].Rows[0]["MaxMarks"].ToString() + "','" + Arr.Tables["MarksValue"].Rows[0]["MinMarks"].ToString() + "','" + Acd.SessMaxMrks + "','" + Acd.SessMinMrks + "','" + Acd.Semester + "','','" + markssec + "','','" + Acd.Year + "','" + Acd.EnterBy + "','" + DateTime.Now + "'," + Acd.SId + ")";
        //                sdb.Save_Record(sSql);
        //                rescount = rescount + 1;
        //            }
        //            else
        //            {
        //                sSql = "Update [tbl_fees_StudentMarks] set [SessMarksSecured]='" + markssec + "' where id=" + Var_SId + " ";
        //                sdb.Save_Record(sSql);
        //                rescount = rescount + 1;
        //            }

        //        }
        //    }
        //    insertedRecords = rescount;
        //    return Json(insertedRecords);
        //}

        public JsonResult SaveSessionalMarks(IEnumerable<Marks> StudentMarks)
        {
            DataSet Arr;
            string markssec;
            List<Student_Detain_Discontinue> Rec = new List<Student_Detain_Discontinue>();
            UserContext sdb = new UserContext();

            int rescount = 0;
            int insertedRecords = 0;
            if (StudentMarks != null)
            {
                foreach (var Acd in StudentMarks)
                {
                    if (Acd.SessMarksSecured == "")
                    {
                        markssec = "ABS";
                    }
                    else
                    {
                        markssec = Acd.SessMarksSecured;
                    }
                    sSql = "Select [MaxMarks],[MinMarks],[Sess_MaxMarks],[Sess_MinMarks],[SId] from [tbl_fees_SubjectMasterDetails] where YearId='" + Acd.Year + "' and Semester='" + Acd.Semester + "' and CourseId='" + Acd.Course + "' and SId=" + Acd.SId + "";
                    Arr = sdb.GetData(sSql, "MarksValue");
                    string Var_SId;
                    string SId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + Acd.RollNo + "'");
                    sSql = "Select id from tbl_fees_StudentInternalMarks where StudentId='" + SId + "' and YearId=" + Acd.Year + " and SemId='" + Acd.Semester + "' and SId=" + Acd.SId + "";
                    Var_SId = sdb.Select_Values(sSql);
                    if (Var_SId.ToString() == "0")
                    {
                        string Batch = sdb.Select_Values("Select BatchId from tbl_fees_StudentEnrollment where StudentId='" + SId + "'");
                        //sSql = "Insert into [tbl_fees_StudentMarks]([Studentid],[SubjectName] ,[ExamMaxMrks],[ExamMinMrks],[SessMaxMrks] ,[SessMinMrks],[SemId],[ExamMarksSecured],[SessMarksSecured] ,[Result],[YearId],[EntryBy],[EntryDate],[SId])values( '" + SId + "','" + Acd.SubjectName + "','" + Arr.Tables["MarksValue"].Rows[0]["MaxMarks"].ToString() + "','" + Arr.Tables["MarksValue"].Rows[0]["MinMarks"].ToString() + "','" + Acd.SessMaxMrks + "','" + Acd.SessMinMrks + "','" + Acd.Semester + "','','" + markssec + "','','" + Acd.Year + "','" + Acd.EnterBy + "','" + DateTime.Now + "'," + Acd.SId + ")";
                        sSql = "Insert into [tbl_fees_StudentInternalMarks]([Studentid], [SessMaxMrks] ,[SessMinMrks],[SemId],[SessMarksSecured1],[SessMarksSecured2],[SessMarksSecured3],[Result],[YearId],[EntryBy],[EntryDate],[SId],[BatchId])values( '" + SId + "','" + Acd.SessMaxMrks + "','" + Acd.SessMinMrks + "','" + Acd.Semester + "','" + Acd.SessMarksSecured1 + "','" + Acd.SessMarksSecured2 + "','" + Acd.SessMarksSecured3 + "','" + Acd.Result + "','" + Acd.Year + "','" + Acd.EnterBy + "','" + DateTime.Now + "'," + Acd.SId + ",'" + Batch + "')";
                        sdb.Save_Record(sSql);
                        rescount = rescount + 1;
                    }
                    else
                    {
                        sSql = "Update [tbl_fees_StudentInternalMarks] set [SessMarksSecured1]='" + Acd.SessMarksSecured1 + "',[SessMarksSecured2]='" + Acd.SessMarksSecured2 + "',[SessMarksSecured3]='" + Acd.SessMarksSecured3 + "',[Result]='" + Acd.Result + "' where id=" + Var_SId + " ";
                        sdb.Save_Record(sSql);
                        rescount = rescount + 1;
                    }

                }
            }
            insertedRecords = rescount;
            return Json(insertedRecords);
        }

        public JsonResult SaveExternalMarks(IEnumerable<Marks> StudentMarks)
        {
            DataSet Arr;
            string markssec;
            string sesssecure;
            List<Student_Detain_Discontinue> Rec = new List<Student_Detain_Discontinue>();
            UserContext sdb = new UserContext();

            int rescount = 0;
            int insertedRecords = 0;
            if (StudentMarks != null)
            {
                foreach (var Acd in StudentMarks)
                {
                    if (Acd.SessMarksSecured == "")
                    {
                        sesssecure = "ABS";
                    }
                    else
                    {
                        sesssecure = Acd.SessMarksSecured;
                    }

                    if (Acd.ExamMarksSecured == "")
                    {
                        markssec = "ABS";
                    }
                    else
                    {
                        markssec = Acd.ExamMarksSecured;
                    }

                    sSql = "Select [MaxMarks],[MinMarks],[Sess_MaxMarks],[Sess_MinMarks],[SId] from [tbl_fees_SubjectMasterDetails] where YearId='" + Acd.Year + "' and Semester='" + Acd.Semester + "' and CourseId='" + Acd.Course + "' and SId=" + Acd.SId + "";
                    Arr = sdb.GetData(sSql, "MarksValue");
                    string Var_SId;
                    string SId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + Acd.RollNo + "'");
                    sSql = "Select id from tbl_fees_StudentMarks where StudentId='" + SId + "' and YearId=" + Acd.Year + " and SemId='" + Acd.Semester + "' and SId=" + Acd.SId + "";
                    Var_SId = sdb.Select_Values(sSql);
                    if (Var_SId.ToString() == "0")
                    {
                        sSql = "Insert into [tbl_fees_StudentMarks]([Studentid],[SubjectName] ,[ExamMaxMrks],[ExamMinMrks],[SessMaxMrks] ,[SessMinMrks],[SemId],[ExamMarksSecured],[SessMarksSecured] ,[Result],[YearId],[EntryBy],[EntryDate],[SId])values( '" + SId + "','" + Acd.SubjectName + "','" + Arr.Tables["MarksValue"].Rows[0]["MaxMarks"].ToString() + "','" + Arr.Tables["MarksValue"].Rows[0]["MinMarks"].ToString() + "','" + Acd.SessMaxMrks + "','" + Acd.SessMinMrks + "','" + Acd.Semester + "','" + markssec + "','" + sesssecure + "','" + Acd.Result + "','" + Acd.Year + "','" + Acd.EnterBy + "','" + DateTime.Now + "'," + Acd.SId + ")";
                        sdb.Save_Record(sSql);
                        rescount = rescount + 1;
                    }
                    else
                    {
                        sSql = "Update [tbl_fees_StudentMarks] set [SessMarksSecured]='" + sesssecure + "',[ExamMarksSecured]='" + markssec + "',[Result]='" + Acd.Result + "' where id=" + Var_SId + " ";
                        sdb.Save_Record(sSql);
                        rescount = rescount + 1;
                    }

                }
            }
            insertedRecords = rescount;
            return Json(insertedRecords);
        }

        public ActionResult TimeTable()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new RequireElements();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);
            var Course = service.GetCourseDetails("");
            model.Courses = GetSelectListItems(Course);

            var Section = service.GetSectionDetails("");
            model.Sections = GetSelectListItems(Course);

            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            var Semester = service.GetSemesterDetails("");
            model.Semesters = GetSelectListItems(Semester);

            var employees = service.GetEmployees("All");
            model.Faculties1 = GetSelectListItems(employees);


            model.Faculties2 = GetSelectListItems(employees);

            var subjects = service.Getsubjects("", "", 0, "");
            model.subjects = GetSelectListItems(subjects);

            var Weeks = service.GetWeeks();
            model.weeks = GetSelectListItems(Weeks);


            return View(model);
        }

        [HttpGet]
        public JsonResult TimeTableRecords(string Semester)
        {
            List<TimeTable> ObjTime = new List<TimeTable>();
            UserContext sdb = new UserContext();
            DataSet dsTimeTable = new DataSet();
            //Creating List 
            string sSql;
            string[] Vals = Regex.Split(Semester, @"/");
            if (Vals != null)
            {
                sSql = "select TId,TT.SId,WId,TT.EmployeeCode,EM.DisplayName,WId,WeekName,SM.SubjectCode,SM.SubjectName,TT.PId,TT.IsGroupBased from tbl_fees_TimeTable TT,tbl_fees_WeekMaster WM,MYHR.dbo.EmployeeMaster EM,tbl_fees_SubjectMasterDetails SM  where TT.EmployeeCode COLLATE DATABASE_DEFAULT =EM.EmployeeCode COLLATE DATABASE_DEFAULT  and  TT.WId =WM.Id and TT.SId =SM.SId and TT.ProgramId ='" + Vals[0].ToString() + "' and TT.CourseId ='" + Vals[1].ToString() + "' and TT.SectionId ='" + Vals[2].ToString() + "' and TT.YearId=" + Vals[3].ToString() + " and TT.id='" + Vals[4].ToString() + "' order by TT.WId,TT.PId  ";
                dsTimeTable = sdb.GetData(sSql, "TimeTable");

            }

            DataTable TotalTable = new DataTable();
            DataTable tbl_Result = new DataTable();
            TotalTable = dsTimeTable.Tables["TimeTable"].DefaultView.ToTable();
            if (TotalTable.Rows.Count > 0)
            {

                for (var W = 1; W <= 6; W++)
                {
                    TotalTable.DefaultView.RowFilter = "WId = " + W + "";
                    tbl_Result = TotalTable.DefaultView.ToTable();

                    DataTable tbl_Period = new DataTable();
                    TimeTable data = new TimeTable();
                    data.SNo = tbl_Result.Rows[0]["WeekName"].ToString();

                    for (var P = 1; P <= 7; P++)
                    {
                        tbl_Period = null;
                        tbl_Result.DefaultView.RowFilter = "PId = " + P + "";
                        tbl_Period = tbl_Result.DefaultView.ToTable();
                        //  data.TId = tbl_Period.Rows[0]["TId"].ToString();
                        if (tbl_Period.Rows.Count > 0)
                        {
                            // data.TId = tbl_Period.Rows[0]["TId"].ToString();
                            if (P == 1)
                            {

                                data.One = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();
                            }
                            else if (P == 2)
                            {
                                data.Two = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();

                            }
                            else if (P == 3)
                            {
                                data.three = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();

                            }
                            else if (P == 4)
                            {
                                data.Four = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();

                            }
                            else if (P == 5)
                            {
                                data.Five = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();

                            }
                            else if (P == 6)
                            {
                                data.Six = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();

                            }
                            else if (P == 7)
                            {
                                data.Seven = tbl_Period.Rows[0]["TId"].ToString() + "/" + tbl_Period.Rows[0]["DisplayName"].ToString() + "/" + tbl_Result.Rows[0]["SubjectName"].ToString();

                            }
                        }
                        else
                        {
                            if (P == 1)
                            {
                                data.One = W + "-" + P + "/ / ";
                            }
                            else if (P == 2)
                            {

                                data.Two = W + "-" + P + "/ / ";

                            }
                            else if (P == 3)
                            {
                                data.three = W + "-" + P + "/ / ";

                            }
                            else if (P == 4)
                            {
                                data.Four = W + "-" + P + "/ / ";

                            }
                            else if (P == 5)
                            {
                                data.Five = W + "-" + P + "/ / ";

                            }
                            else if (P == 6)
                            {
                                data.Six = W + "-" + P + "/ / ";

                            }
                            else if (P == 7)
                            {
                                data.Seven = W + "-" + P + "/ / ";

                            }
                        }


                    }
                    ObjTime.Add(data);
                }
            }
            return Json(ObjTime, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExternalMarksRecords(string Semester)
        {
            List<Marks> ObjMarks = new List<Marks>();
            UserContext sdb = new UserContext();
            DataSet dsMarks = new DataSet();
            string sSql;
            string[] Vals = Regex.Split(Semester, @"/");
            string Var_course = sdb.Select_Values("Select CourseId from tbl_fees_StudentEnrollment where RollNo='" + Vals[0].ToString() + "'");
            if (Vals != null)
            {
                sSql = "Select [SubjectName],[MaxMarks],[MinMarks],[Sess_MaxMarks],[Sess_MinMarks],[SId] from [tbl_fees_SubjectMasterDetails] where YearId='" + Vals[1].ToString() + "' and Semester='" + Vals[2].ToString() + "' and CourseId='" + Var_course + "'";
                dsMarks = sdb.GetData(sSql, "Marks");
                if (dsMarks.Tables["Marks"].Rows.Count > 0)
                {
                    for (var i = 0; i <= dsMarks.Tables["Marks"].Rows.Count - 1; i++)
                    {
                        Marks data = new Marks();
                        data.SubjectName = dsMarks.Tables["Marks"].Rows[i]["SubjectName"].ToString();
                        data.SessMaxMrks = dsMarks.Tables["Marks"].Rows[i]["Sess_MaxMarks"].ToString();
                        data.SessMinMrks = dsMarks.Tables["Marks"].Rows[i]["Sess_MinMarks"].ToString();
                        data.ExamMaxMrks = dsMarks.Tables["Marks"].Rows[i]["MaxMarks"].ToString();
                        data.ExamMinMrks = dsMarks.Tables["Marks"].Rows[i]["MinMarks"].ToString();
                        data.SId = dsMarks.Tables["Marks"].Rows[i]["SId"].ToString();
                        string SId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + Vals[0].ToString() + "'");
                        string Var_Session_Secured = sdb.Select_Values("select Result from tbl_fees_StudentInternalMarks where Studentid='" + SId + "' and SId=" + dsMarks.Tables["Marks"].Rows[i]["SId"].ToString() + "");
                        if (Var_Session_Secured == "ABS")
                        {
                            data.SessMarksSecured = "";
                        }
                        else
                        {
                            data.SessMarksSecured = Var_Session_Secured;
                        }
                        ObjMarks.Add(data);
                    }
                }
            }
            return Json(ObjMarks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SessionalMarksRecords(string Semester)
        {
            List<Marks> ObjMarks = new List<Marks>();
            UserContext sdb = new UserContext();
            DataSet dsMarks = new DataSet();



            string sSql;
            string[] Vals = Regex.Split(Semester, @"/");
            string Var_course = sdb.Select_Values("Select CourseId from tbl_fees_StudentEnrollment where RollNo='" + Vals[0].ToString() + "'");

            if (Vals != null)
            {
                sSql = "Select [SubjectName],[Sess_MaxMarks],[Sess_MinMarks],[SId] from [tbl_fees_SubjectMasterDetails] where YearId='" + Vals[1].ToString() + "' and Semester='" + Vals[2].ToString() + "' and CourseId='" + Var_course + "'";

                dsMarks = sdb.GetData(sSql, "Marks");
                if (dsMarks.Tables["Marks"].Rows.Count > 0)
                {
                    for (var i = 0; i <= dsMarks.Tables["Marks"].Rows.Count - 1; i++)
                    {
                        Marks data = new Marks();
                        data.SubjectName = dsMarks.Tables["Marks"].Rows[i]["SubjectName"].ToString();
                        data.SessMaxMrks = dsMarks.Tables["Marks"].Rows[i]["Sess_MaxMarks"].ToString();
                        data.SessMinMrks = dsMarks.Tables["Marks"].Rows[i]["Sess_MinMarks"].ToString();
                        data.SId = dsMarks.Tables["Marks"].Rows[i]["SId"].ToString();
                        string SId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + Vals[0].ToString() + "'");
                        DataSet dsExist = sdb.GetData("select SessMarksSecured1,SessMarksSecured2,SessMarksSecured3,Result from tbl_fees_StudentInternalMarks where Studentid='" + SId + "' and SId=" + dsMarks.Tables["Marks"].Rows[i]["SId"].ToString() + "", "Exist");
                        if (dsExist != null)
                        {
                            if (dsExist.Tables["Exist"].Rows.Count > 0)
                            {
                                data.SessMarksSecured1 = dsExist.Tables["Exist"].Rows[0]["SessMarksSecured1"].ToString();
                                data.SessMarksSecured2 = dsExist.Tables["Exist"].Rows[0]["SessMarksSecured2"].ToString();
                                data.SessMarksSecured3 = dsExist.Tables["Exist"].Rows[0]["SessMarksSecured3"].ToString();
                                data.Result = dsExist.Tables["Exist"].Rows[0]["Result"].ToString();
                            }
                            else
                            {
                                data.SessMarksSecured1 = "";
                                data.SessMarksSecured2 = "";
                                data.SessMarksSecured3 = "";
                                data.Result = "";
                            }
                        }
                        else
                        {
                            data.SessMarksSecured1 = "";
                            data.SessMarksSecured2 = "";
                            data.SessMarksSecured3 = "";
                            data.Result = "";

                        }

                        ObjMarks.Add(data);
                    }
                }
            }
            return Json(ObjMarks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillSubject(string Prm)
        {
            try
            {
                string[] Vals = Regex.Split(Prm, @"/");
                if (Vals != null)
                {
                    var Subject = service.Getsubjects(Vals[0].ToString(), Vals[1].ToString(), Convert.ToInt32(Vals[3]), Vals[4].ToString());
                    return Json(new SelectList(Subject.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public ActionResult Fill_Attendance_Subject(string Prm)
        {
            try
            {
                string[] Vals = Regex.Split(Prm, @"/");
                if (Vals != null)
                {
                    var Subject = service.Get_Attedance_subjects(Vals[0].ToString(), Vals[1].ToString(), Convert.ToInt32(Vals[2]));
                    return Json(new SelectList(Subject.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public ActionResult Fill_Attendance_Period(string Prm)
        {
            try
            {
                UserContext sdb = new UserContext();
                string[] Vals = Regex.Split(Prm, @"/");
                if (Vals != null)
                {
                    string sid = sdb.Select_Values("Select SId from tbl_fees_TimeTable where TId='" + Vals[0] + "'");
                    var Subject = service.Get_Attedance_Peiod(Convert.ToInt32(sid), Vals[1].ToString(), Convert.ToInt32(Vals[2]), Vals[3].ToString());
                    return Json(new SelectList(Subject.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public ActionResult Fill_Attendance_Employee(string Prm)
        {
            try
            {
                UserContext sdb = new UserContext();
                string[] Vals = Regex.Split(Prm, @"/");
                if (Vals != null)
                {
                    string sid = sdb.Select_Values("Select SId from tbl_fees_TimeTable where TId='" + Vals[0] + "'");
                    var Subject = service.Get_Attedance_Employee(Convert.ToInt32(sid), Vals[1].ToString(), Convert.ToInt32(Vals[2]), Vals[3].ToString(), Vals[4].ToString(), Convert.ToInt32(Vals[5]));
                    return Json(new SelectList(Subject.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public JsonResult AttendanceRecords(string Prm)
        {
            List<Attendance> Objattendance = new List<Attendance>();
            UserContext sdb = new UserContext();
            DataSet dsattend = new DataSet();



            string sSql;
            string[] Vals = Regex.Split(Prm, @"/");
            string Var_course = sdb.Select_Values("Select CourseId from tbl_fees_StudentEnrollment where RollNo='" + Vals[0].ToString() + "'");

            if (Vals != null)
            {
                string sid = sdb.Select_Values("Select SId from tbl_fees_TimeTable where TId='" + Vals[0] + "'");
                sSql = "select TM.TId,TM.SId,TM.ProgramId,TM.YearId,SM.SemesterName ,TM.CourseId,TM.SectionId,IsGroupBased,GId,PM.PName ,SMD.SubjectCode,WM.WeekName,TM.NOH  from tbl_fees_TimeTable TM,tbl_fees_WeekMaster WM,tbl_fees_PeriodMaster PM,tbl_fees_Semester_master SM,tbl_fees_SubjectMasterDetails SMD where TM.WId=WM.Id and PM.PId=TM.PId and TM.id=SM.SemesterId and TM.SId=SMD.SId   and EmployeeCode='" + Vals[6].ToString() + "' and TM.SId=" + Convert.ToInt32(sid) + " and TM.PId=" + Convert.ToInt32(Vals[5].ToString()) + " and TM.WId=" + Convert.ToInt32(Vals[2]) + " union ALL select TM.TId,TM.SId,TM.ProgramId,TM.YearId,SM.SemesterName ,TM.CourseId,TM.SectionId,IsGroupBased,GId,PM.PName ,SMD.SubjectCode,WM.WeekName,TM.NOH  from tbl_fees_TimeTable TM,tbl_fees_WeekMaster WM,tbl_fees_PeriodMaster PM,tbl_fees_Semester_master SM,tbl_fees_SubjectMasterDetails SMD,tbl_fees_TimeTableFaculty TTF where TM.TId=TTF.TId and TM.WId=WM.Id and PM.PId=TM.PId and TM.id=SM.SemesterId and TM.SId=SMD.SId   and TTF.EmployeeCode='" + Vals[6].ToString() + "' and TM.SId=" + Convert.ToInt32(sid) + " and TM.PId=" + Convert.ToInt32(Vals[5].ToString()) + " and TM.WId=" + Convert.ToInt32(Vals[2]) + "";

                dsattend = sdb.GetData(sSql, "attendance");
                if (dsattend.Tables["attendance"].Rows.Count > 0)
                {
                    for (var i = 0; i <= dsattend.Tables["attendance"].Rows.Count - 1; i++)
                    {
                        Attendance data = new Attendance();
                        data.TId = dsattend.Tables["attendance"].Rows[i]["TId"].ToString();
                        data.SId = dsattend.Tables["attendance"].Rows[i]["SId"].ToString();
                        data.ProgramId = dsattend.Tables["attendance"].Rows[i]["ProgramId"].ToString();
                        data.YearId = dsattend.Tables["attendance"].Rows[i]["YearId"].ToString();
                        data.SemesterName = dsattend.Tables["attendance"].Rows[i]["SemesterName"].ToString();
                        data.CourseId = dsattend.Tables["attendance"].Rows[i]["CourseId"].ToString();
                        data.SectionId = dsattend.Tables["attendance"].Rows[i]["SectionId"].ToString();
                        data.IsGroupBased = dsattend.Tables["attendance"].Rows[i]["IsGroupBased"].ToString();
                        data.GId = dsattend.Tables["attendance"].Rows[i]["GId"].ToString();
                        data.PName = dsattend.Tables["attendance"].Rows[i]["PName"].ToString();
                        data.SubjectCode = dsattend.Tables["attendance"].Rows[i]["SubjectCode"].ToString();
                        data.WeekName = dsattend.Tables["attendance"].Rows[i]["WeekName"].ToString();
                        data.NOH = dsattend.Tables["attendance"].Rows[i]["NOH"].ToString();
                        Objattendance.Add(data);
                    }
                }
            }
            return Json(Objattendance, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AttendanceStudentRecords(string Prm)
        {
            string[] Vals = Regex.Split(Prm, @"/");
            List<Student> ObjStudents = new List<Student>();
            UserContext sdb = new UserContext();
            DataSet dsattendStudents = new DataSet();
            DataSet dsValues = sdb.GetData("Select ProgramId,YearId,CourseId,SectionId,GId,IsGroupBased,SId from tbl_fees_TimeTable where TId='" + Vals[0] + "'", "Val");
            if (dsValues != null)
            {
                if (dsValues.Tables["Val"].Rows.Count > 0)
                {
                    if (dsValues.Tables["Val"].Rows[0]["IsGroupBased"].ToString() == "Group")
                    {
                        sSql = "select SE.StudentId,SE.RollNo,SM.DisplayName from tbl_fees_StudentEnrollment SE,tbl_fees_StudentMaster SM where SM.StudentId=SE.StudentId  and SE.ProgramId='" + dsValues.Tables["Val"].Rows[0]["ProgramId"].ToString() + "' and SE.YearId=" + Convert.ToInt32(dsValues.Tables["Val"].Rows[0]["YearId"]) + " and SE.CourseId='" + dsValues.Tables["Val"].Rows[0]["CourseId"].ToString() + "' and SE.SectionId='" + dsValues.Tables["Val"].Rows[0]["SectionId"].ToString() + "' and SE.GId='" + dsValues.Tables["Val"].Rows[0]["GId"].ToString() + "' and SM.Status ='Active' order by RollNo";
                    }
                    else if (dsValues.Tables["Val"].Rows[0]["IsGroupBased"].ToString() == "Theory")
                    {
                        sSql = "select SE.StudentId,SE.RollNo,SM.DisplayName from tbl_fees_StudentEnrollment SE,tbl_fees_StudentMaster SM where SM.StudentId=SE.StudentId  and SE.ProgramId='" + dsValues.Tables["Val"].Rows[0]["ProgramId"].ToString() + "' and SE.YearId=" + Convert.ToInt32(dsValues.Tables["Val"].Rows[0]["YearId"]) + " and SE.CourseId='" + dsValues.Tables["Val"].Rows[0]["CourseId"].ToString() + "' and SE.SectionId='" + dsValues.Tables["Val"].Rows[0]["SectionId"].ToString() + "' and SM.Status ='Active' order by RollNo";
                    }
                    else
                    {
                        sSql = "select RollNo,DisplayName from tbl_fees_StudentEnrollment SE,tbl_fees_StudentMaster SM,tbl_fees_ElectiveAssignment EA where SM.StudentId=SE.StudentId and SE.StudentId=EA.STudentId and SE.YearId=EA.YearId  and SE.ProgramId='" + dsValues.Tables["Val"].Rows[0]["ProgramId"].ToString() + "' and SE.YearId=" + Convert.ToInt32(dsValues.Tables["Val"].Rows[0]["YearId"]) + " and SE.CourseId='" + dsValues.Tables["Val"].Rows[0]["CourseId"].ToString() + "' and SE.SectionId='" + dsValues.Tables["Val"].Rows[0]["SectionId"].ToString() + "' and EA.EBatch='" + dsValues.Tables["Val"].Rows[0]["GId"].ToString() + "' and EA.SId=" + dsValues.Tables["Val"].Rows[0]["SId"].ToString() + " and SM.Status ='Active' order by RollNo";
                    }
                }
            }

            dsattendStudents = sdb.GetData(sSql, "attendance");
            if (dsattendStudents.Tables["attendance"].Rows.Count > 0)
            {
                string AId = "";
                string StrAid = "";
                DateTime Dt = Convert.ToDateTime(Vals[2]).Date;
                AId = Dt.Day.ToString() + Dt.Month.ToString() + Dt.Year.ToString();
                AId = AId + Vals[0].ToString() + Vals[1].ToString();
                sSql = "select Aid from tbl_fees_AttendanceMaster where Aid='" + AId + "' and TId='" + Vals[0].ToString() + "' and PId=" + Vals[1].ToString() + "";
                StrAid = sdb.Select_Values(sSql);


                for (var i = 0; i <= dsattendStudents.Tables["attendance"].Rows.Count - 1; i++)
                {
                    Student data = new Student();
                    data.StudentId = dsattendStudents.Tables["attendance"].Rows[i]["StudentId"].ToString();
                    data.RollNo = dsattendStudents.Tables["attendance"].Rows[i]["RollNo"].ToString();
                    data.FullName = dsattendStudents.Tables["attendance"].Rows[i]["DisplayName"].ToString();
                    if (StrAid != "0")
                    {
                        string AR = sdb.Select_Values("Select Attendance from tbl_fees_AttendanceMasterDetails where Aid='" + AId + "' and StudentId='" + dsattendStudents.Tables["attendance"].Rows[i]["StudentId"].ToString() + "'");
                        data.AR = AR;
                    }
                    ObjStudents.Add(data);
                }
            }

            return Json(ObjStudents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimeTableUpdate(IEnumerable<RequireElements> TimeUpdates)
        {

            UserContext sdb = new UserContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (TimeUpdates != null)
            {
                foreach (var Acd in TimeUpdates)
                {
                    string Sql1 = "";
                    string Sql2 = "";

                    if (Acd.TimeId.Contains("-"))
                    {

                        string[] Tids = Regex.Split(Acd.TimeId, @"-");
                        if (Tids != null)
                        {
                            string strIsGroupBased = "";
                            strIsGroupBased = sdb.Select_Values("Select S_IsTheory  from tbl_fees_SubjectMasterDetails where  SId=" + Acd.subject + "");
                            string Var_Group = "0";
                            string sem_value = sdb.Select_Values("Select  id from tbl_fees_Semester_master where SemesterId='" + Acd.Semester + "'");
                            string Time_Id = Acd.Program.ToString().Substring(0, 2) + Acd.Course.ToString().Substring(0, 2) + Acd.Year + sem_value + Tids[0].ToString() + Tids[1].ToString();
                            Sql1 = "Insert into tbl_fees_TimeTable([TId],[ProgramId],[YearId],[id],[CourseId],[SectionId],[GId],[PId],[SId],[WId],[EmployeeCode],NOSH,IsGroupBased,CId) values('" + Time_Id + "','" + Acd.Program + "','" + Acd.Year + "','" + Acd.Semester + "','" + Acd.Course + "','" + Acd.Section + "','" + Var_Group + "','" + Tids[1] + "'," + Convert.ToInt32(Acd.subject) + ",'" + Tids[0] + "','" + Acd.Faculty1 + "'," + Convert.ToInt32(Acd.NoofHours) + ",'" + strIsGroupBased + "',1)";
                            if (Acd.Faculty2 != null)
                                Sql2 = "Insert into tbl_fees_TimeTableFaculty([TId],[EmployeeCode]) values('" + Time_Id + "','" + Acd.Faculty2 + "')";
                        }
                    }
                    else
                    {
                        Sql1 = "update  tbl_fees_TimeTable set SId='" + Acd.subject + "',EmployeeCode='" + Acd.Faculty1 + "',NOSH=" + Convert.ToInt32(Acd.NoofHours) + ",CId=1 where TId='" + Acd.TimeId + "'";
                        string Var_TIdExist = "";
                        Var_TIdExist = sdb.Select_Values("Select distinct TId from tbl_fees_TimeTableFaculty where TId='" + Acd.TimeId + "'");

                        if (sdb.Save_Record("delete from tbl_fees_TimeTableFaculty where TId='" + Acd.TimeId + "'"))
                        {
                        }
                        if (Acd.Faculty2 != null)
                            Sql2 = "Insert into tbl_fees_TimeTableFaculty([TId],[EmployeeCode]) values('" + Acd.TimeId + "','" + Acd.Faculty2 + "')";
                    }

                    if (sdb.Save_Record(Sql1))
                    {
                        if (Sql2 != "")
                        {
                            sdb.Save_Record(Sql2);
                        }
                        rescount = 1;
                    }
                    else
                    {
                        rescount = 0;
                    }

                }
                insertedRecords = rescount;
                return Json(insertedRecords);
            }

            insertedRecords = rescount;
            return Json(insertedRecords);

        }
        public ActionResult GetTimeDetails(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            string data = udb.Select_Values("Select EmployeeCode + '/' + CAST(SId as varchar(10)) + '/' + CAST(WId  as varchar(10)) + '/' + CAST(CId as varchar(10)) + '/' + CAST(NOSH as varchar(10)) + '/' from tbl_fees_TimeTable where TId='" + Param + "'");

            string data2 = udb.Select_Values("Select EmployeeCode from tbl_fees_TimeTableFaculty where TId='" + Param + "'");
            if (data2 != "")
            {
                data = data + data2;
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SemesterList(string YId)
        {
            try
            {
                var sem = service.GetSemesterDetails(YId);

                return Json(new SelectList(sem.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        private void Grid_Dataset()
        {
            try
            {
                this.Ds_GridRec = new DataSet();
                DataTable table = new DataTable("TimeTable");

                table.Columns.Add("S.No", System.Type.GetType("System.String"));
                table.Columns.Add("1", System.Type.GetType("System.String"));
                table.Columns.Add("2", System.Type.GetType("System.String"));
                table.Columns.Add("3", System.Type.GetType("System.String"));
                table.Columns.Add("4", System.Type.GetType("System.String"));
                table.Columns.Add("5", System.Type.GetType("System.String"));
                table.Columns.Add("6", System.Type.GetType("System.String"));
                table.Columns.Add("7", System.Type.GetType("System.String"));
                table.Columns.Add("8", System.Type.GetType("System.String"));

                this.Ds_GridRec.Tables.Add(table);
                DataRow current;

                current = table.NewRow();

            }
            catch (Exception exception1)
            {

                Exception exception = exception1;

            }
        }


        public List<ListItem> GetYears(string PId)
        {
            List<ListItem> listYear = new List<ListItem>();
            object programid = PId;
            if (programid != null)
            {
                int period = Convert.ToInt32(udb.Select_Values("Select Period from tbl_fees_Program where ProgramId='" + PId + "'"));
                for (int i = 0; i <= period; i++)
                {

                    if (i == 0)
                    {
                        listYear.Insert(i, new ListItem()
                        {
                            Value = null,
                            Text = "SELECT YEAR"
                        });

                    }
                    else
                    {
                        listYear.Insert(i, new ListItem()
                        {
                            Value = i.ToString(),
                            Text = i.ToString() + " Year"
                        });
                    }

                }
            }
            return listYear;
        }

        public JsonResult FillYears(string Batch)
        {

            try
            {
                var yr = service.GetYearDetails(Batch);
                req.Years = GetSelectListItems(yr);
                return Json(new SelectList(yr.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }







        public List<ListItem> GetStatus()
        {
            List<ListItem> listStatus = new List<ListItem>();


            listStatus.Insert(0, new ListItem()
            {
                Value = "Both",
                Text = "Both"
            });

            listStatus.Insert(1, new ListItem()
            {
                Value = "Active",
                Text = "Active"
            });

            listStatus.Insert(1, new ListItem()
            {
                Value = "InActive",
                Text = "InActive"
            });

            return listStatus;
        }

        public List<ListItem> GetRefundStatus()
        {
            List<ListItem> listRefundStatus = new List<ListItem>();


            listRefundStatus.Insert(0, new ListItem()
            {
                Value = "To be Refund",
                Text = "To be Refund"
            });



            return listRefundStatus;
        }


        public List<ListItem> GetDueFrom()
        {
            List<ListItem> listDue = new List<ListItem>();


            listDue.Insert(0, new ListItem()
            {
                Value = "Convenor",
                Text = "Convenor"
            });

            listDue.Insert(1, new ListItem()
            {
                Value = "TSMFC",
                Text = "TSMFC"
            });

            listDue.Insert(1, new ListItem()
            {
                Value = "UFRO",
                Text = "UFRO"
            });

            return listDue;
        }

        public JsonResult FillCourse(string PId)
        {

            try
            {
                Session["PId"] = PId;
                var Course = service.GetCourseDetails(PId);
                req.Courses = GetSelectListItems(Course);
                return Json(new SelectList(Course.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public List<ListItem> GetReadmissionBatches(string TOR)
        {

            List<ListItem> listBatches = new List<ListItem>();
            listBatches.Insert(0, new ListItem()
            {
                Value = null,
                Text = null
            });
            return listBatches;
        }
        public List<ListItem> GetBatches()
        {

            List<ListItem> listBatches = new List<ListItem>();
            listBatches = service.GetBatchDetails();
            listBatches.RemoveAt(0);
            return listBatches;

        }

        public List<ListItem> GetPrograms()
        {
            List<ListItem> listProgram = new List<ListItem>();
            listProgram = service.GetProgramDetails();
            listProgram.RemoveAt(0);
            return listProgram;
        }
        private IEnumerable<SelectListItem> GetSelectListItems(List<ListItem> elements)
        {

            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Value,
                    Text = element.Text
                });
            }

            return selectList;
        }

        public JsonResult FillDueFrom(IEnumerable<RequireElements> Params)
        {

            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            foreach (var Acd in Params)
            {
                req.Batch = Acd.Batch;
                req.Year = Acd.Year;
                req.Program = Acd.Program;
                req.Course = Acd.Course;
                req.Status = Acd.Status;
                req.DueFrom = Acd.DueFrom;
            }
            var FillDue = sdb.GetFillDue(req);

            if (FillDue.Count == 0)
            {
                FillDue = null;
                return Json(FillDue, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillDue.ToArray(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult InsertMassUpdate(IEnumerable<FillDue> CheckAll)
        {
            List<FillDue> listFillDue = new List<FillDue>();
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (CheckAll != null)
            {
                foreach (var Acd in CheckAll)
                {
                    FillDue data = new FillDue()
                    {
                        StudentId = Acd.StudentId,
                        TuitionFee = Acd.TuitionFee,
                        OUFee = Acd.OUFee,
                        Year = Acd.Year,
                        ChallanNo = Acd.ChallanNo,
                        ChallanDate = Acd.ChallanDate,
                        EntryDate = Acd.EntryDate,
                        PaymentId = Acd.PaymentId,
                        ReceivedFrom = Acd.ReceivedFrom,
                        Status = Acd.Status,
                        ProgramId = Acd.ProgramId,
                        EnterBy = Acd.EnterBy
                    };
                    listFillDue.Add(data);
                }
                rescount = sdb.StudentMassUpdate(listFillDue);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }



        [HttpGet]
        public ActionResult ExtraPaid()
        {
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }
            ExtrPaidModel model = new ExtrPaidModel();
            model.ExtraPaids = null;
            var Paymode = service.GetPayModeDetails();
            model.PayModes = GetSelectListItems(Paymode);
            return View(model);

        }

        public JsonResult InsertExtraPaids(IEnumerable<ExtrPaidModel> ExtraPaids)
        {
            List<ExtraPaid> listEP = new List<ExtraPaid>();
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;

            foreach (var Acd in ExtraPaids)
            {
                ExtraPaid dataEP = new ExtraPaid()
                {
                    RollNo = Acd.RollNo,
                    Year = Acd.Year,
                    ChallanNo = Acd.ChallanNo,
                    ChallanDate = Acd.ChallanDate,
                    Amount = Acd.Amount,
                    PayMode = Acd.PayMode,
                    Status = Acd.Status,
                    EnterBy = Acd.EnterBy
                };
                listEP.Add(dataEP);
            }
            rescount = sdb.ExtrPaid(listEP);

            int insertedRecords = rescount;
            return Json(insertedRecords);
        }


        [HttpGet]
        public ActionResult Challan_Confirmation()
        {
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }

            var model = new ChallanConfirmation();


            var Trans = service.GetTransaction(DateTime.Now);
            model.Transactions = GetSelectListItems(Trans);


            return View(model);


        }

        public JsonResult TransactionList(DateTime DT)
        {

            try
            {
                var model = new ChallanConfirmation();
                var Trans = service.GetTransaction(DT);
                model.Transactions = GetSelectListItems(Trans);
                return Json(new SelectList(Trans.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult StudentTransactionDetails(string TranId)
        {

            AcademicContext sdb = new AcademicContext();


            var FillDue = sdb.GetTransactionDetails(TranId);

            if (FillDue.Count == 0)
            {
                FillDue = null;
                return Json(FillDue, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillDue.ToArray(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult ConfirmAcademicChallan(IEnumerable<Challan> StudentTransactions)
        {
            List<Challan> listChallan = new List<Challan>();
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (StudentTransactions != null)
            {

                rescount = sdb.AcadimicChallanConfirmation(StudentTransactions);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        [HttpGet]
        public ActionResult Modal()
        {
            return View();

        }



        [HttpGet]
        public ActionResult ExtrPaidConfirmation()
        {
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }
            return View();

        }

        public ActionResult GetExtraPaid(DateTime PaymentDate)
        {
            Session["Result"] = null;
            AcademicContext sdb = new AcademicContext();

            List<ExtraPaid> lstEP = new List<ExtraPaid>();
            lstEP = sdb.GetExtraPaidDetails(PaymentDate);

            if (lstEP.Count == 0)
            {
                lstEP = null;

                return PartialView("_GridEditPartial", lstEP);

            }
            else

                return PartialView("_GridEditPartial", lstEP);


        }

        [HttpPost]
        public JsonResult ConfirmExtrPaid(IEnumerable<ExtraPaid> CheckAll)
        {

            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (CheckAll != null)
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
                dtColumn.ColumnName = "Sno";
                dtColumn.Caption = "Sno";
                dt.Columns.Add(dtColumn);


                dtColumn = new DataColumn();
                dtColumn.DataType = Type.GetType("System.String");
                dtColumn.ColumnName = "Status";
                dtColumn.Caption = "Status";
                dt.Columns.Add(dtColumn);
                int i = 0;
                string approvedby = "";
                foreach (var Acd in CheckAll)
                {
                    approvedby = Acd.EnterBy;
                    DataRow dr = dt.NewRow();

                    dr["Id"] = i + 1;
                    dr["Sno"] = Convert.ToInt32(Acd.Sno);
                    dr["Status"] = Acd.Status;
                    dt.Rows.Add(dr);
                    i += 1;
                }
                rescount = sdb.ExtrapaidConfirm(dt, approvedby);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }

            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        public ActionResult GetChallanUpdate(string RollNo)
        {
            string Res = "Challan";
            //string RN = form.Get("txtRollNo");
            Session["Result"] = Res;
            AcademicContext sdb = new AcademicContext();

            List<ExtraPaid> lstEP = new List<ExtraPaid>();
            lstEP = sdb.GetChallanUpdate(Res, RollNo);

            if (lstEP.Count == 0)
            {
                lstEP = null;

                return PartialView("_GridEditPartial", lstEP);

            }
            else

                return PartialView("_GridEditPartial", lstEP);


        }

        [HttpGet]
        public ActionResult ChallanUpdate()
        {
            Session["Header"] = Request.QueryString["UT"];
            return View();
        }

        public JsonResult ChallanUpdate(IEnumerable<ExtraPaid> CheckAll)
        {

            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (CheckAll != null)
            {

                foreach (var Acd in CheckAll)
                {

                    rescount = sdb.Challan_Update(Acd.Sno, Acd.ChallanNo, Convert.ToDateTime(Acd.ChallanDate), Acd.Status, Acd.result, Acd.RollNo);

                }


                insertedRecords = rescount;
                return Json(insertedRecords);
            }

            insertedRecords = rescount;
            return Json(insertedRecords);

        }


        public ActionResult DetainUpdate(Student_Detain_Discontinue req)
        {
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            rescount = sdb.DetainUpdate(req);
            insertedRecords = rescount;

            insertedRecords = rescount;
            if (insertedRecords != 0)
            {
                return Content("Record updated!!", "text/html");
            }
            else
            {
                return Content("Record Not updated!!", "text/html");

            }

        }


        public JsonResult ChallanDelete(IEnumerable<ExtraPaid> CheckAll)
        {

            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (CheckAll != null)
            {

                foreach (var Acd in CheckAll)
                {

                    if ((Acd.Status == "Approved") && (Acd.result == "Challan"))
                    {
                        rescount = sdb.Challan_Delete_Approved(Acd.Sno, Acd.Status, Acd.result);

                    }
                    else
                    {
                        rescount = sdb.Challan_Delete(Acd.Sno, Acd.Status, Acd.result);
                    }


                }

            }

            insertedRecords = rescount;
            return Json(insertedRecords);

        }


        public JsonResult FillRefundFrom(IEnumerable<FinancialYearDates> Params)
        {

            AcademicContext sdb = new AcademicContext();
            FinancialYearDates req = new FinancialYearDates();
            foreach (var Acd in Params)
            {
                req.FromDate = Acd.FromDate;
                req.ToDate = Acd.ToDate;
                req.Status = Acd.Status;

            }
            var FillRefund = sdb.GetFillRefund(req);

            if (FillRefund.Count == 0)
            {
                FillRefund = null;
                return Json(FillRefund, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillRefund.ToArray(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult InsertRefund(IEnumerable<ExtraPaid> CheckAll)
        {
            List<ExtraPaid> listRefund = new List<ExtraPaid>();
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (CheckAll != null)
            {
                foreach (var Acd in CheckAll)
                {
                    ExtraPaid data = new ExtraPaid()
                    {
                        StudentId = Acd.StudentId,
                        Year = Acd.Year,
                        ChallanNo = Acd.ChallanNo,
                        ChallanDate = Acd.ChallanDate,
                        Amount = Acd.Amount,
                    };
                    listRefund.Add(data);
                }
                rescount = sdb.StudentRefund(listRefund);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        public JsonResult verifyRollNo(string RollNo)
        {
            AcademicContext sdb = new AcademicContext();
            var verifyRoll = sdb.CheckRollNo(RollNo);
            if (verifyRoll.Count == 0)
            {
                verifyRoll = null;
                return Json(verifyRoll, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(verifyRoll, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff()
        {
            Session["username"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult StudentDetainDiscontinue()
        {
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = DateTime.Now.ToString("yyyy-MM-dd");

            }
            var model = new Student_Detain_Discontinue();
            var Detains = GetDetains();
            model.DetainTypes = GetSelectListItems(Detains);

            var Status = GetDetainStatus();
            model.Statuses = GetSelectListItems(Status);

            return View(model);
        }

        public List<ListItem> GetDetains()
        {
            List<ListItem> listDetain = new List<ListItem>();
            listDetain = service.GetDetainDetails();
            listDetain.RemoveAt(0);
            return listDetain;
        }


        public List<ListItem> GetDetainStatus()
        {
            List<ListItem> listStatus = new List<ListItem>();

            listStatus.Insert(0, new ListItem()
            {
                Value = "Detain",
                Text = "Detain"
            });
            listStatus.Insert(1, new ListItem()
            {
                Value = "DisContinue",
                Text = "DisContinue"
            });


            return listStatus;
        }


        public JsonResult SaveStudentDetain(IEnumerable<Student_Detain_Discontinue> StudentDetain)
        {
            List<Student_Detain_Discontinue> Rec = new List<Student_Detain_Discontinue>();
            AcademicContext sdb = new AcademicContext();
            Student_Detain_Discontinue data = new Student_Detain_Discontinue();
            int rescount = 0;
            int insertedRecords = 0;
            if (StudentDetain != null)
            {
                foreach (var Acd in StudentDetain)
                {
                    data.RollNo = Acd.RollNo;
                    data.Year = Acd.Year;
                    data.DetainId = Acd.DetainId;
                    data.DetainDate = Acd.DetainDate;
                    data.Status = Acd.Status;
                    data.Remarks = Acd.Remarks;
                    data.EnterBy = Acd.EnterBy;
                }
                rescount = sdb.StudentDetain_Discontinue(data);

            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }



        [HttpGet]
        public ActionResult StudentReAdmission()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new StudentReAdmission();
            var mainBatch = GetBatches();

            model.Batchs = GetSelectListItems(mainBatch);
            var mainreadmissionbatch = GetReadmissionBatches("");
            model.ReAdmissionBatchs = GetSelectListItems(mainreadmissionbatch);

            var mainStudent = GetStudentDetails("");
            model.StudentIds = GetSelectListItems(mainStudent);

            var Year = GetReAdmission();
            model.TypeOfReAdmissions = GetSelectListItems(Year);

            var ReYear = GetReadmissionYear("");
            model.ReAdmissionYears = GetSelectListItems(ReYear);
            var Reason = GetReason();
            model.Reasons = GetSelectListItems(Reason);

            return View(model);
        }
        public List<ListItem> GetStudentDetails(string Batch)
        {
            List<ListItem> Item = new List<ListItem>();
            AcademicContext context = new AcademicContext();
            Item = context.GetStudent(Batch);

            return Item;
        }

        public JsonResult FillStudent(string batch)
        {

            try
            {
                StudentReAdmission req = new StudentReAdmission();
                AcademicContext sdb = new AcademicContext();
                var stud = sdb.GetStudent(batch);
                req.StudentIds = GetSelectListItems(stud);
                return Json(new SelectList(stud.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult StudentReAdmissionDetails(string Student)
        {

            AcademicContext sdb = new AcademicContext();


            var FillAmount = sdb.GetStudentReAdmissionDetails(Student);

            if (FillAmount.Count == 0)
            {
                FillAmount = null;
                return Json(FillAmount, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillAmount.ToArray(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult StudentReAdmissionList(string Student)
        {

            AcademicContext sdb = new AcademicContext();


            var FillAmount = sdb.GetStudentReAdmissionList(Student);

            if (FillAmount.Count == 0)
            {
                FillAmount = null;
                return Json(FillAmount, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillAmount.ToArray(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult FillDate(string student, string batch)
        {

            try
            {
                StudentReAdmission req = new StudentReAdmission();
                AcademicContext sdb = new AcademicContext();
                DataSet data = sdb.GetDate(student, batch);
                // var stud = sdb.GetStudent(batch);               
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }


        public List<ListItem> GetReAdmission()
        {
            List<ListItem> listReAdmission = new List<ListItem>();

            listReAdmission.Insert(0, new ListItem()
            {
                Value = "0",
                Text = "Select ReAdmission"
            });
            listReAdmission.Insert(1, new ListItem()
            {
                Value = "Same Batch Readmission",
                Text = "Same Batch Readmission"
            });

            listReAdmission.Insert(2, new ListItem()
            {
                Value = "Readmission",
                Text = "Readmission"
            });

            listReAdmission.RemoveAt(0);

            return listReAdmission;
        }

        public JsonResult listReAdmissionList()
        {
            try
            {
                StudentReAdmission sdb = new StudentReAdmission();
                var ReAdmission = GetReAdmission();
                sdb.TypeOfReAdmissions = GetSelectListItems(ReAdmission);
                return Json(new SelectList(ReAdmission.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public List<ListItem> GetReason()
        {
            List<ListItem> listReason = new List<ListItem>();

            listReason.Insert(0, new ListItem()
            {
                Value = "0",
                Text = "Select Reason"
            });
            listReason.Insert(1, new ListItem()
            {
                Value = "COURT ORDER",
                Text = "COURT ORDER"
            });

            listReason.Insert(2, new ListItem()
            {
                Value = "PROMOTED/O.U ORDER",
                Text = "PROMOTED/O.U ORDER"
            });
            listReason.Insert(3, new ListItem()
            {
                Value = "GENERAL READMISSION",
                Text = "GENERAL READMISSION"
            });

            listReason.RemoveAt(0);
            return listReason;
        }
        public JsonResult ListReason()
        {
            try
            {
                StudentReAdmission sdb = new StudentReAdmission();
                var ReReason = GetReason();
                sdb.TypeOfReAdmissions = GetSelectListItems(ReReason);
                return Json(new SelectList(ReReason.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public List<ListItem> GetReadmissionYear(string batch)
        {
            List<ListItem> listYears = new List<ListItem>();
            if (batch == "")
            {

                listYears.Insert(0, new ListItem()
                {
                    Value = "SELECT YEAR",
                    Text = "SELECT YEAR"
                });
                listYears.RemoveAt(0);
                return listYears;
            }
            else
            {
                List<ListItem> listBatches = new List<ListItem>();
                listYears = service.GETYEARS(batch);
                listYears.RemoveAt(0);
                return listYears;
            }
        }

        public JsonResult GetRAYear(string batch)
        {

            AcademicContext context = new AcademicContext();
            var Item = context.GetYear(batch);
            return Json(Item, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetReAdmissionBatch(string Readmission)
        {

            AcademicContext context = new AcademicContext();
            var Item = context.GetReAdmissionSameBatch(Readmission);
            return Json(Item, JsonRequestBehavior.AllowGet);

        }

        public List<ListItem> GetReAdmissionOtherBatchDeatils(string year)
        {
            List<ListItem> Item = new List<ListItem>();
            AcademicContext context = new AcademicContext();
            Item = context.GetReAdmissionOtherBatch(year);

            return Item;
        }

        public JsonResult GetReAdmissionOtherBatch(string year)
        {

            AcademicContext context = new AcademicContext();

            var Item = context.GetReAdmissionOtherBatch(year);
            return Json(Item, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ConfirmReadmission(IEnumerable<StudentReAdmission> StudentTransactions)
        {
            List<StudentReAdmission> listChallan = new List<StudentReAdmission>();
            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (StudentTransactions != null)
            {

                rescount = sdb.StudentReadmissionSave(StudentTransactions);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        public ActionResult AttendanceRegister()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new RequireElements();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);
            var Course = service.GetCourseDetails("");
            model.Courses = GetSelectListItems(Course);

            var Section = service.GetSectionDetails("");
            model.Sections = GetSelectListItems(Course);

            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            var Semester = service.GetSemesterDetails("");
            model.Semesters = GetSelectListItems(Semester);


            var Weeks = service.GetWeeks();
            model.weeks = GetSelectListItems(Weeks);


            var subjects = service.Get_Attedance_subjects("", "", 0);
            model.subjects = GetSelectListItems(subjects);

            var periods = service.Get_Attedance_Peiod(0, "", 0, "");
            model.Periods = GetSelectListItems(periods);

            var employees = service.Get_Attedance_Employee(0, "", 0, "", "", 0);
            model.Faculties1 = GetSelectListItems(employees);

            var ClassTypes = service.GetClassTypes();
            model.ClassTypes = GetSelectListItems(ClassTypes);

            return View(model);
        }

        public JsonResult AttendanceUpdate(IEnumerable<Attendance> Attendances)
        {

            UserContext sdb = new UserContext();
            int rescount = 0;
            int insertedRecords = 0;
            string AId = "";
            string StrAid = "";
            if (Attendances != null)
            {
                foreach (var AttnId in Attendances)
                {
                    AId = AttnId.todayDate.Day.ToString() + AttnId.todayDate.Month.ToString() + AttnId.todayDate.Year.ToString();
                    AId = AId + AttnId.TId + AttnId.PId;
                    sSql = "select Aid from tbl_fees_AttendanceMaster where Aid='" + AId + "' and TId='" + AttnId.TId + "' and PId=" + AttnId.PId + "";
                    StrAid = sdb.Select_Values(sSql);
                    if (StrAid == "0")
                    {
                        StrAid = "";
                        if (StrAid == "")
                        {
                            DataSet dsvalues = sdb.GetData("Select SubjectCode,NOH, SId  from tbl_fees_TimeTable where TId='" + AttnId.TId + "'", "Vals");
                            string Scode = sdb.Select_Values("Select SubjectCode  from tbl_fees_SubjectMasterDetails where SId='" + dsvalues.Tables["Vals"].Rows[0]["SId"].ToString() + "'");
                            sSql = "Insert into [tbl_fees_AttendanceMaster]([Aid],[TId],[AttendanceDate],[EmployeeCode],[SubjectCode],[WId],[PId],[Status],CId,NOH,SId,YearId) Values( '" + AId + "','" + AttnId.TId + "','" + AttnId.todayDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + AttnId.EmployeeCode + "','" + Scode + "'," + AttnId.WId + "," + AttnId.PId + ",'Completed'," + Convert.ToInt32(AttnId.ClassType) + "," + Convert.ToInt32(dsvalues.Tables["Vals"].Rows[0]["NOH"].ToString()) + "," + Convert.ToInt32(dsvalues.Tables["Vals"].Rows[0]["SId"].ToString()) + "," + AttnId.YearId + ")";
                            sdb.Save_Record(sSql);

                            DataSet ArrECodes;
                            sSql = "Select EmployeeCode from tbl_fees_TimeTableFaculty  where TId='" + AttnId.TId + "' union All Select EmployeeCode from tbl_fees_TimeTable  where TId='" + AttnId.TId + "'";
                            ArrECodes = sdb.GetData(sSql, "OECodes");
                            if (ArrECodes != null)
                            {
                                if (ArrECodes.Tables["OECodes"].Rows.Count > 0)
                                {
                                    for (var EC = 0; EC <= ArrECodes.Tables["OECodes"].Rows.Count - 1; EC++)
                                    {
                                        if (AttnId.EmployeeCode.ToString() != Convert.ToString(ArrECodes.Tables["OECodes"].Rows[EC]["EmployeeCode"]))
                                        {
                                            sSql = "Insert into [tbl_fees_AttendanceMultyFaculty]([Aid],[TId],[EmployeeCode]) Values( '" + AId + "','" + AttnId.TId + "','" + ArrECodes.Tables["OECodes"].Rows[EC][0] + "')";
                                            sdb.Save_Record(sSql);
                                        }
                                    }
                                }
                            }
                        }

                        goto Label_057E;
                    }
                    else
                    {
                        goto Label_057E;
                    }
                }

            Label_057E:
                foreach (var Acd in Attendances)
                {
                    if (AId == StrAid)
                    {

                        if (Acd.AR == "P")
                        {
                            string CorrectionID = sdb.Select_Values("Select StudentId from tbl_fees_AttendanceMasterDetails where StudentId='" + Acd.StudentId + "' and Aid='" + AId + "' and Attendance='P'");
                            if (CorrectionID == "0")
                            {
                                sSql = "update [tbl_fees_AttendanceMasterDetails] set [Attendance]='P' where StudentId='" + Acd.StudentId + "' and Aid='" + AId + "'";
                                sdb.Save_Record(sSql);
                                sSql = "Insert into tbl_fees_AttendanceCorrection (Aid,StudentId,OldAttendance,Attendance,AttendanceDate,ModifiedDate)values ('" + AId + "','" + Acd.StudentId + "','A','P','" + Acd.todayDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "')";
                                sdb.Save_Record(sSql);
                                rescount = rescount + 1;
                            }
                        }
                        else
                        {
                            string CorrectionID = sdb.Select_Values("Select StudentId from tbl_fees_AttendanceMasterDetails where StudentId='" + Acd.StudentId + "' and Aid='" + AId + "' and Attendance='A'");
                            if (CorrectionID == "0")
                            {
                                sSql = "update [tbl_fees_AttendanceMasterDetails] set [Attendance]='A' where StudentId='" + Acd.StudentId + "' and Aid='" + AId + "'";
                                sdb.Save_Record(sSql);
                                sSql = "Insert into tbl_fees_AttendanceCorrection (Aid,StudentId,OldAttendance,Attendance,AttendanceDate,ModifiedDate)values ('" + AId + "','" + Acd.StudentId + "','P','A','" + Acd.todayDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + DateTime.Now.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "')";
                                sdb.Save_Record(sSql);
                                rescount = rescount + 1;
                            }

                        }
                    }
                    else
                    {

                        sSql = "Insert into [tbl_fees_AttendanceMasterDetails]([Aid],StudentId,[Attendance]) Values( '" + AId + "','" + Acd.StudentId + "','" + Acd.AR + "')";
                        sdb.Save_Record(sSql);
                        rescount = rescount + 1;
                    }
                }
                insertedRecords = rescount;
                return Json(insertedRecords);
            }

            insertedRecords = rescount;
            return Json(insertedRecords);

        }




    }
}