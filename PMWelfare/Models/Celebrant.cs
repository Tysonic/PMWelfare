

namespace PMWelfare.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Celebrant
    {
        [Key]
        public int CelebId { get; set; }

        [StringLength(20)]
        [Display(Name ="Celebrants")]
        public string UserName { get; set; }

        [Display(Name = "Event Name")]
        public int EventId { get; set; }


        [NotMapped]
        public IEnumerable<Member> SelectedMembers { get; set; }
        [NotMapped]
        public string[] MembersToSave { get; set; }

        [NotMapped]
        public IList<string> members { get; set; }


        public virtual Celebration Celebration { get; set; }

        public virtual Member Member { get; set; }



    }
}