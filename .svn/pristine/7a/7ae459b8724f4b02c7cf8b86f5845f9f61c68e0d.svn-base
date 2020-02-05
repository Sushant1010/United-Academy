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
    public class InventoryPurchaseRepo
    {
        public int AddInvetoryPurchase(INV_PurchaseBill purchasebill)
        {
            string sql = " insert into INV_PurchaseBill (" +
                        "VendorId,BillNo,BillDate,BillDateBS,TotalAmount,VatApplicable,VATPercent,VatAmount,IsPreviousStock," +
                        "ShippingCharge,DiscountAmount,TotalWithVat,OrganizationId,EnteredBy,EnteredDate,"+
                        "IsVerified,VerifiedBy,VerifiedDate,IsDeleted ,DeletedBy,DeletedDate,LastUpdatedBy,LastUpdatedDate,"  +       
                        "BillSerialNo, Remarks)"+
                         " values " +
                         " (" +
                        "@VendorId,@BillNo,@BillDate,@BillDateBS,@TotalAmount,@VatApplicable,@VATPercent,@VatAmount,@IsPreviousStock," +
                        "@ShippingCharge,@DiscountAmount,@TotalWithVat,@OrganizationId,@EnteredBy,@EnteredDate," +
                        " 1,@EnteredBy,@EnteredDate,@IsDeleted ,0,null,0,null,@BillSerialNo, @Remarks" +
                        ") SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                int a=db.Query<int>(sql, purchasebill).SingleOrDefault();
                //  trsn.Commit();
                //}
                return a;
            }

        }
        public int AddInvetoryPurchaseItem(INV_PurchaseItem purchaseitem)
        {
            string sql = " insert into INV_PurchaseItem(" +
                         "PurchaseId,ItemId,UnitId,Quantity ,Rate,Total,ReturnedUnitId ,ReturnedQuantity," +
                         " ReturnedVerified, ReturnedDate,ReturnedBy,ReturnRemarks, IsVerified,VerifiedBy," +
                         "  VerifiedDate , IsDeleted,DeletedBy, DeletedDate, EnteredDate,EnteredBy," +
                         "  LastUpdatedBy, LastUpdatedDate,OrganizationId" +
                         ")" +
                         " values " +
                         " (@PurchaseId,@ItemId,@UnitId,@Quantity ,@Rate,@Total,@ReturnedUnitId,@ReturnedQuantity," +
                         " @ReturnedVerified, @ReturnedDate,@ReturnedBy,@ReturnRemarks, @IsVerified,@VerifiedBy," +
                         " @VerifiedDate ,@IsDeleted,@DeletedBy, @DeletedDate, @EnteredDate,@EnteredBy," +
                         "  @LastUpdatedBy, @LastUpdatedDate,@OrganizationId)  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                
                 int a=   db.Query<int>(sql, purchaseitem).SingleOrDefault();
                return a;

            }

        }
        public int AddStockItem(int itemId, int quantity, int orgid)
        {
            string sql = " insert into INV_StockItem(" +
                         "ItemId,Quantity,OrganizationId" +
                         ")" +
                         " values (" + itemId + "," + quantity + "," + orgid + ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {

                int a = db.Query<int>(sql).SingleOrDefault();
                return a;

            }

        }
        public void EditInvetoryPurchase(INV_PurchaseBill purchasebill)
        {
            string sql = " update INV_PurchaseBill set " +
                        " VendorId=@VendorId,BillNo=@BillNo,BillDate=@BillDate,BillDateBS=@BillDateBS,TotalAmount=@TotalAmount,IsPreviousStock=@IsPreviousStock," +
                        " VatApplicable=@VatApplicable,VatAmount=@VatAmount,VATPercent=@VATPercent," +
                        " ShippingCharge=@ShippingCharge,DiscountAmount=@DiscountAmount,TotalWithVat=@TotalWithVat,"+
                        " OrganizationId=@OrganizationId," +
                        " LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
                        
                        " where PurchaseId=@PurchaseId";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql, purchasebill);
                //  trsn.Commit();
                //}

            }

        }

        public void EditInvetoryPurchaseBill(INV_PurchaseBill purchasebill)
        {
            string sql = " update INV_PurchaseBill set " +
                        " VendorId=@VendorId,BillNo=@BillNo,BillDate=@BillDate,BillDateBS=@BillDateBS,TotalAmount=@TotalAmount,IsPreviousStock=@IsPreviousStock," +
                        " VatApplicable=@VatApplicable,VatAmount=@VatAmount,VATPercent=@VATPercent," +
                        " ShippingCharge=@ShippingCharge,DiscountAmount=@DiscountAmount,TotalWithVat=@TotalWithVat," +
                        " OrganizationId=@OrganizationId," +
                        " LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
                        "BillSerialNo = @BillSerialNo, Remarks = @Remarks"+

                        " where PurchaseId=@PurchaseId";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql, purchasebill);
                //  trsn.Commit();
                //}

            }

        }
        public void EditInvetoryPurchaseItem(INV_PurchaseItem purchaseitem)
        {
            string sql = " update INV_PurchaseItem  set " +
                         " PurchaseId=@PurchaseId,ItemId=@ItemId,UnitId=@UnitId,Quantity=@Quantity ,Rate=@Rate,Total=@Total,"+
                         //" ReturnedUnitId=@ReturnedUnitId ,ReturnedQuantity=@ReturnedQuantity," +
                         //" ReturnedVerified=@ReturnedVerified, ReturnedDate=@ReturnedDate,ReturnedBy=@ReturnedBy,ReturnRemarks, IsVerified,VerifiedBy," +
                         // "  VerifiedDate ,"+
                         "IsVerified = @Isverified, VerifiedBy = @VerifiedBy, VerifiedDate=@VerifiedDate,"+
                         " IsDeleted=@IsDeleted,"+//,DeletedBy=@DeletedBy, DeletedDate, EnteredDate,EnteredBy," +
                         "  LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate,OrganizationId=@OrganizationId" +
                         " where PurchaseitemId=@PurchaseitemId";
                         

            using (var db = DbHelper.GetDBConnection())
            {

                db.Query<int>(sql, purchaseitem).SingleOrDefault();


            }

        }
        public List<InventoryPurchaseVM> GetPurchaseList(int organizationId, string fromdate, string todate, string billno,int vendorId)
        {
            string sql = "select pb.PurchaseId ,pb.VendorId,v.VendorName, pb.BillNo, pb.BillDate, pb.BillDateBS, pb.TotalAmount,pb.IsPreviousStock," +
                " pb.VatApplicable, pb.VatAmount,pb.VATPercent, pb.ShippingCharge, pb.DiscountAmount, pb.TotalWithVat, " +
                " pb.OrganizationId, pb.IsVerified, pb.VerifiedBy,  u.FullName VerifiedByName" +
                " from INV_PurchaseBill pb " +
                " left join INV_Vendor v on v.VendorId = pb.VendorId" +
                " left join SC_User u on u.UserId = pb.VerifiedBy " +
                " where 1 = 1 and pb.IsDeleted = 0 and pb.OrganizationId=" + organizationId;
                 
            if (!string.IsNullOrEmpty(fromdate))
            {
                sql += " and pb.EnteredDate >='" + fromdate + "'";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                sql += " and pb.EnteredDate <='" + todate + "'";
            }
            if (!string.IsNullOrEmpty(billno))
            {
                sql += " and pb.BillNo like '%" + billno + "%'";
            }
            if (vendorId>0)
            {
                sql += " and pb.VendorId ="+vendorId;
            }
            sql += " Order By pb.EnteredDate asc";
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventoryPurchaseVM>(sql).ToList();
            }
        }
        public InventoryPurchaseVM GetPurchase(int orgid,int id)
        {
            string sql = "select pb.PurchaseId ,pb.VendorId,v.VendorName, pb.BillNo, pb.BillDate, pb.BillDateBS, pb.TotalAmount,pb.IsPreviousStock," +
                " pb.VatApplicable, pb.VatAmount,pb.VATPercent , pb.ShippingCharge, pb.DiscountAmount, pb.TotalWithVat, " +
                " pb.OrganizationId, pb.IsVerified, pb.VerifiedBy,  u.FullName VerifiedByName" +
                " from INV_PurchaseBill pb " +
                " left join INV_Vendor v on v.VendorId = pb.VendorId" +
                " left join SC_User u on u.UserId = pb.VerifiedBy " +
                " where 1 = 1 and pb.IsDeleted = 0 and pb.PurchaseId=" + id+ " and  pb.OrganizationId=" +orgid;

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventoryPurchaseVM>(sql).SingleOrDefault();
            }
        }
        public List<InventoryPurchaseItem> GetPurchaseItemList(int orgid,int purchaseid)
        {
            string sql = "select pd.PurchaseItemId , pd.PurchaseId, pd.ItemId,i.ItemName,i.CategoryId,c.CategoryName, pd.UnitId,u.UnitName, pd.Quantity, pd.Rate, pd.Total," +
                " pd.ReturnedUnitId,ur.UnitName, pd.ReturnedQuantity," +
                "  pd.ReturnedVerified , pd.ReturnedDate, pd.ReturnedBy,ru.FullName ReturnedByName, pd.ReturnRemarks, " +
                "  pd.IsVerified, pd.VerifiedBy,vu.FullName VerifiedByName, pd.VerifiedDate " +
                "  from INV_PurchaseItem pd " +
                "  left join SC_User ru on ru.UserId = pd.ReturnedBy " +
                "  left join SC_User vu on vu.UserId = pd.VerifiedBy " +
                "  left join MS_Unit u on u.UnitId = pd.UnitId " +
                "  left join MS_Item i on i.ItemId = pd.ItemId " +
                "  left join MS_Category c on c.CategoryId = i.CategoryId " +
                "  left join MS_Unit ur on ur.UnitId = pd.ReturnedUnitId " +
                " where 1=1 and pd.IsDeleted=0 and pd.PurchaseId=" + purchaseid+ " and  pd.OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<InventoryPurchaseItem>(sql).ToList();
            }
        }

        public InventoryPurchaseDetail GetPurchaseDetail(int orgid,int id)
        {
            InventoryPurchaseDetail detail = new InventoryPurchaseDetail();
            detail.InventoryPurchase = GetPurchase(orgid,id);
            detail.InventoryPurchaseItems = GetPurchaseItemList(orgid,id);
            return detail;
        }
        public bool DeleteInvetoryPurchase(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_PurchaseBill  set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "',BillSerialNo=0" +
                " where PurchaseId=" + id;


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
            

            //    string sql1 = " Update INV_PurchaseItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            //    " where PurchaseId=" + id;

            //using (var db = DbHelper.GetDBConnection())
            //{
            //    using (var trsn = new TransactionScope())
            //    {
            //        //db.Query(sql);
            //        //  trsn.Commit();
            //        //}
            //        var lst = db.Execute(sql);
            //        trsn.Complete();
            //        db.Close();
            //        if (lst > 0)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }

            //    }
            //}

        }
        public bool DeleteInvetoryItem(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update INV_PurchaseItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
                " where PurchaseId=" + id;

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
            string sql = " Update INV_PurchaseItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
                " where PurchaseId=" + id;

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
        public INV_PurchaseBill GetPurchaseBillById(int orgid, int PBillid)
        {
            string sql = "select * from INV_PurchaseBill " +
                " where 1=1 and IsDeleted=0 and PurchaseId=" + PBillid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_PurchaseBill>(sql).SingleOrDefault();
            }
        }
        public INV_PurchaseItem GetPurchaseItemById(int orgid, int pitemid)
        {
            string sql = "select * from INV_PurchaseItem " +
                " where 1=1 and PurchaseItemId=" + pitemid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<INV_PurchaseItem>(sql).SingleOrDefault();
            }
        }

        //public int GetBillNo(int orgid)
        //{
        //    string sql = "select count(*) from INV_PurchaseBill " +
        //       " where 1=1  and OrganizationId=" + orgid;
        //    using (var db = DbHelper.GetDBConnection())
        //    {
        //        int a = db.Query<int>(sql).SingleOrDefault();
        //        return a + 1;
        //    }
        //}

        public INV_PurchaseBill GetBillSerialNo(int orgid)
        {
            string sql = " select top 1 BillSerialNo from INV_PurchaseBill " +
                "where OrganizationId=" +orgid+
                " order by BillSerialNo desc";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_PurchaseBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public INV_PurchaseBill GetBillSerialNo(int orgid,int id)
        {
            string sql = " select BillSerialNo from INV_PurchaseBill" +
                " where OrganizationId="+orgid+" and PurchaseId="+id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<INV_PurchaseBill>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }
    }
}