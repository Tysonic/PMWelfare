namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Deposit
    {
        [Key]
        public int dep_id { get; set; }

        [StringLength(40)]
        public string user_name { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }
        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Member member { get; set; }
    }
}
