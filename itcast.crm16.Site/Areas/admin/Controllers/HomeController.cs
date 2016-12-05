using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    using IServices;
    using WebHelper;
    using model;
    using WebHelper.Attrs;
    using itcast.crm16.Common;

    [SkipCheckPermiss]
    [SkipCheckLogin]
    public class HomeController : BaseController
    {
        public HomeController(IsysMenusServices mSer, IsysUserInfoServices user):base(mSer)
        {
            base.menuSer = mSer;
            base.userinfoSer = user;
        }

        /// <summary>
        /// 负责返回一个视图此视图负责整个系统的布局
        /// </summary>
        /// <returns></returns>
        [SkipCheckLogin]
        public ActionResult Index()
        {
            //1.0 获取菜单表sysmenus中的所有有效数据
            
            return View();
            //return View();
           
        }

        public ActionResult MyCenter()
        {
            return View();
        }

        /// <summary>
        /// 请求sysuserinfo表获取有所有效用户
        /// </summary>
        /// <returns></returns>
        public ActionResult getusertree()
        {
            var list = userinfoSer.QueryWhere(c => c.uStatus == (int)Enums.EStatus.Normal)
                .Select(c => new
                {
                    c.uID,
                    c.uRealName
                });

            return Json(list);
        }
    }
}
