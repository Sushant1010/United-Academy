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
	public class AcademicStudentClassSectionRepo
	{
		public List<AcademicStudentClassSectionVM> GetAcademicStudentClassSectionList(int BatchId=0,int classId=0,string AcademicYear="")
		{
			string sql = " select scs.StudentClassSectionId,scs.studentId,st.StudentName,st.StudentRegNo,scs.ClassId,c.ClassName,"+
                " scs.SectionId,s.SectionName,scs.AcademicYear,st.BatchId,b.BatchName"+
                "  from ACD_StudentClassSection scs"+
                " left join ACD_Student st on st.StudentId = scs.StudentId"+
                " left join ACD_Class c on c.ClassId = scs.ClassId"+
                " left join ACD_Section s on s.SectionId = scs.SectionId"+
                " left join ACD_Batch b on b.BatchId = st.BatchId" +
			" where 1=1 and scs.IsDeleted=0 ";

			if(classId!=0)
			{
				sql += " c.ClassId="+classId;
			}
			if (BatchId != 0)
			{
				sql += "b.BatchId="+BatchId;
			}
			if (!string.IsNullOrEmpty(AcademicYear))
			{
				sql += " scs.AcademicYear='"+AcademicYear+"'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicStudentClassSectionVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_ClassSection GetAcademicStudentClassSectionById(int classSectionId)
		{
			string sql = " Select *  from ACD_ClassSection where ClassSecctionId=" + classSectionId;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_ClassSection>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicStudentClassSection(ACD_StudentClassSection stdclassSection)
		{
			string sql = " Insert into  ACD_StudentClassSection (StudentId,ClassId,SectionId,AcademicYear,AssignedDate,AssignedDateBS,EnteredBy, EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,DeletedBy,DeletedDate) " +
			" values " +
            "(@StudentId,@ClassId,@SectionId,@AcademicYear,@AssignedDate,@AssignedDateBS,@EnteredBy, @EnteredDate,0,null,0,0,null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, stdclassSection);
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
		public bool UpdateAcademicStudentClassSection(ACD_ClassSection classSection)
		{
			string sql = " Update ACD_StudentClassSection set StudentId=@StudentId,ClassId=@ClassId,SectionId=@SectionId,AcademicYear=@AcademicYear,AssignedDate=@AssignedDate,AssignedDateBS=@AssignedDateBS,LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
            " where StudentClassSectionId=@StudentClassSectionId";
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