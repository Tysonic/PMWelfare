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

        public DateTime EndDate { get; set; }

        [Column(TypeName = "money")]
        public decimal ClosingBalance { get; set; }
    }
}
