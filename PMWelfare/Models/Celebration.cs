namespace PMWelfare.Models
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Celebration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Celebration()
        {
            Celebrants = new HashSet<Celebrant>();
            SupProducts = new HashSet<SupProducts>();
        }

        [Key]
        public int EventId { get; set; }

        public int EventTypeId { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        [DefaultValue(typeof(DateTime), "")]
        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupProducts> SupProducts { get; set; }

        public virtual ICollection<Celebrant> Celebrants { get; set; }
        
        public virtual EventType EventType { get; set; }
    }
}
