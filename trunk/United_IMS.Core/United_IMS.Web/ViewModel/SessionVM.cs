using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
    public class SessionVM
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
       // public int OrganizationId { get; set; }
        //public string RoleName { get; set; }
        //public int ActualRoleId { get; set; }
        //public int CompanyId { get; set; }
        //public string IsManager { get; set; }
        //public string Company { get; set; }
    }
}