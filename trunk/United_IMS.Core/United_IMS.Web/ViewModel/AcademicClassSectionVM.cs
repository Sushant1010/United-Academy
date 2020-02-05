using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
	public class AcademicClassSectionVM
	{
		public int ClassSectionId { get; set; }
		public int? BatchId { get; set; }
		public string BatchName { get; set; }
		public int? ClassId { get; set; }
		public string ClassName { get; set; }
		public int? SectionId { get; set; }
		public string SectionName { get; set; }
		public DateTime? StartDate { get; set; }

		public string StartDateBS { get; set; }
	}
}