using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using United_IMS.Web.Models;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web.Repository
{
    public class KUnitRepo
    {
        public List<KUnitVM> GetKUnitList(int organizationId = 0, string unitName = "", string unitCode = "")
        {
            string sql = " select b.UnitId, b.UnitName, b.UnitCode,b.OrganizationId,o.OrganizationName " +
                         " from KI_Unit b " +
                         " left join MS_Organization o on o.OrganizationId = b.OrganizationId";
            //" left join ACD_Program p on p.ProgramId = b.ProgramId ";
            sql += " where 1=1 and b.IsDeleted=0";
            if (organizationId != 0)
            {
                sql += " and b.OrganizationId=" + organizationId;

            }
            if (!string.IsNullOrEmpty(unitName))
            {
                sql += " and b.UnitName like '%" + unitName + "%'";
            }
            if (!string.IsNullOrEmpty(unitCode))
            {
                sql += " and b.UnitCode like '%" + unitCode + "%'";
            }
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<KUnitVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }
        public K_Unit GetKUnitById(int Id)
        {
            string sql = " Select *  from KI_Unit where UnitId=" + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<K_Unit>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public bool InsertKUnit(K_Unit unit)
        {
            string sql = " Insert into  KI_Unit (UnitName, UnitCode,OrganizationId, EnteredBy, EnteredDate," +
            " LastUpdatedBy, LastUpdatedDate, IsDeleted, DeletedBy, DeletedDate) " +
            " values " +
            "(@UnitName, @UnitCode,@OrganizationId, @EnteredBy, @EnteredDate," +
            " 0, null, 0, 0, null)";
            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Execute(sql);
                    var lst = db.Execute(sql, unit);
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
        public bool UpdateKUnit(K_Unit unit)
        {
            string sql = " Update KI_Unit set UnitName=@UnitName, UnitCode=@UnitCode, " +
            " OrganizationId=@OrganizationId, " +
            " EnteredBy=@EnteredBy, EnteredDate=@EnteredDate,LastUpdatedBy=@LastUpdatedBy, " +
            " LastUpdatedDate=@LastUpdatedDate, IsDeleted=0, DeletedBy=0, DeletedDate=null" +
            " where UnitId=@UnitId";
            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Execute(sql);
                    var lst = db.Execute(sql, unit);
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
        public bool DeleteKUnit(int Id, int deletedBy, DateTime deletedDate)
        {
            string sql = " Update KI_Unit set IsDeleted=1, DeletedBy=" + deletedBy + ", DeletedDate='" + deletedDate + "' where UnitId= " + Id;
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
