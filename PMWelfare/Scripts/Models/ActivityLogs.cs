namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActivityLogs
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceIp { get; set; }

        [Required]
        [StringLength(40)]
        public string DeviceMac { get; set; }

        public DateTime? AcitivityTime { get; set; }

        public string Activity { get; set; }

        public virtual Member Member { get; set; }
    }
}
