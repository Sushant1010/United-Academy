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
    [CustomAuthorize]
    public class InventorySalesController : Controller
    {
        InventorySoldRepo db = new InventorySoldRepo();
        GetDropdown ddl = new GetDropdown();
        SessionRepo sesrepo = new SessionRepo();
        AcademicStudentRepo stdb = new AcademicStudentRepo();
        AcademicTeacherRepo tdb = new AcademicTeacherRepo();
        // GET: InventorySales
        public ActionResult Index(string FromDate="",string ToDate="",string IsStaff="Both",int BatchId=0,int ClassId=0,int SectionId=0,int StudentId=0,int StaffId=0,string BillNo="",string GroupNo="")
        {

            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            //if (string.IsNullOrEmpty(FromDate))
            //{
            //    FromDate = DateTime.Today.ToString("yyyy-MM-dd");
            //}
            //if (string.IsNullOrEmpty(ToDate))
            //{
            //    ToDate = DateTime.Today.ToString("yyyy-MM-dd");
            //}
            //if (IsStaff == "No")
            //{
            //    StaffId = 0;
            //}
            //if (IsStaff == "Yes")
            //{
            //    StudentId = 0;
            //    BatchId = 0;
            //    ClassId = 0;
            //    SectionId = 0;
            //}
            var lst = db.GetSoldList(orgid, FromDate, ToDate, IsStaff, ClassId, StudentId, StaffId, BillNo, GroupNo);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name",BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name",ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name",SectionId);
            ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name",StaffId);
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid,0,0,0), "Id", "Name",StudentId);
            return View(lst);
        }
        public ActionResult Details(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id==null || id == 0)
            {
                return HttpNotFound();
            }
            var details=db.GetSoldDetail(orgid,(int)id);
            if(details.InventorySold==null)
            {
                return HttpNotFound();
            }
            return View(details);
        }
        public ActionResult Create()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            //int orgid= orgid;
            ViewBag.CategoryList=ddl.GetCategoryList(orgid);
            ViewBag.ItemList =ddl.GetItemList(orgid,0);
            ViewBag.UnitList = ddl.GetUnitByItemList(orgid);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name");
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid,0,0,0), "Id", "Name");
            ViewBag.OrganizationId = orgid;
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection frm,string[] hddrowindex)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            INV_SoldBill soldbill = new INV_SoldBill();
            INV_SoldItem solditem;//= new INV_SoldItem();
            int soldid = 0;
            //var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));

            if (frm["SoldBillId"] == "0")
            {
                soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                soldbill.OrganizationId = orgid;// orgid;
                soldbill.BillDateBS = frm["BillDateBS"];
                soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");// frm["BillNo"];
                soldbill.GroupNo = frm["GroupNo"];
                soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                
                if (frm["IsStaff"]=="No")
                {
                    var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
                    if (student.CurrentClassId != null)
                        soldbill.ClassId = student.CurrentClassId;
                    if (student.CurrentSectionId != null)
                        soldbill.SectionId = student.CurrentSectionId;
                    soldbill.StudentId = Convert.ToInt32(frm["StudentId"]);
                    soldbill.IsStaff = false;
                }
                else
                {
                    soldbill.StaffId = Convert.ToInt32(frm["StaffId"]);
                    soldbill.IsStaff = true;
                }
                soldbill.IsDeleted = false;
                soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                soldbill.EnteredDate = DateTime.Now;
                soldid = db.AddInvetorySold(soldbill);
            }
            else
            {
                soldid=Convert.ToInt32(frm["SoldBillId"]);
                soldbill.SoldBillId = soldid;
                soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                soldbill.OrganizationId = orgid;// orgid;
                soldbill.BillDateBS = frm["BillDateBS"];
                soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");// frm["BillNo"];
                soldbill.GroupNo = frm["GroupNo"];
                soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);

                if (frm["IsStaff"] == "No")
                {
                    var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
                    if (student.CurrentClassId != null)
                        soldbill.ClassId = student.CurrentClassId;
                    if (student.CurrentSectionId != null)
                        soldbill.SectionId = student.CurrentSectionId;
                    soldbill.StudentId = Convert.ToInt32(frm["StudentId"]);
                    soldbill.IsStaff = false;
                }
                else
                {
                    soldbill.StaffId = Convert.ToInt32(frm["StaffId"]);
                    soldbill.IsStaff = true;
                }
                soldbill.IsDeleted = false;
                soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                soldbill.EnteredDate = DateTime.Now;
                db.EditInvetorySold(soldbill);
            }
            if(hddrowindex!=null)
            {
                //INV_SoldItem item;
                foreach (var indx in hddrowindex)
                {
                    if (frm["SoldItemId-" + indx] == "0")
                    { 
                    solditem = new INV_SoldItem();
                    solditem.SoldBillId = soldid;
                    solditem.OrganizationId = orgid;// orgid;
                    solditem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                    solditem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                    solditem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                    solditem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                    solditem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                    solditem.EnteredBy = (User as CustomPrincipal).UserId;
                    solditem.IsDeleted = false;
                    solditem.EnteredDate = DateTime.Now;
                    db.AddInvetorySoldItem(solditem);
                    }
                }
            }
            ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitList(orgid);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name");
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid,0,0,0), "Id", "Name");
            ViewBag.OrganizationId = orgid;

            return View();
        }

        public ActionResult SaveSoldBill(string SoldBillId="",string BillDate="", string BillDateBS="", string BillNo = "", string GroupNo = "", decimal TotalAmount=0, string IsStaff = "", string ClassId="", string  SectionId="", string StudentId="" , string StaffId="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            INV_SoldBill soldbill;// = new INV_SoldBill();
            INV_SoldItem solditem;//= new INV_SoldItem();
            //if (StudentId != null)
            //{
            //    var student = stdb.GetAcademicStudentById(Convert.ToInt32(StudentId));
            //}
            //else
            //{
            //    var staff = tdb.GetAcademicTeacherById(Convert.ToInt32(StaffId));
            //}
            {
                if (string.IsNullOrEmpty(SoldBillId) || SoldBillId == "0")
                {

                    soldbill = new INV_SoldBill();
                    soldbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// OrganizationId;// orgid;
                    soldbill.BillDateBS = BillDateBS;
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");
                    soldbill.GroupNo = GroupNo;
                    soldbill.TotalAmount = TotalAmount;
                    if (IsStaff == "No")
                    {
                        var student = stdb.GetAcademicStudentById(Convert.ToInt32(StudentId));
                        if (student.CurrentClassId != null)
                            soldbill.ClassId = student.CurrentClassId;
                        if (student.CurrentSectionId != null)
                            soldbill.SectionId = student.CurrentSectionId;
                        soldbill.StudentId = Convert.ToInt32(StudentId);
                        soldbill.IsStaff = false;
                    }
                    else if (IsStaff == "Yes")
                    {
                        soldbill.StaffId = Convert.ToInt32(StaffId);
                        soldbill.IsStaff = true;
                    }
                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = uid;// (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    int soldid = db.AddInvetorySold(soldbill);
                    return Json(new { result = soldid });
                }
                else
                {
                    soldbill = db.GetSoldBillById(orgid, Convert.ToInt32(SoldBillId));
                    soldbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = BillDateBS;
                    //soldbill.BillNo = BillNo;
                    soldbill.GroupNo = GroupNo;
                    soldbill.TotalAmount = TotalAmount;
                    if (string.IsNullOrEmpty(IsStaff))
                    {
                        var student = stdb.GetAcademicStudentById(Convert.ToInt32(StudentId));
                        if (student.CurrentClassId != null)
                            soldbill.ClassId = student.CurrentClassId;
                        if (student.CurrentSectionId != null)
                            soldbill.SectionId = student.CurrentSectionId;
                        soldbill.StudentId = Convert.ToInt32(StudentId);
                        soldbill.IsStaff = false;
                    }
                    else
                    {
                        soldbill.StaffId = Convert.ToInt32(StaffId);
                        soldbill.IsStaff = true;
                    }
                    soldbill.IsDeleted = false;
                    soldbill.LastUpdatedBy = uid;// (User as CustomPrincipal).UserId;
                    soldbill.LastUpdatedDate = DateTime.Now;
                    int soldid = db.AddInvetorySold(soldbill);
                    return Json(new { result = soldid });
                }
            }
        }
        public ActionResult SaveSolditem(string SoldItemId = "", string SoldBillId = "", string ItemId = "", string UnitId = "", string Quantity = "", string Rate = "", string Total = "")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            INV_SoldItem solditem;//
            if (string.IsNullOrEmpty(SoldItemId) || SoldItemId == "0")
            {
                solditem = new INV_SoldItem();
                solditem.SoldBillId = Convert.ToInt32(SoldBillId);
                solditem.OrganizationId = orgid;// orgid;
                solditem.ItemId = Convert.ToInt32(ItemId);
                solditem.UnitId = Convert.ToInt32(UnitId);
                solditem.Quantity = Convert.ToInt32(Quantity);
                solditem.Rate = Convert.ToDecimal(Rate);
                solditem.Total = Convert.ToDecimal(Total);
                solditem.EnteredBy = uid;
                solditem.IsDeleted = false;
                solditem.EnteredDate = DateTime.Now;
                int soldid = db.AddInvetorySoldItem(solditem);
                return Json(new { result = soldid });

            }
            else
            {
                solditem = db.GetSoldItemById(orgid,Convert.ToInt32(SoldItemId));
                solditem.SoldBillId = Convert.ToInt32(SoldBillId);
                solditem.OrganizationId = orgid;// orgid;
                solditem.ItemId = Convert.ToInt32(ItemId);
                solditem.UnitId = Convert.ToInt32(UnitId);
                solditem.Quantity = Convert.ToInt32(Quantity);
                solditem.Rate = Convert.ToDecimal(Rate);
                solditem.Total = Convert.ToDecimal(Total);
                solditem.LastUpdatedBy = uid;
                solditem.IsDeleted = false;
                solditem.LastUpdatedDate = DateTime.Now;
                db.EditInvetorySoldItem(solditem);
                return Json(new { result = SoldItemId });
            }
        }


        public ActionResult BulkCreate()
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            //int orgid= orgid;
            ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitByItemList(orgid);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name");
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid, 0, 0, 0), "Id", "Name");
            ViewBag.OrganizationId = orgid;

            //var StudentList = new SelectList(ddl.GetStudentList(orgid, 0, 0, 0), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BulkCreate(FormCollection frm, string[] hddrowindex)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            INV_SoldBill soldbill = new INV_SoldBill();
            INV_SoldItem solditem;//= new INV_SoldItem();
            int soldid = 0;
        //var studentArray = $("#StudentId").val();
            string StudentId = frm["StudentId"];
            //string first = frm["FirstId"];
            //string last = frm["LastId"];


            int i = 0;
            //var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));

            if ((frm["FirstId"] == "" && frm["LastId"] == ""))
            {
                if (frm["SectionId"] == "")
                {

                    soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = frm["BillDateBS"];
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");// frm["BillNo"];
                    soldbill.GroupNo = frm["GroupNo"];
                    soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    soldbill.ClassId = Convert.ToInt32(frm["ClassId"]);

                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    //soldid = db.AddInvetorySold(soldbill);

                    //soldid = db.AddInvetoryBulkSold(soldbill, orgid);
                    //int lastNo = ddl.GetLastNo();
                    //int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(frm["ClassId"]));
                    //int firstNo = lastNo - totstudent;
                    //db.AddUpdateInvetoryBulkSold(soldbill, firstNo, lastNo);

                    if (StudentId == null)
                    {
                        soldid = db.AddInvetoryBulkSold(soldbill, orgid);
                        int lastNo = ddl.GetLastNo();
                        int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(frm["ClassId"]));
                        int firstNo = lastNo - totstudent;
                        db.AddUpdateInvetoryBulkSold(soldbill, firstNo, lastNo);
                        //return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                    }
                    else
                    {
                        string[] stuId = StudentId.Split(',');
                        foreach (var item in stuId)
                        {
                            db.AddInvetoryBulkSold(soldbill, orgid, Convert.ToInt32(stuId[i]));
                            i++;
                        }
                        int count = i;
                        //int soldid = 0;
                        int lastNo = ddl.GetLastNo();
                        //int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(ClassId));
                        int firstNo = lastNo - count;
                        db.AddUpdateInvetoryBulkSold(soldbill, firstNo, lastNo);
                        //return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                    }
                }
                else
                {
                    soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = frm["BillDateBS"];
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");// frm["BillNo"];
                    soldbill.GroupNo = frm["GroupNo"];
                    soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    soldbill.ClassId = Convert.ToInt32(frm["ClassId"]);
                    soldbill.SectionId = Convert.ToInt32(frm["SectionId"]);
                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    //soldid = db.AddInvetorySold(soldbill);

                    //soldid = db.AddInvetoryBulkSold1(soldbill, orgid);
                    //int lastNo = ddl.GetLastNo();
                    //int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(frm["ClassId"]), Convert.ToInt32(frm["SectionId"]));
                    //int firstNo = lastNo - totstudent;
                    //db.AddUpdateInvetoryBulkSold1(soldbill, firstNo, lastNo);

                    if (StudentId == null)
                    {
                        soldid = db.AddInvetoryBulkSold1(soldbill, orgid);
                        int lastNo = ddl.GetLastNo();
                        int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(frm["ClassId"]), Convert.ToInt32(frm["SectionId"]));
                        int firstNo = lastNo - totstudent;
                        db.AddUpdateInvetoryBulkSold1(soldbill, firstNo, lastNo);
                        //return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                    }
                    else
                    {
                        string[] stuId = StudentId.Split(',');
                        foreach (var item in stuId)
                        {
                            db.AddInvetoryBulkSold2(soldbill, orgid, Convert.ToInt32(stuId[i]), Convert.ToInt32(frm["SectionId"]));
                            i++;
                        }
                        int count = i;
                        //int soldid = 0;
                        int lastNo = ddl.GetLastNo();
                        //int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(ClassId));
                        int firstNo = lastNo - count;
                        db.AddUpdateInvetoryBulkSold1(soldbill, firstNo, lastNo);
                        //return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                    }
                }
            }
            else
            {
                if (frm["SectionId"] == "")
                {
                    soldid = Convert.ToInt32(frm["SoldBillId"]);
                    int firstNo = Convert.ToInt32(frm["FirstId"]);
                    int lastNo = Convert.ToInt32(frm["LastId"]);
                    soldbill.SoldBillId = soldid;
                    soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = frm["BillDateBS"];
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");// frm["BillNo"];
                    soldbill.GroupNo = frm["GroupNo"];
                    soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    soldbill.ClassId = Convert.ToInt32(frm["ClassId"]);
                    //soldbill.SectionId = Convert.ToInt32(frm["SectionId"]);

                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    db.AddUpdateInvetoryBulkSold(soldbill, firstNo, lastNo);
                    //db.EditInvetorySold(soldbill);
                }
                else
                {
                    soldid = Convert.ToInt32(frm["SoldBillId"]);
                    int firstNo = Convert.ToInt32(frm["FirstId"]);
                    int lastNo = Convert.ToInt32(frm["LastId"]);
                    soldbill.SoldBillId = soldid;
                    soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = frm["BillDateBS"];
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");// frm["BillNo"];
                    soldbill.GroupNo = frm["GroupNo"];
                    soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                    soldbill.ClassId = Convert.ToInt32(frm["ClassId"]);
                    soldbill.SectionId = Convert.ToInt32(frm["SectionId"]);

                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    db.AddUpdateInvetoryBulkSold1(soldbill, firstNo, lastNo);
                }

            }
            if (hddrowindex != null)
            {
                //INV_SoldItem item;
                foreach (var indx in hddrowindex)
                {
                    if (frm["SoldItemId-" + indx] == "" || (frm["FirstItemId-" + indx] == "" && frm["LastItemId-" + indx] == ""))
                    {
                        if (StudentId == null)
                        {
                            solditem = new INV_SoldItem();
                            solditem.SoldBillId = soldid;
                            int lastId = ddl.GetLastNo();
                            int totstudent1 = ddl.GetStudentTotal(orgid, Convert.ToInt32(frm["ClassId"]));
                            int firstId = lastId - totstudent1;
                            solditem.OrganizationId = orgid;// orgid;
                            solditem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            solditem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            solditem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            solditem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            solditem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            solditem.EnteredBy = (User as CustomPrincipal).UserId;
                            solditem.IsDeleted = false;
                            solditem.EnteredDate = DateTime.Now;
                            //db.AddInvetorySoldItem(solditem);

                            db.AddInvetorySoldBulkItem(solditem, firstId, lastId);
                            int lastItemId = ddl.GetLastItemNo();
                            int totstudent = lastId - firstId;
                            int firstItemId = lastItemId - totstudent;
                            db.AddUpdateInvetorySoldBulkItem(solditem, firstItemId, lastItemId);
                        }
                        else
                        {
                            solditem = new INV_SoldItem();
                            solditem.SoldBillId = soldid;
                            int lastId = ddl.GetLastNo();
                            int totstudent1 = Convert.ToInt32(frm["TotStudent"]);
                            int firstId = lastId - totstudent1;
                            solditem.OrganizationId = orgid;// orgid;
                            solditem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            solditem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            solditem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            solditem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            solditem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            solditem.EnteredBy = (User as CustomPrincipal).UserId;
                            solditem.IsDeleted = false;
                            solditem.EnteredDate = DateTime.Now;
                            //db.AddInvetorySoldItem(solditem);

                            db.AddInvetorySoldBulkItem(solditem, firstId, lastId);
                            int lastItemId = ddl.GetLastItemNo();
                            int totstudent = lastId - firstId;
                            int firstItemId = lastItemId - totstudent;
                            db.AddUpdateInvetorySoldBulkItem(solditem, firstItemId, lastItemId);
                        }
                    }
                }
            }
            ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitList(orgid);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name");
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name");
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name");
            ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name");
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid, 0, 0, 0), "Id", "Name");
            ViewBag.OrganizationId = orgid;

            return View();
        }

        public ActionResult SaveSoldBulkBill(FormCollection frm,string SoldBillId = "", string BillDate = "", string BillDateBS = "", string BillNo = "", string GroupNo = "", decimal TotalAmount = 0, string ClassId = "", string SectionId = "", string FirstId ="", string LastId="", string StudentId="")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            INV_SoldBill soldbill;// = new INV_SoldBill();
            string studentId = StudentId;
            string[] stuId = studentId.Split(',');
            int i = 0;
            if (SectionId == "")
            {
                if ((string.IsNullOrEmpty(SoldBillId) || SoldBillId == "0") || ((string.IsNullOrEmpty(FirstId) || FirstId == "0") && (string.IsNullOrEmpty(LastId) || LastId == "0")))
                {

                    soldbill = new INV_SoldBill();
                    soldbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// OrganizationId;// orgid;
                    soldbill.BillDateBS = BillDateBS;
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");
                    soldbill.GroupNo = GroupNo;
                    soldbill.TotalAmount = TotalAmount;
                    soldbill.ClassId = Convert.ToInt32(ClassId);

                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = uid;// (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    if(StudentId == "")
                    {
                        int soldid = db.AddInvetoryBulkSold(soldbill, orgid);
                        int lastNo = ddl.GetLastNo();
                        int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(ClassId));
                        int firstNo = lastNo - totstudent;
                        db.AddUpdateInvetoryBulkSold(soldbill, firstNo, lastNo);
                        return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                    }
                    else
                    {
                        foreach (var item in stuId)
                        {
                            db.AddInvetoryBulkSold(soldbill, orgid, Convert.ToInt32(stuId[i]));
                            i++;
                        }
                        int count = i;
                        int soldid = 0;
                        int lastNo = ddl.GetLastNo();
                        //int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(ClassId));
                        int firstNo = lastNo - count;
                        db.AddUpdateInvetoryBulkSold(soldbill, firstNo, lastNo);
                        return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                    }
                    
                }
                else
                {
                    int firstId = Convert.ToInt32(FirstId);
                    int lastId = Convert.ToInt32(LastId);
                    soldbill = db.GetSoldBulkBillById(orgid, Convert.ToInt32(SoldBillId), firstId, lastId);
                    var allID = ddl.GetSoldBulkBillById(orgid, Convert.ToInt32(SoldBillId), firstId, lastId);
                    foreach (var item in allID)
                    {

                    }
                    soldbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = BillDateBS;
                    //soldbill.BillNo = BillNo;
                    soldbill.GroupNo = GroupNo;
                    soldbill.TotalAmount = TotalAmount;
                    soldbill.ClassId = Convert.ToInt32(ClassId);

                    soldbill.IsDeleted = false;
                    soldbill.LastUpdatedBy = uid;// (User as CustomPrincipal).UserId;
                    soldbill.LastUpdatedDate = DateTime.Now;
                    int soldid = db.AddInvetoryBulkSold(soldbill, orgid);
                    return Json(new { result = soldid });
                }
            }
            else
            {
                if ((string.IsNullOrEmpty(SoldBillId) || SoldBillId == "0") || ((string.IsNullOrEmpty(FirstId) || FirstId == "0") && (string.IsNullOrEmpty(LastId) || LastId == "0")))
                {

                    soldbill = new INV_SoldBill();
                    soldbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// OrganizationId;// orgid;
                    soldbill.BillDateBS = BillDateBS;
                    soldbill.BillNo = (db.GetBillNo(orgid)).ToString("D5");
                    soldbill.GroupNo = GroupNo;
                    soldbill.TotalAmount = TotalAmount;
                    soldbill.ClassId = Convert.ToInt32(ClassId);
                    soldbill.SectionId = Convert.ToInt32(SectionId);
                    soldbill.IsDeleted = false;
                    soldbill.EnteredBy = uid;// (User as CustomPrincipal).UserId;
                    soldbill.EnteredDate = DateTime.Now;
                    int soldid = db.AddInvetoryBulkSold1(soldbill, orgid);
                    int lastNo = ddl.GetLastNo();
                    int totstudent = ddl.GetStudentTotal(orgid, Convert.ToInt32(ClassId), Convert.ToInt32(SectionId));
                    int firstNo = lastNo - totstudent;
                    db.AddUpdateInvetoryBulkSold1(soldbill, firstNo, lastNo);
                    return Json(new { result = soldid, firstno = firstNo, lastno = lastNo });
                }
                else
                {
                    int firstId = Convert.ToInt32(FirstId);
                    int lastId = Convert.ToInt32(LastId);
                    soldbill = db.GetSoldBulkBillById(orgid, Convert.ToInt32(SoldBillId), firstId, lastId);
                    var allID = ddl.GetSoldBulkBillById(orgid, Convert.ToInt32(SoldBillId), firstId, lastId);
                    foreach (var item in allID)
                    {

                    }
                    soldbill.BillDate = DateTime.ParseExact(BillDate, "yyyy-MM-dd", null);
                    soldbill.OrganizationId = orgid;// orgid;
                    soldbill.BillDateBS = BillDateBS;
                    //soldbill.BillNo = BillNo;
                    soldbill.GroupNo = GroupNo;
                    soldbill.TotalAmount = TotalAmount;
                    soldbill.ClassId = Convert.ToInt32(ClassId);

                    soldbill.IsDeleted = false;
                    soldbill.LastUpdatedBy = uid;// (User as CustomPrincipal).UserId;
                    soldbill.LastUpdatedDate = DateTime.Now;
                    int soldid = db.AddInvetoryBulkSold(soldbill, orgid);
                    return Json(new { result = soldid });
                }
            }
        }

        public ActionResult SaveSoldBulkitem(string SoldItemId = "", string SoldBillId = "", string ItemId = "", string UnitId = "", string Quantity = "", string Rate = "", string Total = "", string FirstItemId = "", string LastItemId="", string FirstId = "", string LastId = "")
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            INV_SoldItem solditem;//
            if ((string.IsNullOrEmpty(SoldItemId) || SoldItemId == "0") || ((string.IsNullOrEmpty(FirstItemId) || FirstItemId == "0") && (string.IsNullOrEmpty(LastItemId) || LastItemId == "0")))
            {
                solditem = new INV_SoldItem();
                int firstId = Convert.ToInt32(FirstId);
                int lastId = Convert.ToInt32(LastId);
                //int firstItemId = Convert.ToInt32(FirstItemId);
                //int lastItemId = Convert.ToInt32(LastItemId);
                solditem.SoldBillId = Convert.ToInt32(SoldBillId);
                solditem.OrganizationId = orgid;// orgid;
                solditem.ItemId = Convert.ToInt32(ItemId);
                solditem.UnitId = Convert.ToInt32(UnitId);
                solditem.Quantity = Convert.ToInt32(Quantity);
                solditem.Rate = Convert.ToDecimal(Rate);
                solditem.Total = Convert.ToDecimal(Total);
                solditem.EnteredBy = uid;
                solditem.IsDeleted = false;
                solditem.EnteredDate = DateTime.Now;
                int soldid = db.AddInvetorySoldBulkItem(solditem,firstId,lastId);
                int lastItemId = ddl.GetLastItemNo();
                int totstudent = lastId-firstId;
                int firstItemId = lastItemId - totstudent;
                db.AddUpdateInvetorySoldBulkItem(solditem, firstItemId, lastItemId);
                return Json(new { result = soldid, firstitemId=firstItemId, lastitemId=lastItemId });

            }
            else
            {
                solditem = db.GetSoldItemById(orgid, Convert.ToInt32(SoldItemId));
                solditem.SoldBillId = Convert.ToInt32(SoldBillId);
                solditem.OrganizationId = orgid;// orgid;
                solditem.ItemId = Convert.ToInt32(ItemId);
                solditem.UnitId = Convert.ToInt32(UnitId);
                solditem.Quantity = Convert.ToInt32(Quantity);
                solditem.Rate = Convert.ToDecimal(Rate);
                solditem.Total = Convert.ToDecimal(Total);
                solditem.LastUpdatedBy = uid;
                solditem.IsDeleted = false;
                solditem.LastUpdatedDate = DateTime.Now;
                db.EditInvetorySoldItem(solditem);
                return Json(new { result = SoldItemId });
            }
        }


        public ActionResult Edit(int? id)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            var detail = db.GetSoldDetail(orgid, (int)id);
            if(detail.InventorySold==null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryList = ddl.GetCategoryList(orgid);
            ViewBag.ItemList = ddl.GetItemList(orgid, 0);
            ViewBag.UnitList = ddl.GetUnitByItemList(orgid);
            ViewBag.ItemUnitList = ddl.GetUnitBySold((int)id);
            ViewBag.BatchId = new SelectList(ddl.GetBatchList(orgid), "Id", "Name",detail.InventorySold.BatchId);
            ViewBag.ClassId = new SelectList(ddl.GetClassList(orgid), "Id", "Name",detail.InventorySold.ClassId);
            ViewBag.SectionId = new SelectList(ddl.GetSectionList(orgid), "Id", "Name", detail.InventorySold.SectionId);
            ViewBag.StaffId = new SelectList(ddl.GetStaffList(orgid), "Id", "Name", detail.InventorySold.StaffId);
            ViewBag.StudentId = new SelectList(ddl.GetStudentList(orgid,0,0,0), "Id", "Name", detail.InventorySold.StudentId);
            ViewBag.OrganizationId = orgid;
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection frm, string[] hddrowindex)
        {
            var ses = sesrepo.GetSessionById((User as CustomPrincipal).UserId);
            int orgid = ses.OrganizationId;
            int uid = (User as CustomPrincipal).UserId;
            int soldid = Convert.ToInt32(frm["SoldBillId"]);

            INV_SoldBill soldbill = db.GetSoldBillById(orgid,soldid);
            INV_SoldItem solditem;//= new INV_SoldItem();
            //var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
            try
            {
                soldbill.BillDate = DateTime.ParseExact(frm["BillDate"], "yyyy-MM-dd", null);
                soldbill.OrganizationId = orgid;// orgid;
                soldbill.BillDateBS = frm["BillDateBS"];
                //soldbill.BillNo = frm["BillNo"];
                soldbill.GroupNo = frm["GroupNo"];
                soldbill.TotalAmount = Convert.ToDecimal(frm["TotalAmount"]);
                if (frm["IsStaff"] == "No")
                {
                    var student = stdb.GetAcademicStudentById(Convert.ToInt32(frm["StudentId"]));
                    if (student.CurrentClassId != null)
                        soldbill.ClassId = student.CurrentClassId;
                    if (student.CurrentSectionId != null)
                        soldbill.SectionId = student.CurrentSectionId;
                    soldbill.StudentId = Convert.ToInt32(frm["StudentId"]);
                    soldbill.IsStaff = false;
                }
                else
                {
                    soldbill.StaffId = Convert.ToInt32(frm["StaffId"]);
                    soldbill.IsStaff = true;
                }
                soldbill.IsDeleted = false;
                soldbill.EnteredBy = (User as CustomPrincipal).UserId;
                soldbill.EnteredDate = DateTime.Now;
                db.EditInvetorySold(soldbill);
                db.DeleteAllInvetoryItem(soldid, uid, DateTime.Now);
                if (hddrowindex != null)
                {
                    //INV_SoldItem item;
                    foreach (var indx in hddrowindex)
                    {
                        if (frm["SoldItemId-" + indx] == "0")
                        {
                            solditem = new INV_SoldItem();
                            solditem.SoldBillId = soldid;
                            solditem.OrganizationId = orgid;
                            solditem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            solditem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            solditem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            solditem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            solditem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            solditem.EnteredBy = (User as CustomPrincipal).UserId;
                            solditem.IsDeleted = false;
                            solditem.EnteredDate = DateTime.Now;
                            db.AddInvetorySoldItem(solditem);
                        }
                        else
                        {
                            solditem = db.GetSoldItemById(orgid, Convert.ToInt32(frm["SoldItemId-" + indx]));
                            solditem.SoldBillId = soldid;
                            solditem.OrganizationId = orgid;
                            solditem.ItemId = Convert.ToInt32(frm["ItemId-" + indx]);
                            solditem.UnitId = Convert.ToInt32(frm["UnitId-" + indx]);
                            solditem.Quantity = Convert.ToInt32(frm["Quantity-" + indx]);
                            solditem.Rate = Convert.ToDecimal(frm["Rate-" + indx]);
                            solditem.Total = Convert.ToDecimal(frm["Total-" + indx]);
                            solditem.LastUpdatedBy = uid;
                            solditem.IsDeleted = false;
                            solditem.LastUpdatedDate = DateTime.Now;
                            db.EditInvetorySoldItem(solditem);
                        }
                    }
                }
            }
            catch
            {

                return RedirectToAction("Edit", new { id = soldid });
            }
            return RedirectToAction("Index");
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
            var result = db.DeleteInvetorySold((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            db.DeleteInvetorySoldItem((int)id, (User as CustomPrincipal).UserId, DateTime.Now);
            return RedirectToAction("Index");
        }
    }
}