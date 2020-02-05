using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties  
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool  IsAdmin{ get; set; }
        public int RoleId { get; set; }
        //public string Branch { get; set; }
        //public int CompanyId { get; set; }
        //public int BranchId { get; set; }
        //public int RoleId { get; set; }
        //public string Emails { get; set; }
        //public string Mobile { get; set; }
        #endregion

        public CustomMembershipUser(SessionVM user):base("CustomMembership", user.Email, user.UserId, string.Empty, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)  
        {
            UserId = user.UserId;
            FullName = user.FullName;
            Email = user.Email;
            RoleId = user.RoleId;
            IsAdmin = user.IsAdmin;
            

        }
    }
}