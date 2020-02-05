using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Repository;
using United_IMS.Web.ViewModel;
using United_IMS.Web.Models;
using United_IMS.Web.Security;
using System.Net;

namespace United_IMS.Web.Controllers
{
    public class AssetTransferController : Controller
    {
        AssetTransferRepo db = new AssetTransferRepo();
        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();

        // GET: AssetTransfer
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var lst = db.GetAssetTransferList();

            return View(lst);
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.GetAssetTransferDetail((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            ViewBag.AssetItemId = new SelectList(ddl.GetAssetList1(orgid), "Id", "Name");
            ViewBag.CategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name");
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name");
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name");
            ViewBag.AssignedToId = new SelectList(ddl.GetUserList(orgid), "Id", "Name");
            ViewBag.OrganizationId1 = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            ViewBag.BranchId1 = new SelectList(ddl.GetBranchByOrgId(orgid));
            ViewBag.LocationId1 = new SelectList(ddl.GetLocationByOrgId(orgid));
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
            int uid = (User as CustomPrincipal).UserId;
            FA_AssetTransfer item = new FA_AssetTransfer();
            item.CategoryId = Convert.ToInt32(frm["CategoryId"]);
            item.AssetId = Convert.ToInt32(frm["AssetItemId"]);
            FA_Asset assetid1 = db.getAssetId(item.AssetId);
            if (assetid1 != null)
            {
                int assetid = assetid1.AssetId;
            }
            item.OrganizationId = string.IsNullOrEmpty(frm["OrganizationId1"]) ? 0 : Convert.ToInt32(frm["OrganizationId1"]);
            item.BranchId = string.IsNullOrEmpty(frm["BranchId1"]) ? 0 : Convert.ToInt32(frm["BranchId1"]);
            item.LocationId = string.IsNullOrEmpty(frm["LocationId1"]) ? 0 : Convert.ToInt32(frm["LocationId1"]);
            item.TransferDate = DateTime.ParseExact(frm["TransferDate"], "yyyy-MM-dd", null);
            item.TransferDateBS = frm["TransferDateBS"];
            item.Remarks = frm["Remarks"];
            item.AssignedTo = Convert.ToInt32(frm["AssignedToID"]);
            item.AssignedDate = DateTime.ParseExact(frm["AssignedDate"], "yyyy-MM-dd", null);
            item.IsReceived = null;
            item.ReceivedBy = null;
            item.ReceivedDate = null;
            item.EnteredBy = uid;
            item.EnteredDate = DateTime.Now;
            item.LastUpdatedBy = null;
            item.LastUpdatedDate = null;
            item.IsDeleted = false;
            item.DeletedBy = null;
            item.DeletedDate = null;
            //item.TransferId = 0;
            item.IsTransfered = 0;
            if (ModelState.IsValid)
            {
                db.InsertAssetTransfer(item);
                AssetTransferVM assetTransid = db.GetAssetTransferId();
                int assetTraId = assetTransid.AssetTransferId;
                int assetId = assetTransid.AssetId;
                db.UpdateAsset(assetTraId, assetId);
                AssetTransferVM PrevAssetTraId = db.GetAssetTransferSecondLastValue(assetTraId, assetId);
                if (PrevAssetTraId != null)
                {
                    int PrevAssetTransferId = PrevAssetTraId.AssetTransferId;
                    assetId = PrevAssetTraId.AssetId;
                    db.UpdateAssetTransfer(PrevAssetTransferId, assetId, assetTraId);
                }
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", item.OrganizationId);
            //ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name", item.AssetItemId);
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(orgid), "Id", "Name", item.BranchId);
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(orgid), "Id", "Name", item.LocationId);
            //ViewBag.AccessoryForAssetId = new SelectList(ddl.GetAssetList(orgid), "Id", "Name", item.AccessoryForAssetId);
            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.GetAssetTransferDetail((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", item.OrganizationId);
            ViewBag.CategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", item.CategoryId);
            ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name", item.AssetItemId);
            ViewBag.BranchId = new SelectList(ddl.GetBranchList(item.OrganizationId), "Id", "Name", item.BranchId);
            ViewBag.LocationId = new SelectList(ddl.GetLocationList(item.OrganizationId), "Id", "Name", item.LocationId);
            ViewBag.FullName = new SelectList(ddl.GetUserList(item.OrganizationId), "Id", "Name", item.UserId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            FA_AssetTransfer item = db.GetAssetTransferById(Convert.ToInt32(frm["AssetTransferId"]));
            item.AssetTransferId = Convert.ToInt32(frm["AssetTransferId"]);
            item.CategoryId = Convert.ToInt32(frm["CategoryId"]);
            
            item.AssetId = string.IsNullOrEmpty(frm["AssetId"]) ? 0 : Convert.ToInt32(frm["AssetId"]);
            int assetItemId = Convert.ToInt32(frm["AssetItemId"]);
            item.OrganizationId = string.IsNullOrEmpty(frm["OrganizationId"]) ? 0 : Convert.ToInt32(frm["OrganizationId"]);
            item.BranchId = string.IsNullOrEmpty(frm["BranchId"]) ? 0 : Convert.ToInt32(frm["BranchId"]);
            item.LocationId = string.IsNullOrEmpty(frm["LocationId"]) ? 0 : Convert.ToInt32(frm["LocationId"]);
            item.TransferDate = DateTime.ParseExact(frm["TransferDate"], "yyyy-MM-dd", null);
            item.TransferDateBS = frm["TransferDateBS"];
            item.Remarks = frm["Remarks"];
            item.AssignedTo = Convert.ToInt32(frm["FullName"]);
            item.AssignedDate = DateTime.ParseExact(frm["AssignedDate"], "yyyy-MM-dd", null);


            item.IsDeleted = false;
            item.LastUpdatedBy = (User as CustomPrincipal).UserId;
            item.LastUpdatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.updateAssetForAssetItem(item.AssetId, assetItemId);
                db.UpdateAssetTransfer(item);
                return RedirectToAction("Index");
            }
            //ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", item.OrganizationId);
            //ViewBag.CategoryId = new SelectList(ddl.GetAssetCategoryList(orgid), "Id", "Name", item.CategoryId);
            //ViewBag.AssetItemId = new SelectList(ddl.GetAssetItemList(orgid), "Id", "Name", item.AssetItemId);
            //ViewBag.BranchId = new SelectList(ddl.GetBranchList(item.OrganizationId), "Id", "Name", item.BranchId);
            //ViewBag.LocationId = new SelectList(ddl.GetLocationList(item.OrganizationId), "Id", "Name", item.LocationId);
            //ViewBag.FullName = new SelectList(ddl.GetUserList(item.OrganizationId), "Id", "Name", item.UserId);
            return View(item);
        }

        public ActionResult Transfer(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var assetid = db.GetAssetTransferDetail((int) id);
            int assetId = Convert.ToInt32(assetid.AssetId);
            var lst = db.GetAssetTransferHistory(assetId);

            return View(lst);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assetid = db.GetAssetTransferDetail((int)id);
            int assetId = Convert.ToInt32(assetid.AssetId);
            var asset2Last = db.GetAssetTransferSecondLastValue((int)id,assetId);
            if(asset2Last != null)
            {
                int assetTransferId = Convert.ToInt32(asset2Last.AssetTransferId);
                db.UpdateAssetTransfer(assetTransferId);
                db.UpdateAsset(assetTransferId,assetId);
                db.DeleteAssetTransfer((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            }
            else
            {
                
                db.UpdateAssetTransfer((int)id);
                db.UpdateAsset1((int)id, assetId);
                db.DeleteAssetTransfer((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            }
            
            return RedirectToAction("Index");
        }

    }
}