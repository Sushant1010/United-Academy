namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_AssetItem")]
    public partial class FA_AssetItem
    {
        [Key]
        [Column(Order = 0)]
        public int AssetItemId { get; set; }

         
        [Column(Order = 1)]
         
        public int AssetCategoryId { get; set; }

         
        [Column(Order = 2)]
        [StringLength(50)]
        public string AssetName { get; set; }

         
        [Column(Order = 3)]
        [StringLength(50)]
        public string AssetCode { get; set; }

         
        [Column(Order = 4)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 5)]
        public decimal LifeSpan { get; set; }

         
        [Column(Order = 6)]
        public bool IsDepreciation { get; set; }

        
        [Column(Order = 7)]
         
        public int EnteredBy { get; set; }

        [Column(Order = 8)]
        public DateTime EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public int WarrentyDuration { get; set; }
        public DateTime? WarrentyIndate { get; set; }
        [Column(TypeName ="date")]
        public DateTime? FromDate { get; set; }
        [StringLength(10)]
        public string FromDateBS { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
        [StringLength(10)]
        public string ToDateBS { get; set; }

        public bool? IsWarranty { get; set; }
    }
}
