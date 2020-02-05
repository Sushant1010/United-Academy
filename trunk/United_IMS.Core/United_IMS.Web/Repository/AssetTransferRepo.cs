using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.ViewModel;
using United_IMS.Web.Models;
using Dapper;
using System.Transactions;

namespace United_IMS.Web.Repository
{
    public class AssetTransferRepo
    {
        public List<AssetTransferVM> GetAssetTransferList()
        {
            string sql = "select atr.AssetTransferId,ai.AssetName,o.OrganizationName,b.branchName, l.LocationName, u.FullName,atr.TransferDate,atr.TransferDateBS from FA_AssetTransfer atr" +
                         " left join MS_Organization o on o.OrganizationId = atr.OrganizationId" +
                         " left join FA_Branch b on b.BranchId = atr.BranchId" +
                         " left join fa_location l on l.locationId = atr.locationId" +
                         " left join SC_User u on u.UserId = atr.AssignedTo" +
                         " left join fa_asset a on a.AssetId = atr.AssetId" +
                         " left join FA_AssetItem ai on ai.AssetItemId = a.AssetItemId";
            sql += " where 1=1 and atr.IsDeleted=0 and atr.IsTransfered=0";
            //if (orgid != 0)
            //    sql += " and fa.OrganizationId=" + orgid;

            //if (assetitemid != 0)
            //    sql += " and fa.assetitemid=" + assetitemid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<AssetTransferVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public AssetTransferVM GetAssetTransferDetail(int transferId)
        {
            string sql = "select  atr.AssetId,u.UserId, o.OrganizationId, ai.AssetItemId, atr.CategoryId, ac.CategoryName ,atr.AssetTransferId,ai.AssetName,o.OrganizationName,b.branchName, " +
                "b.branchId,l.LocationName, l.LocationId, u.FullName,atr.TransferDate,atr.TransferDateBS, atr.AssignedDate, atr.Remarks" +
                " from FA_AssetTransfer atr " +
            " left join MS_Organization o on o.OrganizationId = atr.OrganizationId" +
            " left join FA_Branch b on b.BranchId = atr.BranchId" +
            " left join fa_location l on l.locationId = atr.locationId" +
            " left join SC_User u on u.UserId = atr.AssignedTo" +
            " left join fa_asset a on a.AssetId = atr.AssetId" +
            " left join FA_AssetItem ai on ai.AssetItemId = a.AssetItemId" +
            " left join FA_AssetCategory ac on ac.AssetCategoryId = atr.CategoryId" +
            " where 1 = 1 and atr.isdeleted = 0 and atr.AssetTransferId = " + transferId;
            

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetTransferVM>(sql).SingleOrDefault();
                
            }
        }

        public AssetTransferVM GetAssetTransferId()
        {
            string sql = "select Top 1 AssetTransferId, AssetId from FA_AssetTransfer order by AssetTransferId desc ";

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetTransferVM>(sql).SingleOrDefault();

            }
        }
        public bool InsertAssetTransfer(FA_AssetTransfer item)
        {
            string sql = " Insert into  FA_AssetTransfer (AssetId,OrganizationId,BranchId,LocationId," +
                " TransferDate,TransferDateBS,Remarks,AssignedTo,AssignedDate,IsReceived,ReceivedBy,ReceivedDate," +
                     " EnteredBy , EnteredDate , LastUpdatedBy ,  LastUpdatedDate ,  IsDeleted , DeletedBy , DeletedDate,IsTransfered,CategoryId) " +
            " values (" +
                "@AssetId,@OrganizationId, @BranchId, @LocationId , " +
                " @TransferDate,@TransferDateBS,@Remarks,@AssignedTo,@AssignedDate,0,0,null," +
                " @EnteredBy , @EnteredDate , 0 ,  null ,  0 , 0 , null,@IsTransfered,@CategoryId)";

            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Execute(sql);
                    var lst = db.Execute(sql, item);
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

        public void UpdateAsset(int AssTransid, int AssetId)
        {
            string sql = " update FA_Asset set " +
                        " IsTransfered = 1, AssetTransferId="+AssTransid+
                        "where AssetId="+AssetId;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

        }
        public void UpdateAsset1(int AssTransid, int AssetId)
        {
            string sql = " update FA_Asset set " +
                        " IsTransfered = Null, AssetTransferId= Null "+
                        "where AssetId=" + AssetId;

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

        }

        public AssetTransferVM GetAssetTransferSecondLastValue(int assetTraId, int assetId)
        {

            string sql = "select top 1 assettransferid, AssetId from fa_assetTransfer" +
                " where assettransferid<" + assetTraId + " and AssetId ="+assetId+" order by assettransferid desc";


            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<AssetTransferVM>(sql).SingleOrDefault();

            }

        }

