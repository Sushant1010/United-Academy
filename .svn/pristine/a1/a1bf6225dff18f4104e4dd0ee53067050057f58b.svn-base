namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACD_Program
    {
        [Key]
        [Column(Order = 0)]
        public int ProgramId { get; set; }

        
        [Column(Order = 1)]
        [StringLength(50)]
        public string ProgramName { get; set; }

        [StringLength(50)]
        public string ProgramCode { get; set; }

        public int? OrganizationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartedDate { get; set; }

        [StringLength(50)]
        public string StartedDateBS { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
