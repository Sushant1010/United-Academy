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
	public class BranchRepo
	{
		public List<BranchVM> GetBranchList(int orgid=0)
		{
			string sql = " select " +
			" c.BranchId,c.BranchName,c.BranchCode,c.OrganizationId,o.OrganizationName,c.EnteredBy,c.EnteredDate," +
			" c.LastUpdatedBy,c.LastUpdatedDate,c.IsDeleted,c.DeletedDate,c.DeletedBy " +
			" from FA_Branch c" +
			" left join MS_Organization o on o.OrganizationId = c.OrganizationId ";
			
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and c.IsDeleted=0";
			if (orgid != 0)
				sql += " and c.OrganizationId="+orgid;
            sql += " order by c.EnteredDate  desc";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<BranchVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public FA_Branch GetBranchById(int Id)
		{
			string sql = " Select *  from FA_Branch where BranchId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FA_Branch>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertBranch(FA_Branch location)
		{
			string sql = " Insert into  FA_Branch (BranchName,BranchCode,OrganizationId,EnteredBy,EnteredDate,"+
			" LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedDate,DeletedBy) " +
			" values " +
			"(@BranchName,@BranchCode,@OrganizationId,@EnteredBy,@EnteredDate,"+
			"0,null,0,null,0)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, location);
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
		public bool UpdateBranch(FA_Branch location)
		{
			string sql = " Update FA_Branch set BranchName=@BranchName,BranchCode=@BranchCode,OrganizationId=@OrganizationId," +
			" LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
			" where BranchId=@BranchId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, location);
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
		public bool DeleteBranch(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update FA_Branch set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where BranchId= " + Id;
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