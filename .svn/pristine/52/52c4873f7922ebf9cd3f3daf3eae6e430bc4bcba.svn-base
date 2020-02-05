using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
    public class AssetPurchaseVM
    {
        public int PurchaseId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string BillDateBS { get; set; }
        public decimal TotalAmount { get; set; }


        public bool? VatApplicable { get; set; }
        public bool? IsPreviousStock { get; set; }
        public int? VATPercent { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TotalWithVat { get; set; }

        //public decimal VatAmount { get; set; }
        //public decimal GrossTotal { get; set; }
        public string IsVerified { get; set; }
        public int VerifiedBy { get; set; }
        public string VerifiedByName { get; set; }
        public DateTime VerifiedDate { get; set; }

        public int? BillSerialNo { get; set; }

    }
    public class AssetPurchaseItem
    {
        public int PurchaseItemId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public int AssetCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int PurchaseId { get; set; }
        public int AssetItemId { get; set; }
        public string AssetName { get; set; }
        public int PurchaseQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public bool RegisterToAsset { get; set; }
        public int? ReturnedQuantity { get; set; }
        public int? ReturnedBy { get; set; }
        public string ReturnedByName { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string IsVerified { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerifiedByName { get; set; }
        public DateTime? VerifiedDate { get; set; }

    }
    public class AssetPurchaseDetail
    {
        public AssetPurchaseVM AssetPurchase { get; set; }
        public List<AssetPurchaseItem> AssetPurchaseItems { get; set; }
    }
}