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
    using static Common.Enums;

    public partial class MemberDynamicServices:BaseBLL<MemberDynamic>,IMemberDynamicServices
    {
    IMemberDynamicRepository dal;

        public MemberDynamicServices(IMemberDynamicRepository memberDal)
        {
            base.basedal = memberDal;
            this.dal = memberDal;
        }

        public List<MemberDynamic> GetMemberMsgByPage(int pageIndex, int pageSize, out int count, int type,bool isComent)
        {
            return this.dal.QueryByPage(pageIndex, pageSize, out count, c => c.Type == type && c.IsDelete == 0 && c.IsComment == (!isComent), c => c.id);
        }

        public MemberDynamic GetMemberMsgById(int  id)
        {
            List<MemberDynamic> mList = this.dal.QueryWhere(c => c.id == id && c.IsComment == true && c.IsDelete == 0);
            if(mList!=null&&mList.Count>0)
            {
                return mList[0];
            }
            return null;
        }

        public bool PraiseDynamic(int id,int praiseCount)
        {
            MemberDynamic entity = new MemberDynamic() {id=id,Praise=praiseCount };
            this.dal.Edit(entity, new string[] { "Praise" });
            return this.dal.SaveChanges() > 0;
        }
    }
}
