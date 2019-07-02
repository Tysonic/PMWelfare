namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sup_products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sup_products()
        {
            expenses = new HashSet<expens>();
        }

        [Key]
        public int prod_id { get; set; }

        [Required]
        [StringLength(40)]
        public string prod_name { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public int? event_id { get; set; }

        public int? sup_id { get; set; }

        [Required]
        [StringLength(40)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual celebration celebration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expens> expenses { get; set; }

        public virtual supplier supplier { get; set; }
    }
}
