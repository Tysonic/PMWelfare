namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class activity_logs
    {
        public int id { get; set; }

        [StringLength(40)]
        public string user_name { get; set; }

        [Required]
        [StringLength(20)]
        public string device_ip { get; set; }

        [Required]
        [StringLength(40)]
        public string device_mac { get; set; }

        public DateTime? acitivity_time { get; set; }

        public string activity { get; set; }

        public virtual member member { get; set; }
    }
}
