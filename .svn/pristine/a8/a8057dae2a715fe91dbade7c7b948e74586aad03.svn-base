namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.SC_LoginHistory")]
    public partial class SC_LoginHistory
    {
        [Key]
        [Column(Order = 0)]
        public Guid LoginId { get; set; }

         
        [Column(Order = 1)]
         
        public int UserId { get; set; }

         
        [Column(Order = 2)]
        public DateTime LoginDate { get; set; }

         
        [Column(Order = 3)]
         
        public int RoleId { get; set; }

         
        [Column(Order = 4)]
         
        public int OrganizationId { get; set; }

         
        [Column(Order = 5)]
         
        public int ActualOrganizationId { get; set; }

         
        [Column(Order = 6)]
        public DateTime LogOutDate { get; set; }
    }
}
