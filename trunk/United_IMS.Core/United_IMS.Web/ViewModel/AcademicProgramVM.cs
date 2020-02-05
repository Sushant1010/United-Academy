using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
	public class AcademicProgramVM
	{
		public int ProgramId { get; set; }

		public string ProgramName { get; set; }

		public string ProgramCode { get; set; }

		public int? OrganizationId { get; set; }
		public string OrganizationName { get; set; }
		public DateTime? StartedDate { get; set; }

		public string StartedDateBS { get; set; }
	}
}