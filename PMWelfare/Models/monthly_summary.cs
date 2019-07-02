namespace PMWelfare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class monthly_summary
    {
        public int id { get; set; }

        public DateTime? start_at { get; set; }

        public DateTime? end_at { get; set; }

        [Column(TypeName = "money")]
        public decimal closing_balance { get; set; }

        [StringLength(40)]
        public string created_by { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(40)]
        public string updated_by { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
