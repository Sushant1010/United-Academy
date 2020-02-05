using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Models;
using United_IMS.Web.Repository;
using United_IMS.Web.Security;

namespace United_IMS.Web.Controllers
{
    public class KUnitController : Controller
    {
        private KUnitRepo db = new KUnitRepo();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

        }

        // GET: Unit
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetKUnitList(orgid, "", ""));
        }

        // GET: Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            K_Unit k_Unit = db.GetKUnitById((int)id);
            if (k_Unit == null)
            {
                return HttpNotFound();
            }
            return View(k_Unit);
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            K_Unit k_Unit = new K_Unit();
            k_Unit.UnitName = frm["UnitName"];
            //mS_Unit.UnitCode = frm["UnitCode"];
            k_Unit.OrganizationId = orgid;
            k_Unit.EnteredBy = (User as CustomPrincipal).UserId;
            k_Unit.EnteredDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.InsertKUnit(k_Unit);
                return RedirectToAction("Index");
            }

            return View(k_Unit);
        }

        // GET: Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            K_Unit k_Unit = db.GetKUnitById((int)id);
            if (k_Unit == null)
            {
                return HttpNotFound();
            }

            return View(k_Unit);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            K_Unit k_Unit = db.GetKUnitById(Convert.ToInt32(frm["UnitId"]));
            k_Unit.UnitName = frm["UnitName"];
            // mS_Unit.UnitCode = frm["UnitCode"];
            k_Unit.OrganizationId = orgid;
            k_Unit.LastUpdatedBy = (User as CustomPrincipal).UserId;
            k_Unit.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.UpdateKUnit(k_Unit);
                return RedirectToAction("Index");
            }
            return View(k_Unit);
        }

        // GET: Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.DeleteKUnit((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            return RedirectToAction("Index");
        }

        // POST: Unit/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MS_Unit mS_Unit = db.MS_Unit.Find(id);
        //    db.MS_Unit.Remove(mS_Unit);
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
