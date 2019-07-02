namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public string admin_title { get; set; }

        [StringLength(40)]
        public string created_by { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual member member { get; set; }
    }
}
