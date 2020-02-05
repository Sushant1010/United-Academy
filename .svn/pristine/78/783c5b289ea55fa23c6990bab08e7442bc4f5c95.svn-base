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
    public class AcademicSectionController : Controller
    {
        private AcademicSectionRepo db = new AcademicSectionRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicSection
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetAcademicSectionList(orgid, "",""));
        }

        // GET: AcademicSection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Section aCD_Section = db.GetAcademicSectionById((int)id);
            if (aCD_Section == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Section.OrganizationId);
			return View(aCD_Section);
        }

        // GET: AcademicSection/Create
        public ActionResult Create()
        {
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");

			return View();
        }

        // POST: AcademicSection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;

            string fsectionCode = frm["SectionCode"];
            string fsectionName = frm["SectionName"];
            string sc = null, sn = null;
            int isdelete = 0;
            ACD_Section sectionCodeName = db.GetSectionCodeName(fsectionCode, fsectionName, isdelete);

            ACD_Section aCD_Section = new ACD_Section();
            if (sectionCodeName == null)
            {
                aCD_Section.SectionCode = frm["SectionCode"];
                aCD_Section.SectionName = frm["SectionName"];
                aCD_Section.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
                aCD_Section.EnteredBy = (User as CustomPrincipal).UserId;
                aCD_Section.EnteredDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.InsertAcademicSection(aCD_Section);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (sectionCodeName.IsDeleted == false)
                {
                    sn = sectionCodeName.SectionName;
                    sc = sectionCodeName.SectionCode;
                    if (sn == fsectionName)
                    {
                        ViewBag.CNMsg = "Already Exist..";
                    }
                    if (sc == fsectionCode)
                    {
                        ViewBag.CBMsg = "Already Exist..";
                    }
                    else
                    {
                        aCD_Section.SectionCode = frm["SectionCode"];
                        aCD_Section.SectionName = frm["SectionName"];
                        aCD_Section.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
                        aCD_Section.EnteredBy = (User as CustomPrincipal).UserId;
                        aCD_Section.EnteredDate = DateTime.Now;
                        if (ModelState.IsValid)
                        {
                            db.InsertAcademicSection(aCD_Section);
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    aCD_Section.SectionCode = frm["SectionCode"];
                    aCD_Section.SectionName = frm["SectionName"];
                    aCD_Section.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
                    aCD_Section.EnteredBy = (User as CustomPrincipal).UserId;
                    aCD_Section.EnteredDate = DateTime.Now;
                    if (ModelState.IsValid)
                    {
                        db.InsertAcademicSection(aCD_Section);
                        return RedirectToAction("Index");
                    }
                }
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Section.OrganizationId);
			return View(aCD_Section);
        }

        // GET: AcademicSection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Section aCD_Section = db.GetAcademicSectionById((int)id);
            if (aCD_Section == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Section.OrganizationId);
			return View(aCD_Section);
        }

        // POST: AcademicSection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ACD_Section aCD_Section = db.GetAcademicSectionById(Convert.ToInt32(frm["SectionId"]));
			aCD_Section.SectionCode = frm["SectionCode"];
			aCD_Section.SectionName = frm["SectionName"];
            aCD_Section.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Section.LastUpdatedBy = (User as CustomPrincipal).UserId;
			aCD_Section.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateAcademicSection(aCD_Section);
                return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Section.OrganizationId);
			return View(aCD_Section);
        }

        // GET: AcademicSection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.DeleteAcademicSection((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			//if (aCD_Section == null)
			//{
			//    return HttpNotFound();
			//}
			return RedirectToAction("Index");
        }

        // POST: AcademicSection/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ACD_Section aCD_Section = db.ACD_Section.Find(id);
        //    db.ACD_Section.Remove(aCD_Section);
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
