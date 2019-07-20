

namespace PMWelfare.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    public class Celebrant
    {
        [Key]
        public int CelebId { get; set; }

        [StringLength(20)]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Display(Name = "Event Name")]
        public int EventId { get; set; }


        public virtual Celebration Celebration { get; set; }

        public virtual Member Member { get; set; }

    }
}