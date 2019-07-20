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

        public Subscription(string s, decimal? arrearAmount) : this(s)
        {
        }

        [Key]
        public int SubId { get; set; }

        [StringLength(40)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        
        [Display(Name = "Month")]
        public int? SubMonth { get; set; }

        [Display(Name = "Year")]
        public int? SubYear { get; set; }

        public virtual Member Member { get; set; }
    }

}
