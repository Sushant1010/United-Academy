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
    [CustomAuthorize]
    public class AssetPurchaseController : Controller
    {
        AssetPurchaseRepo db = new AssetPurchaseRepo();
        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        InventoryVendorRepo vdrdb = new InventoryVendorRepo();
        // GET: InventorySales
        public ActionResult Index(string FromDate = "", string ToDate = "", string BillNo = "", int VendorId = 0)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            //if (string.IsNullOrEmpty(FromDate))
            //{
            //    FromDate = DateTime.Today.ToString("yyyy-MM-dd");
            //}
            //if (string.IsNullOrEmpty(ToDate))
            //{
            //    ToDate = DateTime.Today.ToString("yyyy-MM-dd");
            //}
            
            var lst = db.GetPurchaseList(orgid, FromDate, ToDate,BillNo,VendorId );
            ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name", VendorId);
            
            return View(lst);
        }
        public ActionResult Details(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            FA_Purchase sn = (db.GetBillSerialNo(orgid, (int)id));
            int BSN = Convert.ToInt32(sn.BillSerialNo);
            ViewBag.BSN = BSN;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetPurchaseDetail(orgid, (int)id);
            if (details.AssetPurchase == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            //int orgid= orgid;
            FA_Purchase sn = (db.GetBillSerialNo(orgid));
            if (sn == null)
            {
                ViewBag.BSN = 1;
            }
            else
            {
                int sn1 = Convert.ToInt32(sn.BillSerialNo) + 1;
                ViewBag.BSN = sn1;
            }
            ViewBag.CategoryList = ddl.GetAssetCategoryList(orgid);
            ViewBag.AssetItemList = ddl.GetAssetItemList(orgid);
            ViewBag.UnitList = ddl.GetUnitList(orgid);
            ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name");
            ViewBag.OrganizationId = orgid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm, string[] hddrowindex)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            FA_Purchase pbill = new FA_Purchase();
            FA_PurchaseItem pitem;//= new INV_SoldItem();
            int purchaseid = 0;
            var cdate = DateTime.Now;
            try
            {
                if (frm["PurchaseId"] == "0")
                {
                    pbill = new FA_Purchase();
                    pbill.OrganizationId = orgid;
                    pbill.VendorId = string.IsNullOrEmpty(frm["VendorId"]) ? 0 : Convert.ToInt32(frm["VendorId"]);
                    pbill.BillNo = frm["BillNo"];
                    pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                    pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                    ViewBag.BSN2 = Convert.ToInt32(frm["BillSerialNo"]) + 1;
                    pbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    pbill.BillDateBS = frm["BillDateBS"];
                    pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    //pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);

                    pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);

                    if (frm["VatApplicable"] == "Yes")
                    {
                        pbill.VatApplicable = true;
                        pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);
                        pbill.VATPercent = Convert.ToInt32(frm["VATPercent"]);
                    }
                    else
                    {
                        pbill.VatApplicable = false;
                        pbill.VatAmount = 0;
                        pbill.VATPercent = 0;
                    }
                    pbill.DiscountAmount = 0;// Convert.ToDecimal(frm["DiscountAmount"]);
                    pbill.EnteredBy = uid;
                    pbill.EnteredDate = cdate;
                    pbill.IsVerified = true;
                    pbill.VerifiedBy = uid;
                    pbill.VerifiedDate = cdate;
                    pbill.IsDeleted = false;
                    purchaseid = db.AddAssetPurchase(pbill);
                    //purchaseid = db.AddInvetoryPurchase(pbill);
                }
                else
                {
                    purchaseid = Convert.ToInt32(frm["PurchaseId"]);
                    pbill = db.GetPurchaseById(orgid, purchaseid);
                    //pbill.OrganizationId = orgid;
                    pbill.VendorId = string.IsNullOrEmpty(frm["VendorId"]) ? 0 : Convert.ToInt32(frm["VendorId"]);
                    pbill.BillNo = frm["BillNo"];
                    pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                    ViewBag.BSN2 = Convert.ToInt32(frm["BillSerialNo"]) + 1;
                    pbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    pbill.BillDateBS = frm["BillDateBS"];
                    pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    //pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);
                    pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);

                    if (frm["VatApplicable"] == "Yes")
                    {
                        pbill.VatApplicable = true;
                        pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);
                        pbill.VATPercent = Convert.ToInt32(frm["VATPercent"]);
                    }
                    else
                    {
                        pbill.VatApplicable = false;
                        pbill.VatAmount = 0;
                        pbill.VATPercent = 0;
                    }
                    pbill.DiscountAmount = 0;
                    pbill.LastUpdatedBy = uid;
                    pbill.LastUpdatedDate = cdate;
                    pbill.IsDeleted = false;
                    db.EditAssetPurchase(pbill);
                }
                if (hddrowindex != null)
                {
                    //INV_SoldItem item;
                    foreach (var indx in hddrowindex)
                    {
                        if (frm["PurchaseItemId-" + indx] == "0")
                        {
                            pitem = new FA_PurchaseItem();
                            pitem.OrganizationId = orgid;
                            pitem.PurchaseId = purchaseid;
                            pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + indx]);
                            pitem.PurchaseQuantity = Convert.ToInt32(frm["PurchaseQuantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.RegisterToAsset = string.IsNullOrEmpty(frm["RegisterToAsset-" + indx]) ? false : true;
                            pitem.IsReturned = false;
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            pitem.OrganizationId = orgid;
                            db.AddAssetPurchaseItem(pitem);
                        }
                        else
                        {
                            pitem = db.GetPurchaseItemById(orgid, Convert.ToInt32(frm["PurchaseItemId-" + indx]));
                            pitem.OrganizationId = orgid;
                            pitem.PurchaseId = purchaseid;
                            pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + indx]);
                            pitem.PurchaseQuantity = Convert.ToInt32(frm["PurchaseQuantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.RegisterToAsset = string.IsNullOrEmpty(frm["RegisterToAsset-" + indx]) ? false : true;
                            pitem.IsReturned = false;
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.LastUpdatedBy = uid;
                            pitem.LastUpdatedDate = cdate;
                            pitem.OrganizationId = orgid;
                            db.EditAssetPurchaseItem(pitem);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.CategoryList = ddl.GetAssetCategoryList(orgid);
                ViewBag.AssetItemList = ddl.GetAssetItemList(orgid);
                ViewBag.UnitList = ddl.GetUnitList(orgid);
                ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name");
                ViewBag.OrganizationId = orgid;
                return View();
            }
        }

        public ActionResult SavePurchaseBill(string PurchaseId="",string VendorId="",string BillNo="", string BillDate="", string BillDateBS="",string TotalAmount="",string VatApplicable="", string VatAmount="0", string DiscountAmount="0", string BillSerialNo="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            FA_Purchase pbill;
            var cdate = DateTime.Now;
            if (string.IsNullOrEmpty(PurchaseId) || PurchaseId == "0")
            {

                pbill = new FA_Purchase();
                pbill.BillSerialNo = Convert.ToInt32(BillSerialNo);
                pbill.OrganizationId = orgid;
                pbill.VendorId = string.IsNullOrEmpty(VendorId) ? 0 : Convert.ToInt32(VendorId);
                pbill.BillNo = BillNo;
                pbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                pbill.BillDateBS = BillDateBS;
                pbill.TotalAmount = Convert.ToDecimal(TotalAmount);
                //pbill.VatAmount = Convert.ToDecimal(VatAmount);

                if (VatApplicable == "Yes")
                {
                    pbill.VatApplicable = true;
                    pbill.VatAmount = string.IsNullOrEmpty(VatAmount) ? 0 : Convert.ToDecimal(VatAmount);
                    //pbill.VATPercent = string.IsNullOrEmpty(VATPercent) ? 0 : Convert.ToInt32(VATPercent);
                }
                else
                {
                    pbill.VatApplicable = false;
                    pbill.VatAmount = 0;
                    pbill.VATPercent = 0;
                }

                pbill.DiscountAmount = Convert.ToDecimal(DiscountAmount);// Convert.ToDecimal(frm["DiscountAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(pbill.TotalWithVat);// + pbill.TotalAmount;// Convert.ToDecimal(TotalWithVat);
                pbill.EnteredBy = uid;
                pbill.EnteredDate = cdate;
                pbill.IsVerified = true;
                pbill.VerifiedBy = uid;
                pbill.VerifiedDate = cdate;
                pbill.IsDeleted = false;
                int purchaseid = db.AddAssetPurchase(pbill); //int soldid = db.AddAssetPurchase(pbill);
                return Json(new { result = purchaseid });
            }
            else
            {
                int purchaseid = Convert.ToInt32(PurchaseId);
                pbill = db.GetPurchaseById(orgid, purchaseid);
                pbill.OrganizationId = orgid;
                //pbill.BillSerialNo = Convert.ToInt32(BillSerialNo);
                pbill.VendorId = string.IsNullOrEmpty(VendorId) ? 0 : Convert.ToInt32(VendorId);
                pbill.BillNo = BillNo;
                pbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                pbill.BillDateBS = BillDateBS;
                pbill.TotalAmount = Convert.ToDecimal(TotalAmount);
                //pbill.VatAmount = Convert.ToDecimal(VatAmount);

                if (VatApplicable == "No")
                {
                    pbill.VatAmount = 0;
                    pbill.VatApplicable = false;
                }
                else if (VatApplicable == "Yes")
                {
                    pbill.VatAmount = Convert.ToInt32(VatAmount);
                    pbill.VatApplicable = true;
                }
                pbill.DiscountAmount = Convert.ToDecimal(DiscountAmount);// Convert.ToDecimal(frm["DiscountAmount"]);
                //pbill.TotalWithVat = Convert.ToDecimal(TotalWithVat);
                pbill.LastUpdatedBy = uid;
                pbill.LastUpdatedDate = cdate;
                pbill.IsDeleted = false;
                db.EditAssetPurchase(pbill);
                return Json(new { result = purchaseid });
            }
        }
        public ActionResult SavePurchaseitem(string PurchaseItemId="",string PurchaseId="", string AssetItemId="", string UnitId="", string PurchaseQuantity="" , string Rate="",string  Total="",string RegisterToAsset="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            FA_PurchaseItem pitem;//
            if (string.IsNullOrEmpty(PurchaseItemId) || PurchaseItemId == "0")
            {
                pitem = new FA_PurchaseItem();
                pitem.PurchaseId = Convert.ToInt32(PurchaseId);
                pitem.OrganizationId = orgid;// orgid;
                pitem.AssetItemId = Convert.ToInt32(AssetItemId);
                pitem.PurchaseQuantity = Convert.ToInt32(PurchaseQuantity);
                pitem.Rate = Convert.ToDecimal(Rate);
                pitem.Total = Convert.ToDecimal(Total);
                pitem.RegisterToAsset = RegisterToAsset=="N" ? false : true;
                pitem.EnteredBy = uid;
                pitem.IsDeleted = false;
                pitem.EnteredDate = DateTime.Now;
                int pid=db.AddAssetPurchaseItem(pitem);
                return Json(new { result = pid });

            }
            else
            {
                pitem = db.GetPurchaseItemById(orgid, Convert.ToInt32(PurchaseItemId));
                pitem.PurchaseId = Convert.ToInt32(PurchaseId);
                pitem.OrganizationId = orgid;// orgid;
                pitem.AssetItemId = Convert.ToInt32(AssetItemId);
                pitem.PurchaseQuantity = Convert.ToInt32(PurchaseQuantity);
                pitem.Rate = Convert.ToDecimal(Rate);
                pitem.Total = Convert.ToDecimal(Total);
                pitem.RegisterToAsset = RegisterToAsset == "N" ? false : true;
                //pitem.Rate = Convert.ToDecimal(Rate);
                //pitem.Total = Convert.ToDecimal(Total);
                pitem.LastUpdatedBy = uid;
                pitem.IsDeleted = false;
                pitem.LastUpdatedDate = DateTime.Now;
                db.EditAssetPurchaseItem(pitem);
                return Json(new { result = PurchaseItemId });
            }

        }
        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var detail = db.GetPurchaseDetail(orgid, (int)id);
            if (detail.AssetPurchase == null)
            {
                return HttpNotFound();
            }
            FA_Purchase sn = (db.GetBillSerialNo(orgid, (int)id));
            int BSN = Convert.ToInt32(sn.BillSerialNo);
            ViewBag.BSN = BSN;

            ViewBag.CategoryList = ddl.GetAssetCategoryList(orgid);
            ViewBag.AssetItemList = ddl.GetAssetItemList(orgid);
            ViewBag.UnitList = ddl.GetUnitList(orgid);
            ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name",detail.AssetPurchase.VendorId);
            ViewBag.OrganizationId = orgid;
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm, string[] hddrowindex)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            int pid = Convert.ToInt32(frm["PurchaseId"]);
            DateTime cdate = DateTime.Now;
            FA_Purchase pbill = db.GetPurchaseById(orgid, pid);
            FA_PurchaseItem pitem;//= new INV_SoldItem();
            //var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
            try
            {
                pbill.VendorId = Convert.ToInt32(frm["VendorId"]);
                pbill.BillNo = frm["BillNo"];
                pbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                pbill.BillDateBS = frm["BillDateBS"];
                pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                if (frm["VatApplicable"] == "Yes")
                {
                    pbill.VatApplicable = true;
                    pbill.VatAmount = Convert.ToDecimal(frm["VatAmount"]);
                    pbill.VATPercent = Convert.ToInt32(frm["VATPercent"]);
                }
                else
                {
                    pbill.VatApplicable = false;
                    pbill.VatAmount = 0;
                    pbill.VATPercent = 0;

                }
                pbill.DiscountAmount = Convert.ToDecimal(frm["DiscountAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                pbill.OrganizationId = orgid;
                pbill.LastUpdatedBy = uid;
                pbill.LastUpdatedDate = cdate;
                pbill.IsVerified = true;
                pbill.VerifiedBy = uid;
                pbill.VerifiedDate = cdate;
                pbill.IsDeleted = false;
                db.DeleteAllInvetoryItem(pid, uid, DateTime.Now);
                db.EditAssetPurchase(pbill);
                if (hddrowindex != null)
                {
                    //INV_SoldItem item;
                    foreach (var indx in hddrowindex)
                    {
                        if (frm["PurchaseItemId-" + indx] == "0")
                        {
                            pitem = new FA_PurchaseItem();
                            pitem.PurchaseId = pid;
                            pitem.OrganizationId = orgid;
                            pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + indx]);
                            pitem.PurchaseQuantity = Convert.ToInt32(frm["PurchaseQuantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.RegisterToAsset = string.IsNullOrEmpty(frm["RegisterToAsset-" + indx]) ? false : true;
                            pitem.IsReturned = false;
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;

                            pitem.EnteredBy = (User as CustomPrincipal).UserId;
                            pitem.IsDeleted = false;
                            pitem.EnteredDate = DateTime.Now;
                            db.AddAssetPurchaseItem(pitem);
                        }
                        else
                        {
                            pitem = db.GetPurchaseItemById(orgid, Convert.ToInt32(frm["PurchaseItemId-" + indx]));
                            pitem.PurchaseId = pid;
                            pitem.OrganizationId = orgid;
                            pitem.AssetItemId = Convert.ToInt32(frm["AssetItemId-" + indx]);
                            pitem.PurchaseQuantity = Convert.ToInt32(frm["PurchaseQuantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.RegisterToAsset = string.IsNullOrEmpty(frm["RegisterToAsset-" + indx]) ? false : true;
                            pitem.LastUpdatedBy = uid;
                            pitem.IsDeleted = false;
                            pitem.LastUpdatedDate = DateTime.Now;
                            db.EditAssetPurchaseItem(pitem);
                        }
                    }
                }
            }
            catch
            {
                ViewBag.CategoryList = ddl.GetAssetCategoryList(orgid);
                ViewBag.AssetItemList = ddl.GetAssetItemList(orgid);
                ViewBag.UnitList = ddl.GetUnitList(orgid);
                ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name",pbill.VendorId);
                ViewBag.OrganizationId = orgid;

                return RedirectToAction("Edit", new { id = pid });
            }
            return RedirectToAction("Index");
        }

        // GET: AcademicStudent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ACD_Student aCD_Student = db.ACD_Student.Find(id);
            //if (aCD_Student == null)
            //{
            //    return HttpNotFound();
            //}
            var result = db.DeleteAssetPurchase((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            db.DeleteAssetItem((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            return RedirectToAction("Index");
        }
    }
}
