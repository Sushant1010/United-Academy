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
    public class AcademicYearController : Controller
    {
        private AcademicYearRepo db = new AcademicYearRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicYear
        public ActionResult Index()
        {
            return View(db.GetAcademicYearList());
        }

        // GET: AcademicYear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MS_AcademicYear mS_AcademicYear = db.GetAcademicYearById((int)id);
            if (mS_AcademicYear == null)
            {
                return HttpNotFound();
            }
            return View(mS_AcademicYear);
        }

        // GET: AcademicYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AcademicYear/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
			MS_AcademicYear obj = new MS_AcademicYear();
			obj.AcademicYearName=frm["AcademicYearName"];
			if (frm["IsCurrent"] == "Y")
				obj.IsCurrent = true;
			else
				obj.IsCurrent = false;
            var aaa = frm["StartDate"];

            obj.StartDate = DateTime.ParseExact(frm["StartDate"], "yyyy-MM-dd", null);
			obj.EndDate = DateTime.ParseExact(frm["EndDate"], "yyyy-MM-dd", null);
			if (ModelState.IsValid)
            {
				db.InsertAcademicYear(obj);
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // GET: AcademicYear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MS_AcademicYear mS_AcademicYear = db.GetAcademicYearById((int)id);
            if (mS_AcademicYear == null)
            {
                return HttpNotFound();
            }
            return View(mS_AcademicYear);
        }

        // POST: AcademicYear/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
			MS_AcademicYear obj = db.GetAcademicYearById(Convert.ToInt32(frm["AcademicYearId"]));
			obj.AcademicYearName = frm["AcademicYearName"];
			if (frm["IsCurrent"] == "Y")
				obj.IsCurrent = true;
			else
				obj.IsCurrent = false;
			obj.StartDate = DateTime.ParseExact(frm["StartDate"], "yyyy-MM-dd", null);
			obj.EndDate = DateTime.ParseExact(frm["EndDate"], "yyyy-MM-dd", null);
			if (ModelState.IsValid)
            {
				db.UpdateAcademicYear(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: AcademicYear/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var result = db.DeleteAcademicYear((int)id,0,DateTime.Now);
			return RedirectToAction("Index");
        }

        // POST: AcademicYear/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MS_AcademicYear mS_AcademicYear = db.MS_AcademicYear.Find(id);
        //    db.MS_AcademicYear.Remove(mS_AcademicYear);
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
