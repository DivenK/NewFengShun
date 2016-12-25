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
    public class InterviewController : BaseController
    {
        public InterviewController(IsysMenusServices mSer, IMemberDynamicServices memberSer) : base(mSer, "/interview/index")
        {
            base.memberSer = memberSer;
        }

        //
        // GET: /Interview/

        public ActionResult Index()
        {
            int count = 0;
            ViewBag.MemberList = memberSer.GetMemberMsgByPage(1, 15, out count, 1, false);
            ViewBag.TotalPage = Math.Ceiling(count * 1.0 / 15);
            return View();
        }

        public ActionResult InterViewByPage(int pageIndex)
        {
            int count = 0;
            List<MemberDynamic> memberList = memberSer.GetMemberMsgByPage(pageIndex, 15, out count, 1, false);
            return WriteSuccess("获取成功", memberList.Select(c => new { c.id, c.Title, CreateTime = c.CreateTime.ToString("yyyy-MM-dd") }));
        }

        public ActionResult MemberDynamicDetail(int id)
        {
            MemberDynamic entity = memberSer.GetMemberMsgById(id);

            ViewBag.Type = entity.Type;

            ViewBag.Entity = entity;
            ViewBag.id = entity.id;
            ViewBag.Praise = entity.Praise;
            ViewBag.LookCount = entity.LookCount;
            return View();
        }

        public ActionResult LookAdd(int id, int lookCount)
        {
            MemberDynamic editModel = new model.MemberDynamic();
            editModel.id = id;
            editModel.LookCount = lookCount + 1;
            memberSer.Edit(editModel, new string[] { "LookCount" });
            memberSer.SaveChanges();
            return WriteSuccess("");
        }

        public ActionResult PraiseDynamic(int id, int praise)
        {
            MemberDynamic editModel = new model.MemberDynamic();
            editModel.id = id;
            editModel.Praise = praise + 1;
            memberSer.Edit(editModel, new string[] { "Praise" });
            memberSer.SaveChanges();
            return WriteSuccess("");
        }

    }
}
