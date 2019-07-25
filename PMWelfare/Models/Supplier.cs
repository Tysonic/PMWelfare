namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            SupProducts = new HashSet<SupProducts>();
        }

        [Key]
        public int SupId { get; set; }

        [StringLength(10)]
        [Display(Name = "Supplier Contact" )]
        public string SupTel { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Supplier Name")]
        public string SupName { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupProducts> SupProducts { get; set; }
        public class supplierlistViewModel
        {


            public string SupplierName { get; set; }
            public string ProductName { get; set; }
            public decimal? ProductPrice { get; set; }

        }
    }
}
