namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chat_room
    {
        [Key]
        public int chat_id { get; set; }

        [StringLength(40)]
        public string user_name { get; set; }

        [Required]
        [StringLength(250)]
        public string message { get; set; }

        public DateTime? posted_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual member member { get; set; }
    }
}
