namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        private string s;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            ActivityLogs = new HashSet<ActivityLogs>();
            ChatRoom = new HashSet<ChatRoom>();
            Deposits = new HashSet<Deposit>();
            Subscriptions = new HashSet<Subscription>();
        }

        public Member(string s)
        {
            this.UserName = s;
        }

        [Key]
        [StringLength(40)]
        public string UserName { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; }

        [StringLength(40)]
        public string OtherName { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOB { get; set; }

        public int? MemberStatus { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityLogs> ActivityLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatRoom> ChatRoom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deposit> Deposits { get; set; }

        public virtual MemberStatus MemberStatus1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }

    }
}
