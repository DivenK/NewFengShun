using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper.Filters
{
    using System.Web.Mvc;

    /// <summary>
    /// 自定义异常过滤器
    /// </summary>
    public class ExpAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //1.0 获取最底层的异常
            Exception innerExp = filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException : filterContext.Exception;
            while (innerExp.InnerException !=null)
            {
                innerExp = innerExp.InnerException;
            }
           
            //2.0 将详细的堆栈信息存入到文本日志(数据表) ,Log4Net.dll
            //千万不要用  System.IO.File.AppendAllText(); 因为会产生多线程并发问题

            //3.0 区分是否为ajax请求
            if (filterContext.HttpContext.Request.IsAjaxRequest()) //表示是一个ajax请求
            {
                //返回:{status:1,msg:}
                JsonResult json=new JsonResult();
                json.Data=new {status=1,msg=innerExp.Message};
                json.JsonRequestBehavior= JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
                //告诉MVC框架我已经处理完毕，你不需要再跳转到统一出错页面
                filterContext.ExceptionHandled = true;
            }
            else
            {
                filterContext.ExceptionHandled = false;//web.config配置错误页面
            }
        }
    }
}
