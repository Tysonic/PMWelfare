namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("expenses")]
    public partial class expens
    {
        [Key]
        public int exp_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime exp_date { get; set; }

        public int? prod_id { get; set; }

        public int quantity { get; set; }

        [Required]
        [StringLength(40)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual sup_products sup_products { get; set; }
    }
}
