namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.SC_UserRole")]
    public partial class SC_UserRole
    {
        [Key]
        [Column(Order = 0)]
         
        public int UserRoleId { get; set; }

         
        [Column(Order = 1)]
         
        public int RoleId { get; set; }

         
        [Column(Order = 2)]
         
        public int UserId { get; set; }

         
        [Column(Order = 3, TypeName = "date")]
        public DateTime AssignedDate { get; set; }

        public int? AssignedBy { get; set; }

         
        [Column(Order = 4)]
        public bool IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
