namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_AssetDepreciation")]
    public partial class FA_AssetDepreciation
    {
        [Key]
        [Column(Order = 0)]
        public Guid AssetDepreciationId { get; set; }

         
        [Column(Order = 1)]
        public int AssetId { get; set; }

         
        [Column(Order = 2)]
        public decimal OpeningValue { get; set; }

         
        [Column(Order = 3)]
        public int DepreciationMethod { get; set; }

         
        [Column(Order = 4)]
        public decimal DepreciatingValue { get; set; }

         
        [Column(Order = 5)]
        public decimal NetValue { get; set; }

         
        [Column(Order = 6)]
        public DateTime DepreciationDate { get; set; }

        [StringLength(10)]
        public string DepreciationDateBS { get; set; }

        public int? DepreciationBy { get; set; }

         
        [Column(Order = 7)]
        public int OrganizationId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
