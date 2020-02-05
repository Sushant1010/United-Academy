using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Models;
using United_IMS.Web.Repository;
using United_IMS.Web.Security;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web.Controllers
{
    [CustomAuthorize]
    public class DashboardController : Controller
    {

        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        // GET: Dashboard
        public ActionResult Index()
        {
            var shistory = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList().Where(c=>c.Id!=shistory.OrganizationId).ToList(), "Id", "Name");
            return View();
        }
        public ActionResult SwitchOrganization(FormCollection frm)
        {
            var shistory = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            sesrepo.EditSession(new SC_LoginHistory()
            {
                UserId = shistory.UserId,
                ActualOrganizationId=shistory.ActualOrganizationId,
                LoginDate=DateTime.Now,
                LoginId=shistory.LoginId,
                LogOutDate=DateTime.Now.AddMinutes(45),
                OrganizationId=Convert.ToInt32(frm["OrganizationId"]),
                RoleId=shistory.RoleId

            });
            return RedirectToAction("Index");
        }
    }
}