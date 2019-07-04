using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMWelfare.Models
{
    public class Report
    {


        public partial class Members
        {
            [Key]
            [StringLength(40)]
            public string user_name { get; set; }

            [StringLength(40)]
            public string first_name { get; set; }

            [StringLength(40)]
            public string other_name { get; set; }

            [Required]
            [StringLength(40)]
            public string email { get; set; }

            [Column(TypeName = "date")]
            public DateTime dob { get; set; }

            public DateTime? created_at { get; set; }

        }
        public partial class Deposits
        {
            [Key]
            public int dep_id { get; set; }

            [StringLength(40)]
            public string user_name { get; set; }

            [Column(TypeName = "money")]
            public decimal amount { get; set; }

            public DateTime? created_at { get; set; }
        }


        public partial class Celebrations
        {
            public int event_id { get; set; }

            [Required]
            [StringLength(40)]
            public string event_name { get; set; }

            public DateTime event_date { get; set; }
        }


        public partial class Expenses
        {
            [Key]
            public int exp_id { get; set; }

            [Column(TypeName = "date")]
            public DateTime exp_date { get; set; }

            public int? prod_id { get; set; }

            public int quantity { get; set; }

        }
        public partial class ActivityLogs
        {
            public int id { get; set; }

            [StringLength(40)]
            public string user_name { get; set; }

            [Required]
            [StringLength(20)]
            public string device_ip { get; set; }

            [Required]
            [StringLength(40)]
            public string device_mac { get; set; }

            public DateTime? acitivity_time { get; set; }

            public string activity { get; set; }
        }


        public partial class Chats
        {
            [Key]
            public int chat_id { get; set; }

            [StringLength(40)]
            public string user_name { get; set; }

            [Required]
            [StringLength(250)]
            public string message { get; set; }

            public DateTime? posted_at { get; set; }
        }
        public partial class MonthlySummary
        {
            public int id { get; set; }

            public DateTime? start_at { get; set; }

            public DateTime? end_at { get; set; }

            [Column(TypeName = "money")]
            public decimal closing_balance { get; set; }

            public DateTime? created_at { get; set; }
        }
        public partial class MemberStatus
        {
            public int id { get; set; }

            [Column("member_status")]
            [Required]
            [StringLength(20)]
            public string member_status1 { get; set; }

        }
        public partial class Subscription
        {
            [Key]
            public int sub_id { get; set; }

            [StringLength(40)]
            public string user_name { get; set; }

            [Column(TypeName = "money")]
            public decimal amount { get; set; }

            public byte sub_month { get; set; }

            public int sub_year { get; set; }
        }


        public partial class ProductSuplied
        {


            [Key]
            public int prod_id { get; set; }

            [Required]
            [StringLength(40)]
            public string prod_name { get; set; }

            [Column(TypeName = "money")]
            public decimal price { get; set; }

            public int? event_id { get; set; }

            public int? sup_id { get; set; }


        }
        public partial class Supplier
        {

            [Key]
            public int sup_id { get; set; }

            [StringLength(10)]
            public string sup_tel { get; set; }

            [Required]
            [StringLength(40)]
            public string sup_name { get; set; }

            [Required]
            [StringLength(40)]
            public string email { get; set; }

        }
    }

}