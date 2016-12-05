using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper
{
    using model;
    using System.Web;
    using Common;

    public class UserMgr
    {
        /// <summary>
        /// 负责获取当前登录用户的实体
        /// </summary>
        /// <returns></returns>
        public static sysUserInfo GetUserInfo()
        {
            if (HttpContext.Current.Session[Keys.uinfo] != null)
            {
                return HttpContext.Current.Session[Keys.uinfo] as sysUserInfo;
            }
            return new sysUserInfo() { };
        }
    }
}
