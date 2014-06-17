using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ConsoleApplication1;
using test;

namespace MvcApplication1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
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
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            //GlobalData.system = new ForumSystemImp();
            //GlobalData.defaultHandler = new UserHandler(GlobalData.system);
            GlobalData.system = new ForumSystemImpl("amir", "1234", "amir9979@gmail.com", "amir m", "db");
            GlobalData.defaultHandler = new UserHandler(GlobalData.system);
            Thread conThread = new Thread(wrap);
            conThread.Start();




            //server.Program.Main();
            //AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            //RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterRoutes(RouteTable.Routes);
        }

        private void wrap()
        {
            server.Program.Run(GlobalData.system);
        }
    }
}