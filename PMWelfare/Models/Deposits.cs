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
        public string UserName { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(40)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime TimeStamp
        {
            get
            {
                if (CreatedAt == null)
                {
                    CreatedAt = DateTime.Now;
                }
                return CreatedAt.Value;
            }
            private set { CreatedAt = value; }
        }

        [StringLength(40)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
