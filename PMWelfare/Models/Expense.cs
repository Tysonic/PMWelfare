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
        [Display(Name = "Date of Expenditure")]
        public DateTime ExpenseDate { get; set; }

        [Display(Name = "Product Name")]
        public int? ProductId { get; set; }

        
        public int Quantity { get; set; }

        
        [StringLength(40)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created at")]
        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual SupProducts SupProducts { get; set; }

        public class MonthlyExpensesViewModel
        {
            
            [Column(TypeName = "date")]
            public DateTime? ExpenseDate { get; set; }

            [StringLength(20)]
            public string ProductName { get; set; }
            [StringLength(20)]
            public string SupplierName { get; set; }

            public int? Quantity { get; set; }
            
            [StringLength(50)]
            public string EventName { get; set; }

            [Column(TypeName = "date")]
            public DateTime? EventDate { get; set; }


            [Column(TypeName = "money")]
            public decimal? UnitPrice { get; set; }

            [Column(TypeName = "money")]
            public decimal? TotalPrice { get; set; }



        }
    }
    }
