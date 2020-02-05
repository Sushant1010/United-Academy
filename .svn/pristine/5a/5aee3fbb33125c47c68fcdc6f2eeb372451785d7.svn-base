namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.FA_AssetTransfer")]
    public partial class FA_AssetTransfer
    {
        [Key]
        [Column(Order = 0)]
         
        public int AssetTransferId { get; set; }

         
        [Column(Order = 1)]
         
        public int AssetId { get; set; }

         
        [Column(Order = 2)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 3)]
         
        public int BranchId { get; set; }

         
        [Column(Order = 4)]
         
        public int LocationId { get; set; }

         
        [Column(Order = 5)]
        public DateTime TransferDate { get; set; }

        [StringLength(10)]
        public string TransferDateBS { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }

        public int? AssignedTo { get; set; }

        public DateTime? AssignedDate { get; set; }

        public bool? IsReceived { get; set; }

        public int? ReceivedBy { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int TransferId { get; set; }

        public int IsTransfered { get; set; }

        public int CategoryId { get; set; }
    }
}
