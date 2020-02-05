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
	public class AcademicTeacherRepo
	{
		public List<AcademicTeacherVM> GetAcademicTeacherList(int organizationId = 0)
		{
			string sql = " select " +
			" t.StaffId,t.StaffName,t.StaffCode,t.OrganizationId,o.OrganizationName,t.TemporaryAddress,t.PermanentAddress,t.Mobile," +
			" t.Email,t.CitizenshipNo,t.JoinDate,t.JoinDateBS,t.DOB,t.DOBBS,t.Photo " +
			" from ACD_Staff t " +
			" left join MS_Organization o on o.OrganizationId = t.OrganizationId ";
			
			//" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and t.IsDeleted=0";
			if (organizationId != 0)
			{
				sql += " and t.OrganizationId=" + organizationId;

			}
			
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicTeacherVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_Staff GetAcademicTeacherById(int Id)
		{
			string sql = " Select *  from ACD_Staff where StaffId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_Staff>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
        public bool InsertAcademicTeacher(ACD_Staff Teacher)
		{
			string sql = " Insert into  ACD_Staff (StaffName,StaffCode,OrganizationId,TemporaryAddress,PermanentAddress,Mobile,"+
			"Email,CitizenshipNo,JoinDate,JoinDateBS,DOB,DOBBS,Photo,EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate,IsDeleted,"+
			"DeletedBy,DeletedDate) " +
			" values " +
			"(@StaffName,@StaffCode,@OrganizationId,@TemporaryAddress,@PermanentAddress,@Mobile," +
			"@Email,@CitizenshipNo,@JoinDate,@JoinDateBS,@DOB,@DOBBS,@Photo,@EnteredBy,@EnteredDate,0,null,0," +
			"0,null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, Teacher);
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
		public bool UpdateAcademicTeacher(ACD_Staff teacher)
		{
			string sql = " Update ACD_Staff set StaffName=@StaffName,StaffCode=@StaffCode,OrganizationId=@OrganizationId,TemporaryAddress=@TemporaryAddress,PermanentAddress=@PermanentAddress,Mobile=@Mobile,"+
			" Email=@Email,CitizenshipNo=@CitizenshipNo,JoinDate=@JoinDate,JoinDateBS=@JoinDateBS,DOB=@DOB,DOBBS=@DOBBS,Photo=@Photo,LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate" +
			" where StaffId=@StaffId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, teacher);
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
		public bool DeleteAcademicTeacher(int Id, int deletedBy, DateTime deletedDate)
		{
			string sql = " Update ACD_Staff set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where StaffId= " + Id;
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