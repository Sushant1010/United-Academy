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
    public class AcademicStudentController : Controller
    {
        private AcademicStudentRepo db = new AcademicStudentRepo();
        private GetDropdown ddl = new GetDropdown();
        private AcademicStudentClassSectionRepo dbst = new AcademicStudentClassSectionRepo();
        SessionRepo sesrepo = new SessionRepo();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
        }
        // GET: AcademicStudent
        public ActionResult Index(int BatchId = 0,int ClassId = 0,int SectionId = 0,string StudentRegNo="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", SectionId);

            return View(db.GetAcademicStudentList(orgid, BatchId, ClassId, SectionId, StudentRegNo));
        }

        // GET: AcademicStudent/Details/5
        public ActionResult Details(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Student obj = db.GetAcademicStudentById((int)id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", obj.OrganizationId);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", obj.BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.SectionId);
            ViewBag.CurrentClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.CurrentClassId);
            ViewBag.CurrentSectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.CurrentSectionId);
            return View(obj);
        }

        // GET: AcademicStudent/Create
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            ViewBag.CurrentClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.CurrentSectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            return View();
        }

        // POST: AcademicStudent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ACD_Student obj = new ACD_Student();
            obj.StudentName = frm["StudentName"];
            obj.StudentCode = frm["StudentCode"];
            obj.StudentRegNo = frm["StudentRegNo"];
            //obj.Gender = frm["Gender"];
            //if(!string.IsNullOrEmpty(frm["OrganizationId"]))
            obj.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
            if (!string.IsNullOrEmpty(frm["BatchId"]))
                obj.BatchId = Convert.ToInt32(frm["BatchId"]);
            if (!string.IsNullOrEmpty(frm["ClassId"]))
                obj.ClassId = Convert.ToInt32(frm["ClassId"]);
            if (!string.IsNullOrEmpty(frm["SectionId"]))
                obj.SectionId = Convert.ToInt32(frm["SectionId"]);
            if (!string.IsNullOrEmpty(frm["CurrentClassId"]))
                obj.CurrentClassId = Convert.ToInt32(frm["CurrentClassId"]);
            if (!string.IsNullOrEmpty(frm["CurrentSectionId"]))
                obj.CurrentSectionId = Convert.ToInt32(frm["CurrentSectionId"]);
            if (!string.IsNullOrEmpty(frm["AdmissionYear"]))
                obj.AdmissionYear = DateTime.ParseExact(frm["AdmissionYear"], "yyyy-MM-dd", null);
            obj.TemporaryAddress = frm["TemporaryAddress"];
            obj.PermanentAddress = frm["PermanentAddress"];
            obj.Phone = frm["Phone"];
            if (!string.IsNullOrEmpty(frm["DOB"]))
                obj.DOB = DateTime.ParseExact(frm["DOB"], "yyyy-MM-dd", null);
            obj.DOBBS = frm["DOBBS"];
            obj.FatherName = frm["FatherName"];
            obj.FatherContact = frm["FatherContact"];
            obj.MotherName = frm["MotherName"];
            obj.MotherContact = frm["MotherContact"];
            obj.EnteredBy = (User as CustomPrincipal).UserId;
            obj.EnteredDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.InsertAcademicStudent(obj);
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", obj.OrganizationId);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", obj.BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.SectionId);
            ViewBag.CurrentClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.CurrentClassId);
            ViewBag.CurrentSectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.CurrentSectionId);

            return View(obj);
        }

        // GET: AcademicStudent/Edit/5
        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACD_Student obj = db.GetAcademicStudentById((int)id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", obj.OrganizationId);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", obj.BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.SectionId);
            ViewBag.CurrentClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.CurrentClassId);
            ViewBag.CurrentSectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.CurrentSectionId);
            return View(obj);
        }

        // POST: AcademicStudent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ACD_Student obj = db.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
            obj.StudentName = frm["StudentName"];
            obj.StudentCode = frm["StudentCode"];
            obj.StudentRegNo = frm["StudentRegNo"];
            //obj.Gender = frm["Gender"];
            //if (!string.IsNullOrEmpty(frm["OrganizationId"]))
            obj.OrganizationId = orgid;// Convert.ToInt32(frm["OrganizationId"]);
            if (!string.IsNullOrEmpty(frm["BatchId"]))
                obj.BatchId = Convert.ToInt32(frm["BatchId"]);
            if (!string.IsNullOrEmpty(frm["ClassId"]))
                obj.ClassId = Convert.ToInt32(frm["ClassId"]);
            if (!string.IsNullOrEmpty(frm["SectionId"]))
                obj.SectionId = Convert.ToInt32(frm["SectionId"]);
            if (!string.IsNullOrEmpty(frm["CurrentClassId"]))
                obj.CurrentClassId = Convert.ToInt32(frm["CurrentClassId"]);
            if (!string.IsNullOrEmpty(frm["CurrentSectionId"]))
                obj.CurrentSectionId = Convert.ToInt32(frm["CurrentSectionId"]);
            if (!string.IsNullOrEmpty(frm["AdmissionYear"]))
                obj.AdmissionYear = DateTime.ParseExact(frm["AdmissionYear"], "yyyy-MM-dd", null);
            obj.TemporaryAddress = frm["TemporaryAddress"];
            obj.PermanentAddress = frm["PermanentAddress"];
            obj.Phone = frm["Phone"];
            if (!string.IsNullOrEmpty(frm["DOB"]))
                obj.DOB = DateTime.ParseExact(frm["DOB"], "yyyy-MM-dd", null);

            obj.DOBBS = frm["DOBBS"];
            obj.FatherName = frm["FatherName"];
            obj.FatherContact = frm["FatherContact"];
            obj.MotherName = frm["MotherName"];
            obj.MotherContact = frm["MotherContact"];
            obj.LastUpdatedBy = (User as CustomPrincipal).UserId;
            obj.LastUpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.UpdateAcademicStudent(obj);
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name", obj.OrganizationId);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", obj.BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.SectionId);
            ViewBag.CurrentClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", obj.CurrentClassId);
            ViewBag.CurrentSectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", obj.CurrentSectionId);
            return View(obj);
        }

        // GET: AcademicStudent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ACD_Student aCD_Student = db.ACD_Student.Find(id);
            //if (aCD_Student == null)
            //{
            //    return HttpNotFound();
            //}
            var result = db.DeleteAcademicStudent((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            return RedirectToAction("Index");
        }
        public ActionResult AssignSection()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            ViewBag.AcademicYear = new SelectList(ddl.GetYearList(), "Id", "Name");
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignSection(FormCollection form,string[] a)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            try
            {
                ACD_StudentClassSection classSection;
                ACD_Student student;
                DateTime time = DateTime.Now;
                if (a != null && a.Count() > 0)
                {
                    foreach (var stid in a)
                    {
                        classSection = new ACD_StudentClassSection();
                        classSection.StudentId = Convert.ToInt32(stid);
                        classSection.ClassId = Convert.ToInt32(form["ClassId"]);
                        classSection.SectionId = Convert.ToInt32(form["SectionId"]);
                        classSection.AcademicYear = form["AcademicYear"];
                        classSection.AssignedDate = time;
                        classSection.EnteredBy = (User as CustomPrincipal).UserId;
                        classSection.EnteredDate = DateTime.Now;
                        dbst.InsertAcademicStudentClassSection(classSection);
                        student = db.GetAcademicStudentById(Convert.ToInt32(stid));
                        student.CurrentClassId = classSection.ClassId;
                        student.CurrentSectionId = classSection.SectionId;
                        student.LastUpdatedBy = (User as CustomPrincipal).UserId;
                        student.LastUpdatedDate = DateTime.Now;
                        db.UpdateAcademicStudent(student);
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.AcademicYear = new SelectList(ddl.GetYearList(), "Id", "Name");
                ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
                ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
                ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
                return View();
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetStudentByBatchId(int id,int Check)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            JsonResult result = new JsonResult();
            var list = db.GetAcademicStudentList(orgid, id, 0, 0);


            //string msg = "<option>-- Select --</option>";
            //foreach (var item in list)
            //{
            //    msg += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            //}
            int i = 1;
            string msg = "<span onclick='GetStudent(1)' style='margin-left:10px;' class='btn btn-xs pull-right btn-primary'>Select All</span>" +
                " <span onclick= 'GetStudent(0)' class='btn btn-xs btn-primary pull-right'>Select None</span>";

            msg += "<table class = 'xyz table table-bordered' id= 'example'><thead><tr><th>student </th><th>Select</th></tr></thead><tbody>";
            if (Check == 0)
            {
                foreach (var item in list)
                {
                    msg += "<tr><td>" + item.StudentName+"["+item.StudentCode + "]</td><td><input id= 'a_" + i + "' name='a' type='checkbox' value='" + item.StudentId + "'></td></tr>";
                    i++;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    msg += "<tr><td>" + item.StudentName + "[" + item.StudentCode + "]</td><td><input id= 'a_" + i + "' name='a' type='checkbox' value='" + item.StudentId + "' checked='checked'></td></tr>";
                    i++;
                }
            }
            msg += "</tbody></table>";

            result.Data = msg;

            return result;
        }
        // POST: AcademicStudent/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ACD_Student aCD_Student = db.ACD_Student.Find(id);
        //    db.ACD_Student.Remove(aCD_Student);
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
