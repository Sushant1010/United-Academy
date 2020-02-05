using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using United_IMS.Web.Models;
using Dapper;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web.Repository
{
    public class LoginRepo
    {
        public SC_User CheckUser(string organizationid,string email,string password)
        {
            string sql = " select * from SC_User where Email='"+email+"' and Password='"+password+"' and OrganizationId="+organizationid;
            using (var db = DbHelper.GetDBConnection())
            {
                
                    //db.Execute(sql);
                    var lst = db.Query<SC_User>(sql).SingleOrDefault();
                    db.Close();
                return lst;
                
            }
        }
        
            public SessionVM CheckLogin(string Username,  string Password, string returnUrl)
            {
                string query = "Select UserId,Email,0 RoleId,FullName,isnull(IsAdmin,0) IsAdmin from SC_USer" +
                    " where Email='" +Username+"'"+
                    " and Password = '" + Password + "' and " +
                    " IsDeleted = 0 and IsActive = 1 ";

                using (var db = DbHelper.GetDBConnection())
                {
                    return db.Query<SessionVM>(query).SingleOrDefault();
                }

            }
            public SessionVM GetUserByUsername(string Username)
            {
            string query = "Select UserId,Email,0 RoleId,FullName,isnull(IsAdmin,0) IsAdmin from SC_User" +
                " where Email='" + Username + "'" +
                //" and Password = '" + Password + "' and " +
                " and IsDeleted = 0 and IsActive = 1 ";

            using (var db = DbHelper.GetDBConnection())
                {
                    return db.Query<SessionVM>(query).SingleOrDefault();
                }

            }
            //GetUserByUsername
            public SC_User getUser(string empCode)
            {
                string query = "Select * from SC_User where Email = '" + empCode + "'";

                using (var db = DbHelper.GetDBConnection())
                {
                    return db.Query<SC_User>(query).SingleOrDefault();
                }
            }

            public void ChangePassword(SC_User personalDetail)
            {
                string query = "Update SC_User set Password = @Password where  UserId = @UserId";

                using (var db = DbHelper.GetDBConnection())
                {
                    db.Execute(query, personalDetail);
                }
            }

            public SC_User getPersonalDetail(int PersonalId)
            {
                string query = "select * from SC_User"+
                    " where UserId = " + PersonalId;

                using (var db = DbHelper.GetDBConnection())
                {
                    var info = db.Query<SC_User>(query).SingleOrDefault();
                    return info;
                }
            }


            public void editUserDetail(SC_User personalDetail)
            {
            //string query = "Update SC_User set DOB=@DOB, DOBBS=@DOBBS, Gender=@Gender, MaritalStatus=@MaritalStatus, BloodGroupId=@BloodGroupId," +
            //    " PersonalEmail=@PersonalEmail, NationalityId=@NationalityId, PersonalNumber=@PersonalNumber";

            //if (personalDetail.EmployeePhoto != null && personalDetail.EmployeePhoto != "")
            //{
            //    query += ", EmployeePhoto = @EmployeePhoto";
            //}
            //query += " where PersonalId=@PersonalId";

            //using (var db = DBHelper.GetDbConnection())
            //{
            //    db.Execute(query, personalDetail);
            //}
        }

            public string GetName(int id)
            {
                string query = "select FullName from SC_User where UserId = " + id;

                using (var db = DbHelper.GetDBConnection())
                {
                    return db.Query<string>(query).SingleOrDefault();
                }
            }
        }
    }