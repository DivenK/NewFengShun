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
    
    public partial class Commerce
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public int Look { get; set; }
        public int IsDelete { get; set; }
        public string Creater { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Type { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> Sort { get; set; }
    }
}
