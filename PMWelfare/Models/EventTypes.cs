using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMWelfare.Models
{
    public class EventTypes
    {
        [Key]
        public int Id { set; get; }

        [Required]
        [StringLength(40)]
        public String Type { set; get; }

        [Required]
        [StringLength(40)]
        public string CreateBy { set; get; }
        
        []
        public DateTime? CreatedAt { set; get; }

        public String UpdatedBy { set; get; }

        public DateTime? UpdatedAt { set; get; }
        
    }
}