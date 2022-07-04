using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYFEELIB.Entities;
using MYFEELIB.Domain;
using MYFEELIB.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MYFEEWEB.Models;
namespace MYFEEWEB.Controllers
{
    public class EnrollmentController : Controller
    {
        Student Stud = new Student();

        AccountService service = new AccountService();
        public ActionResult Index()
        {


            var model = new Student();

            model.RollNo = Session["RollNo"].ToString();
            model.Password = Session["Password"].ToString();


            var Entry = GetEntries();
            model.Entries = GetSelectListItems(Entry);



            var mainProgram = GetPrograms("");
            model.Programs = GetSelectListItems(mainProgram);


            var mainState = GetStates();
            model.States = GetSelectListItems(mainState);

            var District = service.GetDistrictDetails("");
            model.Districts = GetSelectListItems(District);

            var mainCat = GetCategories();
            model.Categories = GetSelectListItems(mainCat);


            var mainSCat = GetSpecialCategories();
            model.SpecialCategories = GetSelectListItems(mainSCat);

            var mainRelig = GetReligious();
            model.Religions = GetSelectListItems(mainRelig);




            var Course = service.GetCourseDetails("");
            model.Courses = GetSelectListItems(Course);

            var Section = service.GetSectionDetails("");
            model.Sections = GetSelectListItems(Section);

            var Year = GetYear("");
            model.Years = GetSelectListItems(Year);

            var quota = service.GetQuotaDetails("");
            model.Quotas = GetSelectListItems(quota);

            var QC = service.GetQuotaCategoryDetails("");
            model.QuotaCategories = GetSelectListItems(QC);

            return View(model);

        }


        public List<ListItem> GetSpecialCategories()
        {
            List<ListItem> listSCategories = new List<ListItem>();
            listSCategories.Insert(0, new ListItem()
            {
                Value = "NONE",
                Text = "NONE"
            });
            listSCategories.Insert(0, new ListItem()
            {
                Value = "SPORTS",
                Text = "SPORTS"
            });
            listSCategories.Insert(0, new ListItem()
            {
                Value = "CAP",
                Text = "CAP"
            });
            listSCategories.Insert(0, new ListItem()
            {
                Value = "PH",
                Text = "PH"
            });

            listSCategories.Insert(0, new ListItem()
            {
                Value = "NCC",
                Text = "NCC"
            });


            return listSCategories;
        }


        public List<ListItem> GetCategories()
        {
            List<ListItem> listCategories = new List<ListItem>();
            listCategories = service.GetCategoryDetails();
            listCategories.RemoveAt(0);
            return listCategories;
        }


        public List<ListItem> GetStates()
        {
            List<ListItem> listStates = new List<ListItem>();
            listStates = service.GetStateDetails();
            listStates.RemoveAt(0);
            return listStates;
        }


        public List<ListItem> GetDistricts(string STId)
        {
            List<ListItem> listdist = new List<ListItem>();
            if (STId != "")
            {
                listdist = service.GetDistrictDetails(STId);
                listdist.RemoveAt(0);
            }
            return listdist;
        }



