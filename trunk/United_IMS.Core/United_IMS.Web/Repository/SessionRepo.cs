using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.Repository;
using United_IMS.Web.Models;
using Dapper;
namespace United_IMS.Web.Repository
{
    public class SessionRepo
    {
        public void AddSession(SC_LoginHistory mS_Country)
        {
            string query = "Insert into SC_LoginHistory (LoginId, UserId, LoginDate, RoleId,OrganizationId,ActualOrganizationId,LogOutDate)" +
                " values (NEWID(), @UserId, @LoginDate, @RoleId, @OrganizationId,@ActualOrganizationId,@LogOutDate)";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Execute(query, mS_Country);
            }
        }

        public void EditSession(SC_LoginHistory sessionobj)
        {
            string query = "Update SC_LoginHistory set LogOutDate = @LogOutDate,OrganizationId=@OrganizationId where LoginId = @LoginId";

            using (var db = DbHelper.GetDBConnection())
            {
                db.Execute(query, sessionobj);
            }

        }



        public SC_LoginHistory GetSessionById(int id)
        {
            string query = "Select top 1 * from SC_LoginHistory where UserId = " + id + " Order by LoginDate Desc";

            using (var db = DbHelper.GetDBConnection())
            {
                return db.Query<SC_LoginHistory>(query).SingleOrDefault();
            }
        }
    }
}