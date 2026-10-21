
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaquetesViajes
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection && !Request.IsLocal)
            {
                string urlSegura = "https://" +
                    Request.Url.Host +
                    Request.RawUrl;

                Response.Redirect(urlSegura, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
