﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FS_SiteEntities1 : DbContext
    {
        public FS_SiteEntities1()
            : base("name=FS_SiteEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ArticleType> ArticleType { get; set; }
        public DbSet<Commerce> Commerce { get; set; }
        public DbSet<FSHistory> FSHistory { get; set; }
        public DbSet<Manage> Manage { get; set; }
        public DbSet<MemberDynamic> MemberDynamic { get; set; }
        public DbSet<MemberMsg> MemberMsg { get; set; }
        public DbSet<New> New { get; set; }
        public DbSet<NewType> NewType { get; set; }
        public DbSet<sysMenus> sysMenus { get; set; }
        public DbSet<sysRole> sysRole { get; set; }
        public DbSet<sysUserInfo> sysUserInfo { get; set; }
        public DbSet<sysUserInfo_Role> sysUserInfo_Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<VipUser> VipUser { get; set; }
        public DbSet<SiteSet> SiteSet { get; set; }
        public DbSet<Policy> Policy { get; set; }
    }
}
