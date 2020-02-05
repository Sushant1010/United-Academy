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
	public class AssetItemRepo
	{
		public List<AssetItemVM> GetAssetItemList(int orgid=0)
		{
            string sql = "select ai.AssetItemId,ai.EnteredDate,ai.AssetCategoryId,ac.CategoryName,ai.AssetName,ai.AssetCode,ai.OrganizationId,o.OrganizationName,ai.LifeSpan,ai.IsDepreciation,dm.MethodName " +
                " from FA_AssetItem ai" +
                " left join FA_AssetCategory ac on ac.AssetCategoryId = ai.AssetCategoryId" +
                " left join MS_Organization o on o.OrganizationId = ai.OrganizationId" +
                " left join FA_DepreciationMethod dm on dm.MethodId = ac.DepreciationMethodId";
              sql += " where 1=1 and ai.IsDeleted=0";
			if (orgid != 0)
				sql += " and ai.OrganizationId="+orgid;
            sql += " order by ai.EnteredDate  desc";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AssetItemVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
        public AssetItemVM GetAssetItemDetail(int itemid)
        {
            string sql = "select ai.AssetItemId,ai.AssetCategoryId,ac.CategoryName,ai.AssetName," +
                "ai.AssetCode,ai.OrganizationId,o.OrganizationName,ai.LifeSpan,ai.IsDepreciation," +
                "ai.FromDate, ai.FromDateBS, ai.ToDate, ai.ToDateBS, ai.WarrentyDuration " +
                " from FA_AssetItem ai" +
                " left join FA_AssetCategory ac on ac.AssetCategoryId = ai.AssetCategoryId" +
                " left join MS_Organization o on o.OrganizationId = ai.OrganizationId";
            sql += " where 1=1 and ai.IsDeleted=0";
            //if (orgid != 0)
            //    sql += " and ac.OrganizationId=" + orgid;
            sql += " and ai.AssetItemId="+itemid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<AssetItemVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public FA_AssetItem GetAssetItemById(int Id)
		{
			string sql = " Select *  from FA_AssetItem where AssetItemId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FA_AssetItem>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}


        public bool InsertAssetItem(FA_AssetItem item)
		{
			string sql = " Insert into  FA_AssetItem ( AssetCategoryId,AssetName,AssetCode,OrganizationId,LifeSpan,IsDepreciation,"+
    " EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate,WarrentyDuration,FromDate,FromDateBS,ToDate," +
    "ToDateBS, IsWarranty) " +
			" values " +
            "( @AssetCategoryId,@AssetName,@AssetCode,@OrganizationId,@LifeSpan,@IsDepreciation,"+
            "@EnteredBy,@EnteredDate,0,null,0,0,null,@WarrentyDuration,@FromDate,@FromDateBS,@ToDate,@ToDateBS,@IsWarranty)";
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
		public bool UpdateAssetItem(FA_AssetItem item)
		{
			string sql = " Update FA_AssetItem set  AssetCategoryId=@AssetCategoryId,AssetName=@AssetName, " +
                " AssetCode=@AssetCode,OrganizationId=@OrganizationId,LifeSpan=@LifeSpan, " +
                " LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate, " +
                "WarrentyDuration=@WarrentyDuration,FromDate=@FromDate," +
                "FromDateBS=@FromDateBS,ToDate=@ToDate,ToDateBS=@ToDateBS, IsWarranty=@IsWarranty"+
                 " where AssetItemId= @AssetItemId";
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
		public bool DeleteAssetItem(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update FA_AssetItem set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where AssetItemId= " + Id;
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