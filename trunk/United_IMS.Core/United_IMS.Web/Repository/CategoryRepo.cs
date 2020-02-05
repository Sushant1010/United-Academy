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
	public class CategoryRepo
	{
		public List<CategoryVM> GetCategoryList(int orgid=0)
		{
			string sql = " select " +
			" c.CategoryId,c.CategoryName,c.CategoryCode,c.OrganizationId,o.OrganizationName,c.EnteredBy,c.EnteredDate," +
			" c.LastUpdatedBy,c.LastUpdatedDate,c.IsDeleted,c.DeletedDate,c.DeletedBy " +
			" from MS_Category c" +
			" left join MS_Organization o on o.OrganizationId = c.OrganizationId ";
			
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and c.IsDeleted=0";
			if (orgid != 0)
				sql += " and c.OrganizationId="+orgid;
            sql += " order by c.EnteredDate  desc";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<CategoryVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public MS_Category GetCategoryById(int Id)
		{
			string sql = " Select *  from MS_Category where CategoryId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_Category>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertCategory(MS_Category category)
		{
			string sql = " Insert into  MS_Category (CategoryName,CategoryCode,OrganizationId,EnteredBy,EnteredDate,"+
			" LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedDate,DeletedBy) " +
			" values " +
			"(@CategoryName,@CategoryCode,@OrganizationId,@EnteredBy,@EnteredDate,"+
			"0,null,0,null,0)";
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
		public bool UpdateCategory(MS_Category category)
		{
			string sql = " Update MS_Category set CategoryName=@CategoryName,CategoryCode=@CategoryCode,OrganizationId=@OrganizationId," +
			" LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
			" where CategoryId=@CategoryId";
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
		public bool DeleteCategory(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update MS_Category set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where CategoryId= " + Id;
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