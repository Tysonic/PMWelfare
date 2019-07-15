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

        public class MonthlyExpensesViewModel
        {
            [Key]
            public int celeb_product_id { get; set; }

            [Column(TypeName = "date")]
            public DateTime? expense_date { get; set; }

            [StringLength(20)]
            public string product_name { get; set; }

            public int? quantity { get; set; }

            public int? supplier_product_id { get; set; }

            public int? event_id { get; set; }
            [StringLength(50)]
            public string event_name { get; set; }

            [Column(TypeName = "date")]
            public DateTime? event__date { get; set; }


            [Column(TypeName = "money")]
            public decimal? productprice { get; set; }

            [Column(TypeName = "money")]
            public decimal? TotalPrice { get; set; }



        }
    }
    




    }
