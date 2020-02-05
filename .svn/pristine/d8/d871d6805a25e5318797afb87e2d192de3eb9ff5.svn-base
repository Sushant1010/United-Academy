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
	public class AcademicSectionRepo
	{
		public List<AcademicSectionVM> GetAcademicSectionList(int organizationId=0,string sectionName="",string sectionCode = "")
		{
			string sql = " select b.SectionId, b.SectionName, b.SectionCode,b.OrganizationId,o.OrganizationName " +
						 " from ACD_Section b " +
						 " left join MS_Organization o on o.OrganizationId = b.OrganizationId";
						 //" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and b.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and b.OrganizationId="+organizationId;

			}
			if (!string.IsNullOrEmpty(sectionName))
			{
				sql += " and b.SectionName like '%" + sectionName + "%'";
			}
			if (!string.IsNullOrEmpty(sectionCode))
			{
				sql += " and b.SectionCode like '%" + sectionCode + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicSectionVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_Section GetAcademicSectionById(int Id)
		{
			string sql = " Select *  from ACD_Section where SectionId=" + Id;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_Section>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}
		public bool InsertAcademicSection(ACD_Section section)
		{
			string sql = " Insert into  ACD_Section (SectionName, SectionCode,OrganizationId, EnteredBy, EnteredDate," +
			" LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
			"(@SectionName, @SectionCode,@OrganizationId, @EnteredBy, @EnteredDate," +
			" 0, null, 0, 0, null)";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, section);
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
		public bool UpdateAcademicSection(ACD_Section section)
		{
			string sql = " Update ACD_Section set SectionName=@SectionName, SectionCode=@SectionCode, " +
			" OrganizationId=@OrganizationId, " +
			" EnteredBy=@EnteredBy, EnteredDate=@EnteredDate,LastUpdatedBy=@LastUpdatedBy, " +
			" LastUpdatedDate=@LastUpdatedDate, IsDeleted=0, DeletedBy=0, DeletedDate=null" +
			" where SectionId=@SectionId";
			using (var db = DbHelper.GetDBConnection())
			{
				using (var trsn = new TransactionScope())
				{
					//db.Execute(sql);
					var lst = db.Execute(sql, section);
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
		public bool DeleteAcademicSection(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update ACD_Section set IsDeleted=1, DeletedBy=" + deletedBy+", DeletedDate='"+deletedDate+ "' where SectionId= " + Id;
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

        public ACD_Section GetSectionCodeName(string fsectionCode, string fsectionName, int isdelete)
        {
            string sql = " select SectionCode, SectionName, IsDeleted from ACD_Section where SectionName='" + fsectionName + "' AND SectionCode='" + fsectionCode + "'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<ACD_Section>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }
    }
}