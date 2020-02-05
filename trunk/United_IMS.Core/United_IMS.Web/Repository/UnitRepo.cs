using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.ViewModel;
using Dapper;
using United_IMS.Web.Models;
using System.Transactions;

namespace United_IMS.Web.Repository
{
	public class UnitRepo
	{
		public List<UnitVM> GetUnitList(int organizationId=0,string unitName="",string unitCode="")
		{
			string sql = " select b.UnitId, b.UnitName, b.UnitCode,b.OrganizationId,o.OrganizationName " +
						 " from MS_Unit b " +
						 " left join MS_Organization o on o.OrganizationId = b.OrganizationId";
						 //" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and b.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and b.OrganizationId="+organizationId;

			}
			if (!string.IsNullOrEmpty(unitName))
			{
				sql += " and b.UnitName like '%" + unitName+"%'";
			}
			if (!string.IsNullOrEmpty(unitCode))
			{
				sql += " and b.UnitCode like '%" + unitCode + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<UnitVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public MS_Unit GetUnitById(int Id)
		{
			string sql = " Select *  from MS_Unit where UnitId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_Unit>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertUnit(MS_Unit unit)
		{
			string sql = " Insert into  MS_Unit (UnitName, UnitCode,OrganizationId, EnteredBy, EnteredDate," +
			" LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
			"(@UnitName, @UnitCode,@OrganizationId, @EnteredBy, @EnteredDate," +
			" 0, null, 0, 0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, unit);
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
		public bool UpdateUnit(MS_Unit unit)
		{
			string sql = " Update MS_Unit set UnitName=@UnitName, UnitCode=@UnitCode, " +
			" OrganizationId=@OrganizationId, " +
			" EnteredBy=@EnteredBy, EnteredDate=@EnteredDate,LastUpdatedBy=@LastUpdatedBy, " +
			" LastUpdatedDate=@LastUpdatedDate, IsDeleted=0, DeletedBy=0, DeletedDate=null" +
			" where UnitId=@UnitId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, unit);
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
		public bool DeleteUnit(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update MS_Unit set IsDeleted=1, DeletedBy=" + deletedBy+", DeletedDate='"+deletedDate+"' where UnitId= "+Id;
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