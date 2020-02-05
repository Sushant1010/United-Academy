using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace United_IMS.Web.ViewModel
{
    public class UserVM
    {

        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string Password { get; set; }

        public string ActivationCode { get; set; }

        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
    }
}