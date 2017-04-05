using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itcast.crm16.Site.Models
{
    /// <summary>
    /// 只是用来view显示辅助
    /// </summary>
    public class CommerceViewModel
    {
        public int Nid { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public int Look { get; set; }

        /// <summary>
        /// 是否显示在网站前台
        /// </summary>
        public bool LookBool { get; set; }

        public int IsDelete { get; set; }
        public string Creater { get; set; }
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 最近的更新时间
        /// </summary>
        public string UpdateTimeStr    { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public string Remark { get; set; }
        public int Sort { get; set; }
        public string ImageUrl { get; set; }
        public int? typeName { get; set; }

        public int NSort { get; set; }
    }
}