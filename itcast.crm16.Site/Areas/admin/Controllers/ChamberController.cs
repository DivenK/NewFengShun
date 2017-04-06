using System;
using itcast.crm16.model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.WebHelper;
using itcast.crm16.IServices;
using itcast.crm16.Site.Models;
using itcast.crm16.WebHelper.Attrs;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
   
    public class ChamberController : BaseController
    {
        public ChamberController(IsysMenusServices mSer, IFSHistoryService Ser) : base(mSer)
        {
            base.FSHistorySer = Ser;
        }
        //
        // GET: /admin/Chamber/

        public ActionResult Index()
        {

            ViewBag.DateList = FSHistorySer.GetItemModel(1, out TotalPage, "",10,1);
            SetViewBagPage();
            return View();
        }

        public ActionResult GetModel(int id)
        {
            if (id <= 0)
            {
                return WriteError("参数错误");
            }
            var model = FSHistorySer.QueryWhere(c => c.id == id).FirstOrDefault();
            return Json(model);
        }
        [ValidateInput(false)]//防止对文本验证，保存不了HTML元素
        public ActionResult UpdateFsHistory(string title, string Content, int id,string author)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return WriteError("标题不能为空");
            }
            if (string.IsNullOrWhiteSpace(Content))
            {
                return WriteError("内容不能为空");
            }
            FSHistory model = new FSHistory();
            model.Title = title;
            model.Contents = Content;
            model.Creater = author;
            model.Image = GetImageUrl(Content);
            if (id > 0)//是不是编辑
            {
                model.id = id;
                FSHistorySer.Edit(model, new string[] { "Title", "Contents", "Image", "Creater" });
            }
            else
            {
                model.Likes = 150;//默认有150个点赞
                model.Look = 200;//默认有250浏览次数
          
                model.Type = 1;
                model.CreaterTime = model.UpdateTime = DateTime.Now;
                FSHistorySer.Add(model);
            }
            var result = FSHistorySer.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("操作成功！");
            }
            return WriteError("操作失败");
        }
        /// <summary>
        /// 给前台页面浏览次数
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateLook(int id)
        {
            if (id <= 0)
            {
                return WriteError("数据错误");
            }
            var model = FSHistorySer.QueryWhere(c => c.id == id).FirstOrDefault();
            FSHistory newModel = new FSHistory();
            newModel.id = id;
            newModel.Look = model.Look + 1;
            FSHistorySer.Edit(newModel, new string[] { "Look" });
            var result = FSHistorySer.SaveChanges();
            return WriteSuccess("浏览成功");
        }

        /// <summary>
        /// 给前台页面浏览次数
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateDisplay(int id, int val)
        {
            if (id <= 0)
            {
                return WriteError("数据错误");
            }
            FSHistory newModel = new FSHistory();
            newModel.id = id;
            newModel.Display = val;
            FSHistorySer.Edit(newModel, new string[] { "Display" });
            var result = FSHistorySer.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("设置成功");
            }
            return WriteError("设置失败");
        }

        /// <summary>
        /// 网站前台的点赞
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateLIkes(int id)
        {
            if (id <= 0)
            {
                return WriteError("数据错误");
            }
            var model = FSHistorySer.QueryWhere(c => c.id == id).FirstOrDefault();
            FSHistory newModel = new FSHistory();
            newModel.id = id;
            newModel.Likes = model.Likes + 1;
            FSHistorySer.Edit(newModel, new string[] { "Likes" });
            var result = FSHistorySer.SaveChanges();
            return WriteSuccess("点赞成功");
        }

        public ActionResult DeleteFSHistory(int id)
        {
            var model = FSHistorySer.QueryWhere(c => c.id == id).FirstOrDefault();
            FSHistorySer.Delete(model, false);
            var result = FSHistorySer.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("删除成功");
            }
            return WriteError("删除失败");
        }

        public ActionResult SearchCommerce(int index, string name)
        {
            var list = FSHistorySer.GetItemModel(index, out TotalPage, name,10,1);
            return Json(new { rowCount = TotalPage, pageCount = ViewBag.PageCount, date = list });
        }

    }
}
