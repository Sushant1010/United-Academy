namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_Asset")]
    public partial class FA_Asset
    {
        [Key]
        public int AssetId { get; set; }
        public int AssetItemId { get; set; }
        public int? PurchaseId { get; set; }
        public DateTime? BillDate { get; set; }
        public string BillDateBs { get; set; }
        public string BillNo { get; set; }
        //public string Asset { get; set; }
        public decimal? OpeningValue { get; set; }
        public decimal? PurchaseValue { get; set; }
        [StringLength(50)]
        public string AssetUniqueCode { get; set; }
        [StringLength(50)]
        public string Barcode { get; set; }
        [Column(Order = 4)]
        public int OrganizationId { get; set; }
        public int BranchId { get; set; }
        public int LocationId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public decimal UsefulLife { get; set; }
        public int AccessoryForAssetId { get; set; }
        public bool IsAccessory { get; set; }
        public bool IsDepreciationApplicable { get; set; }
        public DateTime? DepreciationStartDate { get; set; }
        [StringLength(50)]
        public string DepreciationRemarks { get; set; }
        
        public bool? IsSold { get; set; }
        public DateTime? SoldDate { get; set; }
        public decimal? SoldValue { get; set; }
        public bool IsScrap { get; set; }
        public DateTime? ScrapDate { get; set; }
        public decimal? ScrapRealizedValue { get; set; }
        public int? EnteredBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EnteredDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; } 
            public string CategoryId { get; set; }
        


    }
}
