namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_DemandList")]
    public partial class FA_DemandList
    {
        [Key]
        [Column(Order = 0)]
         
        public int DemandListId { get; set; }

         
        [Column(Order = 1)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 2)]
         
        public int DemandId { get; set; }

         
        [Column(Order = 3)]
         
        public int AssetId { get; set; }

         
        [Column(Order = 4)]
         
        public int DemandQuantity { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public int? ApprovedQuantity { get; set; }

        [StringLength(20)]
        public string IsApproved { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? EnteredBy { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
