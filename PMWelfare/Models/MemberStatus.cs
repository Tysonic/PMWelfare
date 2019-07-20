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
        [Index("IX_X_Category", 1, IsUnique = true)]
        [Display(Name = "Member Status")]
        public string MemberStatus1 { get; set; }

        
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
        public virtual ICollection<Member> Members { get; set; }
    }
}
