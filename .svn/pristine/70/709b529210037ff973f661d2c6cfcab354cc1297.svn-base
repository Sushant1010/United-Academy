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
    public class CategoryController : Controller
    {
        private CategoryRepo db = new CategoryRepo();
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
            return View(db.GetCategoryList(orgid));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MS_Category mS_Category = db.GetCategoryById((int)id);
            if (mS_Category == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name", mS_Category.OrganizationId);
            return View(mS_Category);
        }

        // GET: Category/Create
        public ActionResult Create()
		{
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
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
            MS_Category mS_Category = new MS_Category();
			mS_Category.CategoryName = frm["CategoryName"];
			mS_Category.CategoryCode = frm["CategoryCode"];
            mS_Category.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			mS_Category.EnteredBy = (User as CustomPrincipal).UserId;
			mS_Category.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertCategory(mS_Category);
                return RedirectToAction("Index");
            }

			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Category.OrganizationId);
			return View(mS_Category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MS_Category mS_Category = db.GetCategoryById((int)id);
            if (mS_Category == null)
            {
                return HttpNotFound();
			}
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Category.OrganizationId);
			return View(mS_Category);
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
            MS_Category mS_Category = db.GetCategoryById(Convert.ToInt32(frm["CategoryId"]));
			mS_Category.CategoryName = frm["CategoryName"];
			mS_Category.CategoryCode = frm["CategoryCode"];
            mS_Category.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			mS_Category.LastUpdatedBy = (User as CustomPrincipal).UserId;
			mS_Category.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateCategory(mS_Category);
                return RedirectToAction("Index");
			}
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", mS_Category.OrganizationId);
			return View(mS_Category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteCategory((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
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
