namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.INV_SoldItem")]
    public partial class INV_SoldItem
    {
        [Key]
         
        public int SoldItemId { get; set; }

        public int? SoldBillId { get; set; }

        public int? ItemId { get; set; }

        public int? UnitId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Total { get; set; }

        public int? OrganizationId { get; set; }

        public int? EnteredBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
