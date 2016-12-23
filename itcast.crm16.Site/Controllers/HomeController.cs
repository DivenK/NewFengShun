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
        public HomeController(IsysMenusServices mSer, INewServices newSer, IUserServices user, IFSHistoryService fser, ISiteSetService siteService,ICommerce comSer) :base(mSer)
        {
            base.news = newSer;
            base.FSHistorySer = fser;
            base.siteService = siteService;
            base.Commerce = comSer;
        }
        // GET: /Home/
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.FSHistoryList = FSHistorySer.GetItemModel(1, out TotalPage, "").ToList();
            model.ImageList = siteService.GetItems(1, 5, 0).ToList();
            model.NewList = news.NewPageList(1, 0, "", out TotalPage);
            model.NewListType = news.NewPageList(1, 1, "", out TotalPage);
            model.vipUserImageList = Commerce.GetItemModel(1, "", out TotalPage, 2).ToList();
            ViewBag.ModelList = model;
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
