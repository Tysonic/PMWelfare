using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMWelfare.Models
{
    public class Upload
    {
        //[NotMapped]
        //public List<string> TableName { get; set; }

        public string SelectedTable { get; set; }
        public TableName Names { get; set; }
    }
    public enum TableName
    {
        Subscriptions,
        Members
    }
}