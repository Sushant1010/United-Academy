namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_PurchaseItem")]
    public partial class FA_PurchaseItem
    {
        [Key]
        [Column(Order = 0)]
         
        public int PurchaseItemId { get; set; }

         
        [Column(Order = 1)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 2)]
         
        public int PurchaseId { get; set; }

         
        [Column(Order = 3)]
         
        public int AssetItemId { get; set; }

         
        [Column(Order = 4)]
         
        public int PurchaseQuantity { get; set; }

         
        [Column(Order = 5)]
        public decimal Rate { get; set; }

         
        [Column(Order = 6)]
        public decimal Total { get; set; }

         
        [Column(Order = 7)]
        public bool RegisterToAsset { get; set; }
        public bool? IsReturned { get; set; }
        public int? ReturnedQuantity { get; set; }

        public int? ReturnedBy { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public bool? IsVerified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
