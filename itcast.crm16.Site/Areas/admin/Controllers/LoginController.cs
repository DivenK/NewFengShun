using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    using itcast.crm16.model.ModelViews;
    using IServices;
    using itcast.crm16.WebHelper;
    using itcast.crm16.Common;
    using System.Web.WebPages;
    using itcast.crm16.WebHelper.Attrs;
    using model;

    [SkipCheckLogin]
    [SkipCheckPermiss]
    public class LoginController : BaseController
    {
        //public LoginController(IsysUserInfoServices userSer, IsysPermissListServices pser)
        //{
        //    base.userinfoSer = userSer;
        //    base.permissSer = pser;
        //}

        public LoginController(IsysUserInfoServices userifoMenus, IsysMenusServices mSer) : base(mSer)
        {
            base.userinfoSer = userinfoSer;
        }

        #region 1.0 登录
        [HttpGet]
        public ActionResult Login()
        {
            //1.0 判断浏览器是否将cookie发送过来了
            LoginInfo info = new LoginInfo() { LoginName = "admin", LoginPWD = "123456", IsReMember = false };
            if (Request.Cookies[Keys.isremember] != null)
            {
                info.IsReMember = true;
            }
            return PartialView();
            //return PartialView(info);
        }

        /// <summary>
        /// 被异步对象来进行post请求的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            try
            {
                //1.0 验证码检查
                string vcodeFromSession = string.Empty;
                if (Session[Keys.Vcode] != null)
                {
                    vcodeFromSession = Session[Keys.Vcode].ToString();
                }

                if (model.VCode.IsEmpty()
                    || model.VCode.Equals(vcodeFromSession, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return WriteError("验证码错误，请重新输入");
                }

                //2.0 验证用户名和密码的正确性
                string md5Pwd = Kits.MD5Entry(model.LoginPWD);

                if (model.LoginName.IsEmpty())
                {
                    return WriteError("登录账号不能为空");
                }

                if (model.LoginPWD.IsEmpty())
                {
                    return WriteError("登录密码不能为空");
                }

                var userinfo = userinfoSer.QueryWhere(c => c.uLoginName == model.LoginName &&
                       c.uLoginPWD == md5Pwd).FirstOrDefault();

                if (userinfo == null)
                {
                    return WriteError("用户名或者密码错误，请重新输入");
                }

                //3.0 将userinfo存入session中
                Session[Keys.uinfo] = userinfo;

                //4.0 判断model.isrememeber ==true 则应该写一个cookie存入到浏览器的硬盘中
                if (model.IsReMember)
                {
                    HttpCookie cookie = new HttpCookie(Keys.isremember, userinfo.uID.ToString());
                    //设置cookie的过期时间为3天
                    cookie.Expires = DateTime.Now.AddDays(3);

                    //添加到响应报文头中
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie(Keys.isremember, "");
                    //设置cookie的过期时间为3年前，一定过期
                    cookie.Expires = DateTime.Now.AddYears(-3);

                    //添加到响应报文头中
                    Response.Cookies.Add(cookie);
                }

                //5.0 将当前用户的权限按钮数据缓存起来
                //permissSer.GetPermissListByUid(userinfo.uID);

                return WriteSuccess("登录成功，正在跳转到首页...");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }
        }
        #endregion

        #region 2.0 登出

        [HttpGet]
        public ActionResult Logout()
        {
            //1.0 将session清空
            if (Session[Keys.uinfo] != null)
            {
                Session[Keys.uinfo] = null;
                Session.Abandon();
            }

            //2.0 为了真正退出，放置自动重新生成session，将cookie也清空
            HttpCookie cookie = new HttpCookie(Keys.isremember, "");
            cookie.Expires = DateTime.Now.AddYears(-3);
            Response.Cookies.Add(cookie);

            //3.0 跳转到登录页面
            return RedirectToAction("Login");
        }

        #endregion
    }
}
