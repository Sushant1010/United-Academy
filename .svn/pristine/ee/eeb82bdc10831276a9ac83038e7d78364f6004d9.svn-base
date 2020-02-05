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
    public class AcademicBatchController : Controller
    {
		private AcademicBatchRepo db=new AcademicBatchRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        //private AcademicBatchController(AcademicBatchRepo _db)
        //{
        //	db = _db;
        //}
        //= new AcademicBatchRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicBatch
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetAcademicBatchList(orgid,0,"",""));
        }

        // GET: AcademicBatch/Details/5
        public ActionResult Details(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Batch aCD_Batch = db.GetAcademicBatchById((int)id);
            if (aCD_Batch == null)
            {
                return HttpNotFound();
            }
			ViewBag.ProgramId = new SelectList(ddl.GetProgramList(orgid), "Id", "Name", aCD_Batch.ProgramId);
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Batch.OrganizationId);

			return View(aCD_Batch);
        }

        // GET: AcademicBatch/Create
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.ProgramId = new SelectList(ddl.GetProgramList(orgid), "Id", "Name");
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");

			return View();
        }

        // POST: AcademicBatch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var aCD_Batch = new ACD_Batch();
			aCD_Batch.BatchCode = frm["BatchCode"];
			aCD_Batch.BatchName = frm["BatchName"];
			aCD_Batch.ProgramId = Convert.ToInt32(frm["ProgramId"]);
            aCD_Batch.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Batch.StartDateAD = DateTime.ParseExact(frm["StartDateAD"], "yyyy-MM-dd", null);
			aCD_Batch.StartDateBS = frm["StartDateBS"];
			aCD_Batch.EnteredBy = (User as CustomPrincipal).UserId;
			aCD_Batch.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
                //db.ACD_Batch.Add(aCD_Batch);
                db.InsertAcademicBatch(aCD_Batch);
                return RedirectToAction("Index");
            }
			ViewBag.ProgramId = new SelectList(ddl.GetProgramList(orgid), "Id", "Name", aCD_Batch.ProgramId);
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Batch.OrganizationId);

			return View(aCD_Batch);
        }

        // GET: AcademicBatch/Edit/5
        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Batch aCD_Batch = db.GetAcademicBatchById((int)id);
            if (aCD_Batch == null)
            {
                return HttpNotFound();
            }
			ViewBag.ProgramId = new SelectList(ddl.GetProgramList(orgid), "Id", "Name", aCD_Batch.ProgramId);
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Batch.OrganizationId);
			return View(aCD_Batch);
        }

        // POST: AcademicBatch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var aCD_Batch = db.GetAcademicBatchById(Convert.ToInt32(frm["BatchId"]));
			aCD_Batch.BatchCode = frm["BatchCode"];
			aCD_Batch.BatchName = frm["BatchName"];
			aCD_Batch.ProgramId = Convert.ToInt32(frm["ProgramId"]);
            aCD_Batch.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Batch.StartDateAD = DateTime.ParseExact(frm["StartDateAD"],"yyyy-MM-dd",null);
			aCD_Batch.StartDateBS = frm["StartDateBS"];
			aCD_Batch.LastUpdateBy =(User as CustomPrincipal).UserId;
			aCD_Batch.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
				
				db.UpdateAcademicBatch(aCD_Batch);
                return RedirectToAction("Index");
            }
			ViewBag.ProgramId = new SelectList(ddl.GetProgramList(orgid), "Id", "Name",aCD_Batch.ProgramId);
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name",aCD_Batch.OrganizationId);
			return View(aCD_Batch);
        }

        // GET: AcademicBatch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.DeleteAcademicBatch((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            //if (result == false)
            //{
            //    return HttpNotFound();
            //}
            return RedirectToAction("Index");
        }

        //// POST: AcademicBatch/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ACD_Batch aCD_Batch = db.ACD_Batch.Find(id);
        //    db.ACD_Batch.Remove(aCD_Batch);
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
