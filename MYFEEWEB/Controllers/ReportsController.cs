using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MYFEELIB.Data;
using MYFEELIB.Domain;
using MYFEELIB.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MYFEEWEB.Controllers
{
    public class ReportsController : Controller
    {
        UserContext udb = new UserContext();
        DataTable tempTable, BudgetTable, tempBUDatetable, NexttempTable, tempTableTOF;
        DataTable MonthtempTable;
        DataRow tr;
        AccountService service = new AccountService();
        string Arr;
        string F_Report;
        string Financial_Year;
        string F_Program;
        string sSql;



        //
        // GET: /Reports/
        public ActionResult StudentReport()
        {
            Session["Header"] = Request.QueryString["UT"];
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








            return View(model);
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


        public List<ListItem> GetReportType()
        {
            List<ListItem> listRT = new List<ListItem>();


            listRT.Insert(0, new ListItem()
            {
                Value = "Actuals",
                Text = "Actuals"
            });

            listRT.Insert(1, new ListItem()
            {
                Value = "OtherFee",
                Text = "Other Fee"
            });

            listRT.Insert(1, new ListItem()
            {
                Value = "ActualWithOtherFees",
                Text = "Actual With Other Fees"
            });

            return listRT;
        }

        public List<ListItem> GetBatches()
        {

            List<ListItem> listBatches = new List<ListItem>();
            listBatches = service.GetBatchDetails();
            listBatches.RemoveAt(0);
            return listBatches;

        }

        public List<ListItem> GetProgramsAll()
        {
            List<ListItem> listProgram = new List<ListItem>();
            listProgram = service.GetProgramDetails();
            listProgram.RemoveAt(0);
            listProgram.Insert(0, new ListItem()
            {
                Value = "0",
                Text = "All"
            });

            return listProgram;
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

        public ActionResult GenerateStudentReport(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.StudentReport Rs = new Reports.StudentReport();
                ReportDocument cryRpt;
                cryRpt = (ReportDocument)Rs.StudentYearReportPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[3].ToString(), "admin");
                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\StudentReport.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/StudentReport.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        public ActionResult FillQuota(string PId)
        {
            try
            {
                var quota = service.GetQuotaDetails(PId);
                return Json(new SelectList(quota.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }
        public ActionResult GenerateBatchYearWiseStudent(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();
                ReportDocument cryRpt;
                cryRpt = null;
                if (Vals[5].ToString() == "WR")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                    }
                    else if (Vals[4].ToString() == "OtherFee")
                    {
                        cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                        }
                        else
                        {
                            cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                        }
                    }
                }
                else if (Vals[5].ToString() == "WOR")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsWORTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString());
                    }
                    else if (Vals[4].ToString() == "OtherFee")
                    {
                        cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsWOROFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString());
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.YearQuotaCategoryDetailsWORPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString());
                    }
                }
                else if (Vals[5].ToString() == "TotalSudents")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYStudentTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Year Status", "admin");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYStudentTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Current Status", "admin");


                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYStudentTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Year Status", "admin");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYStudentTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Current Status", "admin");


                    }

                }
                else if (Vals[5].ToString() == "StudentsWithOutQuota")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYSTTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Year Status", "admin");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYSTTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Current Status", "admin");
                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYSTTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Year Status", "admin");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYSTTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "Current Status", "admin");
                    }

                }

                else if (Vals[5].ToString() == "TSMFC")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYAPSMFCTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "PresentStatus");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYAPSMFCTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "CurrentStatus");
                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYAPSMFCTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "PresentStatus");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYAPSMFCTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "CurrentStatus");
                    }

                }
                else if (Vals[5].ToString() == "Convenor")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYConvenorTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYConvenorTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYConvenorTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYConvenorTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin");
                    }

                }
                else if (Vals[5].ToString() == "UFRO")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYUFROTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "PresentStatus");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYUFROTFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "CurrentStatus");
                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYUFROTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "PresentStatus");
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYUFROTFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[3].ToString(), "admin", "CurrentStatus");
                    }

                }

                else if (Vals[5].ToString() == "StudentsWithQuota")
                {
                    if (Vals[4].ToString() == "Actuals")
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYQUOTATFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[7].ToString(), Vals[3].ToString());
                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYQUOTATFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[7].ToString(), Vals[3].ToString());
                    }
                    else
                    {
                        if (Vals[6].ToString() == "YearStatus")
                        {
                            cryRpt = (ReportDocument)Rs.BYQUOTATFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[7].ToString(), Vals[3].ToString());

                        }
                        else
                            cryRpt = (ReportDocument)Rs.BYQUOTATFOFPdf(1, Vals[2].ToString(), Convert.ToInt32(Vals[1]), Vals[0].ToString(), Vals[7].ToString(), Vals[3].ToString());

                    }

                }

                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\StudentReport.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/StudentReport.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        public ActionResult BatchYearWiseStudent()
        {
            Session["Header"] = Request.QueryString["UT"];
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



            var RT = GetReportType();
            model.ReportTypes = GetSelectListItems(RT);

            var quota = service.GetQuotaDetails("");
            model.Quotas = GetSelectListItems(quota);

            return View(model);
        }

        public ActionResult PaymentApprovedReport()
        {
            AcademicContext Ac = new AcademicContext();
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }
            var model = new RequireElements();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            var ApproveType = GetApproveType();
            model.ApproveTypes = GetSelectListItems(ApproveType);

            var Paymode = service.GetPayModeDetails();
            model.PayModes = GetSelectListItems(Paymode);

            var processMonths = service.GetProcessMonths();
            model.ProcessMonths = GetSelectListItems(processMonths);

            return View(model);
        }

        public List<ListItem> GetOption()
        {
            List<ListItem> listRT = new List<ListItem>();


            listRT.Insert(0, new ListItem()
            {
                Value = "All",
                Text = "All"
            });

            listRT.Insert(1, new ListItem()
            {
                Value = "Approved",
                Text = "Approved"
            });


            return listRT;
        }

        public List<ListItem> GetApproveType()
        {
            List<ListItem> listRT = new List<ListItem>();


            listRT.Insert(0, new ListItem()
            {
                Value = "Approved",
                Text = "Approved"
            });

            listRT.Insert(1, new ListItem()
            {
                Value = "NotApproved",
                Text = "Not Approved"
            });

            listRT.Insert(2, new ListItem()
            {
                Value = "TobeRefund",
                Text = "To be Refund"
            });
            listRT.Insert(3, new ListItem()
            {
                Value = "RefundTobeConfirm",
                Text = "Refund To be Confirm"
            });
            listRT.Insert(4, new ListItem()
            {
                Value = "Refunded",
                Text = "Refunded"
            });

            return listRT;
        }


        public JsonResult YearList(string PId)
        {
            try
            {
                var yr = GetYear(PId);

                return Json(new SelectList(yr.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public List<ListItem> GetYear(string PId)
        {
            List<ListItem> listYear = new List<ListItem>();

            if (PId == "ET")
            {
                listYear.Insert(0, new ListItem()
                {
                    Value = "All",
                    Text = "All"
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
                    Value = "All",
                    Text = "All"
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

        public ActionResult GenerateApprovedReport(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();
                ReportDocument cryRpt;
                cryRpt = null;
                if (Vals[4].ToString() == "SelectMonth")
                {
                    DataSet ds = sdb.GetMonthDates(Convert.ToInt32(Vals[5]));
                    if (ds != null)
                    {
                        DateTime Dt = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).Date;
                        DateTime EndDt = Convert.ToDateTime(ds.Tables[0].Rows[0][1]).Date;

                        if (Vals[1].ToString() != "All")
                        {
                            cryRpt = (ReportDocument)Rs.StudentApproveReportYearPdf(1, Vals[0].ToString(), Dt, EndDt, Convert.ToInt32(Vals[1]), Vals[2].ToString(), Vals[3].ToString(), "admin");
                        }
                        else
                        {
                            cryRpt = (ReportDocument)Rs.StudentApproveReportALLPdf(1, Vals[0].ToString(), Dt, EndDt, Vals[2].ToString(), Vals[3].ToString(), "admin");

                        }

                    }

                }
                else if (Vals[4].ToString() == "SelectDate")
                {
                    DateTime Dt = Convert.ToDateTime(Vals[5]).Date;
                    DateTime EndDt = Convert.ToDateTime(Vals[6]).Date;

                    if (Vals[1].ToString() != "All")
                    {
                        cryRpt = (ReportDocument)Rs.StudentApproveReportYearPdf(1, Vals[0].ToString(), Dt, EndDt, Convert.ToInt32(Vals[1]), Vals[2].ToString(), Vals[3].ToString(), "admin");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.StudentApproveReportALLPdf(1, Vals[0].ToString(), Dt, EndDt, Vals[2].ToString(), Vals[3].ToString(), "admin");

                    }
                }
                else if (Vals[4].ToString() == "UserType")
                {
                    DataSet ds = sdb.GetMonthDates(Convert.ToInt32(Vals[5]));
                    if (ds != null)
                    {
                        DateTime Dt = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).Date;
                        DateTime EndDt = Convert.ToDateTime(ds.Tables[0].Rows[0][1]).Date;

                        if (Vals[1].ToString() != "All")
                        {
                            cryRpt = (ReportDocument)Rs.UserStudentApproveReportYearPdf(1, Vals[0].ToString(), Dt, EndDt, Convert.ToInt32(Vals[1]), Vals[2].ToString(), Vals[3].ToString(), "admin");
                        }
                        else
                        {
                            cryRpt = (ReportDocument)Rs.UserStudentApproveReportALLPdf(1, Vals[0].ToString(), Dt, EndDt, Vals[2].ToString(), Vals[3].ToString(), "admin");

                        }

                    }
                }
                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\StudentReport.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/StudentReport.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        public ActionResult TransactionReport()
        {
            AcademicContext Ac = new AcademicContext();
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }
            var model = new RequireElements();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Year = service.GetYearDetails("");
            model.Years = GetSelectListItems(Year);

            var processMonths = service.GetProcessMonths();
            model.ProcessMonths = GetSelectListItems(processMonths);

            var Due = GetDueFrom();
            model.DuesFrom = GetSelectListItems(Due);

            return View(model);
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

            listDue.Insert(2, new ListItem()
            {
                Value = "UFRO",
                Text = "UFRO"
            });

            listDue.Insert(3, new ListItem()
            {
                Value = "BCB",
                Text = "BCB"
            });

            listDue.Insert(4, new ListItem()
            {
                Value = "Student",
                Text = "Student"
            });

            return listDue;
        }

        public ActionResult GenerateTransactionReport(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();
                ReportDocument cryRpt;
                cryRpt = null;
                if (Vals[2].ToString() == "SelectMonth")
                {
                    DataSet ds = sdb.GetMonthDates(Convert.ToInt32(Vals[4]));
                    if (ds != null)
                    {
                        DateTime Dt = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).Date;
                        DateTime EndDt = Convert.ToDateTime(ds.Tables[0].Rows[0][1]).Date;

                        if (Vals[3].ToString() == "SelectTransaction")
                        {
                            if (Vals[1].ToString() != "All")
                            {
                                cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportPdf(1, Vals[0].ToString(), Convert.ToInt32(Vals[1]), Dt, EndDt, "admin");
                            }
                            else
                            {
                                cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportALLPdf(1, Vals[0].ToString(), Dt, EndDt, "admin");

                            }

                        }
                        else if (Vals[3].ToString() == "SelectRF")
                        {
                            if (Vals[5].ToString() == "TSMFC")
                            {
                                cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportRALLPdf(1, Vals[0].ToString(), Dt, EndDt, "APSMFC", "admin");
                            }
                            else
                            {
                                cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportRALLPdf(1, Vals[0].ToString(), Dt, EndDt, Vals[5].ToString(), "admin");

                            }
                        }

                        else if (Vals[3].ToString() == "OD")
                        {
                            cryRpt = (ReportDocument)Rs.ODReportPdf(1, Dt, EndDt, "admin");
                        }
                        else if (Vals[3].ToString() == "Suspense")
                        {
                            cryRpt = (ReportDocument)Rs.SusPenseReportPdf(1, Dt, EndDt, "admin");
                        }



                    }

                }
                else if (Vals[2].ToString() == "SelectDate")
                {
                    DateTime Dt = Convert.ToDateTime(Vals[4]).Date;
                    DateTime EndDt = Convert.ToDateTime(Vals[5]).Date;
                    if (Vals[3].ToString() == "SelectTransaction")
                    {
                        if (Vals[1].ToString() != "All")
                        {
                            cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportPdf(1, Vals[0].ToString(), Convert.ToInt32(Vals[1]), Dt, EndDt, "admin");
                        }
                        else
                        {
                            cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportALLPdf(1, Vals[0].ToString(), Dt, EndDt, "admin");

                        }

                    }
                    else if (Vals[3].ToString() == "SelectRF")
                    {
                        cryRpt = (ReportDocument)Rs.StudentPayDatesTransactionReportRALLPdf(1, Vals[0].ToString(), Dt, EndDt, Vals[6].ToString(), "admin");
                    }

                    else if (Vals[3].ToString() == "OD")
                    {
                        cryRpt = (ReportDocument)Rs.ODReportPdf(1, Dt, EndDt, "admin");
                    }
                    else if (Vals[3].ToString() == "Suspense")
                    {
                        cryRpt = (ReportDocument)Rs.SusPenseReportPdf(1, Dt, EndDt, "admin");
                    }
                }

                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\StudentReport.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/StudentReport.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        public ActionResult ReconciliationReport()
        {
            AcademicContext Ac = new AcademicContext();
            Session["Header"] = Request.QueryString["UT"];
            DataSet ds = Ac.GetFinancialPeriod();

            if (ds != null)
            {
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];

            }
            var model = new RequireElements();

            var mainProgram = GetProgramsAll();
            model.Programs = GetSelectListItems(mainProgram);



            var ApproveType = GetOption();
            model.ApproveTypes = GetSelectListItems(ApproveType);



            var processMonths = service.GetProcessMonths();
            model.ProcessMonths = GetSelectListItems(processMonths);



            return View(model);
        }

        public ActionResult GenerateReconciliationReport(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();
                ReportDocument cryRpt;
                cryRpt = null;
                string PName = null;
                if (Vals[0].ToString() != "0")
                {
                    PName = sdb.Select_Values("Select ProgramName from tbl_fees_Program where ProgramId='" + Vals[0].ToString() + "'");
                }
                else
                {
                    PName = "All";
                }
                if (Vals[2].ToString() == "SelectMonth")
                {
                    DataSet ds = sdb.GetMonthDates(Convert.ToInt32(Vals[4]));
                    string PMonth = null;
                    PMonth = sdb.Select_Values("Select PMonth from tbl_fees_FinancialMonths where Id='" + Vals[4].ToString() + "'");
                    if (ds != null)
                    {
                        DateTime Dt = Convert.ToDateTime(ds.Tables[0].Rows[0][0]).Date;
                        DateTime EndDt = Convert.ToDateTime(ds.Tables[0].Rows[0][1]).Date;

                        if (Vals[3].ToString() == "RC")
                        {
                            if (Vals[1].ToString() != "All")
                            {
                                cryRpt = (ReportDocument)Rs.MonthReconsilationReportPdf(1, Vals[0].ToString(), Dt, EndDt, "admin", PMonth, PName);
                            }
                            else
                            {
                                cryRpt = (ReportDocument)Rs.MonthReconsilationAllReportPdf(1, Vals[0].ToString(), Dt, EndDt, "admin", PMonth, PName);
                            }

                        }
                        else if (Vals[3].ToString() == "RCHOA")
                        {

                            string sSQl;

                            sSQl = "delete from [tbl_fees_HOA]";
                            udb.Save_Record(sSQl);
                            createReconsilationData(Dt, EndDt, PName, Vals[0].ToString());
                            cryRpt = (ReportDocument)Rs.MonthReconsilationAllReportHOAPdf(1, Vals[0].ToString(), Dt, EndDt, "admin", PMonth, PName);

                        }
                    }

                }
                else if (Vals[2].ToString() == "SelectDate")
                {
                    DateTime Dt = Convert.ToDateTime(Vals[4]).Date;
                    DateTime EndDt = Convert.ToDateTime(Vals[5]).Date;
                    if (Vals[3].ToString() == "RC")
                    {
                        if (Vals[1].ToString() != "All")
                        {
                            cryRpt = (ReportDocument)Rs.ReconsilationReportPdf(1, Vals[0].ToString(), Dt, EndDt, "admin", PName);
                        }
                        else
                        {
                            cryRpt = (ReportDocument)Rs.ReconsilationAllReportPdf(1, Vals[0].ToString(), Dt, EndDt, "admin", PName);
                        }

                    }
                    else if (Vals[3].ToString() == "RCHOA")
                    {
                        string sSQl;

                        sSQl = "delete from [tbl_fees_HOA]";
                        if (udb.Save_Record(sSQl))
                        {
                            createReconsilationData(Dt, EndDt, PName, Vals[0].ToString());
                            cryRpt = (ReportDocument)Rs.MonthReconsilationAllReportHOADtPdf(1, Vals[0].ToString(), Dt, EndDt, "admin", PName);
                        }
                    }


                }

                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\StudentReport.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/StudentReport.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        private void CreateReconsilationHOATable()
        {

            tempTable = new DataTable();
            DataColumn dcRef;
            tempTable.Rows.Clear();
            tempTable.Columns.Clear();

            dcRef = new DataColumn();
            dcRef.DataType = Type.GetType("System.String");
            dcRef.ColumnName = "Head Of Acount";
            tempTable.Columns.Add(dcRef);



            dcRef = new DataColumn();
            dcRef.DataType = Type.GetType("System.String");
            dcRef.ColumnName = "Amount";
            tempTable.Columns.Add(dcRef);

        }

        public void createReconsilationData(DateTime strdt, DateTime strEndt, string Program, String Program_Value)
        {
            CreateReconsilationHOATable();
            //DateTime strdt = strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            //DateTime strEndt = strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (Program != "All")
            {
                tr = tempTable.NewRow();
                tr[0] = "Transactions(T.F + O.F + Adcvanced Fee)";
                // sdb.Select_Values(sSql);
                decimal H1 = Convert.ToDecimal(udb.Select_Values("select  SUM(TotalAmount) As Amount from [Vw_ReconsilationConslidate] where ChallanDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and Status ='Approved'  and ProgramId='" + Program_Value + "'"));
                tr[1] = H1;
                tempTable.Rows.Add(tr);

                tr = tempTable.NewRow();
                tr[0] = "To be Refund";
                decimal H2 = Convert.ToDecimal(udb.Select_Values("select  SUM(TotalAmount) As Amount from [Vw_ReconsilationConslidate] where ChallanDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and Status ='To be Refund'  and ProgramId='" + Program_Value + "'"));
                tr[1] = H2;
                tempTable.Rows.Add(tr);


                tr = tempTable.NewRow();
                tr[0] = "Refunded";
                decimal H3 = Convert.ToDecimal(udb.Select_Values("select  SUM(TotalAmount) As Amount from [Vw_ReconsilationConslidate] where ChallanDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and Status ='Refunded'  and ProgramId='" + Program_Value + "'"));
                tr[1] = H3;
                tempTable.Rows.Add(tr);


                tr = tempTable.NewRow();
                tr[0] = "Out Of Data";
                decimal H4 = Convert.ToDecimal(udb.Select_Values("select SUM(Amount) As Amount from tbl_fees_OD where PaymentDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "'  and PrgmID='" + Program_Value + "'"));
                tr[1] = H4;
                tempTable.Rows.Add(tr);
            }
            else
            {
                tr = tempTable.NewRow();
                tr[0] = "Transactions(T.F + O.F + Adcvanced Fee)";
                decimal H1 = Convert.ToDecimal(udb.Select_Values("select  SUM(TotalAmount) As Amount from [Vw_ReconsilationConslidate] where ChallanDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and Status ='Approved'"));
                tr[1] = H1;
                tempTable.Rows.Add(tr);
                tr = tempTable.NewRow();
                tr[0] = "To be Refund";
                decimal H2 = Convert.ToDecimal(udb.Select_Values("select  SUM(TotalAmount) As Amount from [Vw_ReconsilationConslidate] where ChallanDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and Status ='To be Refund'"));
                tr[1] = H2;
                tempTable.Rows.Add(tr);


                tr = tempTable.NewRow();
                tr[0] = "Refunded";
                decimal H3 = Convert.ToDecimal(udb.Select_Values("select  SUM(TotalAmount) As Amount from [Vw_ReconsilationConslidate] where ChallanDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and Status ='Refunded'"));
                tr[1] = H3;
                tempTable.Rows.Add(tr);


                tr = tempTable.NewRow();
                tr[0] = "Out Of Data";
                decimal H4 = Convert.ToDecimal(udb.Select_Values("select SUM(Amount) As Amount from tbl_fees_OD where PaymentDate between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "'"));
                tr[1] = H4;
                tempTable.Rows.Add(tr);
            }

            for (int J = 0; J <= tempTable.Rows.Count - 1; J++)
            {
                string sSQl;

                sSQl = "Insert Into [tbl_fees_HOA] (HOA,[Amount]) Values ('" + tempTable.Rows[J][0].ToString() + "'," + Convert.ToDecimal(tempTable.Rows[J][1]) + ")";
                udb.Save_Record(sSQl);
            }
        }

        public ActionResult FinancialYearReceivedReport()
        {
            AcademicContext Ac = new AcademicContext();
            Session["Header"] = Request.QueryString["UT"];

            var model = new RequireElements();
            var mainProgram = GetProgramsAll();
            model.Programs = GetSelectListItems(mainProgram);
            var FY = Ac.GetFinancialYears();
            model.YearCodes = GetSelectListItems(FY);

            return View(model);
        }

        public JsonResult FYDATES(String YearCode)
        {
            try
            {
                string YC = udb.Select_Values("select  YearCode  from [FinancialPeriods] where SNo='" + YearCode + "'");
                var dates = new List<FYDates>();
                AcademicContext Ac = new AcademicContext();
                DataSet dsDates = new DataSet();
                dsDates = Ac.GetFinancialDates(YC);
                dates = GetDates(dsDates.Tables[0]);
                return Json(dates.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public List<FYDates> GetDates(DataTable Table)
        {
            List<FYDates> ModList = new List<FYDates>();
            try
            {
                foreach (DataRow dr in Table.Rows)
                {
                    FYDates item = new FYDates()
                    {
                        StDate = Convert.ToDateTime(dr["StartDate"]),
                        EndDate = Convert.ToDateTime(dr["EndDate"]),
                    };
                    ModList.Add(item);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return ModList;
        }

        public ActionResult GenerateFinancialYearReceivedReport(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();
                ReportDocument cryRpt;
                cryRpt = null;
                string PName = null;
                string YC = udb.Select_Values("select  YearCode  from [FinancialPeriods] where SNo='" + Vals[3].ToString() + "'");
                if (Vals[4].ToString() != "0")
                {
                    PName = sdb.Select_Values("Select ProgramName from tbl_fees_Program where ProgramId='" + Vals[4].ToString() + "'");
                }
                else
                {
                    PName = "All";
                }
                DateTime Dt = Convert.ToDateTime(Vals[0]).Date;
                DateTime EndDt = Convert.ToDateTime(Vals[1]).Date;
                if (Vals[2].ToString() == "RA")
                {
                    cryRpt = (ReportDocument)Rs.StudentReAdmissionListPdf(1, Dt, EndDt, "All", "admin");
                }
                else if (Vals[2].ToString() == "DC")
                {
                    cryRpt = (ReportDocument)Rs.StudentDiscontinueFYPdf(1, Dt, EndDt, "admin");
                }
                else if (Vals[2].ToString() == "FRA")
                {
                    F_Report = "Financial Reveived All";
                    sSql = "delete from [tbl_fees_QMonthsFinancial]";
                    udb.Save_Record(sSql);
                    Financial_Year = YC;
                    F_Program = Vals[4].ToString();
                    if (Financial_Year != "")
                    {
                        createMonthsData(Financial_Year, "Active", "Financial Months", F_Program, Dt, EndDt);
                        cryRpt = (ReportDocument)Rs.FinancialYearReceivedAllPdf(1, Financial_Year, EndDt.ToString(), "admin", PName);
                    }
                }
                else if (Vals[2].ToString() == "FYRTF")
                {
                    F_Report = "Financial Reveived TF";
                    sSql = "delete from [tbl_fees_QMonthsFinancial]";
                    udb.Save_Record(sSql);
                    Financial_Year = YC;
                    F_Program = Vals[4].ToString();
                    if (Financial_Year != "")
                    {
                        createMonthsData(Financial_Year, "Active", "Financial Months", F_Program, Dt, EndDt);
                        cryRpt = (ReportDocument)Rs.FinancialYearReceivedPdf(1, Financial_Year, EndDt.ToString(), "admin", PName);
                    }
                }
                else if (Vals[2].ToString() == "REFCON")
                {
                    cryRpt = (ReportDocument)Rs.StudentPeriodRefundReportConsolidatePdf(1, Dt, EndDt, "admin");
                }
                else if (Vals[2].ToString() == "REFLIST")
                {
                    cryRpt = (ReportDocument)Rs.StudentPeriodRefundReportPdf(1, Dt, EndDt, "admin");
                }
                else if (Vals[2].ToString ()=="FYRTOTF")
                {
                    cryRpt = (ReportDocument)Rs.StudentReceivedFinancialPdf(1, Vals[4].ToString(), Dt, EndDt, "admin",YC);

                }
                else if (Vals[2].ToString() == "FYRTOF")
                {
                    cryRpt = (ReportDocument)Rs.StudentReceivedFinancialTOPdf(1, Vals[4].ToString(), Dt, EndDt, "admin", YC);

                }
                else if (Vals[2].ToString() == "FYREF")
                {
                    cryRpt = (ReportDocument)Rs.StudentReceivedFinancialEPdf(1, Vals[4].ToString(), Dt, EndDt, "admin", YC);

                }


                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\FR.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1156px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/FR.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }



        public void createMonthsData(string YearCode, string Status, string FY, string Program, DateTime strdt, DateTime strEndt)
        {
            try
            {
                string StrMonths;
                if (FY == "Financial Year")
                    StrMonths = "SELECT Id,PMonth,CONVERT (char(10),MFrom,101) AS MFrom,CONVERT (char(10),MTo,101) AS MTo FROM [tbl_fees_FinancialMonths] where YearCode='" + YearCode + "'";
                else
                {
                    StrMonths = "SELECT Id,PMonth,CONVERT (char(10),MFrom,101) AS MFrom,CONVERT (char(10),MTo,101) AS MTo FROM [tbl_fees_FinancialMonths] where YearCode='" + YearCode + "' and MFrom between '" + strdt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' and '" + strEndt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' ";
                }
                System.Data.DataSet dsDataMonths = new System.Data.DataSet(), DsOB = new System.Data.DataSet();
                System.Data.DataSet dsDataAPSMFC = new System.Data.DataSet();
                System.Data.DataSet dsDataMgmt = new System.Data.DataSet();
                System.Data.DataSet dsDataUFRO = new System.Data.DataSet();
                System.Data.DataSet dsDataStudent = new System.Data.DataSet();
                System.Data.DataSet dsDataConvenor = new System.Data.DataSet();
                System.Data.DataSet dsDataOF = new System.Data.DataSet();
                System.Data.DataSet dsDataEF = new System.Data.DataSet();
                System.Data.DataSet dsDataIA = new System.Data.DataSet();
                System.Data.DataSet dsDataNAPSMFC = new System.Data.DataSet();
                System.Data.DataSet dsDataDAPSMFC = new System.Data.DataSet();
                System.Data.DataSet dsDataNMGMT = new System.Data.DataSet();
                System.Data.DataSet dsDataDMGMT = new System.Data.DataSet();
                System.Data.DataSet dsDataNUFRO = new System.Data.DataSet();
                System.Data.DataSet dsDataDUFRO = new System.Data.DataSet();
                System.Data.DataSet dsDataNStudent = new System.Data.DataSet();
                System.Data.DataSet dsDataDStudent = new System.Data.DataSet();
                System.Data.DataSet dsDataNConvenor = new System.Data.DataSet();
                System.Data.DataSet dsDataDConvenor = new System.Data.DataSet();
                System.Data.DataSet dsDataNOF = new System.Data.DataSet();
                System.Data.DataSet dsDataDOF = new System.Data.DataSet();

                dsDataMonths = udb.GetData(StrMonths, "Months");
                CreateMonthstempTable();
                //string StrOB;
                //StrOB = "SELECT [FMonth],APSMFC,Management,UFRO,Student,Convenor,Total,OtherFee FROM [tbl_fees_OBFinancial]";
                //DsOB = udb.GetData(StrOB, "OB");
                //if (DsOB.Tables["OB"] != null)
                //{
                //    for (int OB = 0; OB <= DsOB.Tables["OB"].Rows.Count - 1; OB++)
                //    {
                //        tr = MonthtempTable.NewRow();
                //        tr[0] = "Opening Balance";
                //        tr[1] = "";
                //        tr[2] = "";

                //        tr[3] = DsOB.Tables["OB"].Rows[OB]["APSMFC"].ToString();
                //        tr[4] = 0;
                //        tr[5] = 0;

                //        tr[6] = DsOB.Tables["OB"].Rows[OB]["Management"].ToString();
                //        tr[7] = 0;
                //        tr[8] = 0;

                //        tr[9] = DsOB.Tables["OB"].Rows[OB]["UFRO"].ToString();
                //        tr[10] = 0;
                //        tr[11] = 0;

                //        tr[12] = DsOB.Tables["OB"].Rows[OB]["Student"].ToString();
                //        tr[13] = 0;
                //        tr[14] = 0;

                //        tr[15] = DsOB.Tables["OB"].Rows[OB]["Convenor"].ToString();
                //        tr[16] = 0;
                //        tr[17] = 0;

                //        tr[18] = DsOB.Tables["OB"].Rows[OB]["OtherFee"].ToString();

                //        tr[19] = 0;
                //        tr[20] = 0;
                //        tr[21] = 0;
                //        tr[22] = 0;
                //        tr[23] = DsOB.Tables["OB"].Rows[OB]["Total"].ToString();

                //        MonthtempTable.Rows.Add(tr);
                //    }
                //}

                decimal dbl_MAPSMFC, dbl_MMGMT, dbl_MUFRO, dbl_MStudent, dbl_MConvenor, dbl_MOF, dbl_MEF, dbl_MIA;
                decimal dbl_NAPSMFC, dbl_NMGMT, dbl_NUFRO, dbl_NStudent, dbl_NConvenor, dbl_NOF;
                decimal dbl_DAPSMFC, dbl_DMGMT, dbl_DUFRO, dbl_DStudent, dbl_DConvenor, dbl_DOF;


                if (dsDataMonths.Tables["Months"] != null)
                {
                    for (int i = 0; i <= dsDataMonths.Tables["Months"].Rows.Count - 1; i++)
                    {
                        dbl_MAPSMFC = 0;
                        dbl_MMGMT = 0;
                        dbl_MUFRO = 0;
                        dbl_MStudent = 0;
                        dbl_MConvenor = 0;
                        dbl_MOF = 0;
                        dbl_MEF = 0;
                        dbl_MIA = 0;

                        dbl_NAPSMFC = 0;
                        dbl_NMGMT = 0;
                        dbl_NUFRO = 0;
                        dbl_NStudent = 0;
                        dbl_NConvenor = 0;
                        dbl_NOF = 0;

                        dbl_DAPSMFC = 0;
                        dbl_DMGMT = 0;
                        dbl_DUFRO = 0;
                        dbl_DStudent = 0;
                        dbl_DConvenor = 0;
                        dbl_DOF = 0;


                        decimal dbl_APSMFCR, dbl_MGMTR, dbl_UFROR, dbl_StudentR, dbl_ConvenorR, dbl_OFR, dbl_EF, dbl_IA;
                        decimal dbl_APSMFCN, dbl_MGMTN, dbl_UFRON, dbl_StudentN, dbl_ConvenorN, dbl_OFN;
                        decimal dbl_APSMFCD, dbl_MGMTD, dbl_UFROD, dbl_StudentD, dbl_ConvenorD, dbl_OFD;



                        string StrYears;
                        if (Program != "0")
                            StrYears = "SELECT distinct BatchId AS BatchId ,PD.YearId AS YearId  FROM Vw_FinancialTransactions PD,tbl_fees_StudentEnrollment SE where PD.StudentId =SE.StudentId and SE.ProgramId='" + Program + "' and ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' group by BatchId ,PD.YearId union SELECT O.BatchId AS BatchId,O.YearId AS YearId  FROM tbl_fees_SDD SD,Vw_OTACTF O where SD.StudentId =O.StudentId and SD.YearId =O.YearId and FromDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "'    group by O.BatchId ,O.YearId   union select distinct BatchId,YearId from tbl_fees_NR where Date between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' group by BatchId ,YearId ";
                        else
                            StrYears = "SELECT distinct BatchId AS BatchId ,PD.YearId AS YearId  FROM Vw_FinancialTransactions PD,tbl_fees_StudentEnrollment SE where PD.StudentId =SE.StudentId  and ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' group by BatchId ,PD.YearId union SELECT O.BatchId AS BatchId,O.YearId AS YearId  FROM tbl_fees_SDD SD,Vw_OTACTF O where SD.StudentId =O.StudentId and SD.YearId =O.YearId and FromDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "'    group by O.BatchId ,O.YearId   union select distinct BatchId,YearId from tbl_fees_NR where Date between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' group by BatchId ,YearId ";


                        System.Data.DataSet dsYears = new System.Data.DataSet();
                        dsYears = null;
                        dsYears = udb.GetData(StrYears, "Years");
                        if (dsYears.Tables["Years"].Rows.Count > 0)
                        {
                            for (int J = 0; J <= dsYears.Tables["Years"].Rows.Count - 1; J++)
                            {
                                dbl_APSMFCR = 0;
                                dbl_MGMTR = 0;
                                dbl_UFROR = 0;
                                dbl_StudentR = 0;
                                dbl_ConvenorR = 0;
                                dbl_OFR = 0;

                                dbl_APSMFCN = 0;
                                dbl_MGMTN = 0;
                                dbl_UFRON = 0;
                                dbl_StudentN = 0;
                                dbl_ConvenorN = 0;
                                dbl_OFN = 0;

                                dbl_APSMFCD = 0;
                                dbl_MGMTD = 0;
                                dbl_UFROD = 0;
                                dbl_StudentD = 0;
                                dbl_ConvenorD = 0;
                                dbl_OFD = 0;

                                dbl_EF = 0;
                                dbl_IA = 0;
                                tr = MonthtempTable.NewRow();
                                tr[0] = dsDataMonths.Tables["Months"].Rows[i]["PMonth"].ToString();
                                tr[1] = dsYears.Tables["Years"].Rows[J]["BatchId"].ToString();
                                tr[2] = dsYears.Tables["Years"].Rows[J]["YearId"].ToString();
                                tr[3] = 0;
                                tr[4] = 0;

                                string StrAPSMFC;
                                if (Program != "0")
                                    StrAPSMFC = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId and SE.ProgramId='" + Program + "' and  [ChallanDate] between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FC.ReceivedFrom in('APSMFC','BCB') and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                else
                                    StrAPSMFC = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId  and  [ChallanDate] between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FC.ReceivedFrom in('APSMFC','BCB') and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";

                                dsDataAPSMFC = null;
                                dsDataAPSMFC = udb.GetData(StrAPSMFC, "APSMFC");

                                if (dsDataAPSMFC.Tables["APSMFC"].Rows.Count > 0)
                                {

                                    tr[5] = dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString();
                                    dbl_APSMFCR = dbl_APSMFCR + Convert.ToDecimal(dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString());
                                    Arr = udb.Select_Values("select Status  from FinancialBatches where YearCode ='" + YearCode + "' and BatchId ='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "");
                                    if (Arr == "Open")
                                        dbl_MAPSMFC = dbl_MAPSMFC + Convert.ToDecimal(dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString());
                                }
                                else
                                    tr[5] = 0;
                                tr[6] = 0;
                                tr[7] = 0;

                                string StrMgmt;
                                if (Program != "0")
                                    StrMgmt = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId and SE.ProgramId='" + Program + "' and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and SE.QuotaId in('MGMT','MNGMNT') and FC.ReceivedFrom ='Student' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                else
                                    StrMgmt = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId  and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and SE.QuotaId in('MGMT','MNGMNT') and FC.ReceivedFrom ='Student' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";

                                dsDataMgmt = null;
                                dsDataMgmt = udb.GetData(StrMgmt, "Mgmt");

                                if (dsDataMgmt.Tables["Mgmt"].Rows.Count > 0)
                                {
                                    tr[8] = dsDataMgmt.Tables["Mgmt"].Rows[0][0].ToString();
                                    dbl_MGMTR = dbl_MGMTR + Convert.ToDecimal(dsDataMgmt.Tables["Mgmt"].Rows[0][0].ToString());
                                    Arr = udb.Select_Values("select Status  from FinancialBatches where YearCode ='" + YearCode + "' and BatchId ='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "");
                                    if (Arr == "Open")
                                        dbl_MMGMT = dbl_MMGMT + Convert.ToDecimal(dsDataMgmt.Tables["Mgmt"].Rows[0][0].ToString());
                                }
                                else
                                    tr[8] = 0;

                                tr[9] = 0;
                                tr[10] = 0;
                                string StrUFRO;
                                if (Program != "0")
                                    StrUFRO = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId and SE.ProgramId='" + Program + "' and   ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FC.ReceivedFrom ='UFRO' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                else
                                    StrUFRO = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId  and   ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FC.ReceivedFrom ='UFRO' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                dsDataUFRO = null;
                                dsDataUFRO = udb.GetData(StrUFRO, "UFRO");

                                if (dsDataUFRO.Tables["UFRO"].Rows.Count > 0)
                                {
                                    tr[11] = dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString();
                                    dbl_UFROR = dbl_UFROR + Convert.ToDecimal(dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString());
                                    Arr = udb.Select_Values("select Status  from FinancialBatches where YearCode ='" + YearCode + "' and BatchId ='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "");
                                    if (Arr == "Open")
                                        dbl_MUFRO = dbl_MUFRO + Convert.ToDecimal(dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString());
                                }
                                else
                                    tr[11] = 0;
                                tr[12] = 0;
                                tr[13] = 0;
                                string StrStudent;
                                if (Program != "0")
                                    StrStudent = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId=SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId and SE.ProgramId='" + Program + "' and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and SE.QuotaId in ('EAMCET','PD','TRF','CONVENOR','SPOT') and FC.ReceivedFrom ='Student' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                else
                                    StrStudent = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId=SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId  and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and SE.QuotaId in ('EAMCET','PD','TRF','CONVENOR','SPOT') and FC.ReceivedFrom ='Student' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                dsDataStudent = null;
                                dsDataStudent = udb.GetData(StrStudent, "Student");

                                if (dsDataStudent.Tables["Student"].Rows.Count > 0)
                                {
                                    tr[14] = dsDataStudent.Tables["Student"].Rows[0][0].ToString();
                                    dbl_StudentR = dbl_StudentR + Convert.ToDecimal(dsDataStudent.Tables["Student"].Rows[0][0].ToString());
                                    Arr = udb.Select_Values("select Status  from FinancialBatches where YearCode ='" + YearCode + "' and BatchId ='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "");
                                    if (Arr == "Open")
                                        dbl_MStudent = dbl_MStudent + Convert.ToDecimal(dsDataStudent.Tables["Student"].Rows[0][0].ToString());
                                }
                                else
                                    tr[14] = 0;
                                tr[15] = 0;
                                tr[16] = 0;
                                string StrConvenor;
                                if (Program != "0")
                                    StrConvenor = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId and SE.ProgramId='" + Program + "' and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FC.ReceivedFrom ='Convenor' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                else
                                    StrConvenor = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId  and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FC.ReceivedFrom ='Convenor' and FC.Status='Approved' and FM.Priority='True' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId ,FC.[ReceivedFrom]";
                                dsDataConvenor = null;
                                dsDataConvenor = udb.GetData(StrConvenor, "Convenor");
                                if (dsDataConvenor.Tables["Convenor"].Rows.Count > 0)
                                {
                                    tr[17] = dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString();
                                    dbl_ConvenorR = dbl_ConvenorR + Convert.ToDecimal(dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString());
                                    Arr = udb.Select_Values("select Status  from FinancialBatches where YearCode ='" + YearCode + "' and BatchId ='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "");
                                    if (Arr == "Open")
                                        dbl_MConvenor = dbl_MConvenor + Convert.ToDecimal(dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString());
                                }
                                else
                                    tr[17] = 0;
                                tr[18] = 0;
                                tr[19] = 0;
                                string StrOF;
                                if (Program != "0")
                                    StrOF = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId=O.YearId and SE.ProgramId='" + Program + "' and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FM.Priority='False' and FC.Status='Approved' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId";
                                else
                                    StrOF = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId =SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId=O.YearId  and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "' and FM.Priority='False' and FC.Status='Approved' and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId";
                                dsDataOF = null;
                                dsDataOF = udb.GetData(StrOF, "OF");
                                if (dsDataOF.Tables["OF"].Rows.Count > 0)
                                {
                                    tr[20] = dsDataOF.Tables["OF"].Rows[0][0].ToString();
                                    dbl_OFR = dbl_OFR + Convert.ToDecimal(dsDataOF.Tables["OF"].Rows[0][0].ToString());

                                    Arr = udb.Select_Values("select Status  from FinancialBatches where YearCode ='" + YearCode + "' and BatchId ='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + "");
                                    if (Arr == "Open")
                                        dbl_MOF = dbl_MOF + Convert.ToDecimal(dsDataOF.Tables["OF"].Rows[0][0].ToString());
                                }
                                else
                                    tr[20] = 0;

                                string StrEF;
                                if (Program != "0")
                                    StrEF = "SELECT Sum([Amount]) As Amount FROM [tbl_fees_PayExtraDetails] PED,tbl_fees_StudentEnrollment SE where PED.StudentId =SE.StudentId and SE.ProgramId='" + Program + "'   and  DateOfConfirmation between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "'  and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and PED.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + " and PED.Status in('Approved','Refund')  group by SE.BatchId ,PED.YearId";
                                else
                                    StrEF = "SELECT Sum([Amount]) As Amount FROM [tbl_fees_PayExtraDetails] PED,tbl_fees_StudentEnrollment SE where PED.StudentId =SE.StudentId   and  DateOfConfirmation between '" + dsDataMonths.Tables["Months"].Rows[i]["MFrom"] + "' and '" + dsDataMonths.Tables["Months"].Rows[i]["MTo"] + "'  and BatchId='" + dsYears.Tables["Years"].Rows[J]["BatchId"].ToString() + "' and PED.YearId=" + dsYears.Tables["Years"].Rows[J]["YearId"].ToString() + " and PED.Status in('Approved')  group by SE.BatchId ,PED.YearId";
                                dsDataEF = null;
                                dsDataEF = udb.GetData(StrEF, "EF");
                                if (dsDataEF.Tables["EF"].Rows.Count > 0)
                                {
                                    tr[21] = dsDataEF.Tables["EF"].Rows[0][0].ToString();
                                    dbl_EF = dbl_EF + Convert.ToDecimal(dsDataEF.Tables["EF"].Rows[0][0].ToString());
                                    dbl_MEF = dbl_MEF + Convert.ToDecimal(dsDataEF.Tables["EF"].Rows[0][0].ToString());
                                }
                                else
                                    tr[21] = 0;
                                tr[22] = 0;
                                // End InActive'
                                tr[23] = Math.Abs((dbl_APSMFCN + dbl_MGMTN + dbl_UFRON + dbl_StudentN + dbl_ConvenorN + dbl_OFN) - ((dbl_APSMFCD + dbl_MGMTD + dbl_UFROD + dbl_StudentD + dbl_ConvenorD + dbl_OFD) + (dbl_APSMFCR + dbl_MGMTR + dbl_UFROR + dbl_StudentR + dbl_ConvenorR + dbl_OFR + dbl_EF + dbl_IA)));
                                MonthtempTable.Rows.Add(tr);
                            }

                            // OB'
                            //tr = MonthtempTable.NewRow();
                            //tr[0] = dsDataMonths.Tables["Months"].Rows[i]["PMonth"].ToString() + " Closing Balance";
                            //tr[1] = "";
                            //tr[2] = "";

                            //tr[3] = (Convert.ToDecimal(MonthtempTable.Rows[MonthtempTable.Rows.Count - (dsYears.Tables["Years"].Rows.Count + 1)][3]) + dbl_NAPSMFC) - (dbl_DAPSMFC + dbl_MAPSMFC);
                            //tr[4] = 0;
                            //tr[5] = 0;

                            //tr[6] = (Convert.ToDecimal(MonthtempTable.Rows[MonthtempTable.Rows.Count - (dsYears.Tables["Years"].Rows.Count + 1)][6]) + dbl_NMGMT) - (dbl_DMGMT + dbl_MMGMT);
                            //tr[7] = 0;
                            //tr[8] = 0;

                            //tr[9] = (Convert.ToDecimal(MonthtempTable.Rows[MonthtempTable.Rows.Count - (dsYears.Tables["Years"].Rows.Count + 1)][9]) + dbl_NUFRO) - (dbl_DUFRO + dbl_MUFRO);
                            //tr[10] = 0;
                            //tr[11] = 0;

                            //tr[12] = (Convert.ToDecimal(MonthtempTable.Rows[MonthtempTable.Rows.Count - (dsYears.Tables["Years"].Rows.Count + 1)][12]) + dbl_NStudent) - (dbl_DStudent + dbl_MStudent);
                            //tr[13] = 0;
                            //tr[14] = 0;

                            //tr[15] = (Convert.ToDecimal(MonthtempTable.Rows[MonthtempTable.Rows.Count - (dsYears.Tables["Years"].Rows.Count + 1)][15]) + dbl_NConvenor) - (dbl_DConvenor + dbl_MConvenor);
                            //tr[16] = 0;
                            //tr[17] = 0;

                            //tr[18] = (Convert.ToDecimal(MonthtempTable.Rows[MonthtempTable.Rows.Count - (dsYears.Tables["Years"].Rows.Count + 1)][18]) + dbl_NOF) - (dbl_DOF + dbl_MOF);
                            //tr[19] = 0;
                            //tr[20] = 0;

                            //tr[21] = 0;
                            //tr[22] = 0;

                            //tr[23] = Convert.ToDecimal(tr[3]) + Convert.ToDecimal(tr[6]) + Convert.ToDecimal(tr[9]) + Convert.ToDecimal(tr[12]) + Convert.ToDecimal(tr[15]) + Convert.ToDecimal(tr[18]);
                            //MonthtempTable.Rows.Add(tr);
                        }
                    }
                    for (int J = 0; J <= MonthtempTable.Rows.Count - 1; J++)
                    {
                        string sSQl;
                        if (MonthtempTable.Rows[J][2].ToString() != "")
                        {
                            sSQl = "Insert Into [tbl_fees_QMonthsFinancial] (FMonth,[BatchId], [YearId], [APSMFC],APSMFCD,APSMFCR,[Management],ManagementD,ManagementR,[UFRO],UFROD,UFROR,[Student],StudentD,StudentR,[Convenor],ConvenorD,ConvenorR,OtherFeeN,OtherFeeD,OtherFee,ExtraPaid,InActive,Total) Values ('" + MonthtempTable.Rows[J][0].ToString() + "','" + MonthtempTable.Rows[J][1].ToString() + "'," + Convert.ToInt32(MonthtempTable.Rows[J][2]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][3]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][4]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][5]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][6]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][7]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][8]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][9]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][10]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][11]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][12]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][13]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][14]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][15]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][16]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][17]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][18]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][19]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][20]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][21]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][22]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][23]) + ")";
                            udb.Save_Record(sSQl);
                        }
                        else
                        {
                            sSQl = "Insert Into [tbl_fees_QMonthsFinancial] (FMonth,[BatchId],[APSMFC],APSMFCD,APSMFCR,[Management],ManagementD,ManagementR,[UFRO],UFROD,UFROR,[Student],StudentD,StudentR,[Convenor],ConvenorD,ConvenorR,Total,OtherFeeN,OtherFeeD,OtherFee) Values ('" + MonthtempTable.Rows[J][0].ToString() + "','" + MonthtempTable.Rows[J][1].ToString() + "'," + Convert.ToDecimal(MonthtempTable.Rows[J][3]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][4]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][5]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][6]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][7]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][8]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][9]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][10]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][11]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][12]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][13]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][14]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][15]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][16]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][17]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][23]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][18]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][19]) + "," + Convert.ToDecimal(MonthtempTable.Rows[J][20]) + ")";
                            udb.Save_Record(sSQl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void CreateMonthstempTable()
        {
            try
            {
                MonthtempTable = new DataTable();
                DataColumn dcRef;
                MonthtempTable.Rows.Clear();
                MonthtempTable.Columns.Clear();

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Month";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Batch";
                MonthtempTable.Columns.Add(dcRef);



                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Year";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFC";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFCD";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFCR";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Management";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ManagementD";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ManagementR";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFRO";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFROD";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFROR";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Student";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "StudentD";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "StudentR";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Convenor";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ConvenorD";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ConvenorR";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "OtherFeeN";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "OtherFeeD";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "OtherFee";
                MonthtempTable.Columns.Add(dcRef);



                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ExtraPaid";
                MonthtempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "InActive";
                MonthtempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Total";
                MonthtempTable.Columns.Add(dcRef);
            }
            catch (Exception ex)
            {
            }
        }



        public ActionResult AuditReports()
        {
            AcademicContext Ac = new AcademicContext();
            Session["Header"] = Request.QueryString["UT"];

            var model = new RequireElements();


            var mainBatch = GetBatches();
            model.Batches = GetSelectListItems(mainBatch);

            var FY = Ac.GetFinancialYears();
            model.YearCodes = GetSelectListItems(FY);

            return View(model);
        }

        public ActionResult GenerateAuditReports(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();

                ReportDocument cryRpt;
                cryRpt = null;
                string YC = udb.Select_Values("select  YearCode  from [FinancialPeriods] where SNo='" + Vals[1].ToString() + "'");
                string Current = udb.Select_Values("select  Status  from [FinancialPeriods] where SNo='" + Vals[1].ToString() + "'");


                DateTime currentdt = DateTime.Now.Date;
                if (Vals[0].ToString() == "FYC")
                {
                    if (Current == "Open")
                    {
                        createBudget(YC);
                        cryRpt = (ReportDocument)Rs.BudgetReportPdf(1, "admin", YC, currentdt.ToString(), "Current Financial Year");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.BudgetReportPdf(1, "admin", YC, currentdt.ToString(), "Financial Year");
                    }

                }
                else if (Vals[0] == "IS")
                {
                    if (Current == "Open")
                    {
                        createBudget(YC);
                        cryRpt = (ReportDocument)Rs.IntakeSummaryReportPdf(1, "admin", YC, currentdt.ToString(), "Current Financial Year");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.IntakeSummaryReportPdf(1, "admin", YC, currentdt.ToString(), "Financial Year");
                    }

                }
                else if (Vals[0] == "IQS")
                {
                    if (Current == "Open")
                    {
                        createBudget(YC);
                        cryRpt = (ReportDocument)Rs.IntakeQuotaSummaryReportPdf(1, "admin", YC, currentdt.ToString(), "Current Financial Year");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.IntakeQuotaSummaryReportPdf(1, "admin", YC, currentdt.ToString(), "Financial Year");
                    }

                }

                else if (Vals[0] == "FYCB")
                {
                    if (Current == "Open")
                    {
                        FinancialReceived("Current Financial Year", YC);
                        cryRpt = (ReportDocument)Rs.JEConsolidatePdf(1, YC, "admin");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.JEConsolidatePdf(1, YC, "admin");
                    }

                }

                else if (Vals[0] == "FYCBS")
                {
                    if (Current == "Open")
                    {
                        FinancialReceived("Current Financial Year", YC);
                        cryRpt = (ReportDocument)Rs.JEConsolidateSummaryPdf(1, YC, "admin");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.JEConsolidateSummaryPdf(1, YC, "admin");
                    }

                }

                else if (Vals[0] == "FYDSL")
                {
                    Reports.StudentReport RDStudent = new Reports.StudentReport();
                    if (Current == "Open")
                    {
                        createYearBakUp();
                        cryRpt = (ReportDocument)RDStudent.ActiveStudentsReportPdf("1", "admin", YC, "Total Active");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)RDStudent.ActiveStudentsReportPdf("1", "admin", YC, "Total Active");
                    }

                }

                else if (Vals[0] == "BR")
                {
                    sSql = "delete from [tbl_NextYear] where FY='Next Financial Year'";
                    udb.Save_Record(sSql);
                    createNextYearData();
                    cryRpt = (ReportDocument)Rs.NextStudentYearFeeActualPdf(1, "Next Financial Year", "admin");
                }

                else if (Vals[0] == "AR")
                {

                    if (Current == "Open")
                    {

                        cryRpt = (ReportDocument)Rs.AcademicReportPdf(1, YC, "admin");
                    }
                    else
                    {
                        cryRpt = (ReportDocument)Rs.AcademicReportPdf(1, YC, "admin");
                    }

                }

                else if (Vals[0] == "DT")
                {
                    if (Current == "Open")
                    {
                        createBudget(YC);
                        sSql = "select CONVERT (char(20),EndYearDate,106) from FinancialPeriods where YearCode='" + YC + "'";
                        string ArrName = udb.Select_Values(sSql);
                        cryRpt = (ReportDocument)Rs.DUEPROGRAMSPdf(1, "admin", YC, ArrName, "Current Financial Year");
                    }
                    else
                    {
                        sSql = "select CONVERT (char(20),getdate(),106)";
                        string ArrName = udb.Select_Values(sSql);
                        cryRpt = (ReportDocument)Rs.DUEPROGRAMSPdf(1, "admin", YC, ArrName, "Financial Year");
                    }
                }
                else if (Vals[0] == "RCB")
                {
                    cryRpt = (ReportDocument)Rs.FinancialYearRefundPdf(1, "admin");
                }
                else if (Vals[0] == "AFYDC")
                {
                    if (Current == "Open")
                    {

                        cryRpt = (ReportDocument)Rs.ALLFYDUEPdf(1, "admin");
                    }

                }
                else if (Vals[0] == "BAR")
                {

                    Reports.StudentReport RDStudent = new Reports.StudentReport();
                    cryRpt = (ReportDocument)RDStudent.StudentBatchAnalysisPdf(1, Vals[1].ToString(), "admin");


                }




                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\Audit.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1300px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/Audit.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        private void createBudget(string YearCode)
        {
            string StrYear = "SELECT B.[ProgramId],B.[BatchId],B.[YearId],[YearStatus],[NOS],[Actual],[Convenor],[APMonority] AS APSMFC,[PaidFee] AS Paid,[DueFee] As Due,[OtherFeeDue] AS OtherFeeDue,OtherFee FROM [Vw_Budeget] B,FinancialBatches  SE where  B.BatchId=SE.BatchId and B.YearId=SE.YearId and SE.Academic ='Current'";
            System.Data.DataSet dsDataYear = new System.Data.DataSet(), dsDetainYear = new System.Data.DataSet();
            dsDataYear = udb.GetData(StrYear, "Year");
            BudgetTableDesign();
            if (dsDataYear.Tables["Year"] != null)
            {
                for (int i = 0; i <= dsDataYear.Tables["Year"].Rows.Count - 1; i++)
                {
                    tr = BudgetTable.NewRow();
                    tr[0] = dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString();
                    tr[1] = dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString();
                    tr[2] = Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]);
                    tr[3] = Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["NOS"]);
                    tr[4] = dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString();
                    tr[5] = dsDataYear.Tables["Year"].Rows[i]["Convenor"].ToString();
                    tr[6] = dsDataYear.Tables["Year"].Rows[i]["APSMFC"].ToString();
                    string StrUFRO;
                    StrUFRO = udb.Select_Values("SELECT [UFRO]  FROM [Vw_BudegetU] where ProgramId='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and BatchId='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId=" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and YearStatus='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'");
                    if (StrUFRO == "")
                        tr[7] = 0;
                    else
                        tr[7] = StrUFRO;

                    string StrStudent;
                    StrStudent = udb.Select_Values("SELECT [Student]  FROM [Vw_BudegetS] where ProgramId='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and BatchId='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId=" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and YearStatus='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'");
                    if (StrStudent == "")
                        tr[8] = 0;
                    else
                        tr[8] = StrStudent;

                    tr[9] = dsDataYear.Tables["Year"].Rows[i]["OtherFeeDue"].ToString();
                    tr[10] = dsDataYear.Tables["Year"].Rows[i]["Paid"].ToString();
                    tr[11] = dsDataYear.Tables["Year"].Rows[i]["Due"].ToString();
                    tr[12] = dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString();


                    if (dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() == "ET")
                    {
                        string Var_APSMFC = "";
                        Var_APSMFC = udb.Select_Values("select sum(NOS) As NOS from [Vw_YQCCONTOF] where BatchId ='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId =" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and ProgramId ='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and YearStatus ='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'  and QuotaId not in('MGMT','UFRO')");
                        if (Var_APSMFC == "")
                            tr[13] = 0;
                        else
                            tr[13] = Var_APSMFC;

                        string Var_MGMT = "";
                        Var_MGMT = udb.Select_Values("select sum(NOS) As NOS from [Vw_YQCCONTOF] where BatchId ='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId =" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and ProgramId ='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and YearStatus ='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'  and QuotaId  in('MGMT')");
                        if (Var_MGMT == "")
                            tr[14] = 0;
                        else

                            tr[14] = Var_MGMT;

                        string Var_UFRO = "";
                        Var_UFRO = udb.Select_Values("select sum(NOS) As NOS from [Vw_YQCCONTOF] where BatchId ='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId =" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and ProgramId ='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and YearStatus ='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'  and QuotaId  in('UFRO')");
                        if (Var_UFRO == "")
                            tr[15] = 0;
                        else
                            tr[15] = Var_UFRO;

                    }
                    else
                    {
                        string Var_APSMFC = "";
                        Var_APSMFC = udb.Select_Values("select sum(NOS) As NOS from [Vw_YQCCONTOF] where BatchId ='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId =" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and ProgramId ='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and YearStatus ='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'  and QuotaId not in('MNGMNT')");
                        if (Var_APSMFC == "")
                            tr[13] = 0;
                        else
                            tr[13] = Var_APSMFC;

                        string Var_MGMT = "";
                        Var_MGMT = udb.Select_Values("select sum(NOS) As NOS from [Vw_YQCCONTOF] where BatchId ='" + dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString() + "' and YearId =" + Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + " and ProgramId ='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "' and YearStatus ='" + dsDataYear.Tables["Year"].Rows[i]["YearStatus"].ToString() + "'  and QuotaId  in('MNGMNT')");
                        if (Var_MGMT == "")
                            tr[14] = 0;
                        else
                            tr[14] = Var_MGMT;
                        tr[15] = "0";
                    }
                    tr[16] = dsDataYear.Tables["Year"].Rows[i]["OtherFee"].ToString();
                    BudgetTable.Rows.Add(tr);
                }
            }
            sSql = "Delete from tbl_Budget where YearCode='" + YearCode + "'";
            udb.Save_Record(sSql);
            for (int J = 0; J <= BudgetTable.Rows.Count - 1; J++)
            {
                string Program;
                sSql = "select  ProgramName from tbl_fees_Program where ProgramId='" + BudgetTable.Rows[J][0].ToString() + "'";
                Program = udb.Select_Values(sSql);
                sSql = "Insert Into [tbl_Budget] ([ProgramId],[BatchId],[YearId],[NOS],[ActualFee],[Convenor],[APSMFC],[UFRO],[Student],[OtherFee],[Paid],[Due],[Status],[YearCode],[APSMFC_NOS],[MGMT_NOS],[UFRO_NOS],[ActualOF]) values('" + Program + "',' " + BudgetTable.Rows[J][1].ToString() + "'," + Convert.ToInt32(BudgetTable.Rows[J][2]) + "," + Convert.ToInt32(BudgetTable.Rows[J][3]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][4]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][5]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][6]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][7]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][8]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][9]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][10]) + "," + Convert.ToDecimal(BudgetTable.Rows[J][11]) + ",'" + BudgetTable.Rows[J][12].ToString() + "','" + YearCode + "'," + BudgetTable.Rows[J][13].ToString() + "," + BudgetTable.Rows[J][14].ToString() + "," + BudgetTable.Rows[J][15].ToString() + "," + Convert.ToDecimal(BudgetTable.Rows[J][16].ToString()) + ")";
                udb.Save_Record(sSql);
            }
        }

        private void BudgetTableDesign()
        {
            try
            {
                BudgetTable = new DataTable();
                DataColumn dcRef;
                BudgetTable.Rows.Clear();
                BudgetTable.Columns.Clear();

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ProgramId";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "BatchId";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "YearId";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "NOS";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Actual";
                BudgetTable.Columns.Add(dcRef);




                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Convenor";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFC";
                BudgetTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFRO";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Student";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "OtherFee";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Paid";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Due";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "YearStatus";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFC_NOS";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "MGMT_NOS";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFRO_NOS";
                BudgetTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ActualOtherFee";
                BudgetTable.Columns.Add(dcRef);
            }
            catch (Exception ex)
            {
            }
        }


        private void FinancialReceived(string FY, string YearCode)
        {
            string StrMonths = "";
            string StrStudent;
            if (FY == "Current Financial Year")
                StrMonths = "SELECT StartYearDate,EndYearDate FROM [FinancialPeriods] where YearCode='" + YearCode + "' and Status='Open'";

            System.Data.DataSet dsDataMonths = new System.Data.DataSet();
            dsDataMonths = udb.GetData(StrMonths, "Months");
            if (dsDataMonths.Tables["Months"] != null)
            {
                string StrYears;
                StrYears = "SELECT distinct Current_FY ,BatchId,YearId,ProgramId  FROM tbl_fees_JEConsolidate where Current_FY='" + YearCode + "'";
                System.Data.DataSet dsYears = new System.Data.DataSet();
                dsYears = null;
                dsYears = udb.GetData(StrYears, "Years");
                if (dsYears != null)
                {
                    if (dsYears.Tables["Years"].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsYears.Tables["Years"].Rows.Count - 1; i++)
                        {
                            StrStudent = "SELECT Sum(FCD.[Amount]) As Amount FROM [tbl_fees_StudentFeeChallan] FC,tbl_fees_StudentFeeChallanDetails FCD,tbl_fees_StudentEnrollment SE,tbl_fees_FeesMaster FM,tbl_fees_OTAC O where FC.StudentId=SE.StudentId and FC.[TransactionId]=FCD.[TransactionId] and FM.FeeId=FCD.FeeId and FC.StudentId=O.StudentId and FC.YearId =O.YearId and SE.ProgramId='" + dsYears.Tables["Years"].Rows[i]["ProgramId"] + "' and  ChallanDate between '" + dsDataMonths.Tables["Months"].Rows[0]["StartYearDate"] + "' and '" + dsDataMonths.Tables["Months"].Rows[0]["EndYearDate"] + "' and FC.Status='Approved' and FC.Remarks<>'InActiveBeforeFinancialYear' and BatchId='" + dsYears.Tables["Years"].Rows[i]["BatchId"].ToString() + "' and FC.YearId=" + dsYears.Tables["Years"].Rows[i]["YearId"].ToString() + "   group by SE.BatchId ,FC.YearId";
                            System.Data.DataSet dsDataStudent = new System.Data.DataSet();
                            dsDataStudent = null;
                            dsDataStudent = udb.GetData(StrStudent, "REC");
                            if (dsDataStudent.Tables["REC"].Rows.Count > 0)
                            {
                                sSql = "update  tbl_fees_JEConsolidate set Due_Received=" + Convert.ToDecimal(dsDataStudent.Tables["REC"].Rows[0][0]) + " where  Current_FY='" + YearCode + "' and BatchId='" + dsYears.Tables["Years"].Rows[i]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[i]["YearId"].ToString() + " and ProgramId='" + dsYears.Tables["Years"].Rows[i]["ProgramId"] + "'";
                                udb.Save_Record(sSql);
                                sSql = "update  tbl_fees_JEConsolidate set Due=((Close_Balance + ReAdmission_amount)-(Due_Received + Det_Dis_amount)) where  Current_FY='" + YearCode + "' and BatchId='" + dsYears.Tables["Years"].Rows[i]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[i]["YearId"].ToString() + " and ProgramId='" + dsYears.Tables["Years"].Rows[i]["ProgramId"] + "'";
                                udb.Save_Record(sSql);
                            }
                            else
                            {
                                sSql = "update  tbl_fees_JEConsolidate set Due_Received=0  where Current_FY='" + YearCode + "' and BatchId='" + dsYears.Tables["Years"].Rows[i]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[i]["YearId"].ToString() + " and ProgramId='" + dsYears.Tables["Years"].Rows[i]["ProgramId"] + "'";
                                udb.Save_Record(sSql);
                                sSql = "update  tbl_fees_JEConsolidate set Due=((Close_Balance + ReAdmission_amount)-(Due_Received + Det_Dis_amount))  where Current_FY='" + YearCode + "' and BatchId='" + dsYears.Tables["Years"].Rows[i]["BatchId"].ToString() + "' and YearId=" + dsYears.Tables["Years"].Rows[i]["YearId"].ToString() + " and ProgramId='" + dsYears.Tables["Years"].Rows[i]["ProgramId"] + "'";
                                udb.Save_Record(sSql);
                            }
                        }
                    }
                }
            }
        }

        public void createYearBakUp()
        {
            string StrDateBase;
            string sSQl;


            StrDateBase = "SELECT O.[BatchId],O.[YearId],O.[StudentId],[DisplayName], (isnull(Actual,0)+isnull(OActual,0)) As Actual,(isnull(Paid,0)+isnull(OPaid,0)) As Paid,(isnull(Due,0)+isnull(ODue,0)) As Due ,O.ProgramId   FROM [Vw_OTACTFOF] O,tbl_fees_StudentEnrollment SE,[FinancialBatches] FB  where O.StudentId =SE.StudentId and O.YearId =SE.YearId  and (isnull(Due,0)+isnull(ODue,0))>0 and O.Status='Active' and FB.Academic ='Current' and FB.BatchId=SE.BatchId and FB.YearId=SE.YearId";

            System.Data.DataSet dsDataMonths = new System.Data.DataSet();
            dsDataMonths = udb.GetData(StrDateBase, "StudentBaK");
            CreateBUTable();

            if (dsDataMonths.Tables["StudentBaK"] != null)
            {
                for (int DB = 0; DB <= dsDataMonths.Tables["StudentBaK"].Rows.Count - 1; DB++)
                {
                    tr = tempBUDatetable.NewRow();
                    tr[0] = dsDataMonths.Tables["StudentBaK"].Rows[DB]["StudentId"].ToString();
                    tr[1] = dsDataMonths.Tables["StudentBaK"].Rows[DB]["YearId"].ToString();
                    tr[2] = dsDataMonths.Tables["StudentBaK"].Rows[DB]["Actual"].ToString();
                    tr[3] = dsDataMonths.Tables["StudentBaK"].Rows[DB]["Paid"].ToString();
                    tr[4] = dsDataMonths.Tables["StudentBaK"].Rows[DB]["Due"].ToString();
                    tr[5] = dsDataMonths.Tables["StudentBaK"].Rows[DB]["ProgramId"].ToString();
                    tempBUDatetable.Rows.Add(tr);
                }
            }

            string strYC = "";

            sSQl = "Select [YearCode] from [FinancialPeriods] where Status='Open'";
            strYC = udb.Select_Values(sSQl);

            sSQl = "delete from [tbl_fees_FinancialYearBak] where YearCode='" + strYC + "'";
            udb.Select_Values(sSQl);

            for (int L = 0; L <= tempBUDatetable.Rows.Count - 1; L++)
            {
                sSQl = "Insert Into [tbl_fees_FinancialYearBak] ([StudentId],[YearId],[Actual],[Paid],[Due],[ProgramId],[YearCode]) Values ('" + tempBUDatetable.Rows[L][0].ToString() + "'," + Convert.ToInt32(tempBUDatetable.Rows[L][1].ToString()) + "," + Convert.ToDecimal(tempBUDatetable.Rows[L][2]) + "," + Convert.ToDecimal(tempBUDatetable.Rows[L][3]) + "," + Convert.ToDecimal(tempBUDatetable.Rows[L][4]) + ",'" + tempBUDatetable.Rows[L][5] + "','" + strYC + "')";
                udb.Save_Record(sSQl);
            }
        }

        private void CreateBUTable()
        {
            try
            {
                tempBUDatetable = new DataTable();
                DataColumn dcRef;
                tempBUDatetable.Rows.Clear();
                tempBUDatetable.Columns.Clear();

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "StudentId";
                tempBUDatetable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Year";
                tempBUDatetable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Actual";
                tempBUDatetable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Paid";
                tempBUDatetable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Due";
                tempBUDatetable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ProgramId";
                tempBUDatetable.Columns.Add(dcRef);
            }


            catch (Exception ex)
            {
            }
        }

        private void createNextYearData()
        {
            string StrYear = "SELECT B.[ProgramId],B.[BatchId],B.[YearId],[YearStatus],[NOS],[Actual],[Convenor],[APMonority] AS APSMFC,[PaidFee] AS Paid,[DueFee] As Due,[OtherFeeDue] AS OtherFeeDue,OtherFee FROM [Vw_Budeget] B,FinancialBatches  SE where  B.BatchId=SE.BatchId and B.YearId=SE.YearId and SE.Academic ='Current' and YearStatus='Active'";
            System.Data.DataSet dsDataYear = new System.Data.DataSet(), dsDetainYear = new System.Data.DataSet();
            dsDataYear = udb.GetData(StrYear, "Year");
            NextYeartempTable();
            if (dsDataYear.Tables["Year"] != null)
            {
                for (int i = 0; i <= dsDataYear.Tables["Year"].Rows.Count - 1; i++)
                {
                    tr = NexttempTable.NewRow();

                    tr[0] = dsDataYear.Tables["Year"].Rows[i]["BatchId"].ToString();
                    tr[1] = Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["YearId"]) + 1;

                    if (Convert.ToInt32(tr[1]) == 2)
                    {
                        if (dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() == "ET")
                        {
                            int NOS = Convert.ToInt32(udb.Select_Values("select [NOS] from [tbl_fees_NextYearEligiblestudent] where YearId=2"));
                            tr[2] = Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["NOS"]) + NOS;
                        }
                        else
                            tr[2] = Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["NOS"]);
                    }
                    else
                        tr[2] = Convert.ToInt32(dsDataYear.Tables["Year"].Rows[i]["NOS"]);

                    if (Convert.ToInt32(tr[1]) == 2)
                    {
                        if (dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() == "ET")
                        {
                            decimal LE = Convert.ToDecimal(udb.Select_Values("select [Actual] from [tbl_fees_NextYearEligiblestudent] where YearId=2"));
                            tr[3] = Convert.ToDecimal(dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString()) + LE;
                        }
                        else
                            tr[3] = dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString();
                    }
                    else
                        tr[3] = dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString();

                    tr[4] = "0";
                    if (Convert.ToInt32(tr[1]) == 2)
                    {
                        if (dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() == "ET")
                        {
                            decimal LE = Convert.ToDecimal(udb.Select_Values("select [Actual] from [tbl_fees_NextYearEligiblestudent] where YearId=2"));
                            tr[5] = Convert.ToDecimal(dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString()) + LE;
                        }
                        else
                            tr[5] = dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString();
                    }
                    else
                        tr[5] = dsDataYear.Tables["Year"].Rows[i]["Actual"].ToString();
                    string Program;
                    sSql = "select  ProgramName from tbl_fees_Program where ProgramId='" + dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString() + "'";
                    Program = udb.Select_Values(sSql);
                    tr[6] = Program;
                    tr[7] = "Next Financial Year";
                    tr[8] = dsDataYear.Tables["Year"].Rows[i]["ProgramId"].ToString();
                    NexttempTable.Rows.Add(tr);
                }
                string StrNewRegister = "SELECT [BatchId],[ProgramId],[YearId],[Actual],[NOS] from  [tbl_fees_NextYearEligiblestudent] where  YearId=1";
                System.Data.DataSet DsNewRegister = new System.Data.DataSet();
                DsNewRegister = udb.GetData(StrNewRegister, "NR");
                if (DsNewRegister.Tables["NR"] != null)
                {
                    for (int j = 0; j <= DsNewRegister.Tables["NR"].Rows.Count - 1; j++)
                    {
                        tr = NexttempTable.NewRow();
                        tr[0] = DsNewRegister.Tables["NR"].Rows[j]["BatchId"].ToString();
                        tr[1] = Convert.ToInt32(DsNewRegister.Tables["NR"].Rows[j]["YearId"]);
                        tr[2] = Convert.ToInt32(DsNewRegister.Tables["NR"].Rows[j]["NOS"]);

                        tr[3] = DsNewRegister.Tables["NR"].Rows[j]["Actual"].ToString();
                        tr[4] = "0";
                        tr[5] = DsNewRegister.Tables["NR"].Rows[j]["Actual"].ToString();

                        string Program;
                        sSql = "select  ProgramName from tbl_fees_Program where ProgramId='" + DsNewRegister.Tables["NR"].Rows[j]["ProgramId"].ToString() + "'";
                        Program = udb.Select_Values(sSql);
                        tr[6] = Program;
                        tr[7] = "Next Financial Year";
                        tr[8] = DsNewRegister.Tables["NR"].Rows[j]["ProgramId"].ToString();
                        NexttempTable.Rows.Add(tr);
                    }
                }
            }
            for (int J = 0; J <= NexttempTable.Rows.Count - 1; J++)
            {
                int Period;
                sSql = "select  Period from tbl_fees_Program where ProgramId='" + NexttempTable.Rows[J][8].ToString() + "'";
                Period = Convert.ToInt32(udb.Select_Values(sSql));
                if (Convert.ToInt32(NexttempTable.Rows[J][1]) <= Period)
                {
                    sSql = "Insert Into [tbl_NextYear] ([BatchId], [YearId], [NOS],[ActualFee],[Paid],[Due],[ProgramId],[FY]) values('" + NexttempTable.Rows[J][0].ToString() + "', " + Convert.ToInt32(NexttempTable.Rows[J][1]) + "," + Convert.ToInt32(NexttempTable.Rows[J][2].ToString()) + "," + Convert.ToDecimal(NexttempTable.Rows[J][3]) + "," + Convert.ToDecimal(NexttempTable.Rows[J][4]) + "," + Convert.ToDecimal(NexttempTable.Rows[J][5]) + ",'" + NexttempTable.Rows[J][6].ToString() + "','" + NexttempTable.Rows[J][7].ToString() + "')";
                    udb.Save_Record(sSql);
                }
            }
        }
        private void NextYeartempTable()
        {
            try
            {
                NexttempTable = new DataTable();
                DataColumn dcRef;
                NexttempTable.Rows.Clear();
                NexttempTable.Columns.Clear();


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "BatchId";
                NexttempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "YearId";
                NexttempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "NOS";
                NexttempTable.Columns.Add(dcRef);



                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Actual";
                NexttempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Paid";
                NexttempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Due";
                NexttempTable.Columns.Add(dcRef);


                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Program";
                NexttempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "FY";
                NexttempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "ProgramId";
                NexttempTable.Columns.Add(dcRef);
            }
            catch (Exception ex)
            {
            }
        }


        public ActionResult FinancialYearDueReports()
        {
            AcademicContext Ac = new AcademicContext();
            Session["Header"] = Request.QueryString["UT"];

            var model = new RequireElements();

            var mainProgram = GetProgramsAll();
            model.Programs = GetSelectListItems(mainProgram);
            var FY = Ac.GetFinancialYears();
            model.YearCodes = GetSelectListItems(FY);

            var status = GetStatus();
            model.Statuses = GetSelectListItems(status);



            var RT = GetReportType();
            model.ReportTypes = GetSelectListItems(RT);

            return View(model);
        }

        public ActionResult GenerateFinancialYearDueReports(String Param)
        {
            AcademicContext sdb = new AcademicContext();
            RequireElements req = new RequireElements();
            string[] Vals = Regex.Split(Param, @"/");
            if (Vals != null)
            {
                Reports.FeesReport Rs = new Reports.FeesReport();

                ReportDocument cryRpt;
                cryRpt = null;
                string PName;
                string YC = udb.Select_Values("select  YearCode  from [FinancialPeriods] where SNo='" + Vals[1].ToString() + "'");
                string Current = udb.Select_Values("select  Status  from [FinancialPeriods] where SNo='" + Vals[1].ToString() + "'");
                if (Vals[2].ToString() != "0")
                {
                    PName = sdb.Select_Values("Select ProgramName from tbl_fees_Program where ProgramId='" + Vals[2].ToString() + "'");
                }
                else
                {
                    PName = "All";
                }

                DateTime currentdt = DateTime.Now.Date;
                if (Vals[0].ToString() == "FY")
                {
                    sSql = "delete from [tbl_fees_QFinancial]";
                    udb.Save_Record(sSql);
                    createCurrentData(YC, Vals[4].ToString(), Vals[2].ToString());
                    sSql = "select distinct CONVERT (char(20),[MTo],106) from  [FinancialBatches] where [YearCode]='" + YC + "'";
                    string ArrName = udb.Select_Values(sSql);
                    cryRpt = (ReportDocument)Rs.FinancialYearPdf(1, YC, ArrName);
                }
                else if (Vals[0].ToString() == "FYQW")
                {
                    sSql = "delete from [tbl_fees_QFinancial]";
                    udb.Save_Record(sSql);
                    if (Vals[3].ToString() == "Actuals")
                    {
                        createCurrentData(YC, Vals[4].ToString(), Vals[2].ToString());
                        sSql = "select CONVERT (char(20),getdate(),106)";
                        string ArrName = udb.Select_Values(sSql);
                        cryRpt = (ReportDocument)Rs.FinancialYearQuotaWisePdf(1, YC, ArrName, "admin");

                    }
                    else
                    {
                        createCurrentDataTOF(YC, Vals[4].ToString(), Vals[2].ToString(), PName);
                        sSql = "select CONVERT (char(20),getdate(),106)";
                        string ArrName = udb.Select_Values(sSql);
                        cryRpt = (ReportDocument)Rs.FinancialYearQuotaWiseTOFPdf(1, YC, ArrName, "admin");
                    }

                }

                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\Audit.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1300px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/Audit.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            return PartialView("_ReportViewer");
        }

        private void CreatetempTable()
        {
            try
            {
                tempTable = new DataTable();
                DataColumn dcRef;
                tempTable.Rows.Clear();
                tempTable.Columns.Clear();

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Batch";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Year";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFC";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Management";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFRO";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Student";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Convenor";
                tempTable.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "OtherFee";
                tempTable.Columns.Add(dcRef);
            }
            catch (Exception ex)
            {
            }
        }

        public void createCurrentData(string YearCode, string Status, string Program)
        {
            string sSQl;
            string StrBatches;
            if (Program != "0")
                StrBatches = "SELECT  FB.YearCode ,FR.[BatchId],FR.[YearId] FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and YearCode ='" + YearCode + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
            else
                StrBatches = "SELECT  FB.YearCode ,FR.[BatchId],FR.[YearId] FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and YearCode ='" + YearCode + "' and FR.Status='" + Status + "'   group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
            System.Data.DataSet dsDataBatches = new System.Data.DataSet();
            System.Data.DataSet dsDataAPSMFC = new System.Data.DataSet();
            System.Data.DataSet dsDataMgmt = new System.Data.DataSet();
            System.Data.DataSet dsDataUFRO = new System.Data.DataSet();
            System.Data.DataSet dsDataStudent = new System.Data.DataSet();
            System.Data.DataSet dsDataConvenor = new System.Data.DataSet();
            System.Data.DataSet dsDataOF = new System.Data.DataSet();
            dsDataBatches = udb.GetData(StrBatches, "Batches");
            CreatetempTable();

            if (dsDataBatches.Tables["Batches"] != null)
            {
                for (int i = 0; i <= dsDataBatches.Tables["Batches"].Rows.Count - 1; i++)
                {
                    tr = tempTable.NewRow();
                    tr[0] = dsDataBatches.Tables["Batches"].Rows[i]["BatchId"].ToString();
                    tr[1] = dsDataBatches.Tables["Batches"].Rows[i]["YearId"].ToString();
                    string StrAPSMFC;
                    if (Program != "0")
                        StrAPSMFC = "SELECT  Sum(isnull([APMinority],0)) AS APMinority FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status  ";
                    else
                        StrAPSMFC = "SELECT  Sum(isnull([APMinority],0)) AS APMinority FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status  ";
                    dsDataAPSMFC = udb.GetData(StrAPSMFC, "APSMFC");
                    if (dsDataAPSMFC.Tables["APSMFC"].Rows.Count > 0)
                    {
                        if (dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString() != "")
                            tr[2] = dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString();
                        else
                            tr[2] = 0;
                    }
                    else
                        tr[2] = 0;

                    string StrMgmt;
                    if (Program != "0")
                        StrMgmt = "SELECT  Sum(isnull([Student],0)) AS Management   FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and QuotaId='MGMT'  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
                    else
                        StrMgmt = "SELECT  Sum(isnull([Student],0)) AS Management   FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and QuotaId='MGMT'  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
                    dsDataMgmt = udb.GetData(StrMgmt, "Mgmt");
                    if (dsDataMgmt.Tables["Mgmt"].Rows.Count > 0)
                    {
                        if (dsDataMgmt.Tables["Mgmt"] != null)
                            tr[3] = dsDataMgmt.Tables["Mgmt"].Rows[0][0].ToString();
                        else
                            tr[3] = 0;
                    }
                    else
                        tr[3] = 0;

                    string StrUFRO;
                    if (Program != "0")
                        StrUFRO = "SELECT  Sum(isnull([Student],0)) AS UFRO   FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId='UFRO' and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
                    else
                        StrUFRO = "SELECT  Sum(isnull([Student],0)) AS UFRO   FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId='UFRO' and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "'   group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
                    dsDataUFRO = udb.GetData(StrUFRO, "UFRO");

                    if (dsDataUFRO.Tables["UFRO"].Rows.Count > 0)
                    {
                        if (dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString() != "")
                            tr[4] = dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString();
                        else
                            tr[4] = 0;
                    }
                    else
                        tr[4] = 0;

                    string StrStudent;
                    if (Program != "0")
                        StrStudent = "SELECT Sum(isnull([Student],0)) AS Student  FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId not in('MGMT','UFRO')  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
                    else
                        StrStudent = "SELECT Sum(isnull([Student],0)) AS Student  FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId not in('MGMT','UFRO')  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status";
                    dsDataStudent = udb.GetData(StrStudent, "Student");
                    if (dsDataStudent.Tables["Student"].Rows.Count > 0)
                    {
                        if (dsDataStudent.Tables["Student"].Rows[0][0].ToString() != "")
                            tr[5] = dsDataStudent.Tables["Student"].Rows[0][0].ToString();
                        else
                            tr[5] = 0;
                    }
                    else
                        tr[5] = 0;

                    string StrConvenor;
                    if (Program != "0")
                        StrConvenor = "SELECT  Sum(isnull([Convinor],0)) AS Convenor FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status  ";
                    else
                        StrConvenor = "SELECT  Sum(isnull([Convinor],0)) AS Convenor FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status  ";
                    dsDataConvenor = udb.GetData(StrConvenor, "Convenor");
                    if (dsDataConvenor.Tables["Convenor"].Rows.Count > 0)
                    {
                        if (dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString() != "")
                            tr[6] = dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString();
                        else
                            tr[6] = 0;
                    }
                    else
                        tr[6] = 0;

                    string StrOF;
                    if (Program != "0")
                        StrOF = "SELECT  Sum(isnull([ODue],0)) AS Convenor FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status  ";
                    else
                        StrOF = "SELECT  Sum(isnull([ODue],0)) AS Convenor FROM [Vw_OTACTFOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.Status='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.Status  ";
                    dsDataOF = udb.GetData(StrOF, "OF");
                    if (dsDataOF.Tables["OF"].Rows.Count > 0)
                    {
                        if (dsDataOF.Tables["OF"].Rows[0][0].ToString() != "")
                            tr[7] = dsDataOF.Tables["OF"].Rows[0][0].ToString();
                        else
                            tr[7] = 0;
                    }
                    else
                        tr[7] = 0;





                    tempTable.Rows.Add(tr);
                }

                for (int J = 0; J <= tempTable.Rows.Count - 1; J++)
                {
                    sSQl = "Insert Into [tbl_fees_QFinancial] ([BatchId], [YearId], [APSMFC],[Management],[UFRO],[Student],[Convenor],[OtherFee]) Values ('" + tempTable.Rows[J][0].ToString() + "', " + Convert.ToInt32(tempTable.Rows[J][1]) + "," + Convert.ToDecimal(tempTable.Rows[J][2].ToString()) + "," + Convert.ToDecimal(tempTable.Rows[J][3].ToString()) + "," + Convert.ToDecimal(tempTable.Rows[J][4].ToString()) + "," + Convert.ToDecimal(tempTable.Rows[J][5].ToString()) + "," + Convert.ToDecimal(tempTable.Rows[J][6].ToString()) + "," + Convert.ToDecimal(tempTable.Rows[J][7].ToString()) + ")";
                    udb.Save_Record(sSQl);
                }

            }

        }

        private void CreatetempTableTOF()
        {
            try
            {
                tempTableTOF = new DataTable();
                DataColumn dcRef;
                tempTableTOF.Rows.Clear();
                tempTableTOF.Columns.Clear();

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Batch";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Year";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "APSMFC";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Management";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "UFRO";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Student";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "Convenor";
                tempTableTOF.Columns.Add(dcRef);

                dcRef = new DataColumn();
                dcRef.DataType = Type.GetType("System.String");
                dcRef.ColumnName = "OtherFee";
                tempTableTOF.Columns.Add(dcRef);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public void createCurrentDataTOF(string YearCode, string Status, string Program, string PName)
        {
            string sSQl;
            string StrBatches;
            if (Program != "0")
                StrBatches = "SELECT  FB.YearCode ,FR.[BatchId],FR.[YearId] FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and YearCode ='" + YearCode + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
            else
                StrBatches = "SELECT  FB.YearCode ,FR.[BatchId],FR.[YearId] FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and YearCode ='" + YearCode + "' and FR.YearStatus='" + Status + "'   group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
            System.Data.DataSet dsDataBatches = new System.Data.DataSet();
            System.Data.DataSet dsDataAPSMFC = new System.Data.DataSet();
            System.Data.DataSet dsDataMgmt = new System.Data.DataSet();
            System.Data.DataSet dsDataUFRO = new System.Data.DataSet();
            System.Data.DataSet dsDataStudent = new System.Data.DataSet();
            System.Data.DataSet dsDataConvenor = new System.Data.DataSet();
            System.Data.DataSet dsDataOF = new System.Data.DataSet();
            dsDataBatches = udb.GetData(StrBatches, "Batches");
            CreatetempTableTOF();
            if (dsDataBatches.Tables["Batches"] != null)
            {
                for (int i = 0; i <= dsDataBatches.Tables["Batches"].Rows.Count - 1; i++)
                {
                    tr = tempTableTOF.NewRow();
                    tr[0] = dsDataBatches.Tables["Batches"].Rows[i]["BatchId"].ToString();
                    tr[1] = dsDataBatches.Tables["Batches"].Rows[i]["YearId"].ToString();
                    string StrAPSMFC;
                    if (Program != "0")
                        StrAPSMFC = "SELECT  Sum([APMonority]) AS APMinority FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus  ";
                    else
                        StrAPSMFC = "SELECT  Sum([APMonority]) AS APMinority FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus  ";
                    dsDataAPSMFC = udb.GetData(StrAPSMFC, "APSMFC");
                    if (dsDataAPSMFC.Tables["APSMFC"].Rows.Count > 0)
                    {
                        if (dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString() != "")
                            tr[2] = dsDataAPSMFC.Tables["APSMFC"].Rows[0][0].ToString();
                        else
                            tr[2] = 0;
                    }
                    else
                        tr[2] = 0;

                    string StrMgmt;
                    if (Program != "0")
                        StrMgmt = "SELECT  Sum([Student]) AS Management   FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and QuotaId='MGMT'  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
                    else
                        StrMgmt = "SELECT  Sum([Student]) AS Management   FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and QuotaId='MGMT'  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
                    dsDataMgmt = udb.GetData(StrMgmt, "Mgmt");
                    if (dsDataMgmt.Tables["Mgmt"].Rows.Count > 0)
                    {
                        if (dsDataMgmt.Tables["Mgmt"] != null)
                            tr[3] = dsDataMgmt.Tables["Mgmt"].Rows[0][0].ToString();
                        else
                            tr[3] = 0;
                    }
                    else
                        tr[3] = 0;

                    string StrUFRO;
                    if (Program != "0")
                        StrUFRO = "SELECT  Sum([Student]) AS UFRO   FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId='UFRO' and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
                    else
                        StrUFRO = "SELECT  Sum([Student]) AS UFRO   FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId='UFRO' and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "'   group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
                    dsDataUFRO = udb.GetData(StrUFRO, "UFRO");

                    if (dsDataUFRO.Tables["UFRO"].Rows.Count > 0)
                    {
                        if (dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString() != "")
                            tr[4] = dsDataUFRO.Tables["UFRO"].Rows[0][0].ToString();
                        else
                            tr[4] = 0;
                    }
                    else
                        tr[4] = 0;

                    string StrStudent;
                    if (Program != "0")
                        StrStudent = "SELECT Sum([Student]) AS Student  FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId not in('MGMT','UFRO')  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
                    else
                        StrStudent = "SELECT Sum([Student]) AS Student  FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId  and QuotaId not in('MGMT','UFRO')  and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus";
                    dsDataStudent = udb.GetData(StrStudent, "Student");
                    if (dsDataStudent.Tables["Student"].Rows.Count > 0)
                    {
                        if (dsDataStudent.Tables["Student"].Rows[0][0].ToString() != "")
                            tr[5] = dsDataStudent.Tables["Student"].Rows[0][0].ToString();
                        else
                            tr[5] = 0;
                    }
                    else
                        tr[5] = 0;

                    string StrConvenor;
                    if (Program != "0")
                        StrConvenor = "SELECT  Sum([Convinor] ) AS Convenor FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus  ";
                    else
                        StrConvenor = "SELECT  Sum([Convinor] ) AS Convenor FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus  ";
                    dsDataConvenor = udb.GetData(StrConvenor, "Convenor");
                    if (dsDataConvenor.Tables["Convenor"].Rows.Count > 0)
                    {
                        if (dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString() != "")
                            tr[6] = dsDataConvenor.Tables["Convenor"].Rows[0][0].ToString();
                        else
                            tr[6] = 0;
                    }
                    else
                        tr[6] = 0;

                    string StrOF;
                    if (Program != "0")
                        StrOF = "SELECT  Sum(isnull([OtherFeeDue],0) ) AS OtherFeeDue FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "' and FR.ProgramId='" + Program + "'  group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus  ";
                    else
                        StrOF = "SELECT  Sum(isnull([OtherFeeDue],0) ) AS OtherFeeDue FROM [Vw_YQCCONTOF] FR,[FinancialBatches] FB where FB.BatchId =FR.BatchId and FB.YearId=FR.YearId   and YearCode ='" + YearCode + "' and FR.BatchId='" + tr[0].ToString() + "' and FR.YearId='" + tr[1].ToString() + "' and FR.YearStatus='" + Status + "'   group by YearCode ,FR.BatchId,FR.YearId,FR.YearStatus  ";
                    dsDataOF = udb.GetData(StrOF, "OF");
                    if (dsDataOF.Tables["OF"].Rows.Count > 0)
                    {
                        if (dsDataOF.Tables["OF"].Rows[0][0].ToString() != "")
                            tr[7] = dsDataOF.Tables["OF"].Rows[0][0].ToString();
                        else
                            tr[7] = 0;
                    }
                    else
                        tr[7] = 0;

                    tempTableTOF.Rows.Add(tr);
                }

                for (int J = 0; J <= tempTableTOF.Rows.Count - 1; J++)
                {
                    if (Program != "0")
                    {
                        sSQl = "Insert Into [tbl_fees_QFinancial] ([BatchId], [YearId], [APSMFC],[Management],[UFRO],[Student],[Convenor],[OtherFee],[PName]) Values ('" + tempTableTOF.Rows[J][0].ToString() + "', " + Convert.ToInt32(tempTableTOF.Rows[J][1]) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][2].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][3].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][4].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][5].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][6].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][7].ToString()) + ",'" + PName + "')";
                        udb.Save_Record(sSQl);
                    }
                    else
                    {
                        sSQl = "Insert Into [tbl_fees_QFinancial] ([BatchId], [YearId], [APSMFC],[Management],[UFRO],[Student],[Convenor],[OtherFee],[PName]) Values ('" + tempTableTOF.Rows[J][0].ToString() + "', " + Convert.ToInt32(tempTableTOF.Rows[J][1]) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][2].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][3].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][4].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][5].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][6].ToString()) + "," + Convert.ToDecimal(tempTableTOF.Rows[J][7].ToString()) + ",'All')";
                        udb.Save_Record(sSQl);
                    }
                }
            }
        }

    }
}