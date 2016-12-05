using itcast.crm16.WebHelper.Filters;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //1.0 添加统一登录验证为全局过滤器
            filters.Add(new CheckLoginAttribute());

            //2.0 添加统一权限验证为全局过滤器
            filters.Add(new CheckPermissAttribute());

            //3.0 添加统一异常过滤器
            filters.Add(new ExpAttribute());
        }
    }
}