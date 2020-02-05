namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.ACD_Staff")]
    public partial class ACD_Staff
    {
        [Key]
        [Column(Order = 0)]
        public int StaffId { get; set; }

        [Column(Order = 1)]
        [StringLength(100)]
        public string StaffName { get; set; }

        [StringLength(50)]
        public string StaffCode { get; set; }

        public int? OrganizationId { get; set; }

        [StringLength(100)]
        public string TemporaryAddress { get; set; }

        [StringLength(100)]
        public string PermanentAddress { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string CitizenshipNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [StringLength(50)]
        public string JoinDateBS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        [StringLength(50)]
        public string DOBBS { get; set; }

        [StringLength(50)]
        public string Photo { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
