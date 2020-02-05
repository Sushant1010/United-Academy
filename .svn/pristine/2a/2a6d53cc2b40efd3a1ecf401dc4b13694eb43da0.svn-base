namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.SC_Role")]
    public partial class SC_Role
    {
        [Key]
        [Column(Order = 0)]
         
        public int RoleId { get; set; }

         
        [Column(Order = 1)]
        [StringLength(50)]
        public string RoleName { get; set; }

         
        [Column(Order = 2)]
        [StringLength(50)]
        public string ModuleIds { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
