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
    public class InventoryVendorController : Controller
    {
        private InventoryVendorRepo db = new InventoryVendorRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicClass
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetInventoryVendorList(orgid, ""));
        }

        // GET: AcademicClass/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aCD_Class = db.GetInventoryVendorById((int)id);
            if (aCD_Class == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name",aCD_Class.OrganizationId);

			return View(aCD_Class);
        }

        // GET: AcademicClass/Create
        public ActionResult Create()
        {
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");

			return View();
        }

        // POST: AcademicClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            INV_Vendor aCD_Class = new INV_Vendor();
            aCD_Class.VendorName = frm["VendorName"];
            aCD_Class.Address = frm["Address"];
            aCD_Class.Phone = frm["Phone"];
            aCD_Class.Mobile = frm["Mobile"];
            aCD_Class.PanNo = frm["PanNo"];

            aCD_Class.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Class.EnteredBy = (User as CustomPrincipal).UserId;
			aCD_Class.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
                db.InsertInventoryVendor(aCD_Class);
                return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Class.OrganizationId);

			return View(aCD_Class);
        }

        // GET: AcademicClass/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aCD_Class = db.GetInventoryVendorById((int)id);
            if (aCD_Class == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Class.OrganizationId);

			return View(aCD_Class);
        }

        // POST: AcademicClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            INV_Vendor aCD_Class = db.GetInventoryVendorById(Convert.ToInt32(frm["VendorId"]));
            aCD_Class.VendorName = frm["VendorName"];
            aCD_Class.Address = frm["Address"];
            aCD_Class.Phone = frm["Phone"];
            aCD_Class.Mobile = frm["Mobile"];
            aCD_Class.PanNo = frm["PanNo"];
            aCD_Class.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Class.LastUpdatedBy = (User as CustomPrincipal).UserId;
			aCD_Class.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateInventoryVendor(aCD_Class);
                return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Class.OrganizationId);

			return View(aCD_Class);
        }

        // GET: AcademicClass/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result= db.DeleteInventoryVendor((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			//if (aCD_Class == null)
			//{
			//    return HttpNotFound();
			//}
			return RedirectToAction("Index");
        }

        // POST: AcademicClass/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ACD_Class aCD_Class = db.ACD_Class.Find(id);
        //    db.ACD_Class.Remove(aCD_Class);
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
