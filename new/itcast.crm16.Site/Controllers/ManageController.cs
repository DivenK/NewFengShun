using itcast.crm16.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.IServices;
using itcast.crm16.model;

namespace itcast.crm16.Site.Controllers
{
    [WebHelper.Attrs.SkipCheckLogin]
    public class ManageController : BaseController
    {
        public ManageController(IsysMenusServices mSer,IManageServices manageSer) : base(mSer, "/manage/index")
        {
            base.manageSer = manageSer;
        }

        //
        // GET: /Manage/

        public ActionResult Index()
        {
            int count = 0;
            List<Manage> mList = manageSer.QueryByPage(1, 15, out count, c => c.Look == 0, c => c.CreateTime);
            ViewBag.ManageList = mList;
            ViewBag.TotalPage = Math.Ceiling(count * 1.0 / 15);
            return View();
        }

        public ActionResult ManageByPage(int pageIndex)
        {
            int count = 0;
            List<Manage> mList= manageSer.QueryByPage(pageIndex, 15, out count, c => c.Look == 0, c => c.CreateTime);
            return WriteSuccess("", mList.Select(c => new { c.id, c.Title, CreateTime = c.CreateTime.ToString("yyyy-MM-dd") }));
        }

        public ActionResult ManageDetail(int id)
        {
            List<Manage> mList = manageSer.QueryWhere(c => c.id == id && c.Look == 0);
            if(mList!=null&mList.Count>0)
            {
                ViewBag.Entity = mList[0];
                ViewBag.id = mList[0].id;
            }
            return View();
        }


        public ActionResult Praise(int id,int count)
        {
            Manage editModel = new Manage() { id = id, Praise = count + 1 };
            manageSer.Edit(editModel, new string[] { "Praise" });
            manageSer.SaveChanges();
            return WriteSuccess("");
        }

    }
}
