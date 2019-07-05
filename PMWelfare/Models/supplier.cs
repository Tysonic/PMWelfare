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
        public string SupTel { get; set; }

        [Required]
        [StringLength(40)]
        public string SupName { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupProducts> SupProducts { get; set; }
    }
}