        public void UpdateAssetTransfer(int PrevAssetTraId, int AssetId, int assetTraId)
        {
            string sql = "update FA_AssetTransfer set "
                          + " IsTransfered = 1, TransferId="+ assetTraId +
                          " where AssetId=" + AssetId+"and AssetTransferId ="+PrevAssetTraId;
            

            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

        }

        public void UpdateAssetTransfer(int assetTraId)
        {
            string sql = "update FA_AssetTransfer set "
                          + " IsTransfered = 0, TransferId=NULL " +
                          " where AssetTransferId =" + assetTraId;


            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

        }

        public FA_Asset getAssetId(int id)
        {
            string sql = "select AssetId from FA_Asset where AssetItemId=" + id;
            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<FA_Asset>(sql).SingleOrDefault();
            }
        }

        public FA_AssetTransfer GetAssetTransferById(int Id)
        {
            string sql = " Select *  from FA_AssetTransfer where AssetTransferId=" + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<FA_AssetTransfer>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        public bool UpdateAssetTransfer(FA_AssetTransfer item)
        {
            string sql = " Update FA_AssetTransfer set  AssetId=@AssetId,OrganizationId=@OrganizationId , BranchId=@BranchId ," +
                " LocationId=@LocationId , LastUpdatedBy=@LastUpdatedBy , LastUpdatedDate=@LastUpdatedDate, Remarks=@Remarks, " +
                " TransferDate=@TransferDate, TransferDateBS=@TransferDateBS, AssignedTo=@AssignedTo,AssignedDate=@AssignedDate" +
                 " where AssetTransferId= @AssetTransferId";
            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Execute(sql);
                    var lst = db.Execute(sql, item);
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

        public void updateAssetForAssetItem(int AssetId,int assetItemId)
        {
            string sql = "update FA_Asset set AssetItemId = "+assetItemId+ 
                          
                          " where AssetId=" + AssetId;


            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }

        }

        public List<AssetTransferVM> GetAssetTransferHistory(int id)
        {
            string sql = "select atr.AssetTransferId,ai.AssetName,o.OrganizationName,b.branchName, l.LocationName, u.FullName,atr.TransferDate,atr.TransferDateBS from FA_AssetTransfer atr" +
                         " left join MS_Organization o on o.OrganizationId = atr.OrganizationId" +
                         " left join FA_Branch b on b.BranchId = atr.BranchId" +
                         " left join fa_location l on l.locationId = atr.locationId" +
                         " left join SC_User u on u.UserId = atr.AssignedTo" +
                         " left join fa_asset a on a.AssetId = atr.AssetId" +
                         " left join FA_AssetItem ai on ai.AssetItemId = a.AssetItemId";
            sql += " where 1=1 and atr.IsDeleted=0 and a.AssetId="+id;
            sql += " Order by atr.EnteredDate desc";
            //if (orgid != 0)
            //    sql += " and fa.OrganizationId=" + orgid;

            //if (assetitemid != 0)
            //    sql += " and fa.assetitemid=" + assetitemid;

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<AssetTransferVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

        public void DeleteAssetTransfer(int Id, int deletedBy, DateTime deletedDate)
        {
            string sql = " Update FA_AssetTransfer set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where AssetTransferId= " + Id;
            
            using (var db = DbHelper.GetDBConnection())
            {
                //using (var trsn = db.BeginTransaction())
                //{
                db.Query(sql);
                //  trsn.Commit();
                //}

            }
        }
    }
}