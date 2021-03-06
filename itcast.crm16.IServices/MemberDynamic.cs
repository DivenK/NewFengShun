//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace itcast.crm16.IServices
{
    using System;
    using System.Collections.Generic;

    using itcast.crm16.model;


    public partial interface IMemberDynamicServices:IBaseBLL<MemberDynamic>
    {


        /// <summary>
        /// 获取会员动态数据
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="count">总页数</param>
        /// <param name="type">1:会员采访；2：会员动态；3：会员招聘</param>
        /// <param name="isComent">不显示的数据</param>
        /// <returns></returns>
        List<MemberDynamic> GetMemberMsgByPage(int pageIndex, int pageSize, out int count, int type,bool isComent);


        /// <summary>
        /// 获取指定id的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MemberDynamic GetMemberMsgById(int id);

        /// <summary>
        /// 点赞增加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="praiseCount"></param>
        /// <returns></returns>
        bool PraiseDynamic(int id, int praiseCount);


    }
}
