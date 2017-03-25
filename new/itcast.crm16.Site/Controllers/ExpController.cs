using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.WebHelper.Attrs;

namespace itcast.crm16.Site.Controllers
{
    [SkipCheckLogin]
    [SkipCheckPermiss]
    public class ExpController : Controller
    {
        //
        // GET: /Exp/

        public ActionResult Error404()
        {
            return Content("您的页面去火星了");
        }

        public ActionResult Error500()
        {
            return Content("服务器有异常");
        }
    }
}
