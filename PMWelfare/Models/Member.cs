namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(40)]
        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date of birth")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Member Status")]
        public int? MemberStatus { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        
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
