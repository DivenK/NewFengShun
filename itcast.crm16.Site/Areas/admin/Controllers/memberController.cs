using itcast.crm16.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.IServices;
using itcast.crm16.model;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    public class memberController : BaseController
    {
        public memberController(IsysMenusServices mSer,IMemberDynamicServices memberSer) : base(mSer)
        {
            base.memberSer = memberSer;
        }

        //
        // GET: /admin/member/

        public ActionResult Index()
        {
            return View();
        }

    }
}
