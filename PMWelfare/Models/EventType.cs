namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
 
    [Table("EventType")]
    public partial class EventType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventType()
        {
            Celebrations = new HashSet<Celebration>();
        }

        public int Id { get; set; }

        [StringLength(40)]
        [Display(Name = "Event type")]
        public string Type { get; set; }

        
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
        public virtual ICollection<Celebration> Celebrations { get; set; }
    }
}
