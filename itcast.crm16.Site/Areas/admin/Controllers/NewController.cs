using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.WebHelper;
using itcast.crm16.WebHelper.Attrs;
using itcast.crm16.IServices;
using itcast.crm16.Site.Models;
using itcast.crm16.Common;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    public class NewController : BaseController
    {
        //
        // GET: /admin/New/
        public NewController(IsysMenusServices mSer, INewServices news, INewTypeServices newsType, ISiteSetService siteS) : base(mSer, siteS)
        {
            base.news = news;
            base.newsType = newsType;
        }
        public ActionResult Index()
        {
            int index = 1;
            var list = newsType.QueryWhere(c => c.IsDelete == 0);
            var newlist= GetItems(index,1, "");
            ViewBag.newList = newlist;
            ViewBag.newsType = list;
            SetViewBagPage();
            return View();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="newModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false)]//防止对文本验证，保存不了HTML元素
        public ActionResult UpdateNews(string Name, string Conent, int TypeId, int id, DateTime CreateTime, string author="")
        {
            model.New newModel = new model.New();
            newModel.Name = Name;
            newModel.Conent = Conent;
            newModel.TypeId = TypeId;
            newModel.id = id;
            newModel.IsComment = true;
            newModel.display = 0;
            newModel.CreatTime = CreateTime;
            newModel.Author = author;
            try
            {
                if (id > 0)
                {

                    news.Edit(newModel, new string[] { "Name", "Conent", "IsComment", "TypeId", "display", "CreatTime", "Author" });
                }
                else
                {
                    newModel.Creator = "丰顺商会";
                    news.Add(newModel);
                }
                news.SaveChanges();
                return WriteSuccess("成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex.Message);
            }
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelelNews(int id)
        {
            if (id > 0)
            {
                model.New model = new crm16.model.New()
                {
                    id = id,
                    IsDelete = 1
                };
                try
                {
                    news.Edit(model, new string[] { "IsDelete" });
                    news.SaveChanges();
                    return WriteSuccess("删除成功");
                }
                catch (Exception ex)
                {
                    return WriteError("删除失败：" + ex.Message);
                }
            }
            return WriteError("删除失败:选择值参数为空，请在尝试");
        }
        /// <summary>
        /// 在网站显示状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateDispay(int id, int display)
        {
            if (id > 0)
            {
                model.New model = new crm16.model.New()
                {
                    id = id,
                    display = display
                };
                try
                {
                    news.Edit(model, new string[] { "display" });
                    news.SaveChanges();
                    return WriteSuccess("设置成功");
                }
                catch (Exception ex)
                {
                    return WriteError("设置失败：" + ex.Message);
                }
            }
            return WriteError("设置失败:选择值参数为空，请在尝试");
        }

        public ActionResult GetModel(int id)
        {
            if (id > 0)
            {
                var model = news.QueryWhere(c => c.IsDelete == 0 && c.id == id).FirstOrDefault();
                return Json(new {
                    model.id,
                    model.TypeId,
                    model.Conent,
                    CreatTime = model.CreatTime.ToyyyyMMddDateTime(),
                    model.Name,
                    model.Author
                });
            }
            return WriteError("修改的索引不能小于0");
        }

        public ActionResult GetList(int index, int typeId,string Name)
        {
            var pagelist=GetItems(index, typeId, Name);
            SetViewBagPage();
            var page = new { index = index, count = TotalPage,pageCount=PageCount};
            return Json(new { page = page, rows = pagelist });
        }

        public IEnumerable<dynamic> GetItems(int index, int typeId, string Name)
        {
            
            int ids = 1;
            if (index > 1)
            {
                ids = (index - 1) * 10 + ids;
            }
            var list = newsType.QueryWhere(c => c.IsDelete == 0);
            var pagelist = news.NewPageList(index, typeId, Name, out TotalPage).Select(c => new NewViewModel
            {
                Nid = ids++,
                id = c.id,
                TypeName = list.Where(s => s.id == c.TypeId).First().TypeName,
                Name = c.Name,
                Conent = c.Conent.Substring(0, 4) + "...",
                Author = c.Author,
                display = c.display,
                CreatTimeStr = c.CreatTime.ToyyyyMMddDateTime(),
                TypeId = c.TypeId,
                CreatTime = c.CreatTime,
                displayBool = c.display == 0 ? true : false
            }).ToList();
            return pagelist;
        }
    }
}
