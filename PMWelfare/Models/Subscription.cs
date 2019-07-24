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
        public Subscription() { }

        public Subscription(string s ,decimal a)
        {
            UserName = s;
            Amount = a;
        }


        public Subscription(string s)
        {
            this.UserName = s;
        }

        [Key]
        public int SubId { get; set; }

        [StringLength(40)]
        public string UserName { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        
        public int SubMonth { get; set; }

        public int SubYear { get; set; }


        public virtual Member Member { get; set; }
    }

}
