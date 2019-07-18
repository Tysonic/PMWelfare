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
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Message { get; set; }

        public DateTime? PostedAt { get; set; }
        public DateTime TimeStamp
        {
            get
            {
                if (PostedAt == null)
                {
                    PostedAt = DateTime.Now;
                }
                return PostedAt.Value;
            }
            private set { PostedAt = value; }
        }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
