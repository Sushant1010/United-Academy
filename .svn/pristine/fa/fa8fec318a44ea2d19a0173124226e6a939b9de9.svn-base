using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Repository;
using United_IMS.Web.Models;
using United_IMS.Web.Security;
using System.Net;

namespace United_IMS.Web.Controllers
{
    public class StockItemController : Controller
    {
        StockItemRepo db = new StockItemRepo();
        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        AcademicStudentRepo stdb = new AcademicStudentRepo();
        AcademicTeacherRepo tdb = new AcademicTeacherRepo();
        // GET: StockItem
        public ActionResult Index()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;

            var lst = db.GetStockItemList(orgid);
            //ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", BatchId);
            //ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", ClassId);
            //ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", SectionId);
            //ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name", StaffId);
            //ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid, 0, 0, 0), "Id", "Name", StudentId);
            return View(lst);
        }

    }
}