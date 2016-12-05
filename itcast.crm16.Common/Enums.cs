using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Common
{
    public class Enums
    {

        public enum EStatus
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 停用. 软删除
            /// </summary>
            Stop = 1
        }

        /// <summary>
        /// 管理节点类型的枚举
        /// </summary>
        public enum ENodeType
        {
            /// <summary>
            /// 开始节点
            /// </summary>
            StartNode = 34,
            /// <summary>
            /// 处理节点
            /// </summary>
            ProcessNode = 35,
            /// <summary>
            /// 结束节点
            /// </summary>
            EndtNode = 36
        }

        public enum EPorcessStatus
        {
            /// <summary>
            /// 处理中
            /// </summary>
            Processing = 40,
            /// <summary>
            /// 驳回上级
            /// </summary>
            back = 41,
            /// <summary>
            /// 拒绝
            /// </summary>
            Reject = 42,
            /// <summary>
            /// 通过
            /// </summary>
            Pass = 43
        }

    }
}
