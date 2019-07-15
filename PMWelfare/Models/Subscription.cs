namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subscription
    {

       
        private string s;
        private String k;

        public Subscription() { }


        public Subscription(string s)
        {
            this.UserName = s;
        }


        [Key]
        public int SubId { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        public Subscription(string s)
        {
            this.UserName = s;
        }

        public Subscription(string u, decimal? a )
        {
            UserName = u;
            Amount = a;
        }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public int SubMonth { get; set; }

        public int SubYear { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; }
        public class SubscriberViewModel
            {
             public String Username { get; set; }
            public Decimal? Amount { get; set; }


}
    }
}
