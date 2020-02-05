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

namespace United_IMS.Web.Controllers
{
    [CustomAuthorize]
    public class DepartmentController : Controller
    {
        private DepartmentRepo db = new DepartmentRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: Department
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetDepartmentList(orgid));
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MS_Department mS_Department = db.GetDepartmentById((int)id);
            if (mS_Department == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name", mS_Department.OrganizationId);
            return View(mS_Department);
        }

        // GET: Department/Create
        public ActionResult Create()
		{
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
			return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            MS_Department mS_Department = new MS_Department();
			mS_Department.DepartmentName = frm["DepartmentName"];
			mS_Department.DepartmentCode = frm["DepartmentCode"];
            mS_Department.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			mS_Department.EnteredBy = (User as CustomPrincipal).UserId;
			mS_Department.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertDepartment(mS_Department);
                return RedirectToAction("Index");
            }

			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Department.OrganizationId);
			return View(mS_Department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MS_Department mS_Department = db.GetDepartmentById((int)id);
            if (mS_Department == null)
            {
                return HttpNotFound();
			}
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Department.OrganizationId);
			return View(mS_Department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            MS_Department mS_Department = db.GetDepartmentById(Convert.ToInt32(frm["DepartmentId"]));
			mS_Department.DepartmentName = frm["DepartmentName"];
			mS_Department.DepartmentCode = frm["DepartmentCode"];
            mS_Department.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			mS_Department.LastUpdatedBy = (User as CustomPrincipal).UserId;
			mS_Department.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateDepartment(mS_Department);
                return RedirectToAction("Index");
			}
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Department.OrganizationId);
			return View(mS_Department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteDepartment((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        // POST: Department/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MS_Department mS_Department = db.MS_Department.Find(id);
        //    db.MS_Department.Remove(mS_Department);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
