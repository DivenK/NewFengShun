using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper
{
    using itcast.crm16.IServices;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;


    public class BaseController : Controller
    {
        #region 1.0 定义所有表的Services的接口成员

        protected IsysMenusServices menuSer;
        protected IsysUserInfoServices userinfoSer;
        protected INewServices news;
        protected INewTypeServices newsType;
        protected IManageServices manageSer;
        protected ICommerce Commerce;
        protected IMemberDynamicServices memberSer;
        protected IArticleTypeServices articleSer;
        protected IFSHistoryService FSHistorySer;
        protected IMemberMsgServices memberMsgSer;
        protected ISiteSetService siteService;
        protected IPolicyServices policySer;
        protected string url = "";//用来首页的菜单选项
        protected int pageSize = 10;
        protected int TotalPage;//总行数
        protected int PageCount;//有多少页
        protected string stringStr = @"<(.[^>]*)/>";

        public BaseController(IsysMenusServices mSer, ISiteSetService siteService, string url = "")
        {
            menuSer = mSer;
            var list = menuSer.QueryWhere(c => c.mStatus == 0).OrderBy(c => c.mSortid).ToList();
            ViewBag.url = url;
            //获取当前左边的菜单
            // var permissMenus = menuSer.RunProc<sysMenus>("USP_GetMenusForUserid16 " + UserMgr.GetUserInfo().uID);
            ViewBag.mList = list;
            this.siteService = siteService;
            var linkList = siteService.QueryWhere(p => p.type.Equals(2) && p.Look==0).OrderByDescending(o => o.CreatTime).ToList();
            ViewBag.linkList = linkList;
        }
        #endregion

        #region 2.0 封装ajax的返回方法

        protected ActionResult WriteSuccess(string msg)
        {
            return Json(new { status = 0, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteSuccess(string msg, object data)
        {
            return Json(new { status = 0, msg = msg, datas = data }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteError(string msg)
        {
            return Json(new { status = 1, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult WriteError(Exception ex)
        {
            //1.0 获取ex的最底层的内部异常信息
            Exception innerExp = ex.InnerException == null ? ex : ex.InnerException;
            while (innerExp.InnerException != null)
            {
                innerExp = innerExp.InnerException;
            }

            return Json(new { status = 1, msg = innerExp.Message }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        protected void SetStatus()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(0, "正常");
            dic.Add(1, "停用");

            SelectList clist = new SelectList(dic, "Key", "Value");

            ViewBag.status = clist;

        }
        /// <summary>
        /// 获取分页需要所有  ViewBag.TotalPage   ViewBag.PageSize ViewBag.PageCount
        /// </summary>
        /// <returns></returns>
        protected void SetViewBagPage()
        {
            ViewBag.TotalPage = TotalPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = PageCount = (int)Math.Ceiling(TotalPage * 1.0 / pageSize);
        }

        /// <summary>
        /// 正则匹配图片
        /// </summary>
        /// <param name="conters"></param>
        /// <returns></returns>
        public string GetImageUrl(string conters)
        {
            System.Text.RegularExpressions.Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(conters);
            if (matches.Count > 0)
            { return matches[0].Groups["imgUrl"].Value; }
            return string.Empty;
        }
    }
}
