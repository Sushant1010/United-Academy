namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.INV_StockItem")]
    public partial class INV_StockItem
    {
        [Key]
         
        public int StockItemId { get; set; }

        public int? ItemId { get; set; }

        public int? Quantity { get; set; }

        public int? OrganizationId { get; set; }
    }
}
