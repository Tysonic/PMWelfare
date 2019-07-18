namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MemberStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberStatus()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public int MembersStatusID { get; set; }

        [Column("MemberStatus")]
        [Required]
        [StringLength(20)]
        public string MemberStatus1 { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime TimeStamp
        {
            get
            {
                if (CreatedAt == null)
                {
                    CreatedAt = DateTime.Now;
                }
                return CreatedAt.Value;
            }
            private set { CreatedAt = value; }
        }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
    }
}
