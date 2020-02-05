using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
	public class InventorySoldVM
	{
        public int SoldBillId { get; set; }
        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string BillDateBS { get; set; }
        public string GroupNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool? IsStaff { get; set; }
        public int? StudentId { get; set; }
        public int? BatchId { get; set; }
        public string BatchName { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int? StaffId { get; set; }
        public string StudentName { get; set; }
        public string RegNo { get; set; }
        public string StaffName { get; set; }
        public string StaffNo { get; set; }
        public int StudentRegNo { get; set; }

        public DateTime? EnteredDate { get; set; }

        public int Quantity { get; set; }

        public string ItemName { get; set; }
        public string ItemCode { get; set; }

    }
	public class InventorySoldItem
	{
        public int SoldItemId { get; set; }

        public int? SoldBillId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ItemId { get; set; }
        public string ItemName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Total { get; set; }

        
        //public DateTime? VerifiedDate { get; set; }

    }
    public class InventorySoldDetail
	{
		public InventorySoldVM InventorySold{ get; set; }
		public List<InventorySoldItem> InventorySoldItems { get; set; }
	}
}