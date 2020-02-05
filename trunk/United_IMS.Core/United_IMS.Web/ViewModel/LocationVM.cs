using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
    public class LocationVM
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string LocationCode { get; set; }
    }
}