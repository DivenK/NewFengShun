//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace itcast.crm16.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberDynamic
    {
        public int id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Praise { get; set; }
        public bool IsComment { get; set; }
        public string Creator { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int IsDelete { get; set; }
        public string Updator { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> LookCount { get; set; }
        public string ImgUrl { get; set; }
    
        public virtual ArticleType ArticleType { get; set; }
    }
}
