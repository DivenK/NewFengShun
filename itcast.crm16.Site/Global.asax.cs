using itcast.crm16.Site.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace itcast.crm16.Site
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //1.0 初始化Autofac的相关功能
            /*
             1、告诉autofac初始化itcast.crm16.Repository.dll中所有类的对象实例以其接口的形式保存
             *2、告诉autofac初始化itcast.crm16.Services.dll中所有类的对象实例以其接口的形式保存
             *3、将MVC默认控制器工厂替换成autofac的工厂
             */
            AutoFacConfig.Register();
        }
    }
}