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
    public class LocationController : Controller
    {
        private LocationRepo db = new LocationRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: Location
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetLocationList(orgid));
        }

        // GET: Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FA_Location location = db.GetLocationById((int)id);
            if (location == null)
            {
                return HttpNotFound();
            }
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name", location.OrganizationId);
            return View(location);
        }

        // GET: Location/Create
        public ActionResult Create()
		{
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
			return View();
        }

        // POST: Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            FA_Location location = new FA_Location();
			location.LocationName = frm["LocationName"];
            location.LocationCode = frm["LocationCode"];
            location.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			location.EnteredBy = (User as CustomPrincipal).UserId;
			location.EnteredDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.InsertLocation(location);
                return RedirectToAction("Index");
            }

			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", location.OrganizationId);
			return View(location);
        }

        // GET: Location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FA_Location location = db.GetLocationById((int)id);
            if (location == null)
            {
                return HttpNotFound();
			}
			//ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", location.OrganizationId);
			return View(location);
        }

        // POST: Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            FA_Location location = db.GetLocationById(Convert.ToInt32(frm["LocationId"]));
			location.LocationName = frm["LocationName"];
            location.LocationCode = frm["LocationCode"];
            location.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			location.LastUpdatedBy = (User as CustomPrincipal).UserId;
			location.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateLocation(location);
                return RedirectToAction("Index");
			}
			return View(location);
        }

        // GET: Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null||id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteLocation((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
			return RedirectToAction("Index");
        }

        // POST: Location/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    FA_Location location = db.FA_Location.Find(id);
        //    db.FA_Location.Remove(location);
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
