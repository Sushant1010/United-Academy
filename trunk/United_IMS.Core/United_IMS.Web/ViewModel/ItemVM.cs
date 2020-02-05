using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
	public class ItemVM
	{
		public int ItemId { get; set; }

		public string ItemName { get; set; }

		public string ItemCode { get; set; }

		public string ItemDescription { get; set; }

		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int? OrganizationId { get; set; }
		public string OrganizationName { get; set; }
	}
}