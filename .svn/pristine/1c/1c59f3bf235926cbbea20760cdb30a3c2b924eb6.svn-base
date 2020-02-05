using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using United_IMS.Web.Repository;
using United_IMS.Web.Security;
using United_IMS.Web.ViewModel;
using Newtonsoft.Json;

namespace United_IMS.Web.Controllers
{
    public class ReportController : Controller
    {
        ReportRepo db = new ReportRepo();
        SessionRepo sesrepo = new SessionRepo();
        GetDropdown ddl = new GetDropdown();
        // GET: Report
        public ActionResult Index()
        {
            return HttpNotFound();
        }
        public string DetailedSalesReport()
        //public List<DetailedSalesVM> DetailedSalesReport()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var result = db.GetSalesDetail(orgid,null,null,null,null,"Both",0,0,0,0);
            return JsonConvert.SerializeObject(result);
        }
        //GetSalesDetailedSummary
        public ActionResult DetailedSummarySalesReport(string FromDate = "", string ToDate = "", string IsStaff = "Both", int BatchId = 0, int ClassId = 0, int SectionId = 0, int StudentId = 0, int StaffId = 0, string BillNo = "", string GroupNo = "")
        //public List<DetailedSalesVM> DetailedSalesReport()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var result = db.GetSalesDetailedSummary(orgid, FromDate,ToDate, GroupNo, BillNo,IsStaff , ClassId, SectionId, StudentId, 0);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name",BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name",ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name",SectionId);
            //ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name",StaffId);
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid,0,0,0), "Id", "Name",StudentId);
            //return JsonConvert.SerializeObject(result);
            return View(result);
        }
        public ActionResult SummarySalesReport(string FromDate = "", string ToDate = "", string IsStaff = "Both", int BatchId = 0, int ClassId = 0, int SectionId = 0, int StudentId = 0, int StaffId = 0, string BillNo = "", string GroupNo = "")
        //public List<DetailedSalesVM> DetailedSalesReport()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            var result = db.GetSalesSummary(orgid, FromDate, ToDate, GroupNo, BillNo, IsStaff, ClassId, SectionId, StudentId, 0);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name", BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name", ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", SectionId);
            //ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name",StaffId);
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid, 0, 0, 0), "Id", "Name", StudentId);
            //return JsonConvert.SerializeObject(result);
            return View(result);
        }
    }
}