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
    public class SiteFSHistoryController : BaseController
    {
        //
        // GET: /FSHistory/
        public SiteFSHistoryController(IsysMenusServices mSer, IFSHistoryService newSer, IFSHistoryService pMenusList) : base(mSer, "SiteFSHistory")
        {
            base.FSHistorySer = newSer;
        }
        public ActionResult Index()
        {
            base.pageSize = 15;
            var list= FSHistorySer.GetItemModel(1, out TotalPage, "", base.pageSize, true);
            SetViewBagPage();
            ViewBag.list = list;
            return View();
        }

        public ActionResult AjaxGetItens(int pageIndex = 1,int type=0)
        {
            base.pageSize = 15;

            var list = FSHistorySer.GetItemModel(pageIndex, out TotalPage, "", base.pageSize, true);
            return WriteSuccess("", list);
        }

        public ActionResult GetModel(int id)
        {
            model.FSHistory model = null;
            if (id > 0)
            {
                model= FSHistorySer.QueryWhere(c => c.id == id).FirstOrDefault();
            }
            ViewBag.model = model;
            return View();
        }

        public ActionResult Look(int count, int id)
        {
            if (id < 0)
            {
                return WriteError("网络错误");
            }
            model.FSHistory model = new crm16.model.FSHistory();
            model.id = id;
            model.Look = ++count;

            FSHistorySer.Edit(model, new string[] { "Look" });
            var result = FSHistorySer.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("", count);
            }
            return WriteError("网络错误");
        }
        public ActionResult AddZan(int count, int id)
        {
            if (id < 0)
            {
                return WriteError("网络错误");
            }
            model.FSHistory model = new crm16.model.FSHistory();
            model.id = id;
            model.Likes = ++count;

            FSHistorySer.Edit(model, new string[] { "Likes" });
            var result = FSHistorySer.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("",count);
            }
            return WriteError("网络错误");
        }
    }
}
