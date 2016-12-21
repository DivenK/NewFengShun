using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.model.ModelViews
{
    public partial class SiteSetViewModel
    {
        public int NId { get; set; }
        public int id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Contents { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreatTime { get; set; }
        public string Remark { get; set; }
        public int type { get; set; }
        public Nullable<int> Look { get; set; }
        public string TimeStr { get; set; }
        public bool LookStr { get; set; }
    }
}
