using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper.Filters
{
    using Autofac;
    using itcast.crm16.Common;
    using System.Web.Mvc;
    using WebHelper.Attrs;
    /// <summary>
    /// 负责进行统一的权限判断
    /// </summary>
    public class CheckPermissAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //0.0   //判断action方法所在的控制器是否有贴
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckPermissAttribute), false))
            {
                return;
            }

            //判断action方法是否有贴
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckPermissAttribute), false))
            {
                return;
            }

            //1.0 获取action名称
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            //2.0 获取控制器名称
            string ctrlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();

            //3.0 获取区域名称
            string areaName = string.Empty;
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            }

            //4.0 根据上述三个条件去用户权限数据缓存中查询
            //4.0.1 获取缓存数据
            var container = CacheMgr.GetData<IContainer>(Keys.autofac);
            //var permissList = container.Resolve<IServices.IsysPermissListServices>().GetPermissListByUid(UserMgr.GetUserInfo().uID);

            //4.0.2 去permissList中查询一下当前请求的action方法是否存在
            ////bool isOk = permissList.Any(c => c.mArea.ToLower() == areaName
            //    && c.mController.ToLower() == ctrlName
            //    && c.fFunction.ToLower() == actionName);

            //如果没有权限
            if (!true)
            {
                //5.0 判断是否为ajax请求
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult json = new JsonResult();
                    json.Data = new { status = 2, msg = "没有权限" };
                    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                    filterContext.Result=json;
                }
                else
                {
                    //浏览器请求
                    ViewResult view = new ViewResult();
                    view.ViewName = "/Views/Shared/NoPermiss.cshtml";

                    filterContext.Result = view;
                }
            }
        }
    }
}
