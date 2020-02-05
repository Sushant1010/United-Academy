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
	public class AcademicClassRepo
	{
		public List<AcademicClassVM> GetAcademicClassList(int organizationId=0,string className="",string classCode="")
		{
			string sql = " select b.ClassId, b.ClassName, b.ClassCode,b.OrganizationId,o.OrganizationName " +
						 " from ACD_Class b " +
						 " left join MS_Organization o on o.OrganizationId = b.OrganizationId";
						 //" left join ACD_Program p on p.ProgramId = b.ProgramId ";
			sql += " where 1=1 and b.IsDeleted=0";	
			if(organizationId!=0)
			{
				sql += " and b.OrganizationId="+organizationId;

			}
			if (!string.IsNullOrEmpty(className))
			{
				sql += " and b.ClassName like '%" + className+"%'";
			}
			if (!string.IsNullOrEmpty(classCode))
			{
				sql += " and b.ClassCode like '%" + classCode + "%'";
			}
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<AcademicClassVM>(sql).ToList();
				db.Close();
				return lst;
			}
		}
		public ACD_Class GetAcademicClassById(int classId)
		{
			string sql = " Select *  from ACD_Class where ClassId="+classId;
			using (var db = DbHelper.GetDBConnection())
			{
				var lst = db.Query<ACD_Class>(sql).SingleOrDefault();
				db.Close();
				return lst;
			}
		}

        //public ACD_Class GetAcademicClassById(int classId)
        //{
        //    string sql = " Select *  from ACD_Class where ClassId=" + classId;
        //    using (var db = DbHelper.GetDBConnection())
        //    {
        //        var lst = db.Query<ACD_Class>(sql).SingleOrDefault();
        //        db.Close();
        //        return lst;
        //    }
        //}

        public bool InsertAcademicClass(ACD_Class aclass)
		{
			string sql = " Insert into  ACD_Class (ClassName, ClassCode,OrganizationId, EnteredBy, EnteredDate," +
			" LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
			" values " +
			"(@ClassName, @ClassCode,@OrganizationId, @EnteredBy, @EnteredDate," +
			" 0, null, 0, 0, null)";
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
		public bool UpdateAcademicClass(ACD_Class aclass)
		{
			string sql = " Update ACD_Class set ClassName=@ClassName, ClassCode=@ClassCode, " +
			" OrganizationId=@OrganizationId, " +
			" EnteredBy=@EnteredBy, EnteredDate=@EnteredDate,LastUpdatedBy=@LastUpdatedBy, " +
			" LastUpdatedDate=@LastUpdatedDate, IsDeleted=0, DeletedBy=0, DeletedDate=null" +
			" where ClassId=@ClassId";
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
		public bool DeleteAcademicClass(int Id,int deletedBy,DateTime deletedDate)
		{
			string sql = " Update ACD_Class set IsDeleted=1, DeletedBy="+deletedBy+", DeletedDate='"+deletedDate+"' where ClassId= "+Id;
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

        public ACD_Class GetClassNameCode(string fclassname, string fclasscode, int isdelete)
        {
            string sql = " select ClassName, ClassCode, IsDeleted from ACD_Class where classname='" + fclassname + "' AND classcode='" + fclasscode + "'";

            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<ACD_Class>(sql).FirstOrDefault();
                db.Close();
                return lst;
            }
        }
    }
}