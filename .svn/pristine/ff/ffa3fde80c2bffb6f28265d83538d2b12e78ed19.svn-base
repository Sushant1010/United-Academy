namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_Branch")]
    public partial class FA_Branch
    {
        [Key]
        [Column(Order = 0)]
         
        public int BranchId { get; set; }

         
        [Column(Order = 1)]
        [StringLength(100)]
        public string BranchName { get; set; }

        [StringLength(50)]
        public string BranchCode { get; set; }


        public int? OrganizationId { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

      
    }
}
