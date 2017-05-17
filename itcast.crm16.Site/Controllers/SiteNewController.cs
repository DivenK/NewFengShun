using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.Site.Models;
using itcast.crm16.IServices;
using itcast.crm16.WebHelper;


namespace itcast.crm16.Site.Controllers
{
    [WebHelper.Attrs.SkipCheckLogin]
    public class SiteNewController : BaseController
    {
        //
        // GET: /New/
        public SiteNewController(IsysMenusServices mSer, INewServices newSer,ISiteSetService siteS) : base(mSer, siteS,"/SiteNew")
        {
            base.news = newSer;
        }
        public ActionResult Index(int type=1,int index = 1)
        {
            base.pageSize = 15;
            var list = news.NewPageList(index, type, "", out TotalPage,base.pageSize , true);
            ViewBag.list = list;
            SetViewBagPage();
            ViewBag.type = type;
            return View();
        }
        public ActionResult GetNew(int type = 1,int pageindex=1)
        {
            base.pageSize = 15;
            var list = news.NewPageList(pageindex, type, "", out TotalPage,base.pageSize, true);
           var modelList= list.Select(p => new NewViewModel()
            {
               id=p.id,
               Name=p.Name,
               Conent=p.Conent,
               CreatTimeStr=p.CreatTime.ToShortDateString(),
            }).ToList();
            return WriteSuccess("",list);
        }

        public ActionResult GetModel(int id)
        {
           var model=  news.QueryWhere(c => c.id == id && c.IsDelete == 0).FirstOrDefault();
            if (model != null)
            {

            }
            ViewBag.Model = model;
            return View();
        }
        public ActionResult Look(int count,int id)
        {
            if (id < 0)
            {
                return WriteError("网络错误");
            }
            model.New model = new crm16.model.New();
            model.id = id;
            model.Look = ++count;

            news.Edit(model, new string[] { "Look" });
           var result= news.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("",count);
            }
            return WriteError("网络错误");
        }
        public ActionResult AddZan(int count, int id)
        {
            if (id < 0)
            {
                return WriteError("网络错误");
            }
            model.New model = new crm16.model.New();
            model.id = id;
            model.Praise = ++count;

            news.Edit(model, new string[] { "Praise" });
            var result = news.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("",count);
            }
            return WriteError("网络错误");
        }
    }
}
