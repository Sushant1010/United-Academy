using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using United_IMS.Web.ViewModel;
using Dapper;
using United_IMS.Web.Models;
using System.Transactions;

namespace United_IMS.Web.Repository
{
	public class AcademicYearRepo
	{
		public List<MS_AcademicYear> GetAcademicYearList(string academicYearName="")
		{
			string sql = " select * " +
						 " from MS_AcademicYear ";
						
			sql += " where 1=1 and IsDeleted=0";	
			
			
			if (!string.IsNullOrEmpty(academicYearName))
			{
				sql += " and b.AcademicYearName like '%" + academicYearName + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_AcademicYear>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public MS_AcademicYear GetAcademicYearById(int Id)
		{
			string sql = " Select *  from MS_AcademicYear where AcademicYearId="+Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<MS_AcademicYear>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicYear(MS_AcademicYear year)
		{
			string sql = " Insert into  MS_AcademicYear (AcademicYearName,StartDate,EndDate,IsCurrent,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate," +
						"IsDeleted,DeletedBy,DeletedDate) " +
			" values " +
			"(@AcademicYearName,@StartDate,@EndDate,@IsCurrent,@EnteredBy,@EnteredDate,0,null,0,0,null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, year);
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
		public bool UpdateAcademicYear(MS_AcademicYear year)
		{
			if (year.IsCurrent != null && year.IsCurrent == true)
			{
				string sql1 = " Update MS_AcademicYear set IsCurrent=@IsCurrent";
			//" IsDeleted,DeletedBy,DeletedDate" +
			//" where AcademicYearId=@AcademicYearId";
				using (var db = DbHelper.GetDBConnection())
				{
					using (var trsn = new TransactionScope())
					{
						//db.Execute(sql);
						var lst = db.Execute(sql1);
						trsn.Complete();
						db.Close();
					}
				}
			}
			string sql = " Update MS_AcademicYear set AcademicYearName=@AcademicYearName,StartDate=@StartDate,EndDate=@EndDate,IsCurrent=@IsCurrent," +
						" LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
						//" IsDeleted,DeletedBy,DeletedDate" +
			" where AcademicYearId=@AcademicYearId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, year);
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
		public bool DeleteAcademicYear(int yearId,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update MS_AcademicYear set IsDeleted=1, DeletedBy=" + deletedBy+", DeletedDate='"+deletedDate+"' where YearId= "+yearId;
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