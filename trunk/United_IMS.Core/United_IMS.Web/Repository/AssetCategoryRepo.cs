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
	public class AssetCategoryRepo
	{
		public List<AssetCategoryVM> GetAssetCategoryList(int orgid=0)
		{
            string sql = "select ac.AssetCategoryId, ac.CategoryName, ac.CategoryCode, ac.DepreciationMethodId, dm.MethodName, ac.DepreciationRate,ac.OrganizationId, o.OrganizationName" +
                " from FA_AssetCategory ac" +
                " left join FA_DepreciationMethod dm on dm.MethodId = ac.DepreciationMethodId" +
                " left join MS_Organization o on o.OrganizationId = ac.OrganizationId";
              sql += " where 1=1 and ac.IsDeleted=0";
			if (orgid != 0)
				sql += " and ac.OrganizationId="+orgid;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AssetCategoryVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
        public AssetCategoryVM GetAssetCategoryDetail(int cateid=0)
        {
            string sql = "select ac.AssetCategoryId, ac.CategoryName, ac.CategoryCode, ac.DepreciationMethodId, dm.MethodName, ac.DepreciationRate,ac.OrganizationId, o.OrganizationName" +
                " from FA_AssetCategory ac" +
                " left join FA_DepreciationMethod dm on dm.MethodId = ac.DepreciationMethodId" +
                " left join MS_Organization o on o.OrganizationId = ac.OrganizationId";
            sql += " where 1=1 and ac.IsDeleted=0";
            //if (orgid != 0)
            //    sql += " and ac.OrganizationId=" + orgid;
            sql += " and ac.AssetCategoryId="+cateid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<AssetCategoryVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public FA_AssetCategory GetAssetCategoryById(int Id)
		{
			string sql = " Select *  from FA_AssetCategory where AssetCategoryId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FA_AssetCategory>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAssetCategory(FA_AssetCategory category)
		{
			string sql = " Insert into  FA_AssetCategory ( CategoryName, CategoryCode, DepreciationMethodId, DepreciationRate, OrganizationId, "+
                " EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
            "( @CategoryName, @CategoryCode, @DepreciationMethodId, @DepreciationRate, @OrganizationId,"+
            "  @EnteredBy, @EnteredDate, 0, null, 0,0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, category);
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
		public bool UpdateAssetCategory(FA_AssetCategory category)
		{
			string sql = " Update FA_AssetCategory set  CategoryName=@CategoryName, CategoryCode=@CategoryCode, DepreciationMethodId=@DepreciationMethodId,"+
                " DepreciationRate=@DepreciationRate, OrganizationId=@OrganizationId, " +
 "LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate" +
                 " where AssetCategoryId= @AssetCategoryId";
            using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, category);
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
		public bool DeleteAssetCategory(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update FA_AssetCategory set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where AssetCategoryId= " + Id;
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