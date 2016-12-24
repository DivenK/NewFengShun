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
        public CemmendController(IsysMenusServices mSer,ICommerce monser):base(mSer)
        {
            base.Commerce = monser;
        }
        public ActionResult Index()
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, 0, 10, true).ToList();
            ViewBag.items= list.OrderByDescending(c => c.Nid).ToList();
            return View();
        }
        public ActionResult GetModel(int id=0,int type=0)
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, type, 10, true).ToList();
            ViewBag.items = list.OrderByDescending(c => c.Nid).ToList();
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetAddCemmend()
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage,1, 10, true).ToList();
            ViewBag.items = list.OrderByDescending(c => c.Nid).ToList();
          
            return View();
        }
    }
}
