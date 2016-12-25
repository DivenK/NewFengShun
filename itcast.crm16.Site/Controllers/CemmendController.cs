using itcast.crm16.IServices;
using itcast.crm16.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Controllers
{
    [WebHelper.Attrs.SkipCheckLogin]
    public class CemmendController : BaseController
    {
        //
        // GET: /Cemmend/
        public CemmendController(IsysMenusServices mSer,ICommerce monser):base(mSer,"/Cemmend")
        {
            base.Commerce = monser;
        }
        public ActionResult Index()
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, 0, 10, true).ToList();
            ViewBag.items= list.OrderByDescending(c => c.Nid).ToList();
            ViewBag.id = 0;
            return View();
        }
        public ActionResult GetModel(int id=0,int type=0)
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, type, 10, true).ToList();
            ViewBag.items = list.OrderByDescending(c => c.Nid).ToList();
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetLeader(int id=0)
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage,0, 10, true).ToList();
            ViewBag.items = list.OrderByDescending(c => c.Nid).ToList();
            var lists = Commerce.GetItemModel(1, "", out TotalPage,2,3, true).ToList();
            base.pageSize = 3;
            SetViewBagPage();
            ViewBag.list = list;
            ViewBag.Items = lists;
            ViewBag.id = id;
            return View();
        }
        public ActionResult AjaxGetLeader(int pageIndex)
        {
            var lists = Commerce.GetItemModel(pageIndex, "", out TotalPage, 2,3, true).ToList();
           
            if (lists.Count > 0)
            {
                return WriteSuccess("", lists);
            }
            else
            {
                return WriteError("");
            }
        }

        public ActionResult LeaderModel(int id)
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, 0, 10, true).OrderByDescending(c => c.Nid).ToList();
            ViewBag.list = list;
            if (id > 0)
            {
            
                var model = Commerce.QueryWhere(c => c.id == id).First();
                ViewBag.Model = model;
            }
            else
            {
                ViewBag.Model = null;
            }
            return View();
        }

    }
}
