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
    public class AssetCategoryController : Controller
    {
        private AssetCategoryRepo db = new AssetCategoryRepo();
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
            return View(db.GetAssetCategoryList(orgid));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetCategoryVM category = db.GetAssetCategoryDetail((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
			//ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(),"Id","Name", category.OrganizationId);
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(orgid), "Id", "Name");
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
            FA_AssetCategory category = new FA_AssetCategory();
            category.CategoryName = frm["CategoryName"];
            category.CategoryCode = frm["CategoryCode"];
            category.DepreciationMethodId = Convert.ToInt32(frm["DepreciationMethodId"]);
            category.DepreciationRate = Convert.ToDecimal(frm["DepreciationRate"]);
            category.OrganizationId = orgid;
            category.IsDeleted = false;
            category.EnteredBy = (User as CustomPrincipal).UserId;
            category.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertAssetCategory(category);
                return RedirectToAction("Index");
            }

            ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(orgid), "Id", "Name",category.DepreciationMethodId);
            return View(category);
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
            FA_AssetCategory category = db.GetAssetCategoryById((int)id);
            if (category == null)
            {
                return HttpNotFound();
			}
            ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(orgid), "Id", "Name", category.DepreciationMethodId);
            return View(category);
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
            FA_AssetCategory category = db.GetAssetCategoryById(Convert.ToInt32(frm["AssetCategoryId"]));
            category.CategoryName = frm["CategoryName"];
            category.CategoryCode = frm["CategoryCode"];
            category.DepreciationMethodId = Convert.ToInt32(frm["DepreciationMethodId"]);
            category.DepreciationRate = Convert.ToDecimal(frm["DepreciationRate"]);
            //category.IsDeleted = false;
            category.LastUpdatedBy = (User as CustomPrincipal).UserId;
            category.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
				db.UpdateAssetCategory(category);
                return RedirectToAction("Index");
			}
            ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(orgid), "Id", "Name", category.DepreciationMethodId);
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteAssetCategory((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        
    }
}
