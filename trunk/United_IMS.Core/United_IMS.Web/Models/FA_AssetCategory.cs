namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_AssetCategory")]
    public partial class FA_AssetCategory
    {
        [Key]
        [Column(Order = 0)]
        public int AssetCategoryId { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

         
        [Column(Order = 1)]
        [StringLength(50)]
        public string CategoryCode { get; set; }

         
        [Column(Order = 2)]
        public int DepreciationMethodId { get; set; }

         
        [Column(Order = 3)]
        public decimal DepreciationRate { get; set; }

         
        [Column(Order = 4)]
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
