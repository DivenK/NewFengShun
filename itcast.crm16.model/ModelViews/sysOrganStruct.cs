//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace itcast.crm16.model.ModelViews
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public partial class sysOrganStructView
    {
    
    
        public int osID { get; set; }
        public int osParentID { get; set; }
        public string osName { get; set; }
        public string osCode { get; set; }
        public int osCateID { get; set; }
        public Nullable<int> osLevel { get; set; }
        public string osShortName { get; set; }
        public string osRemark { get; set; }
        public Nullable<int> osStatus { get; set; }
        public Nullable<int> osCreatorID { get; set; }
        public System.DateTime osCreateTime { get; set; }
        public Nullable<int> osUpdateID { get; set; }
        public System.DateTime osUpdateTime { get; set; }
    
        public virtual sysKeyValueView sysKeyValue { get; set; }
        public virtual ICollection<sysRoleView> sysRole { get; set; }
        public virtual ICollection<sysUserInfoView> sysUserInfo { get; set; }
        public virtual ICollection<sysUserInfoView> sysUserInfo1 { get; set; }
        public virtual ICollection<sysUserInfoView> sysUserInfo2 { get; set; }
    }
}
