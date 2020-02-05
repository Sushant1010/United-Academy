using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace United_IMS.Web.ViewModel
{
	public class AcademicStudentVM
	{
       
		public int StudentId { get; set; }
		public string StudentName { get; set; }

		public string StudentCode { get; set; }

		public string StudentRegNo { get; set; }

		public string Gender { get; set; }
		public int? OrganizationId { get; set; }
		public string OrganizationName { get; set; }
		public int? BatchId { get; set; }
		public string BatchName { get; set; }
		public int? ClassId { get; set; }
		public string ClassName { get; set; }
		public int? SectionId { get; set; }
		public string SectionName { get; set; }
		public int? CurrentClassId { get; set; }
		public string CurrentClassName { get; set; }
		public int? CurrentSectionId { get; set; }
		public string CurrentSectionName { get; set; }
		public DateTime? AdmissionYear { get; set; }

		public string TemporaryAddress { get; set; }

		public string PermanentAddress { get; set; }

		public string Phone { get; set; }

		public DateTime? DOB { get; set; }

		public string DOBBS { get; set; }

		public string FatherName { get; set; }

		public string FatherContact { get; set; }

		public string MotherName { get; set; }

		public string MotherContact { get; set; }

        public DateTime? EnteredDate { get; set; }
    }
}