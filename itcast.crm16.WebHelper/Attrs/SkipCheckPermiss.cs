using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper.Attrs
{
    /// <summary>
    /// 如果在控制器或者actioin上贴了此标签，则表示跳过权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SkipCheckPermissAttribute : Attribute
    {
    }
}
