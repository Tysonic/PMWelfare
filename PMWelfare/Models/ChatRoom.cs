namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChatRoom
    {
        [Key]
        public int ChatId { get; set; }

        [StringLength(40)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        [Display(Name = "Posted at")]
        public DateTime? PostedAt { get; set; }

        public int ParentId { get; set; }


        [StringLength(40)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; }
        public int ParentId { get; internal set; }
    }
}
