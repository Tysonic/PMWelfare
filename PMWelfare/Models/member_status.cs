namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class member_status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public member_status()
        {
            members = new HashSet<member>();
        }

        public int id { get; set; }

        [Column("member_status")]
        [Required]
        [StringLength(20)]
        public string member_status1 { get; set; }

        [StringLength(40)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<member> members { get; set; }
    }
}
