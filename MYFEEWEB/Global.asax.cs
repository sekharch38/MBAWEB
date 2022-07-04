using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MYFEEWEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //void Session_Start(object sender, EventArgs e)
        //{
        //    // Code that runs when a new session is started
        //    if (Session["username"] != null)
        //    {
        //        //Redirect to Welcome Page if Session is not null
        //       // Response.Redirect("Welcome.aspx");

        //    }
        //    else
        //    {
        //        //Redirect to Login Page if Session is null & Expires 
        //       // Response.Redirect("Login.aspx");

        //    }


        //}

        //void Session_End(object sender, EventArgs e)
        //{
        //    // Code that runs when a session ends. 
        //    // Note: The Session_End event is raised only when the sessionstate mode
        //    // is set to InProc in the Web.config file. If session mode is set to StateServer 
        //    // or SQLServer, the event is not raised.
        //   // this .Response .Redirect ()
        //   // this.Response.Redirect("~/Home/Index");

        //    //HttpContext ctx = HttpContext.Current;
        //    //HttpContext.Current.Response.Redirect("~/Home/Index");
        //   // return RedirectToAction("Index", "Admin");
        //    //UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
        //    //filterContext.Result = new RedirectResult(urlHelper.Action("Index", "Home"));

          
        //}

        //protected void Application_Error()
        //{
        //    HttpContext httpContext = HttpContext.Current;
        //    if (httpContext != null)
        //    {
        //        RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
        //        /* When the request is ajax the system can automatically handle a mistake with a JSON response. 
        //           Then overwrites the default response */
        //        if (requestContext.HttpContext.Request.IsAjaxRequest())
        //        {
        //            httpContext.Response.Clear();
        //            string controllerName = requestContext.RouteData.GetRequiredString("controller");
        //            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
        //            IController controller = factory.CreateController(requestContext, controllerName);
        //            ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

        //            JsonResult jsonResult = new JsonResult
        //            {
        //                Data = new { success = false, serverError = "500" },
        //                JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //            };
        //            jsonResult.ExecuteResult(controllerContext);
        //            httpContext.Response.End();
        //        }
        //        else
        //        {
        //            httpContext.Response.Redirect("~/Error");
        //        }
        //    }
        //}
    }
}
