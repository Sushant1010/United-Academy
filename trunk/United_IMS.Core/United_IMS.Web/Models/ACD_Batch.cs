namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACD_Batch
    {
        [Key]
        [Column(Order = 0)]
        public int BatchId { get; set; }

    
        [Column(Order = 1)]
        [StringLength(50)]
        public string BatchName { get; set; }

        [StringLength(50)]
        public string BatchCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDateAD { get; set; }

        [StringLength(10)]
        public string StartDateBS { get; set; }

        public int? OrganizationId { get; set; }

        public int? ProgramId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdateBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
