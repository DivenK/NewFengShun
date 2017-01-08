using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itcast.crm16.Site.Models
{
    public class FSHistoryViewModel
    {
        public int id { get; set; }
        public int Nid { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public int Display { get; set; }
        public bool DisplayStr { get; set; }
        public int Look { get; set; }
        public int Likes { get; set; }
        public string Creater { get; set; }
        public System.DateTime CreaterTime { get; set; }
        public string Image { get; set; }
        public int type { get; set; }

        public string UpdateTimeStr { get; set; }
        public System.DateTime UpdateTime { get; set; }
    }
}