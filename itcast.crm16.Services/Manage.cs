//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace itcast.crm16.Services
{
    using System;
    using System.Collections.Generic;

    using itcast.crm16.IServices;
    using itcast.crm16.model;
    using itcast.crm16.IRepository;
    public partial class ManageServices : BaseBLL<Manage>, IManageServices
    {
        IManageRepository dal;
        public ManageServices(IManageRepository dal)
        {
            this.dal = dal;
            base.basedal = dal;
        }

        /// <summary>
        /// 前端分页获取显示企业管理数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Manage> GetManageByPage(int pageIndex, int pageSize, out int count)
        {
            return this.dal.QueryByPage(pageIndex, pageSize, out count, c => c.Look == 1, c => c.CreateTime);
        }

        /// <summary>
        /// 获取指定id的企业管理详细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Manage GetManageById(int id)
        {
            List<Manage> mList = this.dal.QueryWhere(c => c.id == id && c.Look == 1);
            if (mList != null)
            {
                return mList[0];
            }
            return null;
        }

        /// <summary>
        /// 企业点赞
        /// </summary>
        /// <param name="id"></param>
        /// <param name="praise"></param>
        /// <returns></returns>
        public bool ManagePraise(int id,int praise)
        {
            Manage entity = new Manage() { id = id, Praise = praise };
            this.dal.Edit(entity, new string[] { "Praise" });
            return this.dal.SaveChanges()>0;
        }

    }
}
