namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_Location")]
    public partial class FA_Location
    {
        [Key]
        [Column(Order = 0)]
         
        public int LocationId { get; set; }

         
        [Column(Order = 1)]
        [StringLength(100)]
        public string LocationName { get; set; }

         
        [Column(Order = 2)]
         
        public int OrganizationId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string LocationCode { get; set; }
    }
}
