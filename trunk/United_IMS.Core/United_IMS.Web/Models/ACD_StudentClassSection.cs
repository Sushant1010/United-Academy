namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.ACD_StudentClassSection")]
    public partial class ACD_StudentClassSection
    {
        [Key]
        [Column(Order = 0)]
        public int StudentClassSectionId { get; set; }

         
        [Column(Order = 1)]
        public int StudentId { get; set; }

         
        [Column(Order = 2)]
        public int ClassId { get; set; }

        public int? SectionId { get; set; }

        [StringLength(50)]
        public string AcademicYear { get; set; }

         
        [Column(Order = 3, TypeName = "date")]
        public DateTime AssignedDate { get; set; }

        [StringLength(50)]
        public string AssignedDateBS { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }
    }
}
