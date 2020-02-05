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
    public class AcademicClassSectionController : Controller
    {
        private AcademicClassSectionRepo db = new AcademicClassSectionRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicClassSection
        public ActionResult Index()
        {
			
            return View(db.GetAcademicClassSectionList(0,0,0));
        }

        // GET: AcademicClassSection/Details/5
        public ActionResult Details(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id ==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_ClassSection aCD_ClassSection = db.GetAcademicClassSectionById((int)id);
            if (aCD_ClassSection == null)
            {
                return HttpNotFound();
            }

			ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name",aCD_ClassSection.ClassId);
			ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name",aCD_ClassSection.SectionId);
			ViewBag.BatchId=new SelectList(ddl.GetBatchList(orgid),"Id","Name",aCD_ClassSection.BatchId);
            return View(aCD_ClassSection);
        }

        // GET: AcademicClassSection/Create
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
			ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
			ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
			return View();
        }

        // POST: AcademicClassSection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ACD_ClassSection obj = new ACD_ClassSection();
			obj.BatchId = Convert.ToInt32(frm["BatchId"]);
			obj.ClassId = Convert.ToInt32(frm["ClassId"]);
			obj.SectionId = Convert.ToInt32(frm["SectionId"]);
			obj.StartDate = DateTime.ParseExact(frm["StartDate"], "yyyy-MM-dd", null);
			obj.StartDateBS = frm["StartDateBS"];
            
			obj.EnteredBy = (User as CustomPrincipal).UserId;
			obj.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
                db.InsertAcademicClassSection(obj);
                return RedirectToAction("Index");
            }
			ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name",obj.ClassId);
			ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name",obj.SectionId);
			ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name",obj.BatchId);
			return View(obj);
        }

        // GET: AcademicClassSection/Edit/5
        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_ClassSection obj = db.GetAcademicClassSectionById((int)id);
            if (obj == null)
            {
                return HttpNotFound();
            }
			ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.ClassId);
			ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.SectionId);
			ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", obj.BatchId);
			return View(obj);
        }

        // POST: AcademicClassSection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ACD_ClassSection obj = db.GetAcademicClassSectionById(Convert.ToInt32(frm["ClassSectionId"]));
			obj.BatchId = Convert.ToInt32(frm["BatchId"]);
			obj.ClassId = Convert.ToInt32(frm["ClassId"]);
			obj.SectionId = Convert.ToInt32(frm["SectionId"]);
			obj.StartDate = DateTime.ParseExact(frm["StartDate"], "yyyy-MM-dd", null);
			obj.StartDateBS = frm["StartDateBS"];
			obj.LastUpdatedBy = (User as CustomPrincipal).UserId;
			obj.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateAcademicClassSection(obj);
                return RedirectToAction("Index");
            }
			ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.ClassId);
			ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.SectionId);
			ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", obj.BatchId);
			return View(obj);
        }

        // GET: AcademicClassSection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result= db.DeleteAcademicClassSection((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			//if (aCD_ClassSection == null)
			//{
			//    return HttpNotFound();
			//}
			return RedirectToAction("Index");
        }

        //// POST: AcademicClassSection/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ACD_ClassSection aCD_ClassSection = db.ACD_ClassSection.Find(id);
        //    db.ACD_ClassSection.Remove(aCD_ClassSection);
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