        public List<ListItem> GetPrograms(string entry)
        {
            List<ListItem> listProgram = new List<ListItem>();
            if (entry != "")
            {
                listProgram = service.GetProgramDetails();
                listProgram.RemoveAt(0);
            }
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

        public List<ListItem> GetSections(string CId)
        {
            List<ListItem> listSections = new List<ListItem>();
            if (CId != "")
            {
                listSections = service.GetSectionDetails(CId);
                listSections.RemoveAt(0);
            }
            return listSections;
        }

        public List<ListItem> GetQuota(string PId)
        {
            List<ListItem> listQuota = new List<ListItem>();
            if (PId != "")
            {
                listQuota = service.GetQuotaDetails(PId);
                listQuota.RemoveAt(0);
            }
            return listQuota;
        }


        public List<ListItem> GetQuotCategory(string QId)
        {
            List<ListItem> listQC = new List<ListItem>();
            if (QId != "")
            {
                listQC = service.GetQuotaCategoryDetails(QId);
                listQC.RemoveAt(0);
            }
            return listQC;
        }



        public ActionResult FillDistrict(string SId)
        {

            try
            {

                var mainDist = service.GetDistrictDetails(SId);
                Stud.Districts = GetSelectListItems(mainDist);
                return Json(new SelectList(mainDist.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

     


        public ActionResult FillProgram(string mEntry)
        {

            try
            {
                Session["EId"] = mEntry;
                var mainProgram = service.GetProgramDetails();
                Stud.Programs = GetSelectListItems(mainProgram);
                return Json(new SelectList(mainProgram.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public JsonResult QuotaCatList(string mQuota)
        {
            try
            {
                Session["QId"] = mQuota;
                var QC = service.GetQuotaCategoryDetails(mQuota);
                Stud.QuotaCategories = GetSelectListItems(QC);
                return Json(new SelectList(QC.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }


        public ActionResult FillQuota(string SId)
        {
            try
            {
                var quota = service.GetQuotaDetails(Session["PId"].ToString());
                Stud.Quotas = GetSelectListItems(quota);
                return Json(new SelectList(quota.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }


        public ActionResult FillCourse(string PId)
        {

            try
            {
                Session["PId"] = PId;
                var Course = service.GetCourseDetails(PId);
                Stud.Courses = GetSelectListItems(Course);
                return Json(new SelectList(Course.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);

        }

        public ActionResult FillSection(string CId)
        {
            try
            {
                Session["CId"] = CId;
                var Section = service.GetSectionDetails(CId);
                Stud.Sections = GetSelectListItems(Section);
                return Json(new SelectList(Section.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
        }


        public JsonResult YearList(string QC)
        {
            try
            {
                Session["QC"] = QC;
                var yr = GetYear(Session["EId"].ToString());
                Stud.Years = GetSelectListItems(yr);
                return Json(new SelectList(yr.ToArray(), "Value", "Text"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionLog.ErrorLog(ex);
            }
            return Json(null);
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

        public List<ListItem> GetEntries()
        {
            List<ListItem> listEntry = new List<ListItem>();


            listEntry.Insert(0, new ListItem()
            {
                Value = "1",
                Text = "Direct Entry"
            });

            listEntry.Insert(1, new ListItem()
            {
                Value = "2",
                Text = "Lateral Entry"
            });

            return listEntry;
        }

        public List<ListItem> GetYear(string entry)
        {
            List<ListItem> listYear = new List<ListItem>();
            object programid = Session["PId"];
            if (programid != null)
            {
                if (Session["PId"].ToString() == "ET")
                {
                    if (entry == "1")
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
                    else if (entry == "2")
                    {
                        listYear.Insert(0, new ListItem()
                        {
                            Value = null,
                            Text = "SELECT YEAR"
                        });
                        listYear.Insert(1, new ListItem()
                        {
                            Value = "2",
                            Text = "2 Year"
                        });

                        listYear.Insert(2, new ListItem()
                        {
                            Value = "3",
                            Text = "3 Year"
                        });

                        listYear.Insert(3, new ListItem()
                        {
                            Value = "4",
                            Text = "4 Year"
                        });
                    }
                }
                else
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


        public List<ListItem> GetReligious()
        {
            List<ListItem> listReligious = new List<ListItem>();

            listReligious.Insert(0, new ListItem()
            {
                Value = "Hindu",
                Text = "Hindu"
            });

            listReligious.Insert(1, new ListItem()
            {
                Value = "Muslim",
                Text = "Muslim"
            });
            listReligious.Insert(2, new ListItem()
            {
                Value = "Christian",
                Text = "Christian"
            });
            listReligious.Insert(3, new ListItem()
            {
                Value = "Sikh",
                Text = "Sikh"
            });
            listReligious.Insert(4, new ListItem()
            {
                Value = "Buddhism",
                Text = "Buddhism"
            });
            listReligious.Insert(5, new ListItem()
            {
                Value = "Jainism",
                Text = "Jainism"
            });
            listReligious.Insert(6, new ListItem()
            {
                Value = "Zoroastrianism",
                Text = "Zoroastrianism"
            });
            listReligious.Insert(7, new ListItem()
            {
                Value = "Judaism",
                Text = "Judaism"
            });
            listReligious.Insert(7, new ListItem()
            {
                Value = "Islam",
                Text = "Islam"
            });


            return listReligious;
        }

        [HttpPost]


        public ActionResult SaveRegistration(Student model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegistrationContext sdb = new RegistrationContext();
                    model = sdb.StudentRegister(model);
                    if (model.Status == "Success")
                    {
                        Session["RollNo"] = model.RollNo;
                        Session["StudentId"] = model.StudentId;
                        Session["YearId"] = model.Year;
                        return RedirectToAction("Academics");
                    }
                    else
                    {
                        var Entry = GetEntries();
                        model.Entries = GetSelectListItems(Entry);

                        var mainProgram = GetPrograms(model.Entry);
                        model.Programs = GetSelectListItems(mainProgram);

                        var mainState = GetStates();
                        model.States = GetSelectListItems(mainState);

                        var maindist = GetDistricts(model.State);
                        model.Districts = GetSelectListItems(maindist);


                        var mainCat = GetCategories();
                        model.Categories = GetSelectListItems(mainCat);

                        var mainSCat = GetSpecialCategories();
                        model.SpecialCategories = GetSelectListItems(mainSCat);

                        var mainRelig = GetReligious();
                        model.Religions = GetSelectListItems(mainRelig);

                        var Course = service.GetCourseDetails(model.Program);
                        model.Courses = GetSelectListItems(Course);

                        var Section = service.GetSectionDetails(model.Course);
                        model.Sections = GetSelectListItems(Section);

                        var Year = GetYear(model.Entry);
                        model.Years = GetSelectListItems(Year);

                        var quota = GetQuota(model.Program);
                        model.Quotas = GetSelectListItems(quota);

                        var QC = GetQuotCategory(model.Quota);
                        model.QuotaCategories = GetSelectListItems(QC);

                        return View("Index", model);
                    }
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                model.Status = ex.ToString ();
                return View("Index", model);
            }
        }


        public ActionResult StudentOtherFee()
        {
            var nav = new Navigation();
            nav.RollNo = Session["RollNo"].ToString();
            nav.StudentId = Session["StudentId"].ToString();
            nav.YearId = Convert.ToInt32(Session["YearId"]);

            //Session["RollNo"] = "123654789";
            //Session["StudentId"] = "";
            //Session["YearId"] = "";
            //Session["PId"] = "ET";

            var OtherFee = service.GetOtherFeeDetails(Session["PId"].ToString());
            ViewBag.ListOtherFee = OtherFee.ToArray();
            return View();
        }


        [HttpPost]
        public ActionResult SaveOtherFee(FormCollection formCollection)
        {
            string[] ids = formCollection["FeeId"].Split(new char[] { ',' });
            string SId = Request.Params["StudentId"].ToString();
            int YId = Convert.ToInt32(Request.Params["YearId"]);
            //     Request.Form["QuotaCategory"].ToString();


            if (ids != null || ids.Length != 0)
            {
                int count = 0;
                foreach (string id in ids)
                {

                    RegistrationContext sdb = new RegistrationContext();
                    decimal Amount = sdb.GetFeeAmount(id);
                    if (sdb.StudentOtherFee(SId, id, YId, Amount))
                    {
                        count += 1;

                    }
                }

                if (count == ids.Length)
                {
                    return RedirectToAction("Academics");

                }
                else
                {
                    return View();
                }



            }
            else
            {

                return View();

            }

        }



        public ActionResult PhotoUpload()
        {
            // Session["RollNo"] = "236541789654";
            return View();
        }

        [HttpPost]




        public ActionResult SavePhoto(HttpPostedFileBase UploadFile)
        {
            if (ModelState.IsValid)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(UploadFile.InputStream))
                {
                    bytes = br.ReadBytes(UploadFile.ContentLength);
                }

                RegistrationContext sdb = new RegistrationContext();
                // Session["StudentId"] = "19ETCE003";
                if (sdb.StudentPhoto(Session["StudentId"].ToString(), bytes))
                {

                    return RedirectToAction("StudentReport", "Enrollment");
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View("PhotoUpload");
            }

        }


        public ActionResult GetFeeRecord(int YearId)
        {
            RegistrationContext sdb = new RegistrationContext();
            decimal Amount = sdb.GetTutionFee(YearId, Session["QId"].ToString(), Session["PId"].ToString(), Session["CId"].ToString());

            // string QC = "CDE";//Request.Form["QuotaCategory"].ToString();
            DataSet AmountBifurcation = sdb.GetTutionBifurcationFee(YearId, Session["QC"].ToString());

            decimal var_Convenor = 0;
            decimal var_Minority = 0;
            decimal var_Student = 0;
            if (AmountBifurcation != null && AmountBifurcation.Tables[0].Rows.Count > 0)
            {
                if (AmountBifurcation.Tables[0].Rows[0][3].ToString() == "Yes")
                {
                    var_Convenor = Amount * (Convert.ToDecimal(AmountBifurcation.Tables[0].Rows[0][0]) / 100);
                    var_Minority = Amount * (Convert.ToDecimal(AmountBifurcation.Tables[0].Rows[0][1]) / 100);
                    var_Student = Amount * (Convert.ToDecimal(AmountBifurcation.Tables[0].Rows[0][2]) / 100);
                }
                else
                {
                    var_Convenor = Convert.ToDecimal(AmountBifurcation.Tables[0].Rows[0][0]);
                    var_Minority = Convert.ToDecimal(AmountBifurcation.Tables[0].Rows[0][1]);
                    var_Student = Convert.ToDecimal(AmountBifurcation.Tables[0].Rows[0][2]);
                }
            }

            QuotFee data = new QuotFee
            {
                TFee = Amount,
                Convenor = var_Convenor,
                Minority = var_Minority,
                SFee = var_Student
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult StudentReport()
        {
            RegistrationContext sdb = new RegistrationContext();
            Reports.Attendance Rs = new Reports.Attendance();
            ReportDocument cryRpt;
            string SId = sdb.GetStudentId(Session["RollNo"].ToString());
            Session["StudentId"] = SId;
            string photo_Exists = "";
            photo_Exists = sdb.GetPhotoStudent(SId);

            if (photo_Exists != null)
            {
                ViewBag.PhotoDetails = "IF YOU WANT EDIT PHOTO GO AND CLICK EDIT PHOTO.";
                cryRpt = (ReportDocument)Rs.StudentApplicationWithRollNo(SId);
                string FilePath = Server.MapPath("~/ReportFiles");
                FilePath = FilePath + "\\Report.pdf";
                cryRpt.ExportToDisk(ExportFormatType.PortableDocFormat, FilePath);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"900px\" height=\"1156px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/ReportFiles/Report.pdf"));
                cryRpt.Close();
                cryRpt.Dispose();
            }
            else
            {

                ViewBag.PhotoDetails = "PHOTO NOT UPLOADED ! EDIT FOR UPLOAD PHOTO.";

            }

            return View();
        }



        [HttpGet]
        public ActionResult EditStudent(Student Stud)
        {
            //bool formvalid = false;
            //if (formvalid == false)
            //{ }

            RegistrationContext sdb = new RegistrationContext();
            Stud = sdb.GetStudentData(Session["StudentId"].ToString());

            var Entry = GetEntries();
            Stud.Entries = GetSelectListItems(Entry);


            Session["PId"] = Stud.Program;
            Session["QId"] = Stud.Quota;
            Session["CId"] = Stud.Course;

            Session["Entry"] = Stud.Entry;
            var mainProgram = GetPrograms(Stud.Entry);
            Stud.Programs = GetSelectListItems(mainProgram);

            var mainState = GetStates();
            Stud.States = GetSelectListItems(mainState);

            var maindist = GetDistricts(Stud.State);
            Stud.Districts = GetSelectListItems(maindist);


            var mainCat = GetCategories();
            Stud.Categories = GetSelectListItems(mainCat);

            var mainSCat = GetSpecialCategories();
            Stud.SpecialCategories = GetSelectListItems(mainSCat);

            var mainRelig = GetReligious();
            Stud.Religions = GetSelectListItems(mainRelig);

            var Course = GetCourses(Stud.Program);
            Stud.Courses = GetSelectListItems(Course);

            var Section = GetSections(Stud.Course);
            Stud.Sections = GetSelectListItems(Section);


            var Year = GetYear("");
            Stud.Years = GetSelectListItems(Year);

            List<ListItem> listYear = new List<ListItem>();
            listYear = GetYear(Stud.Entry);
            listYear.RemoveAt(0);
            var Years = listYear;
            Stud.Years = GetSelectListItems(Years);

            var quota = GetQuota(Stud.Program);
            Stud.Quotas = GetSelectListItems(quota);

            var QC = GetQuotCategory(Stud.Quota);
            Stud.QuotaCategories = GetSelectListItems(QC);


            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }
            return View(Stud);



        }


        [HttpPost]


        public ActionResult UpdateRegistration(Student model)
        {
            if (ModelState.IsValid)
            {
                Session["RollNo"] = model.RollNo;
                model.StudentId = Request.Params["StudentId"].ToString();
                RegistrationContext sdb = new RegistrationContext();
                model = sdb.UpdateStudentRegister(model);
                if (model.Status == "Success")
                {
                    return RedirectToAction("StudentReport");
                }
                else
                {
                    var Entry = GetEntries();
                    model.Entries = GetSelectListItems(Entry);



                    var mainProgram = GetPrograms(model.Entry);
                    model.Programs = GetSelectListItems(mainProgram);


                    var mainState = GetStates();
                    model.States = GetSelectListItems(mainState);

                    var maindist = GetDistricts(model.State);
                    model.Districts = GetSelectListItems(maindist);


                    var mainCat = GetCategories();
                    model.Categories = GetSelectListItems(mainCat);

                    var mainSCat = GetSpecialCategories();
                    model.SpecialCategories = GetSelectListItems(mainSCat);

                    var mainRelig = GetReligious();
                    model.Religions = GetSelectListItems(mainRelig);


                    var Course = service.GetCourseDetails(model.Program);
                    model.Courses = GetSelectListItems(Course);

                    var Section = service.GetSectionDetails(model.Course);
                    model.Sections = GetSelectListItems(Section);

                    var Year = GetYear(model.Entry);
                    model.Years = GetSelectListItems(Year);

                    var quota = GetQuota(model.Program);
                    model.Quotas = GetSelectListItems(quota);

                    var QC = GetQuotCategory(model.Quota);
                    model.QuotaCategories = GetSelectListItems(QC);

                    return View("EditStudent", model);
                }
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                ViewBag.Error = errors;
                return View("EditStudent", model);
            }
        }

        [HttpGet]

        public ActionResult Academics()
        {
            //Session["RollNo"] = "1604-19-732-002";
            //Session["StudentId"] = "19ETCE001";
            AcademicModel model = new AcademicModel();
            model.Academics = null;
            return View(model);
        }

        public JsonResult InsertAcademics(IEnumerable<AcademicModel> academics)
        {
            List<Academic> listAC = new List<Academic>();
            RegistrationContext sdb = new RegistrationContext();
            int rescount = 0;
            // string SId = Request.Params["StudentId"].ToString();
            string SId = Session["StudentId"].ToString();
            foreach (var Acd in academics)
            {
                Academic dataModules = new Academic()
                   {
                       HallTicket = Acd.HallTicket,
                       Department = Acd.Department,
                       PassedOut = Acd.PassedOut,
                       Max_Marks = Acd.Max_Marks,
                       Secured_Marks = Acd.Secured_Marks,
                       Division = Acd.Division.ToUpper(),
                       StudentId = SId,
                       Qualification = Acd.Qualification
                   };
                listAC.Add(dataModules);
            }
            rescount = sdb.StudentAcademic(listAC);
            int insertedRecords = rescount;
            return Json(insertedRecords);
        }
    }
}