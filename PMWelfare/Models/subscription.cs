namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class subscription
    {
        [Key]
        public int sub_id { get; set; }

        [StringLength(40)]
        public string user_name { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        public byte sub_month { get; set; }

        public int sub_year { get; set; }

        [Required]
        [StringLength(40)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual member member { get; set; }
    }
}
