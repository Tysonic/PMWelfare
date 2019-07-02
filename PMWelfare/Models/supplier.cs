namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public supplier()
        {
            sup_products = new HashSet<sup_products>();
        }

        [Key]
        public int sup_id { get; set; }

        [StringLength(10)]
        public string sup_tel { get; set; }

        [Required]
        [StringLength(40)]
        public string sup_name { get; set; }

        [Required]
        [StringLength(40)]
        public string email { get; set; }

        [Required]
        [StringLength(40)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sup_products> sup_products { get; set; }
    }
}
