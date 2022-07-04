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
    public class ProcessController : Controller
    {
        AccountService service = new AccountService();
        //
        // GET: /Process/
        [HttpGet]
        public ActionResult AcademicYearProcess()
        {
            return View();
        }

        # region POST: Financi Year Process
        [HttpPost]
        public JsonResult AcademicYearProcess(RequireElements data)
        {
            int rescount = 1;
            ProcessContext sdb = new ProcessContext();
            //rescount = sdb.AcademicYearProcess(data);
            if (rescount == 0)
            {

                return Json(rescount);

            }
            else
            {
                return Json(rescount);

            }




        }
        #endregion



        public JsonResult FillProgram()
        {

            try
            {
                var Program = service.GetProgramDetails();
                return Json(new SelectList(Program.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }
        public JsonResult FinanciYearBatches(string PId)
        {
            ProcessContext sdb = new ProcessContext();
            var FillBatches = sdb.GetFYBatches(PId);
            if (FillBatches.Count == 0)
            {
                FillBatches = null;
                return Json(FillBatches, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(FillBatches.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FinancialYearOpen()
        {
            AcademicContext Ac = new AcademicContext();
            DataSet ds = Ac.GetFinancialPeriod();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["YearCode"] = ds.Tables[0].Rows[0][0];
                    Session["FD"] = ds.Tables[0].Rows[0][1];
                    Session["ED"] = ds.Tables[0].Rows[0][2];
                }
            }
            return View();
        }

        public JsonResult End_Date(DateTime StDate)
        {

            DateTime Dt = StDate.AddYears(1).AddDays(-1);
            return Json(Dt);
        }
        public JsonResult FinancialYearOpen_Create(FYDates Dates)
        {
            ProcessContext sdb = new ProcessContext();
            var result = sdb.FinancialYearOpen(Dates);
            return Json(result);
        }

        [HttpGet]
        public ActionResult FinancialYearClose()
        {
            AcademicContext Ac = new AcademicContext();
            DataSet ds = Ac.GetFinancialPeriod();
            if (ds != null)
            {
                Session["YearCode"] = ds.Tables[0].Rows[0][0];
                Session["FD"] = ds.Tables[0].Rows[0][1];
                Session["ED"] = ds.Tables[0].Rows[0][2];
            }
            return View();
        }

        public JsonResult FinancialYearClosed(string YearCode)
        {
            ProcessContext sdb = new ProcessContext();
            var result = sdb.FinancialYearClose(YearCode, Session["username"].ToString());
            return Json(result);
        }
    }
}