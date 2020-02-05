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
	public class AcademicClassSectionRepo
	{
		public List<AcademicClassSectionVM> GetAcademicClassSectionList(int classId=0,int batchId=0,int sectionId=0)
		{
			string sql = " select " +
			" cs.ClassSectionId, cs.BatchId, b.BatchName, cs.ClassId, c.ClassName, cs.SectionId, s.SectionName , cs.StartDate,cs.StartDateBS " +
			" from ACD_ClassSection cs" +
			" left join ACD_Batch b on b.BatchId = cs.BatchId" +
			" left join ACD_Class c on c.ClassId = cs.ClassId" +
			" left join ACD_Section s on s.SectionId = cs.SectionId" +
			" where 1=1 and cs.IsDeleted=0 ";

			if(classId!=0)
			{
				sql += " cs.ClassId="+classId;
			}
			if (sectionId != 0)
			{
				sql += "cs.SectionId="+sectionId;
			}
			if (batchId != 0)
			{
				sql += " cs.BatchId="+batchId;
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicClassSectionVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_ClassSection GetAcademicClassSectionById(int classSectionId)
		{
			string sql = " Select *  from ACD_ClassSection where ClassSecctionId=" + classSectionId;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_ClassSection>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicClassSection(ACD_ClassSection classSection)
		{
			string sql = " Insert into  ACD_ClassSection (BatchId,ClassId,SectionId,StartDate,StartDateBS,EnteredBy, EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate) " +
			" values " +
			"(@BatchId,@ClassId,@SectionId,@StartDate,@StartDateBS,@EnteredBy, @EnteredDate,0,null,0,0,null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, classSection);
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
		public bool UpdateAcademicClassSection(ACD_ClassSection classSection)
		{
			string sql = " Update ACD_ClassSection set BatchId=@BatchId,ClassId=@ClassId,SectionId=@SectionId,StartDate=@StartDate,StartDateBS=@StartDateBS,LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
			" where ClassSectionId=@ClassSectionId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, classSection);
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
		public bool DeleteAcademicClassSection(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update ACD_ClassSection set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where ClassSectionId= " + Id;
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