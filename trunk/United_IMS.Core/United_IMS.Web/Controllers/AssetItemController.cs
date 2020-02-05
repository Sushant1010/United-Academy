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
    public class AssetItemController : Controller
    {
        private AssetItemRepo db = new AssetItemRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: Category
        public ActionResult Index(string FromDate = "", string ToDate = "")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (string.IsNullOrEmpty(FromDate))
            {
                FromDate = DateTime.Today.ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(ToDate))
            {
                ToDate = DateTime.Today.ToString("yyyy-MM-dd");
            }
            return View(db.GetAssetItemList(orgid));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetItemVM item = db.GetAssetItemDetail((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }
			//ViewBag.DepreciationMethodId = new SelectList(ddl.GetDepreciationList(),"Id","Name", category.OrganizationId);
            return View(item);
        }

        // GET: Category/Create
        public ActionResult Create()
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var AssetNameCode = ddl.GetAssetItemList(orgid);
            ViewBag.AssetCategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name");
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
            FA_AssetItem item = new FA_AssetItem();
            item.AssetName = frm["AssetName"];
            item.AssetCode = frm["AssetCode"];
            item.OrganizationId = orgid;
            item.AssetCategoryId = Convert.ToInt32(frm["AssetCategoryId"]);

            if (frm["IsWarranty"] != "No")
            {
                item.LifeSpan = Convert.ToDecimal(frm["LifeSpan"]);
                item.WarrentyDuration = Convert.ToInt32(frm["WarrentyDuration"]);
                //item.WarrentyIndate = Convert.ToDateTime(frm["WarrentyIndate"]);

                item.FromDate = DateTime.ParseExact(frm["FromDate"], "yyyy-MM-dd", null);
                item.FromDateBS = frm["FromDateBS"];
                item.ToDate = DateTime.ParseExact(frm["ToDate"], "yyyy-MM-dd", null);
                item.ToDateBS = frm["ToDateBS"];
                item.IsWarranty = true;
            }
            else
            {
                item.WarrentyDuration = 0;
                item.WarrentyIndate = null;
                item.LifeSpan = 0;

                item.FromDate = null;
                item.FromDateBS = null;
                item.ToDate = null;
                item.ToDateBS = null;
                item.IsWarranty = false;
            }

            item.IsDepreciation = true;
            item.IsDeleted = false;
            item.EnteredBy = (User as CustomPrincipal).UserId;
            item.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertAssetItem(item);
                return RedirectToAction("Index");
            }

            ViewBag.AssetCategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name",item.AssetCategoryId);
            return View(item);
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
            FA_AssetItem item = db.GetAssetItemById((int)id);
            if (item == null)
            {
                return HttpNotFound();
			}
            ViewBag.AssetCategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", item.AssetCategoryId);
            return View(item);
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
            FA_AssetItem item = db.GetAssetItemById(Convert.ToInt32(frm["AssetItemId"]));
            item.AssetName = frm["AssetName"];
            item.AssetCode = frm["AssetCode"];
            item.AssetCategoryId = Convert.ToInt32(frm["AssetCategoryId"]);
            item.OrganizationId = orgid;
            item.LifeSpan = Convert.ToDecimal(frm["LifeSpan"]);
            item.IsDepreciation = true;
            item.IsDeleted = false;
            item.LastUpdatedBy = (User as CustomPrincipal).UserId;
            item.LastUpdatedDate = DateTime.Now;
            if (frm["IsWarranty"] != "No")
            {
                item.WarrentyDuration = Convert.ToInt32(frm["WarrentyDuration"]);
                item.WarrentyIndate = Convert.ToDateTime(frm["WarrentyIndate"]);

                item.FromDate = DateTime.ParseExact(frm["FromDate"], "yyyy-MM-dd", null);
                item.FromDateBS = frm["FromDateBS"];
                item.ToDate = DateTime.ParseExact(frm["ToDate"], "yyyy-MM-dd", null);
                item.ToDateBS = (frm["ToDateBS"]);
                item.IsWarranty = true;
            }
            else
            {
                item.WarrentyDuration = 0;
                item.WarrentyIndate = null;
                item.LifeSpan = 0;
                item.FromDate = null;
                item.FromDateBS = null;
                item.ToDate = null;
                item.ToDateBS = null;
                item.IsWarranty = false;
            }
            

            if (ModelState.IsValid)
            {
				db.UpdateAssetItem(item);
                return RedirectToAction("Index");
			}
            ViewBag.AssetCategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", item.AssetCategoryId);
            return View(item);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteAssetItem((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        
    }
}
