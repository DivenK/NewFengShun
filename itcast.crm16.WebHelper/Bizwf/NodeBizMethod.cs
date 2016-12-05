using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper
{
    public class NodeBizMethod
    {
        /// <summary>
        /// 对比用户的目标数据是否大于节点最大审核数据
        /// </summary>
        /// <param name="targetNum">要请假的目标天数或者财务报销的目标金额</param>
        /// <param name="maxNum">当前节点的最大审核权限值</param>
        /// <returns></returns>
        public bool Gt(decimal targetNum,decimal maxNum)
        {
            return targetNum > maxNum;
        }
    }
}
