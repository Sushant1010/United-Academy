namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.INV_SoldBill")]
    public partial class INV_SoldBill
    {
        [Key]
         
        public int SoldBillId { get; set; }

        public int? OrganizationId { get; set; }

        public int? ClassId { get; set; }

        public int? SectionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BillDate { get; set; }

        [StringLength(50)]
        public string BillDateBS { get; set; }

        [StringLength(50)]
        public string BillNo { get; set; }

        [StringLength(50)]
        public string GroupNo { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool? IsStaff { get; set; }

        public int? StudentId { get; set; }

        public int? StaffId { get; set; }

        public int? EnteredBy { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
