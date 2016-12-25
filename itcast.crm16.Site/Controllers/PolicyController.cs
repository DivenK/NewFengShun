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
    public class PolicyController : BaseController
    {
        public PolicyController(IsysMenusServices mSer, IPolicyServices policySer) : base(mSer, "/policy/index")
        {
            base.policySer = policySer;
        }

        public ActionResult Index()
        {
            int count = 0;
            List<Policy> policyList = policySer.QueryByPage(1, 15, out count, c => c.IsComment == true && c.IsDelete == false, c => c.CreateTime);
            ViewBag.MemberList = policyList;
            ViewBag.TotalPage = Math.Ceiling(count * 1.0 / 15);
            return View();
        }

        public ActionResult PolicyByPage(int pageIndex)
        {
            int count = 0;
            List<Policy> policyList = policySer.QueryByPage(pageIndex, 15, out count, c => c.IsComment == true && c.IsDelete == false, c => c.CreateTime);
            return WriteSuccess("", policyList.Select(c => new { c.id, CreateTime = c.CreateTime.ToString("yyyy-MM-dd"), c.Title }));
        }


        public ActionResult PolicyDetail(int id)
        {
            var policyList = policySer.QueryWhere(c => c.id == id && c.IsComment == true && c.IsDelete == false);
            if (policyList != null && policyList.Count > 0)
            {
                ViewBag.Entity = policyList[0];
                ViewBag.id = policyList[0].id;
            }
            return View();
        }

        public ActionResult Praise(int id, int count)
        {
            Policy editModel = new Policy() { id = id, Praise = count + 1 };
            policySer.Edit(editModel, new string[] { "Praise" });
            policySer.SaveChanges();
            return WriteSuccess("");
        }
    }
}
