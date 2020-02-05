using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.Models;
using Dapper;
using United_IMS.Web.ViewModel;
using System.Transactions;

namespace United_IMS.Web.Repository
{
    public class StockItemRepo
    {
        public List<InventorySoldVM> GetStockItemList(int organizationId, string FromDate = "", string ToDate = "", string IsStaff = "Both", int ClassId = 0, int StudentId = 0, int StaffId = 0, string BillNo = "", string GroupNo = "")
        {
            string sql = "select mi.ItemName ItemName, mi.ItemCode ItemCode, p.ItemId ,SUM(COALESCE(p.Quantity,0)-COALESCE(s.Quantity,0)) Quantity " +
                " from INV_PurchaseItem p" +
                " left join INV_SoldItem s on s.itemid = p.itemid" +
                " left join MS_Item mi on mi.ItemId = p.itemId" +
                " where p.IsDeleted = 0 and p.OrganizationId = "+ organizationId +
                " Group By p.ItemId,mi.ItemName, mi.ItemCode";
            //string sql = "select sb.SoldBillId, sb.OrganizationId, org.OrganizationName, sb.BillNo, sb.BillDate, sb.BillDateBS, sb.GroupNo," +
            //    "  sb.TotalAmount, sb.IsStaff, sb.StudentId, sb.ClassId, cl.ClassName, sb.SectionId, sc.SectionName," +
            //    "  sb.StaffId, st.StudentName, st.StudentRegNo, sf.StaffName, sf.StaffCode,st.BatchId,b.BatchName" +
            //    " from INV_SoldBill sb" +
            //    "  left join ACD_Student st on st.StudentId = sb.StudentId" +
            //    "  left join ACD_Batch b on b.BatchId = st.BatchId" +
            //    " left join ACD_Staff sf on sf.StaffId = sb.StaffId" +
            //    " left join ACD_Class cl on cl.ClassId = sb.ClassId" +
            //    " left join ACD_Section sc on sc.SectionId = sb.SectionId" +
            //    " left join MS_Organization org on org.OrganizationId = sb.OrganizationId" +
            //    " where 1 = 1 and sb.IsDeleted = 0 and sb.OrganizationId=" + organizationId;
            //if (!string.IsNullOrEmpty(FromDate))
            //{
            //    sql += " and sb.EnteredDate >='" + FromDate + "'";
            //}
            //if (!string.IsNullOrEmpty(ToDate))
            //{
            //    sql += " and sb.EnteredDate <='" + ToDate + "'";
            //}
            //if (!string.IsNullOrEmpty(BillNo))
            //{
            //    sql += " and sb.BillNo like '%" + BillNo + "%'";
            //}
            //if (!string.IsNullOrEmpty(GroupNo))
            //{
            //    sql += " and sb.GroupNo like '%" + GroupNo + "%'";
            //}
            //if (IsStaff != "both")
            //{
            //    if (IsStaff == "No")
            //        if (StudentId > 0)
            //        {
            //            sql += " and sb.StudentId=" + StudentId;
            //        }
            //    if (IsStaff == "Yes")
            //        if (StaffId > 0)
            //        {
            //            sql += " and sb.StaffId=" + StaffId;
            //        }
            //}
            //else
            //{
            //    if (StudentId > 0 && StaffId > 0)
            //    {
            //        sql += "and ( sb.StudentId=" + StudentId + " or sb.StaffId=" + StaffId + ")";
            //    }
            //    else
            //    {
            //        if (StudentId > 0)
            //        {
            //            sql += " and sb.StudentId=" + StudentId;
            //        }
            //        if (StaffId > 0)
            //        {
            //            sql += " and sb.StaffId=" + StaffId;
            //        }
            //    }
            //}
            //sql += " Order By p.EnteredDate desc";

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventorySoldVM>(sql).ToList();
            }
        }

    }
}