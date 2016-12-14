using System;
using itcast.crm16.model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.WebHelper;
using itcast.crm16.IServices;
using itcast.crm16.Site.Models;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    public class FSHistoryController : BaseController
    {
        public FSHistoryController(IsysMenusServices mSer, IFSHistoryService Ser) : base(mSer)
        {
            base.FSHistorySer = Ser;
        }
        //
        // GET: /admin/FSHistory/

        public ActionResult Index()
        {
            ViewBag.DateList= GetItemModel(1,"");
            return View();
        }
        public IEnumerable<FSHistoryViewModel> GetItemModel(int index, string name)
        {
            List<FSHistory> itemList = null;
            if (string.IsNullOrWhiteSpace(name))
            {
                itemList = FSHistorySer.QueryByPage(index, pageSize, out TotalPage, c=>true, c => c.id).ToList<FSHistory>();
            }
            else
            {
                itemList = FSHistorySer.QueryByPage(index, pageSize, out TotalPage, c => c.Title.Contains(name), c => c.id).ToList<FSHistory>();
            }
            int newid = 1;
            return itemList.Select(d => new FSHistoryViewModel
            {
                Nid = newid++,
                DisplayStr=d.Display == 0 ? true : false,
                Likes=d.Likes,
                id = d.id,
                Contents = d.Contents.Length > 10 ? d.Contents.Substring(0, 8) : d.Contents,
                Creater = d.Creater,
                CreaterTime = d.CreaterTime,
                Look = d.Look,
                Title=d.Title,
                UpdateTime = d.UpdateTime,
                UpdateTimeStr = d.UpdateTime.ToShortDateString(),
            });
        }
    }
}
