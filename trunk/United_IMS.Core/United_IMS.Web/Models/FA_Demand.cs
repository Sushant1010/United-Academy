namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_Demand")]
    public partial class FA_Demand
    {
        [Key]
        [Column(Order = 0)]
         
        public int DemandId { get; set; }

         
        [Column(Order = 1)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 2)]
         
        public int DemandedBy { get; set; }

         
        [Column(Order = 3, TypeName = "date")]
        public DateTime DemandDate { get; set; }

        [StringLength(50)]
        public string DemandDateBS { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public int? ApprovedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ApprovedDate { get; set; }

        [StringLength(20)]
        public string IsApproved { get; set; }

        [StringLength(200)]
        public string ApproveRemarks { get; set; }
    }
}
