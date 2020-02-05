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
    public class AssetController : Controller
    {
        private AssetRepo db = new AssetRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: Category
        public ActionResult Index(int CategoryId=0, int AssetItemId=0, string  AssetCode="",int BranchId=0,int LocationId=0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            //var lst = db.Gey(orgid, FromDate);
            ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name", AssetItemId);
            ViewBag.CategoryId = new SelectList(ddl.GetCategoryList(orgid), "Id", "Name", CategoryId);
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name", BranchId);
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name", LocationId);
            

            return View(db.GetAssetList(orgid,CategoryId,AssetItemId,AssetCode,BranchId,LocationId));
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetVM item = db.GetAssetDetail((int)id);
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
            if(orgid == 1)
            {
                ViewBag.org = "UA";
            }
            if (orgid == 2)
            {
                ViewBag.org = "US";
            }
            if (orgid == 3)
            {
                ViewBag.org = "ORG";
            }
            ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name");            
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name");
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name");
            ViewBag.AccessoryForAssetId = new SelectList(ddl.GetAssetList(orgid), "Id", "Name");
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
            FA_Asset item = new FA_Asset();
            item.AssetItemId = Convert.ToInt32(frm["AssetItemId"]);
            item.PurchaseId = 0;
            item.BillDate = DateTime.ParseExact(frm["BillDate"],"yyyy-MM-dd",null);

            item.BillDateBs = frm["BillDateBs"];
            item.BillNo = frm["BillNo"];
            item.OpeningValue = Convert.ToDecimal(frm["OpeningValue"]);
            item.PurchaseValue = Convert.ToDecimal(frm["PurchaseValue"]);
            string AssetUniqueCode1 = string.IsNullOrEmpty(frm["AssetUniqueCode1"]) ? "" : frm["AssetUniqueCode1"];
            string AssetUniqueCode2 = string.IsNullOrEmpty(frm["AssetUniqueCode2"]) ? "" : frm["AssetUniqueCode2"];
            item.AssetUniqueCode = string.IsNullOrEmpty(frm["AssetUniqueCode"]) ? "" : frm["AssetUniqueCode"];

            string AssetUniqueCode3 = item.AssetUniqueCode + "-" + AssetUniqueCode1 + "-" + AssetUniqueCode2;
            item.Barcode = string.IsNullOrEmpty(frm["Barcode"])?"": frm["Barcode"];
            item.OrganizationId = orgid;
            item.BranchId = string.IsNullOrEmpty(frm["BranchId"])?0:Convert.ToInt32(frm["BranchId"]);
            item.LocationId = string.IsNullOrEmpty(frm["LocationId"]) ? 0 : Convert.ToInt32(frm["LocationId"]);
            item.AssetUniqueCode = frm["AssetUniqueCode"];
            item.Description = frm["Description"];
            item.UsefulLife = Convert.ToDecimal(frm["UsefulLife"]);
            item.CategoryId = frm["CategoryId"];
            if (frm["IsAccessory"]=="Yes")
            {
                item.IsAccessory = true;
                item.AccessoryForAssetId = Convert.ToInt32(frm["AccessoryForAssetId"]);
            }
            else
            {
                item.IsAccessory = false;
                item.AccessoryForAssetId = 0;// Convert.ToInt32(frm["AccessoryForAssetId"]);
            }
            if (frm["IsDepreciationApplicable"] == "Yes")
            {
                item.IsDepreciationApplicable = true;
                item.DepreciationStartDate = DateTime.ParseExact(frm["DepreciationStartDate"], "yyyy-MM-dd", null);
                item.DepreciationRemarks = frm["DepreciationRemarks"];
            }
            else
            {
                item.IsDepreciationApplicable = false;
                item.DepreciationStartDate = null;// DateTime.ParseExact(frm["DepreciationStartDate"], "yyyy-MM-dd", null);
                item.DepreciationRemarks = string.Empty;// frm["DepreciationRemarks"];
            }
            if (frm["IsSold"] == "Yes")
            {
                item.IsSold = true;
                item.SoldDate = DateTime.ParseExact(frm["SoldDate"], "yyyy-MM-dd", null);
                item.SoldValue = Convert.ToDecimal(frm["SoldValue"]);
            }
            else
            {
                item.IsSold = false;
                item.SoldDate = null;// DateTime.ParseExact(frm["SoldDate"], "yyyy-MM-dd", null);
                item.SoldValue = 0;// Convert.ToDecimal(frm["SoldValue"]);
            }
            if (frm["IsScrap"] == "Yes")
            {
                item.IsScrap = true;
                item.ScrapDate = DateTime.ParseExact(frm["ScrapDate"], "yyyy-MM-dd", null);
                item.ScrapRealizedValue = Convert.ToDecimal(frm["ScrapRealizedValue"]);
            }
            else
            {
                item.IsScrap = false;
                item.ScrapDate = null;// DateTime.ParseExact(frm["SoldDate"], "yyyy-MM-dd", null);
                item.ScrapRealizedValue = 0;// Convert.ToDecimal(frm["SoldValue"]);
            }
            item.IsDeleted = false;
            item.EnteredBy = (User as CustomPrincipal).UserId;
            item.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertAsset(item, AssetUniqueCode3);
                return RedirectToAction("Index");
            }

            ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name",item.AssetItemId);
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name",item.BranchId);
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name",item.LocationId);
            ViewBag.AccessoryForAssetId = new SelectList(ddl.GetAssetList(orgid), "Id", "Name",item.AccessoryForAssetId);
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
            FA_Asset item = db.GetAssetById((int)id);
            if (item == null)
            {
                return HttpNotFound();
			}
            ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name", item.AssetItemId);
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name", item.BranchId);
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name", item.LocationId);
            ViewBag.AccessoryForAssetId = new SelectList(ddl.GetAssetList(orgid), "Id", "Name", item.AccessoryForAssetId);
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
            FA_Asset item = db.GetAssetById(Convert.ToInt32(frm["AssetId"]));
            //FA_Asset item = new FA_Asset();
            item.AssetItemId = Convert.ToInt32(frm["AssetItemId"]);
            item.PurchaseId = 0;
            item.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);

            item.BillDateBs = frm["BillDateBs"];
            item.BillNo = frm["BillNo"];
            item.OpeningValue = Convert.ToDecimal(frm["OpeningValue"]);
            item.PurchaseValue = Convert.ToDecimal(frm["PurchaseValue"]);
            item.AssetUniqueCode = string.IsNullOrEmpty(frm["AssetUniqueCode"]) ? "" : frm["AssetUniqueCode"];
            item.Barcode = string.IsNullOrEmpty(frm["Barcode"]) ? "" : frm["Barcode"];
            item.OrganizationId = orgid;
            item.BranchId = string.IsNullOrEmpty(frm["BranchId"]) ? 0 : Convert.ToInt32(frm["BranchId"]);
            item.LocationId = string.IsNullOrEmpty(frm["LocationId"]) ? 0 : Convert.ToInt32(frm["LocationId"]);
            item.Description = frm["Description"];
            item.UsefulLife = Convert.ToDecimal(frm["UsefulLife"]);
            if (frm["IsAccessory"] == "Yes")
            {
                item.IsAccessory = true;
                item.AccessoryForAssetId = Convert.ToInt32(frm["AccessoryForAssetId"]);
            }
            else
            {
                item.IsAccessory = false;
                item.AccessoryForAssetId = 0;// Convert.ToInt32(frm["AccessoryForAssetId"]);
            }
            if (frm["IsDepreciationApplicable"] == "Yes")
            {
                item.IsDepreciationApplicable = true;
                item.DepreciationStartDate = DateTime.ParseExact(frm["DepreciationStartDate"], "yyyy-MM-dd", null);
                item.DepreciationRemarks = frm["DepreciationRemarks"];
            }
            else
            {
                item.IsDepreciationApplicable = false;
                item.DepreciationStartDate = null;// DateTime.ParseExact(frm["DepreciationStartDate"], "yyyy-MM-dd", null);
                item.DepreciationRemarks = string.Empty;// frm["DepreciationRemarks"];
            }
            if (frm["IsSold"] == "Yes")
            {
                item.IsSold = true;
                item.SoldDate = DateTime.ParseExact(frm["SoldDate"], "yyyy-MM-dd", null);
                item.SoldValue = Convert.ToDecimal(frm["SoldValue"]);
            }
            else
            {
                item.IsSold = false;
                item.SoldDate = null;// DateTime.ParseExact(frm["SoldDate"], "yyyy-MM-dd", null);
                item.SoldValue = 0;// Convert.ToDecimal(frm["SoldValue"]);
            }
            if (frm["IsScrap"] == "Yes")
            {
                item.IsScrap = true;
                item.ScrapDate = DateTime.ParseExact(frm["ScrapDate"], "yyyy-MM-dd", null);
                item.ScrapRealizedValue = Convert.ToDecimal(frm["ScrapRealizedValue"]);
            }
            else
            {
                item.IsScrap = false;
                item.ScrapDate = null;// DateTime.ParseExact(frm["SoldDate"], "yyyy-MM-dd", null);
                item.ScrapRealizedValue = 0;// Convert.ToDecimal(frm["SoldValue"]);
            }
            item.IsDeleted = false;
            item.LastUpdatedBy = (User as CustomPrincipal).UserId;
            item.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
				db.UpdateAsset(item);
                return RedirectToAction("Index");
			}
            ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name", item.AssetItemId);
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name", item.BranchId);
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name", item.LocationId);
            ViewBag.AccessoryForAssetId = new SelectList(ddl.GetAssetList(orgid), "Id", "Name", item.AccessoryForAssetId);
            return View(item);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteAsset((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        
    }
}
