namespace United_IMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("united_ims.SC_Module")]
    public partial class SC_Module
    {
        [Key]
        [Column(Order = 0)]
         
        public int ModuleId { get; set; }

        [Column(Order = 1)]
        [StringLength(50)]
        public string ModuleName { get; set; }
    }
}
