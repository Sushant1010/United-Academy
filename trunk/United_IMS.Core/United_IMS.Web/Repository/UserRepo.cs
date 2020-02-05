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
    public class UserRepo
    {
        public List<UserVM> GetUserList(int orgid = 0)
        {
            string sql = "select us.UserId,us.FullName,us.Email,us.OrganizationId,o.OrganizationName,us.IsAdmin,us.IsActive" +
                " from SC_User us" +
                " left join MS_Organization o on o.OrganizationId = us.OrganizationId" ;
            sql += " where 1=1 and us.IsDeleted=0";
            if (orgid != 0)
                sql += " and us.OrganizationId=" + orgid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<UserVM>(sql).ToList();
                db.Close();
                return lst;
            }
        }

   
            public UserVM GetUserDetail(int userid = 0)
        {
            string sql = "select us.UserId,us.FullName,us.Email,us.OrganizationId,o.OrganizationName,us.IsAdmin,us.IsActive" +
                " from SC_User us" +
                " left join MS_Organization o on o.OrganizationId = us.OrganizationId";
            sql += " where 1=1 and us.IsDeleted=0";
            //if (orgid != 0)
            //    sql += " and ac.OrganizationId=" + orgid;
            sql += " and us.UserId=" + userid;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<UserVM>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }

        internal object GetUserList(object id)
        {
            throw new NotImplementedException();
        }

        public SC_User GetUserById(int Id)
        {
            string sql = " Select *  from SC_User where UserId=" + Id;
            using (var db = DbHelper.GetDBConnection())
            {
                var lst = db.Query<SC_User>(sql).SingleOrDefault();
                db.Close();
                return lst;
            }
        }
        public bool InsertUser(SC_User user)
        {
            string sql = " Insert into  SC_User (FullName,Email,Password,IsDeleted,OrganizationId,IsAdmin,IsActive," +
    " EnteredBy,EnteredDate,LastUpdatedBy,LastUpdatedDate) " +
            " values " +
            "( @FullName,@Email,@Password,@IsDeleted,@OrganizationId,@IsAdmin,@IsActive," +
            "@EnteredBy,@EnteredDate,0,null)";
            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Execute(sql);
                    var lst = db.Execute(sql, user);
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
        public bool UpdateUser(SC_User user)
        {
            string sql = " Update SC_User set  FullName=@FullName,Email=@Email, Password=@Password," +
                " OrganizationId=@OrganizationId,IsAdmin=@IsAdmin,IsActive=@IsActive, " +
                " LastUpdatedBy=@LastUpdatedBy,LastUpdatedDate=@LastUpdatedDate " +
                 " where UserId= @UserId";
            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    //db.Execute(sql);
                    var lst = db.Execute(sql, user);
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
        
        public bool ChangePassword(SC_User user)
        {
            string sql = "Update Sc_User set Password=@Password"+
                " where UserId=@UserId";
            using (var db = DbHelper.GetDBConnection())
            {
                using (var trsn = new TransactionScope())
                {
                    var lst = db.Execute(sql, user);
                    trsn.Complete();
                    db.Close();
                    if(lst>0)
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

        public bool DeleteUser(int Id, int deletedBy, DateTime deletedDate)
        {
            string sql = " Update SC_User set IsDeleted=1 where UserId= " + Id;
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