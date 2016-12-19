using itcast.crm16.WebHelper.Attrs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    /// <summary>
    /// 控制网站设置
    /// </summary>
    [SkipCheckLogin]
    public class SiteSetController : Controller
    {
        //
        // GET: /admin/SiteSet/

        public ActionResult Index()
        {
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

    }
}
