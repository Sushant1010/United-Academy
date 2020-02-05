using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
	public class InventoryPurchaseVM
	{
        public int PurchaseId { get; set; }

        public int? VendorId { get; set; }
        public string VendorName { get; set; }

        public string BillNo { get; set; }

        public DateTime? BillDate { get; set; }

        public string BillDateBS { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool? VatApplicable { get; set; }
        public bool? IsPreviousStock { get; set; }
        public int? VATPercent { get; set; }
        public decimal? VatAmount { get; set; }

        public decimal? ShippingCharge { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? TotalWithVat { get; set; }

        public int? OrganizationId { get; set; }
        public bool IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string VerifiedByName { get; set; }

    }
	public class InventoryPurchaseItem
	{
        public int PurchaseItemId { get; set; }

        public int? PurchaseId { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ItemId { get; set; }
        public string ItemName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public int? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Total { get; set; }

        public int? ReturnedUnitId { get; set; }

        public int? ReturnedQuantity { get; set; }

        public bool? ReturnedVerified { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public int? ReturnedBy { get; set; }
        public string ReturnedByName { get; set; }
        public string ReturnRemarks { get; set; }

        public bool? IsVerified { get; set; }

        public int? VerifiedBy { get; set; }
        public string VerifiedByName { get; set; }
        //public DateTime? VerifiedDate { get; set; }

    }
    public class InventoryPurchaseDetail
	{
		public InventoryPurchaseVM InventoryPurchase { get; set; }
		public List<InventoryPurchaseItem> InventoryPurchaseItems { get; set; }
	}
}