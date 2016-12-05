using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.WebHelper
{
    using itcast.crm16.IServices;
    using System.Web.Mvc;


    public class BaseController : Controller
    {
        #region 1.0 定义所有表的Services的接口成员

        //protected IsysFunctionServices funSer;
        //protected IsysKeyValueServices keyvalSer;
        //protected IsysMenusServices menuSer;
        //protected IsysOrganStructServices organSer;
        //protected IsysPermissListServices permissSer;
        //protected IsysRoleServices roleSer;
        //protected IsysUserInfoServices userinfoSer;
        //protected IsysUserInfo_RoleServices userinfoRoleSer;
        //protected IwfProcessServices processSer;
        //protected IwfRequestFormServices requestformSer;
        //protected IwfWorkServices workSer;
        //protected IwfWorkBranchServices workbranchSer;
        //protected IwfWorkNodesServices worknodesSer;

        protected IsysMenusServices menuSer;
        protected IsysUserInfoServices userinfoSer;
        protected INewServices news;
        protected INewTypeServices newsType;
        protected IManageServices manageSer;
        protected int pageSize=10;
        protected int TotalPage;//总叶数
        protected int PageCount;//有多少页


        public BaseController(IsysMenusServices mSer)
        {
            menuSer = mSer;
            var list = menuSer.QueryWhere(c => c.mStatus == 0).OrderBy(c => c.mSortid).ToList();


            //获取当前左边的菜单
            // var permissMenus = menuSer.RunProc<sysMenus>("USP_GetMenusForUserid16 " + UserMgr.GetUserInfo().uID);
            ViewBag.mList = list;
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
        protected void SetViewBagPage() {
            ViewBag.TotalPage = TotalPage;
            ViewBag.PageSize = pageSize;
            ViewBag.PageCount=(int)Math.Ceiling(TotalPage * 1.0 / pageSize);
        }
    }
}
