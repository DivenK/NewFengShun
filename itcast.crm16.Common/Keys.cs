using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.Common
{
    public class Keys
    {
        /// <summary>
        /// 用于存放验证码字符串的session key
        /// </summary>
        public const string Vcode = "CRM16Vcode";
        /// <summary>
        /// 用于存放登录成功以后的用户实体 session key
        /// </summary>
        public const string uinfo = "CRM16Uinfo";
        /// <summary>
        /// 用于存放登录成功以后的用户实体中的用户id 的cookie key
        /// </summary>
        public const string isremember = "CRM16isremember";
        /// <summary>
        /// 负责在HttpRuntime.Cache缓存auto发出容器的对象实例 的key
        /// </summary>
        public const string autofac = "CRM16autofac";

        /// <summary>
        /// 每个用户的权限缓存key，注意在后面一定要加上用户id
        /// </summary>
        public const string functions = "CRM16functions";

    }
}
