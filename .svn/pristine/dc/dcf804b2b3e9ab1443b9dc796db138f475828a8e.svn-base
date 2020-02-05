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
    public class AssetPurchaseRepo
    {
        public int AddAssetPurchase(FA_Purchase fapurchase)
        {
            string sql = " insert into FA_Purchase (" +
                        " OrganizationId,VendorId, BillNo, BillDate, BillDateBS, TotalAmount,VatApplicable,VATPercent,VatAmount, IsVerified," +
                        " DiscountAmount,TotalWithVat,VerifiedBy, VerifiedDate, EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy,"+
                        " DeletedDate, BillSerialNo"  +       
                        ")"+
                         " values " +
                         " (" +
                        "@OrganizationId,@VendorId, @BillNo, @BillDate, @BillDateBS, @TotalAmount, @VatApplicable,@VatPercent,@VatAmount, @IsVerified," +
                        " @DiscountAmount,@TotalWithVat,@VerifiedBy, @VerifiedDate, @EnteredBy, @EnteredDate, 0, null, 0, 0," +
                        " null, @BillSerialNo" +
                        ") SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                int a=db.Query<int>(sql, fapurchase).SingleOrDefault();
                //  trsn.Commit();
                //}
                return a;
            }

        }
        public int AddAssetPurchaseItem(FA_PurchaseItem purchaseitem)
        {
            string sql = " insert into FA_PurchaseItem (" +
                         "OrganizationId, PurchaseId, AssetItemId, PurchaseQuantity, Rate, Total,"+
                         " RegisterToAsset, ReturnedQuantity, ReturnedBy, ReturnedDate, IsVerified, VerifiedBy,"+
                         " VerifiedDate, EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted,"+
                         "  DeletedDate, DeletedBy"+
                         ")" +
                         " values " +
                         " (" +
                         "@OrganizationId, @PurchaseId, @AssetItemId, @PurchaseQuantity, @Rate, @Total," +
                         " @RegisterToAsset, 0, 0, null, @IsVerified, @VerifiedBy," +
                         " @VerifiedDate, @EnteredBy, @EnteredDate, 0, null, 0," +
                         "  null, 0 " +
                         ")  SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = DbHelper.GetDBConnection())
            {
                
                 int a=   db.Query<int>(sql, purchaseitem).SingleOrDefault();
                return a;

            }

        }
        public void EditAssetPurchase(FA_Purchase fapurchase)
        {
            string sql = " update FA_Purchase set " +
                        "  OrganizationId=@OrganizationId,VendorId=@VendorId, BillNo=@BillNo, BillDate=@BillDate, BillDateBS=@BillDateBS,"+
                        " TotalAmount=@TotalAmount, " +
                        " VatApplicable=@VatApplicable,VatAmount=@VatAmount,VATPercent=@VATPercent," +
                        " DiscountAmount=@DiscountAmount,TotalWithVat=@TotalWithVat," +
                        " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate " +
                        " where PurchaseId=@PurchaseId";

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql, fapurchase);
                //  trsn.Commit();
                //}

            }

        }
        public void EditAssetPurchaseItem(FA_PurchaseItem purchaseitem)
        {
            string sql = " update FA_PurchaseItem  set " +
                         " OrganizationId=@OrganizationId, PurchaseId=@PurchaseId, " +
                         " AssetItemId=@AssetItemId, PurchaseQuantity=@PurchaseQuantity, Rate=@Rate, Total=@Total," +
                         " RegisterToAsset=@RegisterToAsset, IsDeleted =@IsDeleted, "+
                         " LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate" +
                         " where PurchaseItemId=@PurchaseItemId";
                         

            using (var db = DbHelper.GetDBConnection())
            {

                db.Query<int>(sql, purchaseitem).SingleOrDefault();


            }

        }
        public List<AssetPurchaseVM> GetPurchaseList(int organizationId, string fromdate, string todate, string billno,int vendorId)
        {
            string sql = "select p.PurchaseId, p.OrganizationId, o.OrganizationName, p.VendorId, v.VendorName, p.BillNo, p.BillDate,p.BillDateBS,p.BillSerialNo," +
                " p.TotalAmount, p.VatApplicable, p.VatAmount, p.VatPercent,p.DiscountAmount,p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate"+
                "  from FA_Purchase p"+
                " left join MS_Organization o on o.OrganizationId = p.ORganizationId"+
                " left join INV_Vendor v on v.VendorId = p.VendorId"+
                " left join SC_User u on u.UserId = p.VerifiedBy"+
                " where 1 = 1 and p.IsDeleted = 0 and p.OrganizationId=" + organizationId;
            if (!string.IsNullOrEmpty(fromdate))
            {
                sql += " and p.EnteredDate >='" + fromdate + "'";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                sql += " and p.EnteredDate <='" + todate + "'";
            }
            if (!string.IsNullOrEmpty(billno))
            {
                sql += " and p.BillNo like '%" + billno + "%'";
            }
            if (vendorId>0)
            {
                sql += " and p.VendorId ="+vendorId;
            }
            sql += " order by p.EnteredDate  desc";
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetPurchaseVM>(sql).ToList();
            }
        }

        public FA_Purchase GetDetail(int orgid, int id)
        {
            string sql = "select p.PurchaseId, o.OrganizationName, v.VendorName, p.BillNo, p.BillDate,p.TotalAmount," +
                "p.TotalWithVat, p.IsVerified from FA_Purchase p" +
                " left join MS_Organization o on o.OrganizationId = p.OrganizationId" +
                " left join INV_Vendor v on v.VendorId = p.VendorId" +
                " left join SC_User u on u.UserId = p.VerifiedBy" +
                " where 1 = 1 and p.IsDeleted = 0 and p.PurchaseId=" + id + " and  p.OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<FA_Purchase>(sql).SingleOrDefault();
            }
        }

        public AssetPurchaseVM GetPurchase(int orgid,int id)
        {
            string sql = "select p.PurchaseId, p.OrganizationId, o.OrganizationName, p.VendorId, v.VendorName, " +
                "p.BillNo, p.BillDate,p.BillDateBS, p.TotalAmount,p.VatApplicable, p.VatAmount, p.VatPercent," +
                "p.TotalWithVat, p.IsVerified, p.VerifiedBy, u.FullName VerifiedByName, p.VerifiedDate" +
                " from FA_Purchase p" +
                " left join MS_Organization o on o.OrganizationId = p.OrganizationId" +
                " left join INV_Vendor v on v.VendorId = p.VendorId" +
                " left join SC_User u on u.UserId = p.VerifiedBy" +
                " where 1 = 1 and p.IsDeleted = 0 and p.PurchaseId = "+id+" and p.OrganizationId = "+orgid;

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetPurchaseVM>(sql).SingleOrDefault();
            }
        }
        public List<AssetPurchaseItem> GetPurchaseItemList(int orgid,int purchaseid)
        {
            string sql = "select "+
                " p.PurchaseItemId, p.OrganizationId, o.OrganizationName, ai.AssetCategoryId, ac.CategoryName,"+
                " p.PurchaseId, p.AssetItemId, ai.AssetName, p.PurchaseQuantity, p.Rate, p.Total, p.RegisterToAsset,"+
                " p.ReturnedQuantity, p.ReturnedBy,ru.FullName ReturnedByName, p.ReturnedDate, p.IsVerified, p.VerifiedBy,"+
                " u.FullName VerifiedByName, p.VerifiedDate"+
                " from FA_PurchaseItem p"+
                " left join MS_Organization o on o.OrganizationId = p.ORganizationId"+
                " left join FA_AssetItem ai on ai.AssetItemId = p.AssetItemId"+
                " left join SC_User u on u.UserId = p.VerifiedBy"+
                " left join SC_User ru on ru.UserId = p.ReturnedBy"+
                " left join FA_AssetCategory ac on ac.AssetCategoryId = ai.AssetCategoryId"+
                " where 1=1 and p.IsDeleted=0 and p.PurchaseId=" + purchaseid+ " and  p.OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetPurchaseItem>(sql).ToList();
            }
        }

        public AssetPurchaseDetail GetPurchaseDetail(int orgid,int id)
        {
            AssetPurchaseDetail detail = new AssetPurchaseDetail();
            detail.AssetPurchase = GetPurchase(orgid,id);
            detail.AssetPurchaseItems = GetPurchaseItemList(orgid,id);
            return detail;
        }
        public bool DeleteAssetPurchase(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update FA_Purchase set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
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

            //string sql1 = " Update FA_PurchaseItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
            //    " where PurchaseId=" + id;

            //using (var db = DbHelper.GetDBConnection())
            //{
            //    //using (var trsn = db.BeginTransaction())
            //    //{
            //    db.Query(sql);
            //    //  trsn.Commit();
            //    //}

            //}
        }
        public void DeleteAssetItem(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update FA_PurchaseItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
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
        public void DeleteAllInvetoryItem(int id, int deletedby, DateTime deleteddate)
        {
            string sql = " Update FA_PurchaseItem set IsDeleted=1,DeletedBy=" + deletedby + ",DeletedDate= '" + deleteddate + "'" +
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
        public FA_Purchase GetPurchaseById(int orgid, int PBillid)
        {
            string sql = "select * from FA_Purchase " +
                " where 1=1 and IsDeleted=0 and PurchaseId=" + PBillid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<FA_Purchase>(sql).SingleOrDefault();
            }
        }
        public FA_PurchaseItem GetPurchaseItemById(int orgid, int pitemid)
        {
            string sql = "select * from FA_PurchaseItem " +
                " where 1=1 and PurchaseItemId=" + pitemid + " and OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<FA_PurchaseItem>(sql).SingleOrDefault();
            }
        }

        public FA_Purchase GetBillSerialNo(int orgid)
        {
            string sql = " select top 1 BillSerialNo from FA_Purchase " +
                "where OrganizationId=" + orgid +" and IsDeleted=0"+
                " order by BillSerialNo desc";
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<FA_Purchase>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

        public FA_Purchase GetBillSerialNo(int orgid, int id)
        {
            string sql = " select BillSerialNo from FA_Purchase" +
                " where OrganizationId=" + orgid + " and PurchaseId=" + id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<FA_Purchase>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }

    }
}