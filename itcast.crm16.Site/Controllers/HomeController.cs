using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Controllers
{
    using IServices;
    using model.ModelViews;
    //using EntityMapping;
    using WebHelper;
    [WebHelper.Attrs.SkipCheckLogin]
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Content("修改成功");
        }

    }
}
