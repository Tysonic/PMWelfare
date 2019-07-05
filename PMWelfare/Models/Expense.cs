namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("expenses")]
    public partial class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpenseDate { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual SupProducts sup_products { get; set; }

        public class ReportViewModel
        {
            public decimal price { get; internal set; }

            [Key]
            public int exp_id { get; set; }

            [Column(TypeName = "date")]
            public DateTime exp_date { get; set; }

            public int? prod_id { get; set; }

            public int quantity { get; set; }

            public int event_id { get; set; }

            [Required]
            [StringLength(40)]
            public string event_name { get; set; }

            public DateTime event_date { get; set; }



            [Required]
            [StringLength(40)]
            public string prod_name { get; set; }



            public int? sup_id { get; set; }
        }
    }
    




    }
