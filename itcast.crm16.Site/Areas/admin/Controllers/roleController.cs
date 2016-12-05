using itcast.crm16.IServices;
using itcast.crm16.WebHelper;
using itcast.crm16.WebHelper.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    [SkipCheckLogin]
    public class roleController :BaseController
    {
        public roleController(IsysMenusServices mSer, INewServices news, INewTypeServices newsType) : base(mSer)
        {
            base.news = news;
            base.newsType = newsType;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
