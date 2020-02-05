using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Repository;

namespace United_IMS.Web.Security
{

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
		SessionRepo sesrepo = new SessionRepo();
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
			if ((CurrentUser != null && string.IsNullOrEmpty(Roles)))
			{
                return true;
            }
            else
            if ((CurrentUser != null && !string.IsNullOrEmpty(Roles)))
            {
				var fo = false;
				string[] rolelist = Roles.Split(',');

				var sesssion1 = sesrepo.GetSessionById(CurrentUser.UserId);
				foreach(var r in rolelist)
				{
					if(r==sesssion1.RoleId.ToString())
					{
						fo= true;
						break;
					}

				}
                return fo;
            }
            else if ( CurrentUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            //return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Login",
                        action = "Index",
                    }
                    ));
            }
            else
            {
                routeData = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary
                 (new
                 {
                     controller = "Error",
                     action = "AccessDenied"
                 }
                 ));
            }

            filterContext.Result = routeData;
        }

    }
}
