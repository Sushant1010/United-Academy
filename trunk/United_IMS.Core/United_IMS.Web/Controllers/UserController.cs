using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Models;
using United_IMS.Web.Repository;
using United_IMS.Web.Security;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web.Controllers
{
    [CustomAuthorize]
    public class UserController : Controller
    {
        private UserRepo db = new UserRepo();
        private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

        }
        // GET: Category
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            //int orgid = ses.OrganizationId;
            //return View(db.GetUserList(orgid));
            return View(db.GetUserList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVM user = db.GetUserDetail((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }

            //ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(),"Id","Name", category.OrganizationId);
            return View(user);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            // ViewBag.AssetCategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name");
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            SC_User user = new SC_User();
            user.FullName = frm["FullName"];
            user.Email = frm["Email"];
            user.Password = frm["Password"];
            user.OrganizationId = Convert.ToInt32(frm["OrganizationId"]);
            if (frm["IsAdmin"] == "Y")
                user.IsAdmin = true;
            else
                user.IsAdmin = false;
            if (frm["IsActive"] == "Y")
                user.IsActive = true;
            else
                user.IsActive = false;
            user.IsDeleted = false;
            user.EnteredBy = (User as CustomPrincipal).UserId;
            user.EnteredDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.InsertUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");

            // ViewBag.UserId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", user.UserId);
            return View(user);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            SC_User user = db.GetUserById((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            //  ViewBag.UserId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", user.UserId);
            return View(user);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            SC_User user = db.GetUserById(Convert.ToInt32(frm["UserId"]));
            user.FullName = frm["FullName"];
            user.Email = frm["Email"];
            //user.Password = frm["Password"];
            user.OrganizationId = Convert.ToInt32(frm["OrganizationId"]);
            if (frm["IsAdmin"] == "Y")
                user.IsAdmin = true;
            else
                user.IsAdmin = false;
            if (frm["IsActive"] == "Y")
                user.IsActive = true;
            else
                user.IsActive = false;
            user.IsDeleted = false;
            user.LastUpdatedBy = (User as CustomPrincipal).UserId;
            user.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.UpdateUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            //ViewBag.UserId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", user.UserId);
            return View(user);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.DeleteUser((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            return RedirectToAction("Index");
        }


    }
}
