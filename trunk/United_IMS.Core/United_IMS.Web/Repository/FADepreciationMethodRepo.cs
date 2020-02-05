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
	public class FADepreciationMethodRepo
    {
		public List<FADepreciationMethodVM> GetDepreciationMethodList(int orgid=0)
		{
			string sql = " select dm.MethodId, dm.OrganizationId, o.OrganizationName,dm.MethodName,dm.DepreciationRate,dm.Description from FA_DepreciationMethod dm"+
                " left join MS_Organization o on o.OrganizationId = dm.OrganizationId ";
			
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and dm.IsDeleted=0";
			if (orgid != 0)
				sql += " and dm.OrganizationId="+orgid;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FADepreciationMethodVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public FA_DepreciationMethod GetDepreciationMethodById(int Id)
		{
			string sql = " Select *  from FA_DepreciationMethod where MethodId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FA_DepreciationMethod>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertDepreciationMethod(FA_DepreciationMethod method)
		{
			string sql = " Insert into  FA_DepreciationMethod (OrganizationId, MethodName,DepreciationRate,Description ,EnteredBy,"+
                " EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate) " +
			" values " +
            "(@OrganizationId, @MethodName,@DepreciationRate,@Description ,@EnteredBy,"+
            "@EnteredDate,0,null,0,0,null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, method);
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
		public bool UpdateDepreciationMethod(FA_DepreciationMethod method)
		{
			string sql = " Update FA_DepreciationMethod set OrganizationId=@OrganizationId, MethodName=@MethodName,DepreciationRate=@DepreciationRate," +
                "Description=@Description ,LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate"+
                " where MethodId=@MethodId";
            using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, method);
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
		public bool DeleteDepreciationMethod(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update FA_DepreciationMethod set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where MethodId= " + Id;
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