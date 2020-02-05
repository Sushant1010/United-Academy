namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.INV_PurchaseItem")]
    public partial class INV_PurchaseItem
    {
        [Key]
         
        public int PurchaseItemId { get; set; }

        public int? PurchaseId { get; set; }

        public int? ItemId { get; set; }

        public int? UnitId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Total { get; set; }

        public int? ReturnedUnitId { get; set; }

        public int? ReturnedQuantity { get; set; }

        public bool? ReturnedVerified { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public int? ReturnedBy { get; set; }

        [StringLength(200)]
        public string ReturnRemarks { get; set; }

        public bool? IsVerified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? OrganizationId { get; set; }

        public bool? IsReturned { get; set; }
    }
}
