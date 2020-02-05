namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.ACD_ClassSection")]
    public partial class ACD_ClassSection
    {
        [Key]
        public int ClassSectionId { get; set; }

        public int? BatchId { get; set; }

        public int? ClassId { get; set; }

        public int? SectionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [StringLength(50)]
        public string StartDateBS { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
