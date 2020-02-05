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
	public class AcademicStudentRepo
	{
		public List<AcademicStudentVM> GetAcademicStudentList(int organizationId = 0, int batchId = 0, int classId = 0, int sectionId = 0,string reg="")
		{
			string sql = " select "+
			" st.StudentId,st.StudentName,st.StudentCode,st.StudentRegNo,st.Gender,st.BatchId,st.OrganizationId,o.OrganizationName,"+
			" b.BatchName, st.ClassId, c.ClassName, st.SectionId, s.SectionName, "+
			" st.CurrentClassId, cc.ClassName CurrentClassName, st.CurrentSectionId, cs.SectionName CurrentSectionName, st.AdmissionYear,"+
			" st.TemporaryAddress,st.PermanentAddress,st.Phone,st.DOB, st.DOBBS,st.FatherName,st.FatherContact,st.EnteredDate,"+
			" st.MotherName,st.MotherContact "+
			" from ACD_Student st "+
			" left join MS_Organization o on o.OrganizationId = st.OrganizationId " +
			" left join ACD_Batch b on b.BatchId = st.BatchId " +
			" left join ACD_Class c on c.ClassId = st.ClassId "+
			" left join ACD_Section s on s.SectionId = st.SectionId "+
			" left join ACD_Class cc on cc.ClassId = st.CurrentClassId "+
			" left join ACD_Section cs on cs.SectionId = st.CurrentSectionId";
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and st.IsDeleted=0";
			if (organizationId != 0)
			{
				sql += " and st.OrganizationId=" + organizationId;

			}
			if (batchId != 0)
			{
				sql += " and st.BatchId=" + batchId;

			}
			if (classId != 0)
			{
				sql += " and st.CurrentClassId=" + classId;

			}
			if (sectionId != 0)
			{
				sql += " and st.CurrentSectionId=" + sectionId;

			}
            if(!string.IsNullOrEmpty(reg))
            {
                sql += " and st.StudentRegNo like '%" + reg + "%'";
            }
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicStudentVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_Student GetAcademicStudentById(int Id)
		{
			string sql = " Select *  from ACD_Student where StudentId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_Student>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicStudent(ACD_Student student)
		{
			string sql = " Insert into  ACD_Student (StudentName, StudentCode, StudentRegNo,Gender,OrganizationId,BatchId, "+
			"ClassId , SectionId, CurrentClassId, CurrentSectionId, AdmissionYear ,"+
			"  TemporaryAddress, PermanentAddress, Phone, DOB, DOBBS, FatherName , FatherContact,"+
			" MotherName, MotherContact, EnteredBy ,"+
			" EnteredDate, LastUpdatedBy , LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
			"(@StudentName, @StudentCode, @StudentRegNo,@Gender,@OrganizationId,@BatchId,"+
			" @ClassId , @SectionId, @CurrentClassId, @CurrentSectionId, @AdmissionYear ,"+
			" @TemporaryAddress, @PermanentAddress, @Phone, @DOB, @DOBBS, @FatherName , @FatherContact,"+
			" @MotherName, @MotherContact, @EnteredBy ,"+
			" @EnteredDate, 0 , null, 0, 0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, student);
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
		public bool UpdateAcademicStudent(ACD_Student aclass)
		{
			string sql = " Update ACD_Student set StudentName=@StudentName, StudentCode=@StudentCode, StudentRegNo=@StudentRegNo,Gender=@Gender,OrganizationId=@OrganizationId,BatchId=@BatchId, " +
			" ClassId=@ClassId , SectionId=@SectionId, CurrentClassId=@CurrentClassId, CurrentSectionId=@CurrentSectionId, AdmissionYear=@AdmissionYear ," +
			"  TemporaryAddress=@TemporaryAddress, PermanentAddress=@PermanentAddress, Phone=@Phone, DOB=@DOB, DOBBS=@DOBBS, FatherName=@FatherName , FatherContact=@FatherContact," +
			" MotherName=@MotherName, MotherContact=@MotherContact, LastUpdatedBy=@LastUpdatedBy , LastUpdatedDate=@LastUpdatedDate" +
			" where StudentId=@StudentId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, aclass);
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
        
        public bool DeleteAcademicStudent(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update ACD_Student set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where StudentId= " + Id;
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