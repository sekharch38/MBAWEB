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

namespace MYFEEWEB.Controllers
{
    public class AttendanceController : Controller
    {
        //
        // GET: /Attendance/
        AccountService service = new AccountService();
        public ActionResult Index()
        {
            return View();
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
            model.Sections = GetSelectListItems(Section);

            var Year = GetYear("");
            model.Years = GetSelectListItems(Year);

            return View(model);
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
        public List<ListItem> GetPrograms()
        {
            List<ListItem> listProgram = new List<ListItem>();
            listProgram = service.GetProgramDetails();
            listProgram.RemoveAt(0);
            return listProgram;
        }

        public List<ListItem> GetCourses(string PId)
        {
            List<ListItem> listCourses = new List<ListItem>();
            if (PId != "")
            {
                listCourses = service.GetCourseDetails(PId);
                listCourses.RemoveAt(0);
            }
            return listCourses;
        }

        public List<ListItem> GetYear(string Program)
        {
            List<ListItem> listYear = new List<ListItem>();

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
            return listYear;
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

    }
}