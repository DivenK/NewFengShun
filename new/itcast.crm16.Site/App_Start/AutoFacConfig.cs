using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itcast.crm16.Site.App_Start
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Reflection;
    using System.Web.Mvc;
    using Common;

    /// <summary>
    /// 负责初始化Autofac容器的相关数据
    /// </summary>
    public class AutoFacConfig
    {
        public static void Register()
        {
            //1.0 构造一个autofac的buider容器
            var builder = new ContainerBuilder();

            //2.0 告诉AutoFac 的控制器工厂，控制器类的创建去哪些程序集中查找
            //2.0.1 从当前运行的bin目录下加载itcast.crm16.Site.dll
            Assembly controllerAss = Assembly.Load("itcast.crm16.Site");
            builder.RegisterControllers(controllerAss);


            //3.0 告诉autofac容器，创建数据仓储项目中的所有类的对象实体，以接口的形式存储
            //3.0.1反射扫描itcast.crm16.Repository.dll所有的类
            Assembly respAssems = Assembly.Load("itcast.crm16.Repository");
            Type[] respTypes = respAssems.GetTypes();
            builder.RegisterTypes(respTypes).AsImplementedInterfaces();

            #region  单个类的注册方式写法，可以用于调试
            //builder.RegisterType(typeof(sysFunctionRepository)).As(typeof(IsysFunctionRepository));
            //builder.RegisterType(typeof(sysKeyValueRepository)).As(typeof(IsysKeyValueRepository));


            //builder.RegisterType(typeof(sysFunctionServices)).As(typeof(IsysFunctionServices));
            //builder.RegisterType(typeof(sysKeyValueServices)).As(typeof(IsysKeyValueServices)); 
            #endregion

            //4.0 告诉autofac容器，创建业务逻辑项目中的所有类的对象实体，以接口的形式存储
            //4.0.1反射扫描itcast.crm16.Services.dll所有的类
            Assembly serAssems = Assembly.Load("itcast.crm16.Services");
            Type[] serTypes = serAssems.GetTypes();
            builder.RegisterTypes(serTypes).AsImplementedInterfaces();

            //5.0 创建一个真正的autofac的工作容器
            IContainer container = builder.Build();

            //5.0.1 将container存入全局缓存对象HttpRuntime.Cache[Keys.autofac]中
            CacheMgr.SetData(Keys.autofac, container);

            // 从Autofac容器内部根据指定的接口 IsysFunctionServices 获取其实现类的对象实例
            //var obj = container.Resolve<IsysFunctionServices>();

            //6.0 将当前的容器中的控制器工厂替换MVC默认的控制器工厂,此处使用的是将autofac工作容器交给mvc底层
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}