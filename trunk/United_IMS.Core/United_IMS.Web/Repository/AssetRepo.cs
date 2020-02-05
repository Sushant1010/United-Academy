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
	public class AssetRepo
	{
		public List<AssetVM> GetAssetList(int orgid=0,int CategoryId=0,int AssetItemId=0,string assetcode="", int BranchId = 0, int LocationId = 0)
		{
            string sql = "select  fa.AssetId,fa.EnteredDate,ai.AssetCategoryId,ac.CategoryName, fa.AssetItemId, ai.AssetName, fa.PurchaseId, fa.BillDate, fa.BillDateBs, fa.BillNo, fa.OpeningValue,"+
                "  fa.PurchaseValue, fa.AssetUniqueCode, fa.Barcode, fa.OrganizationId, o.OrganizationName, fa.BranchId,"+
                "   b.BranchName, fa.LocationId, l.LocationName, fa.Description , fa.UsefulLife, fa.IsAccessory,"+
                "    fa.AccessoryForAssetId, ffa.AssetUniqueCode AccessoryForAssetName, fa.IsDepreciationApplicable , dm.MethodName DepreciationName,"+
                "     fa.DepreciationStartDate, fa.DepreciationRemarks, fa.IsSold, fa.SoldDate, fa.SoldValue, fa.IsScrap, "+
                " 	fa.ScrapDate, fa.ScrapRealizedValue"+
                "     from FA_Asset fa "+
                "     left join FA_AssetItem ai on ai.AssetItemId = fa.AssetItemId"+
                "     left join FA_AssetCategory ac on ac.AssetCategoryId = ai.AssetCategoryId"+
                "     left join MS_Organization o on o.OrganizationId = fa.OrganizationId"+
                "     left join FA_Branch b on b.BranchId = fa.BranchId"+
                "     left join FA_Location l on l.LocationId = fa.LocationId"+
                "     left join FA_Asset ffa on ffa.AssetId = fa.AccessoryForAssetId"+
                "     left join FA_DepreciationMethod dm on dm.MethodId = ac.DepreciationMethodId";
              sql += " where 1=1 and fa.IsDeleted=0";
			if (orgid != 0)
				sql += " and fa.OrganizationId="+orgid;
      

            if (AssetItemId != 0)
                sql += " and fa.AssetItemId=" + AssetItemId;
            if (BranchId != 0)
            {
                sql += " and fa.BranchId=" + BranchId;

            }
            if (CategoryId != 0)
            {
                sql += " and fa.CategoryId=" + CategoryId;

            }
            if (LocationId != 0)
            {
                sql += " and fa.locationId=" + LocationId;
            }
            if (!string.IsNullOrEmpty(assetcode))
            {
                sql += " fa.AssetUniqueCode like '%"+assetcode+"%'";
            }
            sql += " order by fa.EnteredDate  desc";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AssetVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
        public AssetVM GetAssetDetail(int id=0)
        {
            string sql = "select  fa.AssetId,ai.AssetCategoryId,ac.CategoryName, fa.AssetItemId, ai.AssetName, fa.PurchaseId, fa.BillDate, fa.BillDateBs, fa.BillNo, fa.OpeningValue," +
                "  fa.PurchaseValue, fa.AssetUniqueCode, fa.Barcode, fa.OrganizationId, o.OrganizationName, fa.BranchId," +
                "   b.BranchName, fa.LocationId, l.LocationName, fa.Description , fa.UsefulLife, fa.IsAccessory," +
                "    fa.AccessoryForAssetId, ffa.AssetUniqueCode AccessoryForAssetName, fa.IsDepreciationApplicable , dm.MethodName DepreciationName," +
                "     fa.DepreciationStartDate, fa.DepreciationRemarks, fa.IsSold, fa.SoldDate, fa.SoldValue, fa.IsScrap, " +
                " 	fa.ScrapDate, fa.ScrapRealizedValue" +
                "     from FA_Asset fa " +
                "     left join FA_AssetItem ai on ai.AssetItemId = fa.AssetItemId" +
                "     left join FA_AssetCategory ac on ac.AssetCategoryId = ai.AssetCategoryId" +
                "     left join MS_Organization o on o.OrganizationId = fa.OrganizationId" +
                "     left join FA_Branch b on b.BranchId = fa.BranchId" +
                "     left join FA_Location l on l.LocationId = fa.LocationId" +
                "     left join FA_Asset ffa on ffa.AssetId = fa.AccessoryForAssetId" +
                "     left join FA_DepreciationMethod dm on dm.MethodId = ac.DepreciationMethodId";
            sql += " where 1=1 and ai.IsDeleted=0";
            if (id != 0)
                sql += " and fa.AssetId=" + id;
           
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<AssetVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public FA_Asset GetAssetById(int Id)
		{
			string sql = " Select *  from FA_Asset where AssetId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FA_Asset>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAsset(FA_Asset item, string AssetUniqueCode3)
		{
			string sql = " Insert into  FA_Asset ( AssetItemId,PurchaseId, BillDate, BillDateBs , BillNo , OpeningValue, PurchaseValue, AssetUniqueCode,Barcode,"+
                " OrganizationId , BranchId ,  LocationId ,  Description ,  UsefulLife ,  AccessoryForAssetId ,  IsAccessory ,  IsDepreciationApplicable ,"+
                " DepreciationStartDate ,  DepreciationRemarks , IsSold , SoldDate ,  SoldValue , IsScrap , ScrapDate , ScrapRealizedValue ,"+
                " EnteredBy , EnteredDate , LastUpdatedBy ,  LastUpdatedDate ,  IsDeleted , DeletedBy , DeletedDate, CategoryId) " +
			" values " +
            "( @AssetItemId,@PurchaseId, @BillDate, @BillDateBs , @BillNo , @OpeningValue, @PurchaseValue,'"+ AssetUniqueCode3+"',@Barcode," +
                " @OrganizationId , @BranchId ,  @LocationId ,  @Description ,  @UsefulLife ,  @AccessoryForAssetId ,  @IsAccessory ,  @IsDepreciationApplicable ," +
                " @DepreciationStartDate ,  @DepreciationRemarks , @IsSold , @SoldDate ,  @SoldValue , @IsScrap , @ScrapDate , @ScrapRealizedValue ," +
                " @EnteredBy , @EnteredDate , 0 ,  null ,  0 , 0 , null ,@CategoryId)";
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
		public bool UpdateAsset(FA_Asset item)
		{
			string sql = " Update FA_Asset set  AssetItemId=@AssetItemId,PurchaseId=@PurchaseId, BillDate=@BillDate,"+
                " BillDateBs=@BillDateBs , BillNo=@BillNo , OpeningValue=@OpeningValue, PurchaseValue=@PurchaseValue, "+
                " AssetUniqueCode=@AssetUniqueCode, Barcode=@Barcode, OrganizationId=@OrganizationId , BranchId=@BranchId ,"+
                " LocationId=@LocationId ,  Description=@Description ,  UsefulLife=@UsefulLife ,  AccessoryForAssetId=@AccessoryForAssetId ,"+
                " IsAccessory=@IsAccessory ,  IsDepreciationApplicable=@IsDepreciationApplicable , DepreciationStartDate=@DepreciationStartDate ,"+
                " DepreciationRemarks=@DepreciationRemarks , IsSold=@IsSold , SoldDate=@SoldDate ,  SoldValue=@SoldValue , IsScrap=@IsScrap ,"+
                " ScrapDate=@ScrapDate , ScrapRealizedValue=@ScrapRealizedValue ,LastUpdatedBy=@LastUpdatedBy , "+
                " LastUpdatedDate=@LastUpdatedDate "+
                 " where AssetId= @AssetId";
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
		public bool DeleteAsset(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update FA_Asset set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where AssetId= " + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
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
	}
}