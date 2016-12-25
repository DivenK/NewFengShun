using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.Site.Models;

namespace itcast.crm16.Site.Controllers
{
    using Common;
    using IServices;
    using model.ModelViews;
    //using EntityMapping;
    using WebHelper;
    [WebHelper.Attrs.SkipCheckLogin]
    public class HomeController : BaseController
    {
        public HomeController(IsysMenusServices mSer, INewServices newSer, IMemberMsgServices Mser, IMemberDynamicServices MemberDynamicSer, IFSHistoryService fser, ISiteSetService siteService,ICommerce comSer) :base(mSer)
        {
            base.news = newSer;
            base.FSHistorySer = fser;
            base.siteService = siteService;
            base.Commerce = comSer;
            base.memberMsgSer = Mser;
            base.memberSer = MemberDynamicSer;
        }
        // GET: /Home/
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.FSHistoryList = FSHistorySer.GetItemModel(1, out TotalPage, "",10,true).ToList();
            model.ImageList = siteService.GetItems(1, 5, 0,true).ToList();
            model.NewList = news.NewPageList(1, 1, "", out TotalPage,10,true);
            model.NewListType = news.NewPageList(1,1, "", out TotalPage,10,true);
            model.vipUserImageList = Commerce.GetItemModel(1, "", out TotalPage, 2,1000,true).ToList();
            model.MemberTellList = memberSer.GetMemberMsgByPage(1, pageSize, out TotalPage, 1, false);
            model.MemberNewsList = memberSer.GetMemberMsgByPage(1, pageSize, out TotalPage,2, false);
            model.MemberZhaoList = memberSer.GetMemberMsgByPage(1, pageSize, out TotalPage,3, false);
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

        public ActionResult GetNews(int index)
        {
           var NewItems= news.NewPageList(1, 3, "", out TotalPage,13);
            ViewBag.NewItems = NewItems;
            return View();
        }
        public ActionResult AjaxGetNews(int index)
        {
            var NewItems = news.NewPageList(1, 3, "", out TotalPage, 13);
            return Json(NewItems);
        }


    }
}
