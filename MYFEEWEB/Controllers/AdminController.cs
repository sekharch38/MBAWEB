using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYFEELIB.Domain;
using MYFEELIB.Entities;
using MYFEELIB.Data;
using WebApplication.Filters;
using AutoCompleteInMVCJson.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing;
using System.IO;
using QRCoder;
using System.Text.RegularExpressions;

namespace MYFEEWEB.Controllers
{
    [SessionTimeout]
    public class AdminController : Controller
    {
        List<Modules> lstModules = new List<Modules>();
        AccountService service = new AccountService();
        UserContext UC = new UserContext();



        public ActionResult DashBoard()
        {
            //Session["Header"] = "Dash Board";
            Session["Header"] = Request.QueryString["UT"];
            DataSet DsStudents = UC.GetActiveStudents();
            Session["ActiveStudent"] = DsStudents;
            return View();
        }




        public JsonResult FillStudents(int RId)
        {

            try
            {
                DataSet Ds = new DataSet();
                Ds = (Session["ActiveStudent"]) as DataSet;
                List<ActiveStudents> Students = new List<ActiveStudents>();
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    Students.Add(new ActiveStudents
                    {
                        Year = dr["Year"].ToString(),
                        NOS = Convert.ToInt32(dr["NOS"]),

                    });
                }
                foreach (DataRow dr in Ds.Tables[1].Rows)
                {
                    Students.Add(new ActiveStudents
                    {
                        Year = dr["Year"].ToString(),
                        NOS = Convert.ToInt32(dr["NOS"]),

                    });
                }
                foreach (DataRow dr in Ds.Tables[2].Rows)
                {

                    Students.Add(new ActiveStudents
                    {
                        Year = dr["Year"].ToString(),
                        NOS = Convert.ToInt32(dr["NOS"]),

                    });
                }
                foreach (DataRow dr in Ds.Tables[3].Rows)
                {

                    Students.Add(new ActiveStudents
                    {
                        Year = dr["Year"].ToString(),
                        NOS = Convert.ToInt32(dr["NOS"]),

                    });
                }


                if (Students.Count == 0)
                {
                    Students = null;
                    return Json(Students, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(Students.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }


        public JsonResult FillProgramStudents(int RId)
        {

            try
            {
                DataSet Ds = new DataSet();
                Ds = (Session["ActiveStudent"]) as DataSet;
                List<Program_Students> ProgramStudents = new List<Program_Students>();
                foreach (DataRow dr in Ds.Tables[4].Rows)
                {
                    ProgramStudents.Add(new Program_Students
                    {
                        ProgramName = dr["ProgramName"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Year = dr["Year"].ToString(),
                        NOS = Convert.ToInt32(dr["NOS"]),

                    });
                }



                if (ProgramStudents.Count == 0)
                {
                    ProgramStudents = null;
                    return Json(ProgramStudents, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(ProgramStudents.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult FillUsers(int RId)
        {

            try
            {
                DataSet Ds = new DataSet();
                Ds = (Session["ActiveStudent"]) as DataSet;
                List<User> users = new List<User>();
                foreach (DataRow dr in Ds.Tables[5].Rows)
                {
                    users.Add(new User
                    {
                        Username = dr["Name"].ToString(),
                        Name = dr["DisplayName"].ToString(),
                        Form_Access = dr["Form_Access"].ToString(),

                    });
                }



                if (users.Count == 0)
                {
                    users = null;
                    return Json(users, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(users.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult FillModules(int RId)
        {

            try
            {
                var modules = new List<Modules>();
                Session["RId"] = RId;
                if (Session["Modules"] != null)
                {
                    DataSet dataset = new DataSet();
                    dataset = (Session["Modules"]) as DataSet;
                    modules = GetModules(dataset.Tables[0]);

                }
                else
                {
                    DataSet dataset = new DataSet();
                    dataset = service.GetModules(RId);
                    modules = GetModules(dataset.Tables[0]);
                    Session["Modules"] = dataset;

                }


                return Json(modules.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public List<Modules> GetModules(DataTable Table)
        {
            List<Modules> ModList = new List<Modules>();
            try
            {
                foreach (DataRow dr in Table.Rows)
                {
                    Modules item = new Modules()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        F_Class = dr["F_Class"].ToString(),
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



        public List<SubModule> GetSubModules(DataTable Table)
        {
            List<SubModule> subModList = new List<SubModule>();
            try
            {
                foreach (DataRow dr in Table.Rows)
                {
                    SubModule item = new SubModule()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        F_Id = Convert.ToInt32(dr["F_Id"]),
                        cont = dr["Controller"].ToString(),
                        WebPage = dr["WebPage"].ToString(),
                        Name = dr["F_FormName"].ToString(),
                    };
                    subModList.Add(item);
                }

            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return subModList;
        }

        public JsonResult FillSubModules(int SId)
        {

            try
            {

                DataSet dataset = new DataSet();
                dataset = (Session["Modules"]) as DataSet;
                List<SubModule> submodTypes = GetSubModules(dataset.Tables[1]);
                var SModule = submodTypes.Where(x => x.Id == SId).ToList();
                return Json(SModule.ToArray(), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult RoleSubModules(int RId)
        {

            try
            {

                DataSet dataset = new DataSet();
                dataset = (Session["Modules"]) as DataSet;
                List<SubModule> submodTypes = GetSubModules(dataset.Tables[1]);
                var SModule = submodTypes.ToList();
                return Json(SModule.ToArray(), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }


        public JsonResult SelectRoleSubModules(int RId)
        {

            try
            {

                UserContext sdb = new UserContext();
                List<SubModule> submodTypes = sdb.GetSELECTROLEModules(RId);
                var SelectModule = submodTypes.ToList();
                return Json(SelectModule.ToArray(), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }



        public JsonResult AccessUpdate(IEnumerable<User> Access)
        {

            UserContext sdb = new UserContext();
            User data = new User();
            int rescount = 0;
            int insertedRecords = 0;
            if (Access != null)
            {
                foreach (var Acd in Access)
                {
                    data.Username = Acd.Username;
                    data.Form_Access = Acd.Form_Access;

                }
                rescount = sdb.FormAccess(data);

            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        [HttpGet]
        public ActionResult DiscontinueFeeWaiver()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new FeeWaiver();
            return View(model);
        }


        [HttpGet]
        public ActionResult RegularFeeWaiver()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new StudentFeeMaster();
            return View(model);
        }

        public JsonResult StudentFeeDetails(string Student)
        {

            AcademicContext sdb = new AcademicContext();
            var Waiver_Status = sdb.GetFeeWaiverStatus(Student);
            if (Waiver_Status == "Fee Waiver Not Done")
            {
                var FillAmount = sdb.GetStudentFeeDetails(Student, "All");

                if (FillAmount.Count == 0)
                {
                    FillAmount = null;
                    return Json(FillAmount, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(FillAmount.ToArray(), JsonRequestBehavior.AllowGet);
            }
            else

                return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Generate_BC(string student)
        {
            AcademicContext sdb = new AcademicContext();
            var status = sdb.Generate_BC(student);
            //if (status == 1)
            //{

            //    Reports.StudentReport Rs = new Reports.StudentReport();
            //    ReportDocument cryRpt;
            //    if (student != null)
            //    {

            //        cryRpt = (ReportDocument)Rs.BCPreviewDoc(1, "20MBMB2", "BC");
            //        string FilePath = Server.MapPath("~/ReportFiles");
            //        FilePath = FilePath + "\\BC.pdf";
            //        cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
            //        string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
            //        embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            //        embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            //        embed += "</object>";
            //        TempData["BC"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/BC.pdf"));
            //        cryRpt.Close();
            //        cryRpt.Dispose();
            //        //return RedirectToAction("Index");
            //    }
            //}
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentBC(string RollNo)
        {
            AcademicContext sdb = new AcademicContext();
            //var status = sdb.Generate_BC(RollNo);
            //if (status == 1)
            //{
            //}
            Reports.StudentReport Rs = new Reports.StudentReport();
            ReportDocument cryRpt;
            if (RollNo != null)
            {

                cryRpt = (ReportDocument)Rs.BCPreviewDoc(1, "20MBMB2", "BC");
                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\BC.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/BC.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();

            }
            return PartialView("_ReportViewer");
        }

        public ActionResult StudentIdCard(string RollNo)
        {
            AcademicContext sdb = new AcademicContext();
            string Var_StudentId;
            Var_StudentId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + RollNo + "'");
            if (Var_StudentId != null)
            {
                if (Var_StudentId != "")
                {
                    byte[] databar;
                    byte[] dataQR;
                    string barCode = RollNo;
                    //string barCode = "123456789123456";

                    using (Bitmap bitMap = new Bitmap(barCode.Length * 26, 80))
                    {
                        using (Graphics graphics__1 = Graphics.FromImage(bitMap))
                        {
                            Font oFont = new Font("IDAutomationHC39M", 16);
                            PointF point = new PointF(2.0F, 2.0F);
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            SolidBrush whiteBrush = new SolidBrush(Color.White);
                            graphics__1.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                            graphics__1.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                        }
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            databar = ms.ToArray();
                        }
                    }

                    string QUOTA = sdb.Select_Values("Select QuotaId from tbl_fees_StudentEnrollment where RollNo='" + RollNo + "'");
                    string QRMsg;
                    if (QUOTA == "UFRO")
                        QRMsg = sdb.Select_Values("select  + 'Verified:RN:' + RollNo +','+ DisplayName +',PSP:'+ Passport +',Emerg.Num:'+ PhoneNo   from Vw_StudentSearch where RollNo ='" + RollNo + "'");
                    else if (QUOTA == "UFROME")
                        QRMsg = sdb.Select_Values("select  + 'Verified:RN:' + RollNo +','+ DisplayName +',PSP:'+ Passport +',Emerg.Num:'+ PhoneNo   from Vw_StudentSearch where RollNo ='" + RollNo + "'");
                    else if (QUOTA == "UFROMTECH")
                        QRMsg = sdb.Select_Values("select  + 'Verified:RN:' + RollNo +','+ DisplayName +',PSP:'+ Passport +',Emerg.Num:'+ PhoneNo   from Vw_StudentSearch where RollNo ='" + RollNo + "'");
                    else if (QUOTA == "NRI")
                        QRMsg = sdb.Select_Values("select  + 'Verified:RN:' + RollNo +','+ DisplayName +',PSP:'+ Passport +',Emerg.Num:'+ PhoneNo   from Vw_StudentSearch where RollNo ='" + RollNo + "'");
                    else
                        QRMsg = sdb.Select_Values("select  + 'Verified:RN:' + RollNo +','+ DisplayName +',AD:'+ AdhaarCardNo +',Emerg.Num:'+ PhoneNo   from Vw_StudentSearch where RollNo ='" + RollNo + "'");

                    string code = QRMsg;
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                    imgBarCode.Height = 190;
                    imgBarCode.Width = 190;
                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            dataQR = ms.ToArray();
                        }
                    }
                    sdb.Update_QR_BAR(databar, dataQR, Var_StudentId);
                }

            }

            string Var_Batch = "";
            string Batch;
            string Program;
            string Period;
            Batch = sdb.Select_Values("select BatchId from tbl_fees_StudentEnrollment where StudentId='" + Var_StudentId + "'");
            Program = sdb.Select_Values("select ProgramId from tbl_fees_StudentEnrollment where StudentId='" + Var_StudentId + "'");
            Period = sdb.Select_Values("select Period from tbl_fees_StudentEnrollment SE,tbl_fees_Program P where SE.ProgramId=P.ProgramId and SE.StudentId='" + Var_StudentId + "'");
            string[] splitBatch = Regex.Split(Batch, "-");
            Var_Batch = splitBatch[0];

            if (Program == "ME")
                Batch = (Convert.ToInt32(splitBatch[0]) + Convert.ToInt32(Period)).ToString().Substring(2, 2);
            else if (Program == "MTECH")
                Batch = (Convert.ToInt32(splitBatch[0]) + Convert.ToInt32(Period)).ToString().Substring(2, 2);
            else
                Batch = (Convert.ToInt32(splitBatch[0]) + Convert.ToInt32(Period)).ToString();
            Var_Batch = Var_Batch + "-" + Batch;

            Reports.Attendance Rs = new Reports.Attendance();
            ReportDocument cryRpt;
            if (RollNo != null)
            {
                cryRpt = (ReportDocument)Rs.StudentIdCardPDf(1, Var_StudentId, Var_Batch);
                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\Id.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/Id.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();

            }
            return PartialView("_ReportViewer");
        }


        public ActionResult CertificateTC(string RollNo)
        {
            AcademicContext sdb = new AcademicContext();
            string Var_StudentId;
            Var_StudentId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + RollNo + "'");
            if (Var_StudentId != null)
            {
                if (Var_StudentId != "")
                {
                    Reports.StudentReport Rs = new Reports.StudentReport();
                    ReportDocument cryRpt;
                    cryRpt = (ReportDocument)Rs.StudentTCPreviewPdf(1, Var_StudentId, "TC", "ORIGINAL");
                    string FilePath = Server.MapPath("~/ReportFiles");
                    FilePath = FilePath + "\\TC.pdf";
                    cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/TC.pdf"));
                    cryRpt.Close();
                    cryRpt.Dispose();
                }

            }
            return PartialView("_ReportViewer");
        }

        public ActionResult CertificateBC(string RollNo)
        {
            AcademicContext sdb = new AcademicContext();
            string Var_StudentId;
            Var_StudentId = sdb.Select_Values("Select StudentId from tbl_fees_StudentEnrollment where RollNo='" + RollNo + "'");
            if (Var_StudentId != null)
            {
                if (Var_StudentId != "")
                {
                    Reports.StudentReport Rs = new Reports.StudentReport();
                    ReportDocument cryRpt;
                    cryRpt = (ReportDocument)Rs.StudentBCPreviewPdf(1, Var_StudentId, "BC");
                    string FilePath = Server.MapPath("~/ReportFiles");
                    FilePath = FilePath + "\\Id.pdf";
                    cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    TempData["Report"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/Id.pdf"));
                    cryRpt.Close();
                    cryRpt.Dispose();
                }

            }
            return PartialView("_ReportViewer");
        }

        public JsonResult StudentTutionFeeDetails(string Student)
        {

            AcademicContext sdb = new AcademicContext();
            var FillAmount = sdb.GetStudentFeeDetails(Student, "TF");

            if (FillAmount.Count == 0)
            {
                FillAmount = null;
                return Json(FillAmount, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillAmount.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentSearchFeeDetails(string student)
        {

            AcademicContext sdb = new AcademicContext();
            var FillFee = sdb.GetStudentSearchFee(student);

            if (FillFee.Count == 0)
            {
                FillFee = null;
                return Json(FillFee, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillFee.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult StudentAttendanceDetails(string student)
        {

            AcademicContext sdb = new AcademicContext();
            var FillAttendance = sdb.GetStudentAttendance(student);

            if (FillAttendance.Count == 0)
            {
                FillAttendance = null;
                return Json(FillAttendance, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillAttendance.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentSearchExtraFeeDetails(string student)
        {

            AcademicContext sdb = new AcademicContext();
            var FillFee = sdb.GetStudentSearchExtraFee(student);

            if (FillFee.Count == 0)
            {
                FillFee = null;
                return Json(FillFee, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillFee.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentSearchChallanFeeDetails(string student)
        {

            AcademicContext sdb = new AcademicContext();
            var FillFee = sdb.GetStudentSearchChallanFee(student);

            if (FillFee.Count == 0)
            {
                FillFee = null;
                return Json(FillFee, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillFee.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentSearchDetainDetails(string student)
        {

            AcademicContext sdb = new AcademicContext();
            var Fill = sdb.GetStudentSearchDetain(student);

            if (Fill.Count == 0)
            {
                Fill = null;
                return Json(Fill, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(Fill.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentDetainDetails(string ID)
        {
            AcademicContext sdb = new AcademicContext();
            var model = new Student_Detain_Discontinue();
            model = sdb.GetStudentDetain(ID);
            var Detains = GetDetains();
            model.DetainTypes = GetSelectListItems(Detains);

            var Status = GetDetainStatus();
            model.Statuses = GetSelectListItems(Status);
            model.Status = "DisContinue";
            return PartialView("_DetainUpdate", model);
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


        public JsonResult StudentSearchRADetails(string student)
        {
            AcademicContext sdb = new AcademicContext();
            var Fill = sdb.GetStudentSearchReadmission(student);
            if (Fill.Count == 0)
            {
                Fill = null;
                return Json(Fill, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(Fill.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TCDetails(string Student)
        {

            AcademicContext sdb = new AcademicContext();
            string TC = sdb.GetTCDetails(Student);
            return Json(TC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FutureAmount(string Student)
        {

            AcademicContext sdb = new AcademicContext();
            decimal FutureAmount = sdb.GetFutureAmount(Student);
            return Json(FutureAmount, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DiscontinueFeeWaiver(IEnumerable<FeeWaiver> StudentFeeWaiver)
        {
            AcademicContext sdb = new AcademicContext();
            FeeWaiver data = new FeeWaiver();
            int rescount = 0;
            int insertedRecords = 0;
            if (StudentFeeWaiver != null)
            {
                foreach (var Acd in StudentFeeWaiver)
                {
                    data.RollNo = Acd.RollNo;
                    data.Current_Due = Acd.Current_Due;
                    data.Future_Amount = Acd.Future_Amount;
                    data.Final_Collected = Acd.Final_Collected;
                    data.Fee_Waiver = Acd.Fee_Waiver;
                    data.UserName = Acd.UserName;
                    data.Remarks = Acd.Remarks;
                }
                rescount = sdb.Discontinue_FeeWiver(data);

            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        public JsonResult Regular_FeeWaiver_Save(IEnumerable<StudentFeeMaster> StudentFeeWaiver)
        {

            AcademicContext sdb = new AcademicContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (StudentFeeWaiver != null)
            {

                rescount = sdb.RegularFeeWaiverSave(StudentFeeWaiver);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }

        public ActionResult StudentSearch(string RollNo)
        {
            AcademicContext sdb = new AcademicContext();
            StudentSearch stud = new StudentSearch();
            stud = sdb.GetStudentSearch(RollNo);
            stud.studentPhoto = sdb.GetStudentPhoto(RollNo);
            return PartialView("_StudentSearch", stud);
        }

        public JsonResult UserDetails()
        {

            UserContext sdb = new UserContext();
            var FillUser = sdb.GetUserDetails();

            if (FillUser.Count == 0)
            {
                FillUser = null;
                return Json(FillUser, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillUser.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Roles()
        {
            UserContext sdb = new UserContext();
            var FillRole = sdb.GetUserDetails();

            if (FillRole.Count == 0)
            {
                FillRole = null;
                return Json(FillRole, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillRole.ToArray(), JsonRequestBehavior.AllowGet);
        }

     
    }
}