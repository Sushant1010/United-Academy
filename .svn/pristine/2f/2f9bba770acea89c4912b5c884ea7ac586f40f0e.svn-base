namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_Maintenance")]
    public partial class FA_Maintenance
    {
        [Key]
        [Column(Order = 0)]
         
        public int MaintenanceId { get; set; }

         
        [Column(Order = 1)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 2)]
         
        public int AssetId { get; set; }

         
        [Column(Order = 3)]
        public DateTime MaintenanceDate { get; set; }

         
        [Column(Order = 4)]
        [StringLength(10)]
        public string MaintenanceDateBS { get; set; }

         
        [Column(Order = 5)]
        public DateTime ReturnedDate { get; set; }

        [StringLength(10)]
        public string ReturnedDateBS { get; set; }

         
        [Column(Order = 6)]
        public decimal MaintenancePrice { get; set; }

        [StringLength(200)]
        public string MaintenanceReason { get; set; }

        public int? VendorId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
