namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.INV_PurchaseBill")]
    public partial class INV_PurchaseBill
    {

        [Key]
        public int PurchaseId { get; set; }

        public int? VendorId { get; set; }

        [StringLength(50)]
        public string BillNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BillDate { get; set; }

        [StringLength(10)]
        public string BillDateBS { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool? VatApplicable { get; set; }

        public int? VATPercent { get; set; }
        public decimal? VatAmount { get; set; }

        public decimal? ShippingCharge { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TotalWithVat { get; set; }

        public int? OrganizationId { get; set; }

        public int? EnteredBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnteredDate { get; set; }

        public bool? IsVerified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsPreviousStock { get; set; }

        public int BillSerialNo {get; set;}

        public string Remarks { get; set; }
    }
}
