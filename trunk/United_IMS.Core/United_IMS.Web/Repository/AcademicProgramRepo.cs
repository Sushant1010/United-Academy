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
	public class AcademicProgramRepo
	{
		public List<AcademicProgramVM> GetAcademicProgramList(int organizationId=0,string programName="",string programCode="")
		{
			string sql = " select p.ProgramId, p.ProgramName, p.ProgramCode, p.OrganizationId, o.OrganizationName, p.StartedDate, p.StartedDateBS" +
						 " from ACD_Program p " +
						 " left join MS_Organization o on o.OrganizationId = p.OrganizationId";
						 
			sql += " where 1=1 and p.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and p.OrganizationId="+organizationId;
			}
			if (!string.IsNullOrEmpty(programName))
			{
				sql += " and p.programName like '%" + programName + "%'";
			}
			if (!string.IsNullOrEmpty(programCode))
			{
				sql += " and p.programCode like '%" + programCode + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicProgramVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_Program GetAcademicProgramById(int Id)
		{
			string sql = " Select *  from ACD_Program where ProgramId="+Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_Program>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicProgram(ACD_Program program)
		{
			string sql = " Insert into  ACD_Program ( ProgramName, ProgramCode, OrganizationId, StartedDate, StartedDateBS, "+
			" EnteredBy, EnteredDate, LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
			"( @ProgramName, @ProgramCode, @OrganizationId, @StartedDate, @StartedDateBS, "+
			" @EnteredBy, @EnteredDate, 0, null, 0, 0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, program);
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
		public bool UpdateAcademicProgram(ACD_Program program)
		{
			string sql = " Update ACD_Program set  ProgramName=@ProgramName, ProgramCode=@ProgramCode, OrganizationId=@OrganizationId, StartedDate=@StartedDate, StartedDateBS=@StartedDateBS, "+
						" LastUpdatedBy=@LastUpdatedBy, LastUpdatedDate=@LastUpdatedDate " +
						" where ProgramId=@ProgramId ";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, program);
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
		public bool DeleteAcademicProgram(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update ACD_Program set IsDeleted=1, DeletedBy="+deletedBy+", DeletedDate='"+deletedDate+"' where ProgramId= "+Id;
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					var lst=db.Execute(sql);
					trsn.Complete();
					//var lst = db.Execute(sql);
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