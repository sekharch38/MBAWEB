using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYFEELIB.Entities;
using MYFEELIB.Domain;
using MYFEELIB.Data;
using System.Data;

namespace MYFEEWEB.Controllers
{
    public class MasterController : Controller
    {
        //
        // GET: /Master/
        AccountService service = new AccountService();
        #region UserMaster view Get method.
        public ActionResult UserMaster()
        {

            Session["Header"] = Request.QueryString["UT"];
            var model = new Registration();
            var mainRole = GetRoles();
            model.Roles = GetSelectListItems(mainRole);
            return View(model);
        }
        public IEnumerable<SelectListItem> GetSelectListItems(List<ListItem> elements)
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
        public List<ListItem> GetRoles()
        {
            List<ListItem> listRoles = new List<ListItem>();
            listRoles = service.GetRoles();
            listRoles.RemoveAt(0);
            return listRoles;
        }
        #endregion
        # region POST: UserMaster
        //[HttpPost]
        public JsonResult UserMasterSave(Registration data)
        {

            UserContext sdb = new UserContext();
            data = sdb.UserSave(data);
            var status = data;
            return Json(status);



        }
        #endregion

        # region GET: Role Master
        public ActionResult RoleMaster()
        {
            Session["Header"] = Request.QueryString["UT"];
            return View();
        }
        #endregion
        public JsonResult ROLE_SAVE(List<SubModule> CheckSubAll)
        {
            AcademicContext sdb = new AcademicContext();
            string result = null;

            List<int> MainIds = new List<int>();
            string RId = null;
            string RoleName = null;

            DataTable dtSMA = new DataTable();
            DataColumn dtColumnSMA;

            dtColumnSMA = new DataColumn();
            dtColumnSMA.DataType = Type.GetType("System.Int32");
            dtColumnSMA.ColumnName = "RId";
            dtColumnSMA.Caption = "RId";
            dtSMA.Columns.Add(dtColumnSMA);

            dtColumnSMA = new DataColumn();
            dtColumnSMA.DataType = Type.GetType("System.Int32");
            dtColumnSMA.ColumnName = "Form_Id";
            dtColumnSMA.Caption = "Form_Id";
            dtSMA.Columns.Add(dtColumnSMA);

            dtColumnSMA = new DataColumn();
            dtColumnSMA.DataType = Type.GetType("System.Int32");
            dtColumnSMA.ColumnName = "MId";
            dtColumnSMA.Caption = "MId";
            dtSMA.Columns.Add(dtColumnSMA);




            foreach (var Acd in CheckSubAll)
            {
                DataRow dr = dtSMA.NewRow();
                dr["RId"] = Acd.R_Id;
                dr["Form_Id"] = Acd.F_Id;
                dr["MId"] = Acd.Id;
                dtSMA.Rows.Add(dr);

                RId = Acd.R_Id.ToString();
                RoleName = Acd.RoleName;
                MainIds.Add(Acd.Id);
            }
            var Main_Ids = MainIds.Distinct();

            DataTable dtMA = new DataTable();
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "RId";
            dtColumn.Caption = "RId";
            dtMA.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "MId";
            dtColumn.Caption = "MId";
            dtMA.Columns.Add(dtColumn);

            foreach (var item in Main_Ids)
            {
                DataRow dr = dtMA.NewRow();
                dr["RId"] = Convert.ToInt32(RId);
                dr["MId"] = item;
                dtMA.Rows.Add(dr);
            }

            result = sdb.Role_Save(dtSMA, dtMA, Convert.ToInt32(RId), RoleName);

            return Json(result);
        }

