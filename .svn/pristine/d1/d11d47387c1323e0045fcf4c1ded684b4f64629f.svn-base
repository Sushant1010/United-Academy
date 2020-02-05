namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.SC_User")]
    public partial class SC_User
    {
        [Key]
        [Column(Order = 0)]
         
        public int UserId { get; set; }

         
        [Column(Order = 1)]
        [StringLength(100)]
        public string FullName { get; set; }

         
        [Column(Order = 2)]
        [StringLength(50)]
        public string Email { get; set; }

         
        [Column(Order = 3)]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string ActivationCode { get; set; }

        public bool? IsActive { get; set; }

         
        [Column(Order = 4)]
        public bool IsDeleted { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? OrganizationId { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? InventoryModuleAllow { get; set; }

        public bool? FixedAssetModuleAllow { get; set; }

        public bool? Organization1Allow { get; set; }

        public int? OrganizationId1 { get; set; }

        public bool? Organization2Allow { get; set; }

        public int? OrganizationId2 { get; set; }

        public bool? OrganizationId2Allow { get; set; }

        public int? OrganizationId3 { get; set; }
    }
}
