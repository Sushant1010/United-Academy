namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.ACD_Student")]
    public partial class ACD_Student
    {
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }

        [Column(Order = 1)]
        [StringLength(100)]
        public string StudentName { get; set; }

        [StringLength(50)]
        public string StudentCode { get; set; }

        [StringLength(50)]
        public string StudentRegNo { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        public int? OrganizationId { get; set; }

        public int? BatchId { get; set; }

        public int? ClassId { get; set; }

        public int? SectionId { get; set; }

        public int? CurrentClassId { get; set; }

        public int? CurrentSectionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AdmissionYear { get; set; }

        [StringLength(100)]
        public string TemporaryAddress { get; set; }

        [StringLength(100)]
        public string PermanentAddress { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        [StringLength(50)]
        public string DOBBS { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(50)]
        public string FatherContact { get; set; }

        [StringLength(100)]
        public string MotherName { get; set; }

        [StringLength(50)]
        public string MotherContact { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