        public JsonResult Get_Roles()
        {
            List<ListItem> listRoles = new List<ListItem>();
            listRoles = service.GetRoles();
            listRoles.RemoveAt(0);
            return Json(new SelectList(listRoles.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);

        }

        public ActionResult MainModule()
        {

            Session["Header"] = Request.QueryString["UT"];
            return View();
        }

        public ActionResult SubModuleMaster()
        {

            Session["Header"] = Request.QueryString["UT"];
            var module = new SubModule();
            var mainModule = GetModules_SubModules();
            module.Modules = GetSelectListItems(mainModule);
            return View(module);
        }

        public List<ListItem> GetModules_SubModules()
        {
            List<ListItem> listModules = new List<ListItem>();
            listModules = service.GetModules();
            listModules.RemoveAt(0);
            return listModules;
        }



        # region POST: MainModule
        //[HttpPost]
        public JsonResult MainModuleSave(Modules data)
        {
            UserContext sdb = new UserContext();
            var status = sdb.MainModuleSave(data);

            return Json(status);
        }

        public JsonResult SubModuleSave(SubModule data)
        {
            UserContext sdb = new UserContext();
            var status = sdb.SubModuleSave(data);
            return Json(status);
        }
        #endregion


        public JsonResult MainModuleDetails()
        {
            UserContext sdb = new UserContext();
            var MainModule = sdb.GetMainModuleDetails();

            if (MainModule.Count == 0)
            {
                MainModule = null;
                return Json(MainModule, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(MainModule.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubModuleDetails()
        {
            UserContext sdb = new UserContext();
            var subModule = sdb.GetSubModuleDetails();

            if (subModule.Count == 0)
            {
                subModule = null;
                return Json(subModule, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(subModule.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public List<ListItem> GetModules()
        {
            List<ListItem> listModules = new List<ListItem>();
            listModules = service.GetRoles();
            listModules.RemoveAt(0);
            return listModules;
        }


        public ActionResult JournalVoucherTransaction()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new JournalVoucher();
            var mainTrans = GetTransactions();
            model.TNames = GetSelectListItems(mainTrans);
            return View(model);
        }


        public List<ListItem> GetTransactions()
        {
            List<ListItem> listTrans = new List<ListItem>();
            listTrans = service.GetTrans();
            listTrans.RemoveAt(0);
            return listTrans;
        }

        public JsonResult JournalVoucherTransactionDetails()
        {
            UserContext sdb = new UserContext();
            var JV = sdb.GetJVDetails();

            if (JV.Count == 0)
            {
                JV = null;
                return Json(JV, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(JV.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public JsonResult JV_Save(JournalVoucher data)
        {
            UserContext sdb = new UserContext();
            var status = sdb.JV_SAVE(data);
            return Json(status);
        }

        public ActionResult FeeTobeReceived()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new TobeReceived();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Years = GetYear("");
            model.Years = GetSelectListItems(Years);

            var QC = GetQC("");
            model.QuotaCategories = GetSelectListItems(QC);
            return View(model);
        }

        public JsonResult YearList(string Program)
        {
            try
            {

                var yr = GetYear(Program);

                return Json(new SelectList(yr.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }

        public List<ListItem> GetQC(string Program)
        {
            List<ListItem> listQC = new List<ListItem>();
            if (Program == "")
            {

                listQC.Insert(0, new ListItem()
                {
                    Value = null,
                    Text = "SELECT QUOTA CATEGORY"
                });
                listQC.RemoveAt(0);
                return listQC;
            }
            else
            {
                listQC = service.GetQuotaCategoryTBR(Program);
                listQC.RemoveAt(0);
                return listQC;
            }
        }

        public List<ListItem> GetPrograms()
        {
            List<ListItem> listProgram = new List<ListItem>();
            listProgram = service.GetProgramDetails();
            listProgram.RemoveAt(0);
            return listProgram;
        }


        public List<ListItem> GetYear(string Program)
        {

            List<ListItem> listYear = new List<ListItem>();
            if (Program != null)
            {
                if (Program == "ET")
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
                else if (Program == "ME")
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


            }
            return listYear;



        }
        public JsonResult QuotaCatList(string Program)
        {
            try
            {
                var QC = service.GetQuotaCategoryTBR(Program);
                return Json(new SelectList(QC.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }


        public JsonResult TobeReceived_Save(TobeReceived data)
        {
            UserContext sdb = new UserContext();
            var status = sdb.TBR_SAVE(data);
            return Json(status);
        }


        public JsonResult TobeReceivedDetails()
        {
            UserContext sdb = new UserContext();
            var TBR = sdb.GetTBRDetails();

            if (TBR.Count == 0)
            {
                TBR = null;
                return Json(TBR, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(TBR.ToArray(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult CustodianDetails()
        {
            Session["Header"] = Request.QueryString["UT"];
            //   var model = new TobeReceived();

            //var mainProgram = GetPrograms();
            //model.Programs = GetSelectListItems(mainProgram);

            //var Years = GetYear("");
            //model.Years = GetSelectListItems(Years);

            //var QC = GetQC("");
            //model.QuotaCategories = GetSelectListItems(QC);
            return View();
        }

        public JsonResult Custodian_Details(string RN)
        {
            UserContext sdb = new UserContext();
            var CD = sdb.CustodianDetails(RN);

            if (CD.Count == 0)
            {
                CD = null;
                return Json(CD, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(CD.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertCustodian(IEnumerable<Custodian> CheckAll)
        {
            List<Custodian> listcust = new List<Custodian>();
            UserContext sdb = new UserContext();
            int rescount = 0;
            int insertedRecords = 0;
            if (CheckAll != null)
            {
                foreach (var Acd in CheckAll)
                {
                    Custodian data = new Custodian()
                    {
                        RollNo = Acd.RollNo,
                        CustodianId = Acd.CustodianId,

                    };
                    listcust.Add(data);
                }
                rescount = sdb.SaveCustodian(listcust);

                insertedRecords = rescount;
                return Json(insertedRecords);
            }
            insertedRecords = rescount;
            return Json(insertedRecords);

        }





        //////////////////////////////////////////////////////////////////////


        public ActionResult CourseMaster()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new Course();
            //Session["Header"] = Request.QueryString["UT"];
            //var model = new Registration();
            //var mainRole = GetRoles();
            //model.Roles = GetSelectListItems(mainRole);
            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);
            return View(model);
        }

        //public List<ListItem> GetPrograms()
        //{
        //    List<ListItem> listProgram = new List<ListItem>();
        //    listProgram = service.GetProgramDetails();
        //    listProgram.RemoveAt(0);
        //    return listProgram;
        //}
        # region POST: CourseMaster
        [HttpPost]
        public JsonResult CourseMasterSave(Course data)
        {

            UserContext sdb = new UserContext();
            data = sdb.CourseSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);



        }
        #endregion
        public JsonResult CourseDetails()
        {

            UserContext sdb = new UserContext();
            var FillCourse = sdb.GetCourseDetails();

            if (FillCourse.Count == 0)
            {
                FillCourse = null;
                return Json(FillCourse, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillCourse.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SectionMaster()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new Section();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Course = service.GetCourseDetails("");
            model.Courses = GetSelectListItems(Course);

            return View(model);
        }
        public JsonResult SectionMasterSave(Section data)
        {

            UserContext sdb = new UserContext();
            data = sdb.SectionSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);



        }

        public JsonResult SectionDetails()
        {

            UserContext sdb = new UserContext();
            var FillSection = sdb.GetSectionDetails();

            if (FillSection.Count == 0)
            {
                FillSection = null;
                return Json(FillSection, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillSection.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillCourse(string PId)
        {

            try
            {
                Section sec = new Section();
                var Course = service.GetCourseDetails(PId);
                sec.Courses = GetSelectListItems(Course);
                return Json(new SelectList(Course.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public ActionResult ProgramMaster()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new Program();

            return View(model);
        }
        public JsonResult ProgramMasterSave(Program data)
        {

            UserContext sdb = new UserContext();
            data = sdb.ProgramSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }

        public JsonResult ProgramDetails()
        {

            UserContext sdb = new UserContext();
            var FillSection = sdb.GetProgramDetails();

            if (FillSection.Count == 0)
            {
                FillSection = null;
                return Json(FillSection, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(FillSection.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentQuotaMaster()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new StudentQuota();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            return View(model);
        }
        public JsonResult StudentQuotaSave(StudentQuota data)
        {

            UserContext sdb = new UserContext();
            data = sdb.StudentQuotaSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }

        public JsonResult StudentQuotaDetails()
        {

            UserContext sdb = new UserContext();
            var FillStudentQuota = sdb.GetStudentQuotaDetails();

            if (FillStudentQuota.Count == 0)
            {
                FillStudentQuota = null;
                return Json(FillStudentQuota, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillStudentQuota.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillQuota(string SId)
        {
            try
            {
                QuotaFee Stud = new QuotaFee();
                var quota = service.GetQuotaDetails(SId);
                Stud.Quotas = GetSelectListItems(quota);
                return Json(new SelectList(quota.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }
         
        public ActionResult StudentQuotaCategoryMaster()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new StudentQuotaCategory();

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var quota = service.GetQuotaDetails("");
            model.Quotas = GetSelectListItems(quota);

            return View(model);
        }
        public JsonResult StudentQuotaCategorySave(StudentQuotaCategory data)
        {

            UserContext sdb = new UserContext();
            data = sdb.StudentQuotaCategorySave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }
        public JsonResult StudentQuotaCategoryDetails()
        {

            UserContext sdb = new UserContext();
            var StudentQuotaCategory = sdb.GetStudentQuotaCategoryDetails();

            if (StudentQuotaCategory.Count == 0)
            {
                StudentQuotaCategory = null;
                return Json(StudentQuotaCategory, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(StudentQuotaCategory.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CertificateMaster()
        {
            Session["Header"] = Request.QueryString["UT"];          

            return View();
        }
        public JsonResult CertificateMasterSave(CertificateMaster data)
        {

            UserContext sdb = new UserContext();
            data = sdb.CertificateMasterSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }

        public JsonResult CertificateMasterDetails()
        {

            UserContext sdb = new UserContext();
            var FillCertificate = sdb.GetCertificateMasterDetails();

            if (FillCertificate.Count == 0)
            {
                FillCertificate = null;
                return Json(FillCertificate, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(FillCertificate.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UniversitySave(University data)
        {

            UserContext sdb = new UserContext();
            data = sdb.UniversitySave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }

        public JsonResult UniversityDetails()
        {

            UserContext sdb = new UserContext();
            var FillUniversity = sdb.GetUniversityDetails();

            if (FillUniversity.Count == 0)
            {
                FillUniversity = null;
                return Json(FillUniversity, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(FillUniversity.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Certificate()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new Certificate();
          

            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Batch = service.GetBatch("");
            model.Batchs = GetSelectListItems(Batch);

            var Certificate = GetCertificates();
            model.SelectCertificates = GetSelectListItems(Certificate);

            var Course = GetSelectCourse();
            model.Courses = GetSelectListItems(Course);

            return View(model);
        }
        public ActionResult FillBatch(string PId)
        {

            try
            {
                UserContext sdb = new UserContext();
                Certificate sec = new Certificate();
               // var Batch = service.GetBatchDetails(PId);
                var Batch = sdb.GetBatchDetails(PId);
                sec.Batchs = GetSelectListItems(Batch);
                return Json(new SelectList(Batch.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }
        public List<ListItem> GetCertificates()
        {
            UserContext sdb = new UserContext();
            List<ListItem> listCertificate = new List<ListItem>();
            listCertificate = sdb.GetCertificates();
            listCertificate.RemoveAt(0);
            return listCertificate;
        }

        public JsonResult CertificateSave(Certificate data)
        {
            string rescount = "";
            UserContext sdb = new UserContext();
            rescount = sdb.CertificateSave(data);
            //if (data.Status == "Success")
            //{
            return Json(rescount);
                //return Json(data.Status);

            //}
            //return Json(data.Status);
        }

        //public JsonResult CertificateDetails()
        //{

        //    UserContext sdb = new UserContext();
        //    var FillCertificate = sdb.GetCertificateMasterDetails();

        //    if (FillCertificate.Count == 0)
        //    {
        //        FillCertificate = null;
        //        return Json(FillCertificate, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(FillCertificate.ToArray(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult CertificateGridDetails(string rollno)
        {

            UserContext sdb = new UserContext();
            var CertificateGrid = sdb.GetCertificateGridDetails(rollno);

            if (CertificateGrid.Count == 0)
            {
                CertificateGrid = null;
                return Json(CertificateGrid, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(CertificateGrid.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult University()
        {
            Session["Header"] = Request.QueryString["UT"];

            return View();
        }
       
        public List<ListItem> GetSelectCourse()
        {

            List<ListItem> SelectCourse = new List<ListItem>();
           
                SelectCourse.Insert(0, new ListItem()
                {
                    Value = null,
                    Text = "SELECT Course"
                });
                SelectCourse.Insert(1, new ListItem()
                {
                    Value = "Completed",
                    Text = "Completed"
                });

                SelectCourse.Insert(2, new ListItem()
                {
                    Value = "Not Completed",
                    Text = "Not Completed"
                });
            
            return SelectCourse;

        }
        public JsonResult SelectCertificateDetails(string rollno)
        {

            UserContext sdb = new UserContext();
            var SelectCertificate = sdb.GetSelectCertificateDetails(rollno);

            if (SelectCertificate.Count == 0)
            {
                SelectCertificate = null;
                return Json(SelectCertificate, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(SelectCertificate.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectCertificateSNODetails(string CertificateId)
        {

            UserContext sdb = new UserContext();
            var SelectCertificate = sdb.GetSelectCertificateSNODetails(CertificateId);

            if (SelectCertificate.Count == 0)
            {
                SelectCertificate = null;
                return Json(SelectCertificate, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(SelectCertificate.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FinancialBatches()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new FinancialBatches();


            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var mainBatch = GetBatchId();
            model.Batchs = GetSelectListItems(mainBatch);

            var mainYearCode = GetYearCodes();
            model.YearCodes = GetSelectListItems(mainYearCode);

            var Years = GetYear("");
            model.Years = GetSelectListItems(Years);

            var Academic = GetAcademic();
            model.Academics = GetSelectListItems(Academic);

            return View(model);
        }

        public List<ListItem> GetAcademic()
        {

            List<ListItem> SelectAcademic = new List<ListItem>();

            SelectAcademic.Insert(0, new ListItem()
            {
                Value = null,
                Text = "SELECT Academic"
            });
            SelectAcademic.Insert(1, new ListItem()
            {
                Value = "Current",
                Text = "Current"
            });

            SelectAcademic.Insert(2, new ListItem()
            {
                Value = "Before 31st March",
                Text = "Before 31st March"
            });
            SelectAcademic.Insert(2, new ListItem()
            {
                Value = "Old",
                Text = "Old"
            });
            SelectAcademic.RemoveAt(0);
            return SelectAcademic;

        }

        public List<ListItem> GetBatchId()
        {
            List<ListItem> Item = new List<ListItem>();
            UserContext context = new UserContext();
            Item = context.GetBatchIdDetails();
            Item.RemoveAt(0);
            return Item;
        }
        public ActionResult FillBatchId()
        {

            try
            {
                UserContext sdb = new UserContext();
                FinancialBatches sec = new FinancialBatches();               
                var Batch = sdb.GetBatchIdDetails();
                sec.Batchs = GetSelectListItems(Batch);
                return Json(new SelectList(Batch.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }
        public List<ListItem> GetYearCodes()
        {
            List<ListItem> Item = new List<ListItem>();
            UserContext context = new UserContext();
            Item = context.GetYearCode();
            Item.RemoveAt(0);
            return Item;
        }
        public ActionResult FillYearCode()
        {

            try
            {
                UserContext sdb = new UserContext();
                FinancialBatches sec = new FinancialBatches();
                var year = sdb.GetYearCode();
                sec.YearCodes = GetSelectListItems(year);
                return Json(new SelectList(year.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult FinancialBatchMasterSave(FinancialBatches data)
        {

            UserContext sdb = new UserContext();
            data = sdb.FinancialBatchSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }

        public JsonResult FinancialBatchDetails()
        {
            UserContext sdb = new UserContext();
            var FillFinancialBatch = sdb.GetFinancialBatchDetails();

            if (FillFinancialBatch.Count == 0)
            {
                FillFinancialBatch = null;
                return Json(FillFinancialBatch, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(FillFinancialBatch.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Messages()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new Message();


            var MessageType = GetMessageType();
            model.MessageTypes = GetSelectListItems(MessageType);           

            return View(model);
        }

        public List<ListItem> GetMessageType()
        {

            List<ListItem> SelectAcademic = new List<ListItem>();

            SelectAcademic.Insert(0, new ListItem()
            {
                Value = null,
                Text = "SELECT MessageType"
            });
            SelectAcademic.Insert(1, new ListItem()
            {
                Value = "Attendance",
                Text = "Attendance"
            });

            SelectAcademic.Insert(2, new ListItem()
            {
                Value = "Fee",
                Text = "Fee"
            });
            SelectAcademic.RemoveAt(0);
            return SelectAcademic;

        }

        public JsonResult MessagesMasterSave(Message data)
        {

            UserContext sdb = new UserContext();
            data = sdb.MessageSave(data);
            if (data.Status == "Success")
            {

                return Json(data.Status);

            }
            return Json(data.Status);
        }

        public JsonResult MessagesMasterDetails()
        {
            UserContext sdb = new UserContext();
            var FillFinancialBatch = sdb.GetMessageDetails();

            if (FillFinancialBatch.Count == 0)
            {
                FillFinancialBatch = null;
                return Json(FillFinancialBatch, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(FillFinancialBatch.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MassMailingSMS()
        {
            Session["Header"] = Request.QueryString["UT"];
            var model = new MassMailingSMS();
            
            var mainProgram = GetPrograms();
            model.Programs = GetSelectListItems(mainProgram);

            var Course = service.GetCourseDetails("");
            model.Courses = GetSelectListItems(Course);

            var Batch = service.GetBatch("");
            model.Batchs = GetSelectListItems(Batch);

            var Years = GetYear("");
            model.Years = GetSelectListItems(Years);

            return View(model);
        }

        public JsonResult QCNameDetails(string PId)
        {
            UserContext sdb = new UserContext();
            var FillFinancialBatch = sdb.GetQCName(PId);
           // ViewBag.ListQCName = FillFinancialBatch.ToArray();
            if (FillFinancialBatch.Count == 0)
            {
                FillFinancialBatch = null;
                return Json(FillFinancialBatch, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(FillFinancialBatch.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}