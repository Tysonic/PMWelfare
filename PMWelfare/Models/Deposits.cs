namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Deposit
    {
        private string s;
        public Deposit() { }

        public Deposit(string s)
        {
            this.UserName = s;
        }

        [Key]
        public int DepositId { get; set; }

        [StringLength(40)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        
        [StringLength(40)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created at")]
        public DateTime? CreatedAt { get; set; }

        [StringLength(40)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
