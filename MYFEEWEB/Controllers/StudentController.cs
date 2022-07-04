using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYFEELIB.Entities;
using MYFEELIB.Domain;
using MYFEELIB.Data;
using System.Text;
using System.IO;

namespace MYFEEWEB.Controllers
{
    public class StudentController : Controller
    {
        StudentAuth Stud = new StudentAuth();
        public ActionResult Index()
        {
            return View(Stud);
        }



        public ActionResult ValidateStudent(StudentAuth data)
        {
            if (ModelState.IsValid)
            {
                Session["RollNo"] = data.RollNo;
                Session["Password"] = data.Password;
                AccountService service = new AccountService();
                Stud = service.ValidateStudent(data);
                if (Stud.Status == "Active")
                    return RedirectToAction("StudentReport", "Enrollment");
                else
                    return RedirectToAction("Index", "Enrollment");
            }

            else
            {
                return View("Index", data);
            }

        }

        //public ActionResult Registration(StudentAuth data)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Session["RollNo"] = data.RollNo;
        //        Session["Password"] = data.Password;
        //        return RedirectToAction("Index", "Enrollment");
        //    }

        //    else
        //    {
        //        return View("Index", data);
        //    }
        //}

    }



}