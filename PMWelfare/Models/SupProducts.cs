namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SupProducts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupProducts()
        {
            Expenses = new HashSet<Expense>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int? EventId { get; set; }

        public int? SupId { get; set; }
        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Celebration Celebration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
