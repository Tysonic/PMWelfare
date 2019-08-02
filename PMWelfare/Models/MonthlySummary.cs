namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MonthlySummary
    {
        public int Id { get; set; }

        [Display(Name = "End of Month")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Closing Balance")]
        public decimal ClosingBalance { get; set; }
    }
}
