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
    public class memberController : BaseController
    {
        public memberController(IsysMenusServices mSer, IMemberMsgServices memberMsgSer,IMemberDynamicServices memberDynamicSer) : base(mSer)
        {
            base.memberMsgSer = memberMsgSer;
            base.memberSer = memberDynamicSer;
        }

        //
        // GET: /member/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMember(MemberMsg entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.IsDelete = false;
            memberMsgSer.Add(entity);
            int addRet = memberMsgSer.SaveChanges();
            if(addRet>0)
            {
                return WriteSuccess("");
            }
            else
            {
                return WriteError("");
            }
        }

        public ActionResult MemberDynamic(int id)
        {
            int count = 0;
            List<MemberDynamic> memberList = memberSer.GetMemberMsgByPage(1, 15, out count, id, false);
            ViewBag.MemberList = memberList;
            ViewBag.Type = id;
            ViewBag.TotalPage = Math.Ceiling(count * 1.0 / 15);
            return View();
        }

        public ActionResult MemberDynamicByPage(int pageIndex,int id)
        {
            int count = 0;
            List<MemberDynamic> memberList = memberSer.GetMemberMsgByPage(pageIndex, 15, out count, id, false);
            return WriteSuccess("获取成功", memberList.Select(c=>new {c.id,c.Title,CreateTime=c.CreateTime.ToString("yyyy-MM-dd") }));
        }

        public ActionResult MemberDynamicDetail(int id)
        {
            MemberDynamic entity = memberSer.GetMemberMsgById(id);

            ViewBag.Type = entity.Type;
            if (entity!=null)
            {
                memberSer.SaveChanges();
                entity.LookCount += 1;
                memberSer.Edit(entity, new string[] { "LookCount" });
                memberSer.SaveChanges();
            }
            ViewBag.Entity = entity;
            return View();
        }
    }
}
