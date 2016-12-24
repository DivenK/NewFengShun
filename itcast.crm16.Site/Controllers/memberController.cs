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
        public memberController(IsysMenusServices mSer, IMemberMsgServices memberMsgSer) : base(mSer)
        {
            base.memberMsgSer = memberMsgSer;
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

    }
}
