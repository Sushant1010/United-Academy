namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.MS_Item")]
    public partial class MS_Item
    {
        [Key]
         
        public int ItemId { get; set; }

        [StringLength(100)]
        public string ItemName { get; set; }

        [StringLength(50)]
        public string ItemCode { get; set; }

        [StringLength(2000)]
        public string ItemDescription { get; set; }

        public int? CategoryId { get; set; }

        public int? OrganizationId { get; set; }

        public int? EnteredBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
