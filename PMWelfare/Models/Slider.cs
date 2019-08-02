using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMWelfare.Models
{
    public class Slider
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? FileSize { get; set; }
        public string FilePath { get; set; }
    }
}