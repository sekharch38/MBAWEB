using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYFEELIB.Domain;
using MYFEELIB.Entities;
//using TextBox_Validation_MVC.Models;

namespace MYFEEWEB.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        User usr = new User();
        public ActionResult Index()
        {
            var model = new User();
            return View(model);
        }

        public ActionResult ValidateUserLogin(User data)
        {
            AccountService service = new AccountService();
            usr = service.ValidateUser(data);
            Session["user"] = usr;

            if (usr.isValid)
            {
                Session["user"] = usr;
                Session["username"] = usr.Username;
                Session["type"] = usr.UserType;
                //if (usr.UserType == 1)
                return RedirectToAction("DashBoard", "Admin");
            }
            else
            {
                return View("Index", data);
            }
            return View("Index", data);
        }

    }
}