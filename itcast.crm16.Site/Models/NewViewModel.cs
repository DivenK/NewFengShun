using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itcast.crm16.Site.Models
{
    public class NewViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Conent { get; set; }
        public bool IsComment { get; set; }
        public int IsDelete { get; set; }
        public string Author { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string TypeName { get; set; }

        public System.DateTime CreatTime { get; set; }
        public string CreatTimeStr { get; set; }
        public string Creator { get; set; }
        public int Praise { get; set; }
        public int Look { get; set; }
        public int display { get; set; }
        public bool displayBool { get; set; }
    }
}