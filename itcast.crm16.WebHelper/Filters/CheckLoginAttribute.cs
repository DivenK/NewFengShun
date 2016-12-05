using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper.Filters
{
    using System.Web.Mvc;
    using Common;
    using IServices;
    using System.Web.WebPages;
    using Autofac;
    using itcast.crm16.WebHelper.Attrs;


    /// <summary>
    /// 统一对系统的action进行截获处理，如果没有登录则统一跳转到登陆页面
    /// </summary>
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //1.0 判断如果贴有    [SkipCheckLogin]则跳过登录检查
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
            {
                return;
            }

            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
            {
                return;
            }

            if (filterContext.HttpContext.Session[Keys.uinfo] == null)
            {
                //1.0 判断cookie是否有值
                if (filterContext.HttpContext.Request.Cookies[Keys.isremember] == null)
                {
                    //2.0 跳转到登录页面
                    ToLogin(filterContext);
                }
                else
                {
                    //3.0 获取cookie中存好的用户id
                    string uid = filterContext.HttpContext.Request.Cookies[Keys.isremember].Value;

                    //4.0 存全局缓存中获取autofac容器对象
                    var container = CacheMgr.GetData<IContainer>(Keys.autofac);

                    //4.0 根据uid去表sysuserinfo中查询用户对象重新赋值给Session[Keys.uinfo]
                    //IsysUserInfoServices userServics = container.Resolve<IsysUserInfoServices>();
                    //int iuserid = uid.AsInt();
                    //var userinfo = userServics.QueryWhere(c => c.uID == iuserid).FirstOrDefault();
                    //if (userinfo == null)
                    //{
                    //    ToLogin(filterContext);
                    //}
                    //else
                    //{
                    //    filterContext.HttpContext.Session[Keys.uinfo] = userinfo;

                    //    //设置缓存
                    //    container.Resolve<IsysPermissListServices>().GetPermissListByUid(userinfo.uID);
                    //}
                }
            }
        }

        /// <summary>
        /// 负责跳转到登录页面
        /// </summary>
        /// <param name="filterContext"></param>
        void ToLogin(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JsonResult json = new JsonResult();
                json.Data = new { status = 3, msg = "您没有登录,即将跳转到登录页面" };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                filterContext.Result = json;
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/admin/Login/Login");
            }
        }
    }
}
