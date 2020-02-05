using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
//using System.Web.Http;
using United_IMS.Web.Security;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["CookieUNITED1"];
            // Request.Cookies["CookieUNITED1"];
            if (authCookie != null )//&& authCookie.Expires>DateTime.Now)
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    var serializeModel = JsonConvert.DeserializeObject<SessionVM>(authTicket.UserData);

                    CustomPrincipal principal = new CustomPrincipal(authTicket.Name);
                    principal.UserId = serializeModel.UserId;
                    principal.FullName = serializeModel.FullName;
                    principal.Email = serializeModel.Email;
                    principal.RoleId = serializeModel.RoleId;
                    principal.IsAdmin = serializeModel.IsAdmin;
                    //principal.CompanyId = serializeModel.CompanyId;
                    //principal.BranchId = serializeModel.BranchId;
                    //principal.UserRole = serializeModel.UserRole.ToArray<string>();
                    //principal.RegistrationDate = serializeModel.RegistrationDate;
                    //principal.Mobile = serializeModel.Mobile;
                    //principal.UserId = serializeModel.UserId;
                    //principal.FullName = serializeModel.FirstName;
                    //principal.LastName = serializeModel.LastName;
                    ////principal.Roles = serializeModel.RoleNames.ToArray<string>();
                    //principal.LocationId = serializeModel.LocationId;
                    //principal.LocationTitle = serializeModel.LocationTitle;
                    HttpContext.Current.User = principal;
                }
                catch (CryptographicException cex1)
                {
                    
                    FormsAuthentication.SignOut();
                }
            }


        }
    }
}
