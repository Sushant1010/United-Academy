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
    public class AcademicProgramController : Controller
    {
        private AcademicProgramRepo db = new AcademicProgramRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicProgram
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetAcademicProgramList(orgid, "",""));
        }

        // GET: AcademicProgram/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Program aCD_Program = db.GetAcademicProgramById((int)id);
            if (aCD_Program == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Program.OrganizationId);

			return View(aCD_Program);
        }

        // GET: AcademicProgram/Create
        public ActionResult Create()
        {
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");

			return View();
        }

        // POST: AcademicProgram/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var aCD_Program = new ACD_Program();
			aCD_Program.ProgramCode = frm["ProgramCode"];
			aCD_Program.ProgramName = frm["ProgramName"];
            //aCD_Program.ProgramId = Convert.ToInt32(frm["ProgramId"]);
            aCD_Program.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Program.StartedDate = DateTime.ParseExact(frm["StartedDate"], "yyyy-MM-dd", null);
			aCD_Program.StartedDateBS = frm["StartedDateBS"];
			aCD_Program.EnteredBy = (User as CustomPrincipal).UserId;
			aCD_Program.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
			{
				//db.ACD_Batch.Add(aCD_Batch);
				db.InsertAcademicProgram(aCD_Program);
				return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Program.OrganizationId);

			return View(aCD_Program);
        }

        // GET: AcademicProgram/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Program aCD_Program = db.GetAcademicProgramById((int)id);
            if (aCD_Program == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Program.OrganizationId);

			return View(aCD_Program);
        }

        // POST: AcademicProgram/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
		{
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var aCD_Program = db.GetAcademicProgramById(Convert.ToInt32(frm["ProgramId"]));
			aCD_Program.ProgramCode = frm["ProgramCode"];
			aCD_Program.ProgramName = frm["ProgramName"];
            //aCD_Program.ProgramId = Convert.ToInt32(frm["ProgramId"]);
            aCD_Program.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			aCD_Program.StartedDate = DateTime.ParseExact(frm["StartedDate"], "yyyy-MM-dd", null);
			aCD_Program.StartedDateBS = frm["StartedDateBS"];
			aCD_Program.LastUpdatedBy = (User as CustomPrincipal).UserId;
			aCD_Program.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
			{

				db.UpdateAcademicProgram(aCD_Program);
				return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", aCD_Program.OrganizationId);

			return View(aCD_Program);
        }

        // GET: AcademicProgram/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result= db.DeleteAcademicProgram((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			//ACD_Program aCD_Program = db.ACD_Program.Find(id);
			//if (aCD_Program == null)
			//         {
			//             return HttpNotFound();
			//         }
			return RedirectToAction("Index");
        }

        // POST: AcademicProgram/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ACD_Program aCD_Program = db.ACD_Program.Find(id);
        //    db.ACD_Program.Remove(aCD_Program);
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
