namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class celebration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public celebration()
        {
            sup_products = new HashSet<sup_products>();
        }

        [Key]
        public int event_id { get; set; }

        [Required]
        [StringLength(40)]
        public string event_name { get; set; }

        public DateTime event_date { get; set; }

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
