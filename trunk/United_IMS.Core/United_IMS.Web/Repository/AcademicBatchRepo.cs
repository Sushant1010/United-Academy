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
	public class AcademicBatchRepo
	{
		public List<AcademicBatchVM> GetAcademicBatchList(int organizationId=0,int programId=0,string batchName="",string batchCode="")
		{
			string sql = " select b.BatchId, b.BatchName, b.BatchCode, b.StartDateAD, b.StartDateBS,"+
						 " b.OrganizationId,o.OrganizationName,b.ProgramId,p.ProgramName"+
						 " from ACD_Batch b "+
						 " left join MS_Organization o on o.OrganizationId = b.OrganizationId"+
						 " left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and b.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and b.OrganizationId="+organizationId;
			}
			if (programId != 0)
			{
				sql += " and b.ProgramId=" + programId;
			}
			if (!string.IsNullOrEmpty(batchName))
			{
				sql += " and b.BatchName like '%" + batchName+"%'";
			}
			if (!string.IsNullOrEmpty(batchCode))
			{
				sql += " and b.BatchCode like '%" + batchCode + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicBatchVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_Batch GetAcademicBatchById(int batchId)
		{
			string sql = " Select *  from ACD_Batch where BatchId="+batchId;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_Batch>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicBatch(ACD_Batch batch)
		{
			string sql = " Insert into  ACD_Batch (BatchName, BatchCode, StartDateAD, StartDateBS, OrganizationId, ProgramId, EnteredBy, EnteredDate," +
			" LastUpdateBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
			"(@BatchName, @BatchCode, @StartDateAD, @StartDateBS, @OrganizationId, @ProgramId, @EnteredBy, @EnteredDate," +
			" 0, null, 0, 0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, batch);
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
		public bool UpdateAcademicBatch(ACD_Batch batch)
		{
			string sql = " Update ACD_Batch set BatchName=@BatchName, BatchCode=@BatchCode, " +
			" StartDateAD=@StartDateAD, StartDateBS=@StartDateBS, OrganizationId=@OrganizationId, ProgramId=@ProgramId, " +
			" EnteredBy=@EnteredBy, EnteredDate=@EnteredDate,LastUpdateBy=@LastUpdateBy, " +
			" LastUpdatedDate=@LastUpdatedDate, IsDeleted=0, DeletedBy=0, DeletedDate=null" +
			" where BatchId=@BatchId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, batch);
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
		public bool DeleteAcademicBatch(int batchId,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update ACD_Batch set IsDeleted=1, DeletedBy="+deletedBy+", DeletedDate='"+deletedDate+"' where BatchId= "+batchId;
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