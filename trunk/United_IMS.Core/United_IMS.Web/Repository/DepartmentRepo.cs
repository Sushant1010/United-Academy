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
	public class DepartmentRepo
	{
		public List<MS_Department> GetDepartmentList(int orgid=0)
		{
			string sql = " select " +
			" c.DepartmentId,c.DepartmentName,c.DepartmentCode,c.OrganizationId,o.OrganizationName,c.EnteredBy,c.EnteredDate," +
			" c.LastUpdatedBy,c.LastUpdatedDate,c.IsDeleted,c.DeletedDate,c.DeletedBy " +
			" from MS_Department c" +
			" left join MS_Organization o on o.OrganizationId = c.OrganizationId ";
			
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and c.IsDeleted=0";
			if (orgid != 0)
				sql += " and c.OrganizationId="+orgid;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_Department>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public MS_Department GetDepartmentById(int Id)
		{
			string sql = " Select *  from MS_Department where DepartmentId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_Department>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertDepartment(MS_Department Department)
		{
			string sql = " Insert into  MS_Department (DepartmentName,DepartmentCode,OrganizationId,EnteredBy,EnteredDate,"+
			" LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedDate,DeletedBy) " +
			" values " +
			"(@DepartmentName,@DepartmentCode,@OrganizationId,@EnteredBy,@EnteredDate,"+
			"0,null,0,null,0)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, Department);
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
		public bool UpdateDepartment(MS_Department Department)
		{
			string sql = " Update MS_Department set DepartmentName=@DepartmentName,DepartmentCode=@DepartmentCode,OrganizationId=@OrganizationId," +
			" LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
			" where DepartmentId=@DepartmentId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, Department);
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
		public bool DeleteDepartment(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update MS_Department set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where DepartmentId= " + Id;
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