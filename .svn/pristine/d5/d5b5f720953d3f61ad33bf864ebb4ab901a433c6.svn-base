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
    public class AcademicTeacherController : Controller
    {
        private AcademicTeacherRepo db = new AcademicTeacherRepo();
		private GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicTeacher
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            return View(db.GetAcademicTeacherList(orgid));
        }

        // GET: AcademicTeacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Staff obj = db.GetAcademicTeacherById((int)id);
            if (obj == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name",obj.OrganizationId);
            return View(obj);
        }

        // GET: AcademicTeacher/Create
        public ActionResult Create()
        {
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
			return View();
        }

        // POST: AcademicTeacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm,HttpPostedFileBase file)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var teacher = new ACD_Staff();
			teacher.StaffName = frm["StaffName"];
			teacher.StaffCode = frm["StaffCode"];
            teacher.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			teacher.TemporaryAddress = frm["TemporaryAddress"];
			teacher.PermanentAddress = frm["PermanentAddress"];
			teacher.Mobile = frm["Mobile"];
			teacher.Email = frm["Email"];
			teacher.CitizenshipNo = frm["CitizenshipNo"];
			//teacher.JoinDate = DateTime.ParseExact(frm["JoinDate"],"yyyy-MM-dd",null);
			teacher.JoinDateBS = frm["JoinDateBS"];
			//teacher.DOB = DateTime.ParseExact(frm["DOB"], "yyyy-MM-dd", null); ;
			teacher.DOBBS = frm["DOBBS"];
			teacher.Photo = frm["Photo"];
			teacher.EnteredBy = (User as CustomPrincipal).UserId;
			teacher.EnteredDate = DateTime.Now;

			if (ModelState.IsValid)
            {
				db.InsertAcademicTeacher(teacher);
                return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", teacher.OrganizationId);
			return View(teacher);
        }

        // GET: AcademicTeacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Staff obj = db.GetAcademicTeacherById((int)id);
            if (obj == null)
            {
                return HttpNotFound();
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", obj.OrganizationId);
			return View(obj);
        }

        // POST: AcademicTeacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var teacher = db.GetAcademicTeacherById(Convert.ToInt32(frm["StaffId"]));
			teacher.StaffName = frm["StaffName"];
			teacher.StaffCode = frm["StaffCode"];
            teacher.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
			teacher.TemporaryAddress = frm["TemporaryAddress"];
			teacher.PermanentAddress = frm["PermanentAddress"];
			teacher.Mobile = frm["Mobile"];
			teacher.Email = frm["Email"];
			teacher.CitizenshipNo = frm["CitizenshipNo"];
            teacher.JoinDate = DateTime.ParseExact(frm["JoinDate"], "yyyy-MM-dd", null);
			teacher.JoinDateBS = frm["JoinDateBS"];
			teacher.DOB = DateTime.ParseExact(frm["DOB"], "yyyy-MM-dd", null); ;
			teacher.DOBBS = frm["DOBBS"];
			teacher.Photo = frm["Photo"];
			teacher.LastUpdatedBy = (User as CustomPrincipal).UserId;
			teacher.LastUpdatedDate = DateTime.Now;
			if (ModelState.IsValid)
            {
				db.UpdateAcademicTeacher(teacher);
                return RedirectToAction("Index");
            }
			ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", teacher.OrganizationId);
			return View(teacher);
        }

        // GET: AcademicTeacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ACD_Staff obj = db.GetAcademicTeacherById((int)id);
            //if (obj == null)
            //{
            //    return HttpNotFound();
            //}
            var result= db.DeleteAcademicTeacher((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            return RedirectToAction("Index");
        }
      
    }
}
