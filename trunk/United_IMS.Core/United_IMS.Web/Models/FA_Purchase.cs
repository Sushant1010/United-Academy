namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;

    [Table("united_ims.FA_Purchase")]
    public partial class FA_Purchase
    {
        [Key]
        [Column(Order = 0)]
         
        public int PurchaseId { get; set; }

         
        [Column(Order = 1)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 2)]
         
        public int VendorId { get; set; }

         
        [Column(Order = 3)]
        [StringLength(50)]
        public string BillNo { get; set; }

         
        [Column(Order = 4, TypeName = "date")]
        public DateTime BillDate { get; set; }

         
        [Column(Order = 5)]
        [StringLength(10)]
        public string BillDateBS { get; set; }

         
        [Column(Order = 6)]
        public decimal TotalAmount { get; set; }

         
        [Column(Order = 7)]
        public decimal VatAmount { get; set; }

        public bool? IsVerified { get; set; }

        public int? VerifiedBy { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public int? EnteredBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? VATPercent { get; set; }
       // public decimal? GrossTotal { get; set; }
        public decimal? TotalWithVat { get; set; }

        public bool? VatApplicable { get; set; }
        public decimal? DiscountAmount { get; set; }

        public int? BillSerialNo { get; set; }
    }
}
