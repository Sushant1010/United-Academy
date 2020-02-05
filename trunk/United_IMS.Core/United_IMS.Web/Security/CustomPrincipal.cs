using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace United_IMS.Web.Security
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties  

        public int UserId { get; set; }
        public string FullName { get; set; }

		public string Email { get; set; }
		public int RoleId { get; set; }
        public bool IsAdmin { get; set; }
        //public string Email { get; set; }
		//public int CompanyId { get; set; }
        //public int BranchId { get; set; }
        //public int RoleId { get; set; }
        //public DateTime RegistrationDate { get; set; }
        //public string Mobile { get; set; }
        public IIdentity Identity
        {
            get; private set;
        }
        public int GetPersonalId()
        {
            return UserId;
        }
        public bool IsInRole(int role)
        {
            if (RoleId == null)
            {
                return false;
            }
            //else if (UserRole.Contains(role))
            else if(role==RoleId)
            //else if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkrole(string role)
        {
            string[] ab = role.Split(',');
            //foreach (var a in RoleId)
            //{
            //    if (ab.Contains(a))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

		public bool IsInRole(string role)
		{
			throw new NotImplementedException();
		}

		public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
        #endregion
    }
}
