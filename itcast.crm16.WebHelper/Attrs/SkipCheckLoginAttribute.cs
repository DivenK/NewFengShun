using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper.Attrs
{
    /// <summary>
    /// 当action或者控制器上贴有此标签则跳过登录过滤器的检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SkipCheckLoginAttribute : Attribute
    {
    }
}
