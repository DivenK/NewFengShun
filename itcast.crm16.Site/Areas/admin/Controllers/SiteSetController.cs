using itcast.crm16.IServices;
using itcast.crm16.WebHelper;
using itcast.crm16.WebHelper.Attrs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model = itcast.crm16.model;
using itcast.crm16.model.ModelViews;
using System.Text.RegularExpressions;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    /// <summary>
    /// 控制网站设置
    /// </summary>
    public class SiteSetController : BaseController
    {
        //
        // GET: /admin/SiteSet/

        public SiteSetController(IsysMenusServices mSer,ISiteSetService siteService):base(mSer)
        {
            this.siteService = siteService;
        }
        public ActionResult Index(int index=1,int pageSize=10,int type=0)
        {
           ViewBag.items=siteService.GetItems(index, pageSize, type);
            return View();
        }

        /// <summary>
        /// 后面抽取出公共方法，文件的下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="filenName"></param>
        /// <returns></returns>
        public ActionResult SystemWork(string filePath, string filenName)
        {
            filePath = Server.MapPath(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filenName));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }

        public ActionResult UpdateDispay(int id,int look)
        {
            if (id <= 0)
            {
                return WriteError("参数错误！请重新尝试");
            }
            model.SiteSet model = new model.SiteSet();
            model.id = id;
            model.Look = look;
            siteService.Edit(model,new string[] { "Look" });
            var result = siteService.SaveChanges();
            if (result > 0)
            {
               return WriteSuccess("修改成功！");
            }
            return WriteError("修改失败！");
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Content"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Update(string title, string Content, int type,int id)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(Content))
            {
                return WriteError("标题或者内容不能为空！");
            }
            model.SiteSet model = new model.SiteSet();
            model.id = id;
            model.Contents = Content;
            model.Title = title;
            model.Image = GetImageUrl(Content);
            model.type = type;
            if (id > 0)
            {
                siteService.Edit(model, new string[] { "Title","Contents","Image","type" });
            }
            else
            {
                model.Creator = "丰顺商会";
                model.CreatTime = DateTime.Now;
                model.Look = 0;
                siteService.Add(model);
            }
            var result = siteService.SaveChanges();
            if (result > 0)
            {
                return WriteSuccess("操作成功！");
            }
            return WriteError("操作失败！");
        }

        public ActionResult DelModel(int id)
        {
            if (id > 0)
            {
                model.SiteSet model = new crm16.model.SiteSet();
                model.id = id;
                siteService.Delete(model, false);
                var result = siteService.SaveChanges();
                if (result > 1)
                {
                    return WriteSuccess("删除成功！");
                }
                return WriteError("删除失败！");
            }
            return WriteError("参数错误！");
        }

        public string GetImageUrl(string conters)
        {
            System.Text.RegularExpressions.Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 

            MatchCollection matches = regImg.Matches(conters);
            if (matches.Count > 0)
            { return matches[0].Groups["imgUrl"].Value; }
            return string.Empty;
        }

        public ActionResult GetItems(int index = 1, int pageSize = 10, int type = 0)
        {
           var list= siteService.GetItems(index, pageSize, type);
            return Json(list);
        }
        public ActionResult GetModel(int id)
        {
            if (id > 0)
            {
                model.SiteSet model = siteService.QueryWhere(c => c.id == id).First();
                return Json(model);
            }
            return WriteError("参数错误！");
        }
    }
}
