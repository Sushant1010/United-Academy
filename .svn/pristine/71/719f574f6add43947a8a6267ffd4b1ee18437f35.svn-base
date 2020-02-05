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
    public class BranchController : Controller
    {
        private BranchRepo db = new BranchRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: Branch
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetBranchList(orgid));
        }

        // GET: Branch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FA_Branch branch = db.GetBranchById((int)id);
            if (branch == null)
            {
                return HttpNotFound();
            }
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name", branch.OrganizationId);
            return View(branch);
        }

        // GET: Branch/Create
        public ActionResult Create()
		{
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
			return View();
        }

        // POST: Branch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            FA_Branch branch = new FA_Branch();
			branch.BranchName = frm["BranchName"];
            branch.BranchCode = frm["BranchCode"];
            branch.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			branch.EnteredBy = (User as CustomPrincipal).UserId;
			branch.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertBranch(branch);
                return RedirectToAction("Index");
            }

			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", branch.OrganizationId);
			return View(branch);
        }

        // GET: Branch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FA_Branch branch = db.GetBranchById((int)id);
            if (branch == null)
            {
                return HttpNotFound();
			}
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", branch.OrganizationId);
			return View(branch);
        }

        // POST: Branch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            FA_Branch branch = db.GetBranchById(Convert.ToInt32(frm["BranchId"]));
			branch.BranchName = frm["BranchName"];
            branch.BranchCode = frm["BranchCode"];
            branch.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			branch.LastUpdatedBy = (User as CustomPrincipal).UserId;
			branch.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateBranch(branch);
                return RedirectToAction("Index");
			}
			return View(branch);
        }

        // GET: Branch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteBranch((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        // POST: Branch/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    FA_Branch branch = db.FA_Branch.Find(id);
        //    db.FA_Branch.Remove(branch);
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
