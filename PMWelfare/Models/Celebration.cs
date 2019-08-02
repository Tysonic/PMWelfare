namespace PMWelfare.Models
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

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

        [Required]
        [StringLength(40)]
        [Display(Name = "Event Title")]
        public string EventName { get; set; }
        [Display(Name = "Event Type")]
        public int EventTypeId { get; set; }
        [Display(Name = "Date of Event")]
        public DateTime? EventDate { get; set; }

        
        [StringLength(40)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created at")]
        [DefaultValue(typeof(DateTime), "")]
        public DateTime? CreatedAt { get; set; }
       [StringLength(40)]

        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }
        [Display(Name = "Updated at")]
        public DateTime? UpdatedAt { get; set; }
        [NotMapped]
        public IEnumerable<Member> SelectedMembers { get; set; }
        [NotMapped]
        public string[] MembersToSave { get; set; }

        [NotMapped]
        public IList<string> members { get; set; }


        public class Celebrationsviewmodel
        {
            [Display(Name ="User Name")]
            public string UserName { set; get; }
            [Display(Name ="Date of Event")]
            public DateTime? EventDate { set; get; }
            [Display(Name ="Event Type")]
            public string EventType { set; get; }
            [Display(Name ="Event Name")]
            public string EventName{ get; set; }

            
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupProducts> SupProducts { get; set; }

        public virtual ICollection<Celebrant> Celebrants { get; set; }
        
        public virtual EventType EventType { get; set; }
    }
}
