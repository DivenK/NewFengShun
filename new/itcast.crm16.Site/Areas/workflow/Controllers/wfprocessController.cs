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
    using System.Reflection;

    public class wfprocessController : Controller
    {
        //public wfprocessController(IwfWorkServices workSer
        //    , IwfWorkNodesServices nodeSer
        //    , IsysKeyValueServices keySer
        //    , IwfWorkBranchServices bSer
        //    , IwfRequestFormServices formSer
        //    , IsysUserInfo_RoleServices urSer
        //    , IwfProcessServices processSer
        //    , IsysRoleServices roleSer
        //    , IsysUserInfoServices uSer)
        //{
        //    base.workSer = workSer;
        //    base.worknodesSer = nodeSer;
        //    base.keyvalSer = keySer;
        //    base.workbranchSer = bSer;
        //    base.requestformSer = formSer;
        //    base.userinfoRoleSer = urSer;
        //    base.processSer = processSer;
        //    base.roleSer = roleSer;
        //    base.userinfoSer = uSer;
        //}

        #region 1.0 我的审核单列表

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult getlist()
        {
            //1.0 获取当前登录用户的角色id
            //int uid = UserMgr.GetUserInfo().uID;
            //var rolelist = userinfoRoleSer.QueryWhere(c => c.uID == uid).Select(c => c.rID).ToList();

            ////2.0 
            //var list = processSer.QueryJoin(c => rolelist.Contains(c.wfProcessor), new string[] { "wfRequestForm", "sysKeyValue" })
            //    .Select(c => new
            //    {
            //        c.wfPID,
            //        uRealName = userinfoSer.QueryWhere(d => d.uID == c.wfRequestForm.fCreatorID).FirstOrDefault().uRealName //可以确定的是：c.wfRequestForm.CreateID
            //        ,
            //        c.wfRequestForm.wfRFRemark,
            //        c.wfRequestForm.wfRFNum,
            //        StatusName = c.sysKeyValue.KName,
            //        c.wfRFStatus
            //    }).OrderBy(c => c.wfRFStatus).ThenBy(c => c.wfPID).ToList();

            //return Json(new { Rows = list });
            return Json("");
        }

        #endregion

        #region 2.0 审批操作方法

        /// <summary>
        /// 返回一个处理明细页面视图
        /// </summary>
        /// <param name="id">处理明细wfProcess表中的主键 wfPID</param>
        /// <returns></returns>
        public ActionResult checkform(int id)
        {
            //ViewBag.wfpid = id;

            ////1.0 根据wfpid获取wfprocess的实体,在通过导航属性获取此明细数据所在的申请单下的所有明细数据
            //var processList = processSer.QueryWhere(c => c.wfPID == id).FirstOrDefault().wfRequestForm.wfProcess.OrderBy(c => c.wfPID).ToList();

            ////2.0 
            //return View(processList);
            return View();
        }

        #region 2.0.1 审核状态为拒绝操作
        /// <summary>
        /// 拒绝操作
        /// </summary>
        /// <param name="id">wfpid</param>
        /// <returns></returns>
        [HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        public ActionResult Reject(int id)
        {
            ////1.0 获取拒绝理由
            //string des = Request.Form["des"];

            //if (des.IsEmpty())
            //{
            //    return WriteError("拒绝理由必须填写");
            //}

            ////1.0 更新id所在表wfProcess中的 状态和理由
            //var crProcess = processSer.QueryWhere(c => c.wfPID == id).FirstOrDefault();
            ////1.0.1 判断当前明细数据的状态不等于处理中的状态就应该提醒用户您已经审核过了
            //if (crProcess.wfRFStatus != (int)Enums.EPorcessStatus.Processing)
            //{
            //    return WriteError("此单已经审核过，请勿重复操作");
            //}

            //crProcess.wfRFStatus = (int)Enums.EPorcessStatus.Reject;
            //crProcess.wfPDescription = des;
            //crProcess.wfPExt1 = UserMgr.GetUserInfo().uID.ToString();//当前审核人  

            ////2.0 向wfProcess表中插入一个结束数据
            ////2.0.1 获取当前工作流的最后一个节点
            //var lastNode = crProcess.wfWorkNodes.wfWork.wfWorkNodes.OrderBy(c => c.wfnOrderNo).LastOrDefault();
            //if (lastNode == null)
            //{
            //    return WriteError("当前工作流没有结束节点,联系管理员");
            //}

            //wfProcess model = new wfProcess()
            //{
            //    fCreateTime = DateTime.Now,
            //    fUpdateTime = DateTime.Now,
            //    fCreatorID = UserMgr.GetUserInfo().uID,
            //    wfnID = lastNode.wfnID,
            //    wfRFStatus = (int)Enums.EPorcessStatus.Reject,
            //    wfRFID = crProcess.wfRFID,
            //    wfPDescription = "申请单已经被拒绝",
            //    wfPExt1 = UserMgr.GetUserInfo().uID.ToString(),
            //    wfPExt2 = crProcess.wfPID
            //};
            ////将model追加到EF容器中
            //processSer.Add(model);

            ////3.0 更新申请单的状态为拒绝状态
            //var rfmodel = requestformSer.QueryWhere(c => c.wfRFID == crProcess.wfRFID).FirstOrDefault();
            //rfmodel.wfRFStatus = (int)Enums.EPorcessStatus.Reject;

            ////4.0 开启分布式事务
            //using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
            //{
            //    requestformSer.SaveChanges();

            //    scop.Complete();
            //}

            //return WriteSuccess("申请单已拒绝");
            return View();
        }
        #endregion

        #region 2.0.2 通过操作

        [HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        public ActionResult pass(int id)
        {
            //通过理由
            //string des = Request.Form["des"];
            //int uid = UserMgr.GetUserInfo().uID;


            #region 1.0 修改当前处理节点的明细数据
            ////1.0 更新id在wfProcess表中的状态为通过 1状态 2 理由 3 操作人 Ext1
            //var currentProcess = processSer.QueryWhere(c => c.wfPID == id).FirstOrDefault();
            ////1.0.1 判断当前明细数据的状态
            //if (currentProcess.wfRFStatus != (int)Enums.EPorcessStatus.Processing)
            //{
            //    return WriteError("此单已处理，请勿重复处理");
            //}

            ////1.0.2 修改 1状态 2 理由 3 操作人 Ext1
            //currentProcess.wfRFStatus = (int)Enums.EPorcessStatus.Pass;
            //currentProcess.wfPDescription = des;
            //currentProcess.wfPExt1 = uid.ToString();
            //#endregion

            //#region 2.0 通过反射执行一段逻辑判断，确定结果为true/false
            ////2.0 获取当前id所在明细数据的节点数据中获取bizMethod(请假天数,最大天数) 得到结果 true/false
            //var currentNode = currentProcess.wfWorkNodes;
            //string bizMethod = currentNode.wfnBizMethod; //当前节点的逻辑判断方法
            //decimal maxNum = currentNode.wfnMaxNum; //当前节点的最大审核权限值
            //decimal targetNum = currentProcess.wfRequestForm.wfRFNum; //当前请假单的天数

            ////2.0.1 通过反射动态执行bizMethod变量中的方法:此时是 Geq
            ////2.0.1.1从web.config中获取程序集的名字字符串格式:程序集1,程序集2,......
            //string assStr = System.Configuration.ConfigurationManager.AppSettings["bizWfAss"];
            //if (assStr.IsEmpty())
            //{
            //    return WriteError("工作流的业务逻辑类所在的程序集配置有问题，请联系管理员检查web.config中是否有配置key为bizWfAss的节点");
            //}

            ////2.0.1.2 拆解程序集为一个数组
            //string[] ass = assStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //bool isBreak = false;
            //string result = "";

            //foreach (var assName in ass)
            //{
            //    //加载bin目录下指定的程序集到内存中
            //    Assembly assbly = Assembly.Load(assName);
            //    Type[] types = assbly.GetTypes();
            //    foreach (var type in types)
            //    {
            //        //获取指定方法的MethodInfo对象
            //        MethodInfo minfo = type.GetMethod(bizMethod);
            //        if (minfo != null)
            //        {
            //            //创建type的实例
            //            object instance = Activator.CreateInstance(type);
            //            //调用方法
            //            object obj = minfo.Invoke(instance, new object[] { targetNum, maxNum });

            //            //赋值给result
            //            result = obj != null ? obj.ToString() : "";

            //            isBreak = true;
            //            break; //跳出遍历Type类型的for循环
            //        }
            //    }
            //    //跳出遍历程序集的for循环
            //    if (isBreak)
            //    {
            //        break;
            //    }
            //}
            //#endregion

            //#region 3.0 下一个节点的获取和逻辑判断
            ////3.0 根据结果和当前节点的wfnid去表wfWorkBranch中查找下一个节点的id
            //int curWfnid = currentNode.wfnID;
            //var nextNode = workbranchSer.QueryWhere(c => c.wfnToken == result && c.wfnID == curWfnid).FirstOrDefault().wfWorkNodes1;
            //if (nextNode == null)
            //{
            //    return WriteError("下一个节点无法获取，请联系管理员");
            //}

            ////4.0 判断下一个节点是否为结束几点还是处理逻辑节点
            //if (nextNode.wfNodeType == (int)Enums.ENodeType.EndtNode)
            //{
            //    #region 2.0.1 表示当前下一个节点是一个结束节点
            //    //4.0.1 如果是结束节点：向wfprocess中插入一条结束明细数据，同时修改wfRequestForm表中的状态为通过状态
            //    wfProcess nextProcess = new wfProcess()
            //    {
            //        wfRFID = currentProcess.wfRFID,
            //        fCreateTime = DateTime.Now,
            //        fUpdateTime = DateTime.Now,
            //        fCreatorID = uid,
            //        wfnID = nextNode.wfnID,
            //        wfPDescription = "审核已结束，状态为通过",
            //        wfPExt1 = uid.ToString(),
            //        wfPExt2 = currentProcess.wfPID,
            //        wfRFStatus = (int)Enums.EPorcessStatus.Pass,
            //        wfProcessor = userinfoRoleSer.QueryWhere(c => c.uID == uid).FirstOrDefault().rID
            //    };

            //    //4.0.1.1 添加到EF容器中
            //    processSer.Add(nextProcess);

            //    //4.0.1.2 更新wfRequestform的状态
            //    var rfModel = requestformSer.QueryWhere(c => c.wfRFID == currentProcess.wfRFID).FirstOrDefault();
            //    rfModel.wfRFStatus = (int)Enums.EPorcessStatus.Pass;
            //    #endregion

            //}
            //else
            //{
            //    #region 2.0.2 下一个节点是一个执行节点
            //    //4.0.0 获取当前节点的审批角色类型和当前用户所在工作组id
            //    int roletype = nextNode.wfnRoleType;
            //    int workGroupId = UserMgr.GetUserInfo().uWorkGroupID.Value;

            //    //根据上述roletype和workGroupId的查询出sysrole表中的rid
            //    var rid = roleSer.QueryWhere(c => c.RoleType == roletype && c.eDepID == workGroupId).FirstOrDefault().rID;

            //    //4.0.2如果是逻辑节点：向wfprocess中插入一条结束明细数据
            //    wfProcess nextProcess = new wfProcess()
            //    {
            //        wfRFID = currentProcess.wfRFID,
            //        fCreateTime = DateTime.Now,
            //        fUpdateTime = DateTime.Now,
            //        fCreatorID = uid,
            //        wfnID = nextNode.wfnID,
            //        wfPExt2 = currentProcess.wfPID,
            //        wfPDescription = null,
            //        wfRFStatus = (int)Enums.EPorcessStatus.Processing,
            //        wfProcessor = rid
            //    };

            //    //4.0.2.1 添加到EF容器中
            //    processSer.Add(nextProcess);
            //    #endregion
            //}
            //#endregion

            //#region 4.0 开启事务统一保存
            ////5.0 开启事务统一保存
            //using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
            //{
            //    requestformSer.SaveChanges();

            //    scop.Complete();
            //}
            //#endregion

            return Content("审核已通过");
        }

        #endregion

        #region 2.0.3 驳回上级操作

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">代表 wfProcess中的主键,当前要处理的明细数据</param>
        /// <returns></returns>
        [HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        public ActionResult back(int id)
        {
            //string des = Request.Form["des"];
            //int uid = UserMgr.GetUserInfo().uID;

            ////1.0 更新id对于的明细数据 1 状态 2 理由 3 处理人 Ext1
            //var currentProcess = processSer.QueryWhere(c => c.wfPID == id).FirstOrDefault();
            //currentProcess.wfRFStatus = (int)Enums.EPorcessStatus.back;
            //currentProcess.wfPDescription = des;
            //currentProcess.wfPExt1 = uid.ToString();

            ////2.0 继续向wfProcess中插入一个数据
            //var preProcess = processSer.QueryWhere(c => c.wfPID == currentProcess.wfPExt2).FirstOrDefault();

            ////2.0.1 new wfProcess
            //wfProcess nextProcess = new wfProcess()
            //{
            //    fCreateTime = DateTime.Now,
            //    fUpdateTime = DateTime.Now,
            //    fCreatorID = uid,
            //    wfnID = preProcess.wfnID,
            //    wfPDescription = null,
            //    wfPExt1 = null,
            //    wfPExt2 = preProcess.wfPExt2,
            //    wfProcessor = preProcess.wfProcessor,
            //    wfRFID = preProcess.wfRFID,
            //    wfRFStatus = (int)Enums.EPorcessStatus.Processing
            //};

            ////2.0.2 添加到EF容器中
            //processSer.Add(nextProcess);

            ////3.0 开启事务统一保存
            //using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
            //{
            //    processSer.SaveChanges();

            //    scop.Complete();
            //}

            ////4.0 
            return Content("审核单已经被驳回");

        }

        #endregion

        #endregion

        #endregion
    }
}
