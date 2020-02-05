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
    public class InventorySoldRepo
    {
        public int AddInvetorySold(INV_SoldBill soldbill)
        {
            string sql = " insert into INV_SoldBill (OrganizationId, ClassId, SectionId, BillDate,BillDateBS,BillNo,GroupNo,TotalAmount,IsStaff,StudentId,StaffId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
                         " values " +
                         " (@OrganizationId, @ClassId, @SectionId, @BillDate,@BillDateBS,@BillNo,@GroupNo, @TotalAmount,@IsStaff,@StudentId,@StaffId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                return db.Query<int>(sql, soldbill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }
        public void EditInvetorySold(INV_SoldBill soldbill)
        {
            string sql = " Update INV_SoldBill set ClassId=@ClassId, SectionId=@SectionId, BillDate=@BillDate,BillDateBS=@BillDateBS,"+
                " BillNo=@BillNo,GroupNo=@GroupNo, TotalAmount=@TotalAmount,IsStaff=@IsStaff,StudentId=@StudentId,StaffId=@StaffId,LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate,IsDeleted=@IsDeleted," +
                " DeletedBy=0,DeletedDate=null" +
                " where SoldBillId=@SoldBillId";
                         
            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query<int>(sql, soldbill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }
        public bool DeleteInvetorySold(int id,int deletedby,DateTime deleteddate)
        {
            string sql = " Update INV_SoldBill set IsDeleted=1,DeletedBy=" +deletedby+",DeletedDate= '"+deleteddate+"'"+
                " where SoldBillId="+id;


            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Query(sql);
                    //  trsn.Commit();
                    //}
                    var lst = db.Execute(sql);
                    trsn.Complete();
                    db.Close();
                    if (lst > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }

        }

        public bool DeleteInvetorySoldItem(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_SoldItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
                " where SoldBillId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Query(sql);
                    //  trsn.Commit();
                    //}
                    var lst = db.Execute(sql);
                    trsn.Complete();
                    db.Close();
                    if (lst > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }
            public void EditInvetorySoldItem(INV_SoldItem solditem)
        {
            string sql = " Update INV_SoldItem set ItemId=@ItemId, UnitId=@UnitId, Quantity=@Quantity,Rate=@Rate," +
                " Total=@Total,LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate,IsDeleted=@IsDeleted, " +
                " DeletedBy=0,DeletedDate=null"+
                " where SoldItemId=@SoldItemId";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query<int>(sql, solditem).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }
        public void DeleteInvetoryItem(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_SoldItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate +"'"+
                " where SoldItemId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

            //string sql1 = " Update INV_SoldItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= " + deleteddate +
            //    " where SoldBillId=" + id;

            //using (var db = DbHelper.GetDBConnection())
            //{
            //    //using (var trsn = db.BeginTransaction())
            //    //{
            //    db.Query(sql);
            //    //  trsn.Commit();
            //    //}

            //}
        }
        public void DeleteAllInvetoryItem(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_SoldItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate +"'"+
                " where SoldBillId=" + id;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

            //string sql1 = " Update INV_SoldItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= " + deleteddate +
            //    " where SoldBillId=" + id;

            //using (var db = DbHelper.GetDBConnection())
            //{
            //    //using (var trsn = db.BeginTransaction())
            //    //{
            //    db.Query(sql);
            //    //  trsn.Commit();
            //    //}

            //}
        }

        public int AddInvetorySoldItem(INV_SoldItem solditem)
        {
            string sql = " insert into INV_SoldItem (SoldBillId, ItemId,UnitId, Quantity, Rate, Total,OrganizationId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
                         " values " +
                         " (@SoldBillId, @ItemId,@UnitId, @Quantity, @Rate, @Total, @OrganizationId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                    int id= db.Query<int>(sql, solditem).SingleOrDefault();
                db.Close();
                return id;
                //trsn.Commit();
                //}

            }

        }
        public List<InventorySoldVM> GetSoldList(int organizationId, string FromDate = "", string ToDate = "", string IsStaff = "Both", int ClassId = 0, int StudentId = 0, int StaffId = 0, string BillNo = "", string GroupNo = "")
        {
            string sql = "select sb.SoldBillId, sb.OrganizationId, org.OrganizationName, sb.BillNo, sb.BillDate, sb.BillDateBS, sb.GroupNo,"+
                "  sb.TotalAmount, sb.IsStaff, sb.StudentId, sb.ClassId, cl.ClassName, sb.SectionId, sc.SectionName,"+
                "  sb.StaffId, st.StudentName, st.StudentRegNo, sf.StaffName, sf.StaffCode,st.BatchId,b.BatchName"+
                " from INV_SoldBill sb"+
                "  left join ACD_Student st on st.StudentId = sb.StudentId" +
                "  left join ACD_Batch b on b.BatchId = st.BatchId" +
                " left join ACD_Staff sf on sf.StaffId = sb.StaffId" +
                " left join ACD_Class cl on cl.ClassId = sb.ClassId"+
                " left join ACD_Section sc on sc.SectionId = sb.SectionId"+
                " left join MS_Organization org on org.OrganizationId = sb.OrganizationId" +
                " where 1 = 1 and sb.IsDeleted = 0 and sb.OrganizationId=" + organizationId;
            if (!string.IsNullOrEmpty(FromDate))
            {
                sql += " and sb.EnteredDate >='" + FromDate + "'";
            }
            if (!string.IsNullOrEmpty(ToDate))
            {
                sql += " and sb.EnteredDate <='" + ToDate + "'";
            }
            if (!string.IsNullOrEmpty(BillNo))
            {
                sql += " and sb.BillNo like '%" + BillNo + "%'";
            }
            if (!string.IsNullOrEmpty(GroupNo))
            {
                sql += " and sb.GroupNo like '%" + GroupNo + "%'";
            }
            if (IsStaff != "both")
            {
                if(IsStaff=="No")
                if (StudentId > 0)
                {
                    sql += " and sb.StudentId=" + StudentId;
                }
                if(IsStaff=="Yes")
                if (StaffId > 0)
                {
                    sql += " and sb.StaffId=" + StaffId;
                }
            }
            else
            {
                if (StudentId > 0 && StaffId > 0)
                {
                    sql += "and ( sb.StudentId=" + StudentId + " or sb.StaffId=" + StaffId + ")";
                }
                else
                {
                    if (StudentId > 0)
                    {
                        sql += " and sb.StudentId=" + StudentId;
                    }
                    if (StaffId > 0)
                    {
                        sql += " and sb.StaffId=" + StaffId;
                    }
                }
            }
            sql += " Order By sb.EnteredDate desc";

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventorySoldVM>(sql).ToList();
            }
        }
        public InventorySoldVM  GetSoldBill(int orgid,int id)
        {
            string sql = "select sb.SoldBillId, sb.OrganizationId, org.OrganizationName, sb.BillNo, sb.BillDate, sb.BillDateBS, sb.GroupNo," +
                "  sb.TotalAmount, sb.IsStaff, sb.StudentId, sb.ClassId, cl.ClassName, sb.SectionId, sc.SectionName," +
                "  sb.StaffId, st.StudentName, st.StudentRegNo, sf.StaffName, sf.StaffCode,st.BatchId,b.BatchName" +
                " from INV_SoldBill sb" +
                "  left join ACD_Student st on st.StudentId = sb.StudentId" +
                "  left join ACD_Batch b on b.BatchId = st.BatchId" +
                " left join ACD_Staff sf on sf.StaffId = sb.StaffId" +
                " left join ACD_Class cl on cl.ClassId = sb.ClassId" +
                " left join ACD_Section sc on sc.SectionId = sb.SectionId" +
                " left join MS_Organization org on org.OrganizationId = sb.OrganizationId" +
                " where 1 = 1 and sb.IsDeleted = 0 and sb.OrganizationId=" + orgid+" and sb.SoldBillId="+id;

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventorySoldVM>(sql).SingleOrDefault();
            }
        }
        public List<InventorySoldItem> GetSoldItemList(int orgid,int soldBillid)
        {
            string sql = "select si.SoldItemId, si.SoldBillId, si.ItemId, i.ItemName, si.UnitId, u.UnitName, "+
                " i.CategoryId, c.CategoryName, si.Quantity, si.Rate, si.Total" +
                " from INV_SoldItem si" +
                " left join MS_Item i on i.ItemId = si.ItemId" +
                " left join MS_Unit u on u.UnitId = si.UnitId" +
                " left join MS_Category c on c.CategoryId = i.CategoryId" +
                " where 1=1 and si.IsDeleted=0 and si.SoldBillId=" + soldBillid+" and si.OrganizationId="+orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventorySoldItem>(sql).ToList();
            }
        }
        public InventorySoldDetail GetSoldDetail(int orgid,int id)
        {
            InventorySoldDetail detail = new InventorySoldDetail();
            detail.InventorySold = GetSoldBill(orgid,id);
            detail.InventorySoldItems = GetSoldItemList(orgid,id);
            return detail;
        }
        public INV_SoldBill GetSoldBillById(int orgid, int soldBillid)
        {
            string sql = "select * from INV_SoldBill "+
                " where 1=1 and IsDeleted=0 and SoldBillId=" + soldBillid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_SoldBill>(sql).SingleOrDefault();
            }
        }
        public INV_SoldItem GetSoldItemById(int orgid, int solditemid)
        {
            string sql = "select * from INV_SoldItem " +
                " where 1=1 and SoldItemId=" + solditemid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_SoldItem>(sql).SingleOrDefault();
            }
        }

        public int AddInvetoryBulkSold(INV_SoldBill soldbill, int orgid) //if group of student not selected
        {
            string sql = " insert into INV_SoldBill(StudentId, ClassId, SectionId)" +
                " select StudentId, CurrentClassID, CurrentSectionId from acd_student" +
                " where organizationid="+orgid+ " and CurrentClassId=@ClassId";
            //string sql = " insert into INV_SoldBill (OrganizationId, ClassId, SectionId, BillDate,BillDateBS,BillNo,GroupNo,TotalAmount,IsStaff,StudentId,StaffId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
            //             " values " +
            //             " (@OrganizationId, @ClassId, @SectionId, @BillDate,@BillDateBS,@BillNo,@GroupNo, @TotalAmount,@IsStaff,@StudentId,@StaffId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                return db.Query<int>(sql, soldbill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }

        public int AddInvetoryBulkSold(INV_SoldBill soldbill, int orgid, int studentId)
        {
            string sql = " insert into INV_SoldBill(StudentId, ClassId, SectionId)" +
                " select StudentId, CurrentClassID, CurrentSectionId from acd_student" +
                " where organizationid=" + orgid + " and CurrentClassId=@ClassId and StudentId=" + studentId;
            //string sql = " insert into INV_SoldBill (OrganizationId, ClassId, SectionId, BillDate,BillDateBS,BillNo,GroupNo,TotalAmount,IsStaff,StudentId,StaffId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
            //             " values " +
            //             " (@OrganizationId, @ClassId, @SectionId, @BillDate,@BillDateBS,@BillNo,@GroupNo, @TotalAmount,@IsStaff,@StudentId,@StaffId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                return db.Query<int>(sql, soldbill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }

        public int AddInvetoryBulkSold1(INV_SoldBill soldbill, int orgid)
        {
            string sql = " insert into INV_SoldBill(StudentId, ClassId, SectionId)" +
                " select StudentId, CurrentClassID, CurrentSectionId from acd_student" +
                " where organizationid=" + orgid + " and CurrentClassId=@ClassId and CurrentSectionId=@SectionId";
            //string sql = " insert into INV_SoldBill (OrganizationId, ClassId, SectionId, BillDate,BillDateBS,BillNo,GroupNo,TotalAmount,IsStaff,StudentId,StaffId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
            //             " values " +
            //             " (@OrganizationId, @ClassId, @SectionId, @BillDate,@BillDateBS,@BillNo,@GroupNo, @TotalAmount,@IsStaff,@StudentId,@StaffId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                return db.Query<int>(sql, soldbill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }

        public int AddInvetoryBulkSold2(INV_SoldBill soldbill, int orgid, int studentId, int sectionId)
        {
            string sql = " insert into INV_SoldBill(StudentId, ClassId, SectionId)" +
                " select StudentId, CurrentClassID, CurrentSectionId from acd_student" +
                " where organizationid=" + orgid + " and CurrentClassId=@ClassId and StudentId=" + studentId+ " and CurrentSectionId=" + sectionId;
            //string sql = " insert into INV_SoldBill (OrganizationId, ClassId, SectionId, BillDate,BillDateBS,BillNo,GroupNo,TotalAmount,IsStaff,StudentId,StaffId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
            //             " values " +
            //             " (@OrganizationId, @ClassId, @SectionId, @BillDate,@BillDateBS,@BillNo,@GroupNo, @TotalAmount,@IsStaff,@StudentId,@StaffId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                return db.Query<int>(sql, soldbill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }

        public int GetBillNo(int orgid)
        {
            string sql = "select count(*) from INV_SoldBill " +
               " where 1=1  and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                int a= db.Query<int>(sql).SingleOrDefault();
                return a + 1;
            }
        }

        public void AddUpdateInvetoryBulkSold(INV_SoldBill soldBill, int firstNo, int lastNo)
        {
            string sql = " Update INV_SoldBill set OrganizationId=@OrganizationId, BillDate=@BillDate, BillDateBS=@BillDateBS,BillNo=@BillNo," +
                " GroupNo=@GroupNo,EnteredBy=@EnteredBy,EnteredDate=@EnteredDate,IsDeleted=0, " +
                " DeletedBy=0,DeletedDate=null,TotalAmount=@TotalAmount" +
                " where SoldBillId>"+firstNo+ "and SoldBillId<="+lastNo;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query<int>(sql, soldBill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }

        public void AddUpdateInvetoryBulkSold1(INV_SoldBill soldBill, int firstNo, int lastNo)
        {
            string sql = " Update INV_SoldBill set OrganizationId=@OrganizationId, BillDate=@BillDate, BillDateBS=@BillDateBS,BillNo=@BillNo," +
                " GroupNo=@GroupNo,EnteredBy=@EnteredBy,EnteredDate=@EnteredDate,IsDeleted=0, " +
                " DeletedBy=0,DeletedDate=null,SectionId=@SectionID,TotalAmount=@TotalAmount" +
                " where SoldBillId>" + firstNo + "and SoldBillId<=" + lastNo;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query<int>(sql, soldBill).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }

        public INV_SoldBill GetSoldBulkBillById(int orgid, int soldBillid, int firstid, int lastid)
        {
            string sql = "select * from INV_SoldBill " +
                " where 1=1 and IsDeleted=0 and SoldBillId=" + soldBillid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_SoldBill>(sql).SingleOrDefault();
            }
        }

        public int AddInvetorySoldBulkItem(INV_SoldItem solditem,int firstid, int lastid)
        {
            string sql = "insert into INV_SoldItem(SoldBillId)" +
                " select SoldBillId from INV_SoldBill where soldbillid>"+firstid+
                " and soldbillid<="+lastid+ " and OrganizationId=@OrganizationId";
            //string sql = " insert into INV_SoldItem (SoldBillId, ItemId,UnitId, Quantity, Rate, Total,OrganizationId,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate)" +
            //             " values " +
            //             " (@SoldBillId, @ItemId,@UnitId, @Quantity, @Rate, @Total, @OrganizationId,@EnteredBy,@EnteredDate,0,null,0,0,null) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                int id = db.Query<int>(sql, solditem).SingleOrDefault();
                db.Close();
                return id;
                //trsn.Commit();
                //}

            }

        }

        public void AddUpdateInvetorySoldBulkItem(INV_SoldItem solditem, int firstItemId, int lastItemId)
        {
            string sql = " Update INV_SoldItem set OrganizationId=@OrganizationId, ItemId=@ItemId, UnitId=@UnitId,Quantity=@Quantity," +
                " Rate=@Rate,Total=@Total,EnteredBy=@EnteredBy,EnteredDate=@EnteredDate,IsDeleted=@IsDeleted, " +
                " DeletedBy=0,DeletedDate=null,LastUpdatedBy=0" +
                " where SoldItemId>" + firstItemId + "and SoldItemId<=" + lastItemId;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query<int>(sql, solditem).SingleOrDefault();
                //  trsn.Commit();
                //}

            }

        }
    }
}