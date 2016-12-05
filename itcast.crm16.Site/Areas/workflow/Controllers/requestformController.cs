using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.workflow.Controllers
{
    using WebHelper;
    using IServices;
    using model;
    using model.ModelViews;
    using Common;
    using System.Web.WebPages;
    //using EntityMapping;

    public class requestformController:Controller
    {
        //public requestformController(IwfWorkServices workSer
        //    , IwfWorkNodesServices nodeSer
        //    , IsysKeyValueServices keySer
        //    , IwfWorkBranchServices bSer
        //    , IwfRequestFormServices formSer
        //    , IsysUserInfo_RoleServices urSer
        //    , IwfProcessServices processSer
        //    , IsysRoleServices roleSer)
        //{
        //    base.workSer = workSer;
        //    base.worknodesSer = nodeSer;
        //    base.keyvalSer = keySer;
        //    base.workbranchSer = bSer;
        //    base.requestformSer = formSer;
        //    base.userinfoRoleSer = urSer;
        //    base.processSer = processSer;
        //    base.roleSer = roleSer;
        //}

        #region 1.0 列表
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///获取当前登录用户的申请单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult getlist()
        {
            //1.0 获取uid
            //int uid = UserMgr.GetUserInfo().uID;

            ////2.0 获取当前登录用户的申请单数据
            //var list = requestformSer.QueryJoin(c => c.fCreatorID == uid, new string[] { "sysKeyValue", "wfWork" }).Select(
            //    c => new
            //    {
            //        c.wfRFID,
            //        c.wfWork.wfTitle,
            //        c.wfRFTitle,
            //        c.wfRFRemark,
            //        Priority = c.sysKeyValue.KName, //优先级
            //        wfRFStatusName = c.sysKeyValue1.KName //当前申请单的状态
            //        ,
            //        c.wfRFNum,
            //        c.wfRFStatus
            //    }
            //    );

            //return Json(new { Rows = list });
            return Json("");
        }
        #endregion

        #region 2.0 提交申请单

        [HttpGet]
        public ActionResult add()
        {
            //1.0 给ViewBag.wfworks设置：来源于wfwork表中的所有有效数据
            SetWfWorks();

            //2.0 ViewBag.Prioritys:来源于syskeyvalue表中ktype=4
            SetPrioritys();

            return View();
        }

        private void SetPrioritys()
        {
            //var list = keyvalSer.QueryWhere(c => c.KType == 4);
            //SelectList clist = new SelectList(list, "KID", "KName");
            //ViewBag.Prioritys = clist;
        }

        private void SetWfWorks()
        {
            //var list = workSer.QueryWhere(c => c.wfStatus == (int)Enums.EStatus.Normal);
            //SelectList clist = new SelectList(list, "wfID", "wfTitle");
            //ViewBag.wfworks = clist;
        }

        [HttpPost]
        public ActionResult add(wfRequestFormView model)
        {
            //int uid = UserMgr.GetUserInfo().uID;
            //using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
            //{
            //    //1.0 向wfRequestForm表中插入一条申请单数据
            //    model.fCreateTime = DateTime.Now;
            //    model.fCreatorID = UserMgr.GetUserInfo().uID;
            //    model.fUpdateTime = DateTime.Now;
            //    model.wfRFStatus = (int)Enums.EPorcessStatus.Processing;

            //    //1.0.2 将实体追加到EF容器中
            //    wfRequestForm entity = model.EntityMap();
            //    requestformSer.Add(entity);

            //    //1.0.3 统一保存 ,entity.wfRFID=自增主键
            //    requestformSer.SaveChanges();

            //    //2.0 向wfprocess表中插入一条申请通过的明细数据（相当于是工作流的第一个节点）
            //    //2.0.1 获取当前工作流的第一个节点和第二个节点
            //    var nodeList = worknodesSer.QueryWhere(c => c.wfID == model.wfID).OrderBy(c => c.wfnOrderNo).ToList();
            //    //2.0.2 判断nodelist的长度是否大于2
            //    if (nodeList.Count() <= 2)
            //    {
            //        return WriteError("当前工作流程节点设定异常，请联系管理员");
            //    }
            //    var fistNode = nodeList[0];
            //    var sedNode = nodeList[1];

            //    //2.0.3 获取当前登录用所在的角色id
            //    sysUserInfo_Role urmodel = userinfoRoleSer.QueryWhere(c => c.uID == uid).FirstOrDefault();

            //    wfProcess fistProcess = new wfProcess()
            //    {
            //        fCreateTime = DateTime.Now,
            //        fUpdateTime = DateTime.Now,
            //        fCreatorID = uid,
            //        wfnID = fistNode.wfnID,
            //        wfRFStatus = (int)Enums.EPorcessStatus.Pass,
            //        wfPDescription = "申请单提交成功",
            //        wfPExt1 = uid.ToString(),
            //        wfRFID = entity.wfRFID,
            //        wfProcessor = urmodel.rID
            //    };
            //    //将第一个处理明细数据添加到EF容器中
            //    processSer.Add(fistProcess);

            //    //3.0向wfprocess表中插入一条工作流第2个节点的审核中数据
            //    //3.0.1 
            //    int sedRoleType = sedNode.wfnRoleType;
            //    int groupid = UserMgr.GetUserInfo().uWorkGroupID.HasValue ? UserMgr.GetUserInfo().uWorkGroupID.Value : 0; // int? :可空类型 Datetime?
            //    //3.0.2 去表sysRole中查找出rid  select rid from sysRole where eDepID=groupid and RoleType=sedRoleType
            //    sysRole role = roleSer.QueryWhere(c => c.eDepID == groupid && c.RoleType == sedRoleType).FirstOrDefault();
            //    if (role == null)
            //    {
            //        return WriteError("当前节点审批角色不存在，请联系管理员");
            //    }

            //    wfProcess sedProcess = new wfProcess()
            //    {
            //        fCreateTime = DateTime.Now,
            //        fUpdateTime = DateTime.Now,
            //        fCreatorID = uid,
            //        wfnID = sedNode.wfnID,
            //        wfRFStatus = (int)Enums.EPorcessStatus.Processing,
            //        wfPDescription = null,
            //        wfRFID = entity.wfRFID,
            //        wfProcessor = role.rID //1 获取第二个节点中的审批角色类型 wfnroletype 2 结合当前登录用户所在的工作组id 去sysrole表获取rid
            //    };

            //    //将第二个处理明细数据添加到EF容器中
            //    processSer.Add(sedProcess);

            //    //
            //    processSer.SaveChanges();

            //    //事务提交
            //    scop.Complete();
            //}

            ////返回成功
            //return WriteSuccess("新增成功");
            return Json("");
        }
        #endregion

        #region 3.0 查看审核进度

        /// <summary>
        /// 返回一个视图，在视图上直接显示审核进度 ,属于应该从表wfProcess中获取
        /// </summary>
        /// <param name="id">代表申请单表wfRequestForm 的主键 wfrfid</param>
        /// <returns></returns>
        public ActionResult getDetils(int id)
        {
            //var list = processSer.QueryJoin(c => c.wfRFID == id, new string[] { "wfRequestForm", "sysKeyvalue", "wfWorkNodes" });

            //return View(list);
            return View();
        }

        #endregion

    }
}
