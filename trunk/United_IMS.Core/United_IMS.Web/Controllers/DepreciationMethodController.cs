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
    public class DepreciationMethodController : Controller
    {
        private FADepreciationMethodRepo db = new FADepreciationMethodRepo();
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
            int orgid = ses.OrganizationId;
            return View(db.GetDepreciationMethodList(orgid));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FA_DepreciationMethod method = db.GetDepreciationMethodById((int)id);
            if (method == null)
            {
                return HttpNotFound();
            }
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name", method.OrganizationId);
            return View(method);
        }

        // GET: Category/Create
        public ActionResult Create()
		{
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
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
            FA_DepreciationMethod method = new FA_DepreciationMethod();
            method.MethodName = frm["MethodName"];
            method.DepreciationRate = Convert.ToDecimal(frm["DepreciationRate"]);
            method.Description = frm["Description"];
            method.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
            method.EnteredBy = (User as CustomPrincipal).UserId;
            method.EnteredDate = DateTime.Now;
            method.IsDeleted = false;

            if (ModelState.IsValid)
            {
				db.InsertDepreciationMethod(method);
                return RedirectToAction("Index");
            }

			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Category.OrganizationId);
			return View(method);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FA_DepreciationMethod method = db.GetDepreciationMethodById((int)id);
            if (method == null)
            {
                return HttpNotFound();
			}
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Category.OrganizationId);
			return View(method);
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
            FA_DepreciationMethod method = db.GetDepreciationMethodById(Convert.ToInt32(frm["MethodId"]));
            method.MethodName = frm["MethodName"];
            method.DepreciationRate = Convert.ToDecimal(frm["DepreciationRate"]);
            method.Description = frm["Description"];
            method.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
            method.LastUpdatedBy = (User as CustomPrincipal).UserId;
            method.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
				db.UpdateDepreciationMethod(method);
                return RedirectToAction("Index");
			}
			return View(method);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteDepreciationMethod((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        // POST: Category/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MS_Category mS_Category = db.MS_Category.Find(id);
        //    db.MS_Category.Remove(mS_Category);
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
