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
    
    public partial class MemberMsg
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string RealName { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Enterprise { get; set; }
        public string Post { get; set; }
        public string Contacts { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string ContactAddress { get; set; }
        public string Postcode { get; set; }
        public bool IsPass { get; set; }
        public bool IsFormal { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string WhoPass { get; set; }
        public Nullable<System.DateTime> PassTime { get; set; }
    }
}