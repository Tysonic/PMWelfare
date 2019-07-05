namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subscription
    {
        [Key]
        public int SubId { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public byte SubMonth { get; set; }

        public int SubYear { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
