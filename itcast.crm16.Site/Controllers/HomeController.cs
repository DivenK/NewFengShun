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
        //IsysFunctionServices fSer;
        //IsysKeyValueServices keySer;
        //public HomeController(IsysFunctionServices fSer, IsysKeyValueServices keySer)
        //{
        //    //this.fSer = fSer;
        //    //this.keySer = keySer;
        //}

        //
        // GET: /Home/
        public ActionResult Index()
        {
            //var list = fSer.QueryWhere(c => c.fID <= 5);

            //return Content(list[0].fName);
            return Content("");
        }

        public ActionResult Edit()
        {
            //var model = fSer.QueryWhere(c => c.fID == 0)
            //    .Select(c => c.EntityMap())
            //    .FirstOrDefault();

            //return View(model);
            return View();
        }

        public ActionResult Test()
        {
            //1.0 修改sysfunction表中的fid=0的字段fname的值
            //var sysfun = fSer.QueryWhere(c => c.fID == 0).FirstOrDefault();
            //sysfun.fName = "默认123";
            ////fSer.SaveChanges();

            ////2.0 修改syskeyvalue中的 kid=2 的字段KName的值
            //var keyvalue = keySer.QueryWhere(c => c.KID == 2).FirstOrDefault();
            //keyvalue.KName = "集团123";

            //keySer.SaveChanges();

            return Content("修改成功");

        }

    }
}
