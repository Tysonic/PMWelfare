

namespace PMWelfare.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    public class Celebrant
    {
        [Key]
        public int CelebId { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        public int EventId { get; set; }

        public virtual Celebration Celebration { get; set; }

        public virtual Member Member { get; set; }
        public class Celebransviewmodel
        {
            public string UserName { set; get; }
            public DateTime EventDate { set; get; }
            public string EventType { set; get; }
        }
    }
}