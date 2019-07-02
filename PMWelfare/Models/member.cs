namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public member()
        {
            activity_logs = new HashSet<activity_logs>();
            chat_room = new HashSet<chat_room>();
            deposits = new HashSet<deposit>();
            subscriptions = new HashSet<subscription>();
            Admins = new HashSet<Admin>();
        }

        [Key]
        [StringLength(40)]
        public string user_name { get; set; }

        [StringLength(40)]
        public string first_name { get; set; }

        [StringLength(40)]
        public string other_name { get; set; }

        [Required]
        [StringLength(40)]
        public string email { get; set; }

        [Column(TypeName = "date")]
        public DateTime dob { get; set; }

        public int? member_status { get; set; }

        public bool is_admin { get; set; }

        [Required]
        [StringLength(40)]
        public string created_by { get; set; }


        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<activity_logs> activity_logs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chat_room> chat_room { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<deposit> deposits { get; set; }

        public virtual member_status member_status1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<subscription> subscriptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }
    }
}
