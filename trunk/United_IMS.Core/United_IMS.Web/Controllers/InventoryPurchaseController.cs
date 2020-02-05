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
    public class InventoryPurchaseController : Controller
    {
        InventoryPurchaseRepo db = new InventoryPurchaseRepo();
        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        AcademicStudentRepo stdb = new AcademicStudentRepo();
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
            INV_PurchaseBill sn = (db.GetBillSerialNo(orgid, (int)id));
            int BSN = Convert.ToInt32(sn.BillSerialNo);
            ViewBag.BSN = BSN;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var details = db.GetPurchaseDetail(orgid, (int)id);
            if (details.InventoryPurchase == null)
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
            INV_PurchaseBill sn = (db.GetBillSerialNo(orgid));
            if (sn == null)
            {
                ViewBag.BSN = 1;
            }
            else
            {
                int sn1 = Convert.ToInt32(sn.BillSerialNo) + 1;
                ViewBag.BSN = sn1;
            }
            ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitByItemList(orgid);
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
                INV_PurchaseBill pbill = new INV_PurchaseBill();
                INV_PurchaseItem pitem;//= new INV_SoldItem();
                int purchaseid = 0;
                var cdate = DateTime.Now;
                try
            {
                //var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));

                if (frm["PurchaseId"] == "0")
                {
                    db.EditInvetoryPurchase(pbill);
                    pbill.VendorId = Convert.ToInt32(frm["VendorId"]);
                    pbill.BillNo = frm["BillNo"];
                    pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                    pbill.Remarks = frm["Remarks"];
                    pbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    pbill.BillDateBS = frm["BillDateBS"];
                    pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                    pbill.IsPreviousStock = frm["IsPreviousStock"] == "Yes" ? true : false;

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
                    pbill.ShippingCharge = 0;// frm[""];
                    pbill.DiscountAmount = 0;// Convert.ToDecimal(frm["DiscountAmount"]);
                    //pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                    pbill.OrganizationId = orgid;
                    pbill.EnteredBy = uid;
                    pbill.EnteredDate = cdate;
                    pbill.IsVerified = true;
                    pbill.VerifiedBy = uid;
                    pbill.VerifiedDate = cdate;
                    pbill.IsDeleted = false;
                    purchaseid = db.AddInvetoryPurchase(pbill);
                    
                }
                else
                {
                    purchaseid = Convert.ToInt32(frm["PurchaseId"]);
                pbill = db.GetPurchaseBillById(orgid,purchaseid);
                pbill.VendorId = Convert.ToInt32(frm["VendorId"]);
                pbill.BillNo = frm["BillNo"];
                pbill.Remarks = frm["Remarks"];
                pbill.BillSerialNo = Convert.ToInt32(frm["BillSerialNo"]);
                ViewBag.BSN2 = Convert.ToInt32(frm["BillSerialNo"]) + 1;
                pbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                pbill.BillDateBS = frm["BillDateBS"];
                pbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                pbill.IsPreviousStock = frm["IsPreviousStock"] == "Yes" ? true : false;

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
                pbill.ShippingCharge = 0;// frm[""];
                pbill.DiscountAmount = 0;// Convert.ToDecimal(frm["DiscountAmount"]);
                                         //pbill.TotalWithVat = Convert.ToDecimal(frm["TotalWithVat"]);
                pbill.OrganizationId = orgid;
                pbill.EnteredBy = uid;
                pbill.EnteredDate = cdate;
                pbill.IsVerified = true;
                pbill.VerifiedBy = uid;
                pbill.VerifiedDate = cdate;
                pbill.IsDeleted = false;
                    db.EditInvetoryPurchase(pbill);
            }
                if (hddrowindex != null)
                {
                    //INV_SoldItem item;
                    foreach (var indx in hddrowindex)
                    {
                        if (frm["PurchaseItemId-" + indx] == "0")
                        {
                            pitem = new INV_PurchaseItem();
                            pitem.PurchaseId = purchaseid;
                            pitem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            pitem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            pitem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.IsReturned = false;


                            //pitem.ReturnedUnitId = frm[""];
                            //pitem.ReturnedQuantity = frm[""];
                            //pitem.ReturnedVerified = frm[""];
                            //pitem.ReturnedDate = frm[""];
                            //pitem.ReturnedBy = frm[""];
                            //pitem.ReturnRemarks = frm[""];
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            pitem.OrganizationId = orgid;

                            db.AddInvetoryPurchaseItem(pitem);
                        }
                        else
                        {
                            pitem = db.GetPurchaseItemById(orgid,Convert.ToInt32(frm["PurchaseItemId-" + indx]));
                            pitem.PurchaseId = purchaseid;
                            pitem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            pitem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            pitem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.IsReturned = false;


                            //pitem.ReturnedUnitId = frm[""];
                            //pitem.ReturnedQuantity = frm[""];
                            //pitem.ReturnedVerified = frm[""];
                            //pitem.ReturnedDate = frm[""];
                            //pitem.ReturnedBy = frm[""];
                            //pitem.ReturnRemarks = frm[""];
                            pitem.IsVerified = true;
                            pitem.VerifiedBy = uid;
                            pitem.VerifiedDate = cdate;
                            pitem.IsDeleted = false;
                            pitem.EnteredBy = uid;
                            pitem.EnteredDate = cdate;
                            pitem.OrganizationId = orgid;

                            db.EditInvetoryPurchaseItem(pitem);
                        }
                    }
                }



                return RedirectToAction("Index");
        }
            catch (Exception ex)
            {
                 ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitList(orgid);
            ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name");
            ViewBag.OrganizationId = orgid;
                //ex.Message;
                return View();
            }

        }

        public ActionResult SavePurchaseBill(string PurchaseId = "", string VendorId = "", string BillNo = "", string BillDate = "", string BillDateBS = "", string TotalAmount = "", string VatApplicable = "", string VatAmount = "0", string VATPercent = "0", string DiscountAmount="0", string TotalWithVat = "0",string IsPreviousStock="", string BillSerialNo="" , string Remarks="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            INV_PurchaseBill pbill;// = new INV_SoldBill();
            //INV_SoldItem solditem;//= new INV_SoldItem();
            //var student = stdb.GetAcademicStudentById(Convert.ToInt32(StudentId));
            if (string.IsNullOrEmpty(PurchaseId) || PurchaseId == "0")
            {

                pbill = new INV_PurchaseBill
                {
                    BillSerialNo = Convert.ToInt32(BillSerialNo),
                    Remarks = Remarks,
                    BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null),
                    OrganizationId = orgid,// OrganizationId;// orgid;
                    BillDateBS = BillDateBS,
                    BillNo = BillNo,
                    IsPreviousStock = IsPreviousStock == "Yes" ? true : false,
                    //pbill.GroupNo = GroupNo;
                    TotalAmount = Convert.ToDecimal(TotalAmount)
                };
                if (VatApplicable == "Yes")
                {
                    pbill.VatApplicable = true;
                    pbill.VatAmount = string.IsNullOrEmpty(VatAmount)?0:Convert.ToDecimal(VatAmount);
                    pbill.VATPercent = string.IsNullOrEmpty(VATPercent) ? 0 : Convert.ToInt32(VATPercent);
                }
                else
                {
                    pbill.VatApplicable = false;
                    pbill.VatAmount = 0;
                    pbill.VATPercent = 0;
                }
                //if (VatApplicable == "No")
                //{
                //    pbill.VatAmount = 0;
                //    pbill.VatApplicable = false;
                //}
                //else if (VatApplicable == "Yes")
                //{
                //    pbill.VatAmount = Convert.ToInt32(VatAmount);
                //    pbill.VatApplicable = true;
                //}
                
                pbill.ShippingCharge = 0;// frm[""];
                pbill.DiscountAmount =Convert.ToDecimal(DiscountAmount);// Convert.ToDecimal(frm["DiscountAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(pbill.TotalWithVat);// + pbill.TotalAmount;// Convert.ToDecimal(TotalWithVat);
                pbill.IsDeleted = false;
                pbill.EnteredBy = uid;// (User as CustomPrincipal).UserId;
                pbill.EnteredDate = DateTime.Now;
                int soldid = db.AddInvetoryPurchase(pbill);
                return Json(new { result = soldid });
            }
            else
            {
                
                pbill = db.GetPurchaseBillById(orgid, Convert.ToInt32(PurchaseId));
                pbill.BillSerialNo = Convert.ToInt32(BillSerialNo);
                pbill.Remarks = Remarks;
                pbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                pbill.OrganizationId = orgid;// orgid;
                pbill.BillDateBS = BillDateBS;
                pbill.BillNo = BillNo;
                //pbill.GroupNo = GroupNo;
                pbill.TotalAmount = Convert.ToDecimal(TotalAmount);
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
                pbill.ShippingCharge = 0;// frm[""];
                pbill.DiscountAmount = Convert.ToDecimal(DiscountAmount);// Convert.ToDecimal(frm["DiscountAmount"]);
                pbill.TotalWithVat = Convert.ToDecimal(TotalWithVat);
                pbill.IsDeleted = false;
                pbill.LastUpdatedBy = uid;// (User as CustomPrincipal).UserId;
                pbill.LastUpdatedDate = DateTime.Now;
                int soldid = db.AddInvetoryPurchase(pbill);
                return Json(new { result = soldid });
            }
        }
        public ActionResult SavePurchaseitem(string PurchaseItemId="",string PurchaseId="", string ItemId="", string UnitId="", string Quantity="" , string Rate="",string  Total="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            INV_PurchaseItem pitem;//
            if (string.IsNullOrEmpty(PurchaseItemId) || PurchaseItemId == "0")
            {
                pitem = new INV_PurchaseItem();
                pitem.PurchaseId = Convert.ToInt32(PurchaseId);
                pitem.OrganizationId = orgid;// orgid;
                pitem.ItemId = Convert.ToInt32(ItemId);
                pitem.UnitId = Convert.ToInt32(UnitId);
                pitem.Quantity = Convert.ToInt32(Quantity);
                pitem.Rate = Convert.ToDecimal(Rate);
                pitem.Total = Convert.ToDecimal(Total);
                pitem.EnteredBy = uid;
                pitem.IsDeleted = false;
                pitem.EnteredDate = DateTime.Now;
                int itemId = Convert.ToInt32(ItemId);
                int quantity = Convert.ToInt32(Quantity);
                int sid = db.AddStockItem(itemId, quantity, orgid);
                int pid=db.AddInvetoryPurchaseItem(pitem);
                return Json(new { result = pid});

            }
            else
            {
                pitem = db.GetPurchaseItemById(orgid, Convert.ToInt32(PurchaseItemId));
                pitem.PurchaseId = Convert.ToInt32(PurchaseId);
                pitem.OrganizationId = orgid;// orgid;
                pitem.ItemId = Convert.ToInt32(ItemId);
                pitem.UnitId = Convert.ToInt32(UnitId);
                pitem.Quantity = Convert.ToInt32(Quantity);
                pitem.Rate = Convert.ToDecimal(Rate);
                pitem.Total = Convert.ToDecimal(Total);
                pitem.LastUpdatedBy = uid;
                pitem.IsDeleted = false;
                pitem.LastUpdatedDate = DateTime.Now;
                db.EditInvetoryPurchaseItem(pitem);
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
            if (detail.InventoryPurchase == null)
            {
                return HttpNotFound();
            }
            INV_PurchaseBill sn = (db.GetBillSerialNo(orgid, (int)id));
            int BSN = Convert.ToInt32(sn.BillSerialNo);
            ViewBag.BSN = BSN;

            ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitByItemList(orgid);
            ViewBag.ItemUnitList = ddl.GetUnitByPurchase((int)id);
            ViewBag.OrganizationId = orgid;
            ViewBag.VendorId = new SelectList(ddl.GetInvVenodrList(orgid), "Id", "Name",detail.InventoryPurchase.VendorId);
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
            INV_PurchaseBill pbill = db.GetPurchaseBillById(orgid, pid);
            INV_PurchaseItem pitem;//= new INV_SoldItem();
            //var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
            try
            {
                pbill.IsPreviousStock = frm["IsPreviousStock"] == "Yes" ? true : false;
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
                pbill.ShippingCharge = 0;// frm[""];
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
                db.EditInvetoryPurchase(pbill);
                if (hddrowindex != null)
                {
                    //INV_SoldItem item;
                    foreach (var indx in hddrowindex)
                    {
                        if (frm["PurchaseItemId-" + indx] == "0")
                        {
                            pitem = new INV_PurchaseItem();
                            pitem.PurchaseId = pid;
                            pitem.OrganizationId = orgid;
                            pitem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            pitem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            pitem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.EnteredBy = (User as CustomPrincipal).UserId;
                            pitem.IsDeleted = false;
                            pitem.EnteredDate = DateTime.Now;
                            db.AddInvetoryPurchaseItem(pitem);
                        }
                        else
                        {
                            pitem = db.GetPurchaseItemById(orgid, Convert.ToInt32(frm["PurchaseItemId-" + indx]));
                            pitem.PurchaseId = pid;
                            pitem.OrganizationId = orgid;
                            pitem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            pitem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            pitem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            pitem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            pitem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            pitem.LastUpdatedBy = uid;
                            pitem.IsDeleted = false;
                            pitem.LastUpdatedDate = DateTime.Now;
                            db.EditInvetoryPurchaseItem(pitem);
                        }
                    }
                }
            }
            catch(Exception ex)
            {

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
            var result = db.DeleteInvetoryPurchase((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
             db.DeleteInvetoryItem((int)id, (User as CustomPrincipal).UserId, DateTime.Now); 
            return RedirectToAction("Index");
        }
    }
}