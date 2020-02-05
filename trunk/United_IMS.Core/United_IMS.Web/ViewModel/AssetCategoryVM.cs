using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
    public class AssetCategoryVM
    {
        public int AssetCategoryId { get; set; }

    public string CategoryName { get; set; }

    public string CategoryCode { get; set; }

    public int DepreciationMethodId { get; set; }
    public string MethodName { get; set; }
    public decimal DepreciationRate { get; set; }

    public int OrganizationId { get; set; }
    public string OrganizationName { get; set; }
    }
}