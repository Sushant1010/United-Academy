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
	public class LocationRepo
	{
		public List<LocationVM> GetLocationList(int orgid=0)
		{
			string sql = " select " +
			" c.LocationId,c.LocationName,c.OrganizationId,o.OrganizationName,c.EnteredBy,c.EnteredDate," +
			" c.LastUpdatedBy,c.LastUpdatedDate,c.IsDeleted,c.DeletedDate,c.DeletedBy,c.LocationCode " +
			" from FA_Location c" +
			" left join MS_Organization o on o.OrganizationId = c.OrganizationId ";
			
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and c.IsDeleted=0";
			if (orgid != 0)
				sql += " and c.OrganizationId="+orgid;
            sql += " order by c.EnteredDate  desc";
            using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<LocationVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public FA_Location GetLocationById(int Id)
		{
			string sql = " Select *  from FA_Location where LocationId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<FA_Location>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertLocation(FA_Location location)
		{
			string sql = " Insert into  FA_Location (LocationName,OrganizationId,EnteredBy,EnteredDate,"+
			" LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedDate,DeletedBy,LocationCode) " +
			" values " +
			"(@LocationName,@OrganizationId,@EnteredBy,@EnteredDate,"+
			"0,null,0,null,0,@LocationCode)";
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
		public bool UpdateLocation(FA_Location location)
		{
			string sql = " Update FA_Location set LocationName=@LocationName,OrganizationId=@OrganizationId," +
			" LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate,LocationCode =@LocationCode" +
			" where LocationId=@LocationId";
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
		public bool DeleteLocation(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update FA_Location set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where LocationId= " + Id;
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