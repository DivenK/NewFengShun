using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.Site.Models;

namespace itcast.crm16.Site.Controllers
{
    using IServices;
    using model.ModelViews;
    //using EntityMapping;
    using WebHelper;
    [WebHelper.Attrs.SkipCheckLogin]
    public class HomeController : BaseController
    {
        public HomeController(IsysMenusServices mSer, INewServices newSer, IUserServices user, IFSHistoryService fser, ISiteSetService siteService) :base(mSer)
        {
            base.news = newSer;
            base.FSHistorySer = fser;
            base.siteService = siteService;
        }
        // GET: /Home/
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.FSHistoryList = FSHistorySer.GetItemModel(1, out TotalPage, "").ToList();
            model.ImageList = siteService.GetItems(1, 5, 0).ToList();
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
