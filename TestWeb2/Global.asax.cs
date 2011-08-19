using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace TestWeb2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication():base()
        {
            //this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
            //this.EndRequest += new EventHandler(MvcApplication_EndRequest);
            this.AcquireRequestState += new EventHandler(MvcApplication_AcquireRequestState);
            
        }

        void MvcApplication_AcquireRequestState(object sender, EventArgs e)
        {
             
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            LogEntry lg = new LogEntry();
            lg.Message = context.Request.Url.ToString();
            IDictionary<string, object> mp = new Dictionary<string, object>();
            mp.Add("SessionId", context.Session == null ? "0" : context.Session.SessionID);
            mp.Add("UserId", "F15D3366-7ECF-49DF-9565-526694D493EA");
            mp.Add("PreviousUrl", context.Request.UrlReferrer == null ? "" : context.Request.UrlReferrer.ToString());
            mp.Add("NowUrl", context.Request.Url.ToString());
            Logger.Write(lg, mp);
        }


        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Module", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        //void Application_Error(object sender, EventArgs e)
        //{
        //    // 在出现未处理的错误时运行的代码
        //    var ex = Server.GetLastError();


        //    //处理异常
        //    HandleException(ex, "Policy");

        //    //清空异常
        //    Server.ClearError();

        //}
        
        
       /// <summary>
       /// 异常处理方法
       /// </summary>
       /// <param name="ex">异常信息</param>
       /// <param name="policy">异常处理策略</param>

        protected void HandleException(Exception ex, string policy)

        {
            try
            {
                bool rethrow = false;

                var exManager = EnterpriseLibraryContainer.Current.GetInstance<ExceptionManager>();

                rethrow = exManager.HandleException(ex, "Policy");

                //if (rethrow)
                //{


                    //    this.RedirectPermanent("~/error.aspx");


                //}
            }
            catch (Exception eex)
            {
                throw eex;
            }


        }

    }
}