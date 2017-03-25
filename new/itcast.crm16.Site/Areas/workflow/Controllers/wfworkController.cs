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

    public class wfworkController : Controller
    {
        //public wfworkController(IwfWorkServices workSer
        //    , IwfWorkNodesServices nodeSer
        //    , IsysKeyValueServices keySer
        //    , IwfWorkBranchServices bSer)
        //{
        //    base.workSer = workSer;
        //    base.worknodesSer = nodeSer;
        //    base.keyvalSer = keySer;
        //    base.workbranchSer = bSer;
        //}
        //#region 1.0 列表

        ///// <summary>
        ///// 返回一个工作流定义信息的视图
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 负责取得wfwork表中的工作流定义信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult getlist()
        //{
        //    var list = workSer.QueryWhere(c => true).Select(c => new
        //    {
        //        c.wfID,
        //        c.wfRemark,
        //        c.wfStatus,
        //        c.wfTitle
        //    }).OrderByDescending(c => c.wfID);

        //    return Json(new { Rows = list });
        //}
        //#endregion

        //#region 2.0 新增

        //[HttpGet]
        //public ActionResult add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult add(wfWorkView model)
        //{
        //    //1.0 补全model中是属性
        //    model.fCreateTime = DateTime.Now;
        //    model.fUpdateTime = DateTime.Now;
        //    model.fCreatorID = UserMgr.GetUserInfo().uID;

        //    //2.0 将model转换成wfWork
        //    workSer.Add(model.EntityMap());
        //    workSer.SaveChanges();

        //    return WriteSuccess("新增成功");

        //}

        //#endregion

        //#region 3.0 设置工作流节点

        ///// <summary>
        ///// 返回视图
        ///// id:代表的是wfwork表中主键 wfid
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult setNodes(int id)
        //{
        //    ViewBag.wfid = id;

        //    return View();
        //}

        ///// <summary>
        ///// 负责将当前工作流下的所有节点数据组装成页面要求的json格式
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="n"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult setNodes(int id, FormCollection form)
        //{
        //    var list = worknodesSer.QueryJoin(c => c.wfID == id, new string[] { "sysKeyValue" })
        //        .Select(c => new
        //        {
        //            c.wfnID,
        //            c.wfnOrderNo,
        //            NodeTypeName = c.sysKeyValue.KName,
        //            c.wfNodeType,
        //            c.wfNodeTitle,
        //            RoleTypeName = c.sysKeyValue1.KName
        //            ,
        //            BizProcess = c.wfNodeType == (int)Enums.ENodeType.ProcessNode ? c.wfnBizMethod + "(目标天数/金额," + c.wfnMaxNum + ")" : ""
        //        }).OrderBy(c => c.wfnOrderNo);

        //    return Json(new { Rows = list });
        //}

        //#endregion

        //#region 4.0 新增节点

        //[HttpGet, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult addNode()
        //{
        //    //1.0 设置ViewBag.nodetypes和ViewBag.roletypes 
        //    SetTypes();

        //    return View();
        //}

        ///// <summary>
        ///// 获取syskeyvalue中ktype=2和 ktype=3的数据
        ///// </summary>
        //private void SetTypes()
        //{
        //    var list = keyvalSer.QueryWhere(c => c.KType == 2 || c.KType == 3);

        //    SelectList roletypelist = new SelectList(list.FindAll(c => c.KType == 2), "KID", "KName");
        //    SelectList nodetypelist = new SelectList(list.FindAll(c => c.KType == 3), "KID", "KName");
        //    ViewBag.nodetypes = nodetypelist;
        //    ViewBag.roletypes = roletypelist;
        //}

        ///// <summary>
        ///// 根据传入的wfid新增一个节点数据
        ///// id:wfwork表的主键 wfid
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult addNode(int id, wfWorkNodesView model)
        //{
        //    //1.0 补全数据
        //    model.wfID = id;
        //    model.wfnOrderNo = 1;
        //    model.fCreateTime = DateTime.Now;
        //    model.fUpdateTime = DateTime.Now;
        //    model.fCreatorID = UserMgr.GetUserInfo().uID;

        //    //2.0 补全排序号
        //    var nodes = worknodesSer.QueryWhere(c => c.wfID == id);
        //    if (nodes != null && nodes.Any())
        //    {
        //        int neworderno = nodes.Count + 1;
        //        model.wfnOrderNo = neworderno;
        //    }


        //    //3.0 将model转换成wfWorkNodes实体对象
        //    worknodesSer.Add(model.EntityMap());
        //    worknodesSer.SaveChanges();

        //    return WriteSuccess("新增成功");

        //}
        //#endregion

        //#region 5.0 上移操作

        ///// <summary>
        ///// 上移操作
        ///// </summary>
        ///// <param name="id">代表节点表的主键 wfnid</param>
        ///// <returns></returns>
        //[HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult toup(int id)
        //{
        //    //1.0 根据id获取当前要上移的节点对象
        //    var currentNode = worknodesSer.QueryWhere(c => c.wfnID == id).FirstOrDefault();

        //    //2.0 判断当前选择的节点类型如果是结束节点，则不能再上移
        //    if (currentNode.wfNodeType == (int)Enums.ENodeType.EndtNode)
        //    {
        //        return WriteError("结束节点不能再上移");
        //    }

        //    //3.0 上移操作
        //    //3.0.1 获取当前要上移的节点的上一个节点对象
        //    int preSortNo = currentNode.wfnOrderNo - 1;
        //    var preNode = worknodesSer.QueryWhere(c => c.wfID == currentNode.wfID && c.wfnOrderNo == preSortNo).FirstOrDefault();

        //    //3.0.2判断上一个节点如果是一个开始节点，则提醒不能上移
        //    if (preNode.wfNodeType == (int)Enums.ENodeType.StartNode)
        //    {
        //        return WriteError("当前要移动的节点已经到了最顶端，不能再上移");
        //    }

        //    //3.0.2 将当前节点和上一个节点中的wfnOrderNo值对调
        //    currentNode.wfnOrderNo -= 1;
        //    preNode.wfnOrderNo += 1;

        //    using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
        //    {
        //        worknodesSer.SaveChanges();

        //        scop.Complete();
        //    }

        //    //4.0 
        //    return WriteSuccess("上移成功");
        //}

        //#endregion

        //#region 6.0 下移
        ///// <summary>
        ///// 下移操作
        ///// </summary>
        ///// <param name="id">代表节点表的主键 wfnid</param>
        ///// <returns></returns>
        //[HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult todown(int id)
        //{
        //    //1.0 根据id获取当前要下移的节点对象
        //    var currentNode = worknodesSer.QueryWhere(c => c.wfnID == id).FirstOrDefault();

        //    //2.0 判断当前选择的节点类型如果是开始节点，则不能再下移
        //    if (currentNode.wfNodeType == (int)Enums.ENodeType.StartNode)
        //    {
        //        return WriteError("开始节点不能再下移");
        //    }

        //    //3.0 下移操作
        //    //3.0.1 获取当前要下移的节点的下一个节点对象
        //    int nextSortNo = currentNode.wfnOrderNo + 1;
        //    var nextNode = worknodesSer.QueryWhere(c => c.wfID == currentNode.wfID && c.wfnOrderNo == nextSortNo).FirstOrDefault();

        //    //3.0.2判断下一个节点如果是一个结束节点，则提醒不能下移
        //    if (nextNode.wfNodeType == (int)Enums.ENodeType.EndtNode)
        //    {
        //        return WriteError("当前要移动的节点已经到了最低端，不能再下移");
        //    }

        //    //3.0.2 将当前节点和下一个节点中的wfnOrderNo值对调
        //    currentNode.wfnOrderNo += 1;
        //    nextNode.wfnOrderNo -= 1;

        //    using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
        //    {
        //        worknodesSer.SaveChanges();

        //        scop.Complete();
        //    }

        //    //4.0 
        //    return WriteSuccess("下移成功");
        //}

        //#endregion

        //#region 7.0 物理删除选择的节点
        ///// <summary>
        ///// 物理删除选择的节点，同时要将当前节点下面的所有节点排序号减一
        ///// </summary>
        ///// <param name="id">代表节点的主键 wfnid</param>
        ///// <returns></returns>
        //[HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult delNode(int id)
        //{
        //    //1.0 删除
        //    var node = worknodesSer.QueryWhere(c => c.wfnID == id).FirstOrDefault();
        //    worknodesSer.Delete(node, true);

        //    //2.0 更新下面的节点排序号
        //    var nextNodes = worknodesSer.QueryWhere(c => c.wfID == node.wfID && c.wfnOrderNo > node.wfnOrderNo);
        //    nextNodes.ForEach(c => c.wfnOrderNo -= 1);

        //    //3.0开启分布式事务进行操作
        //    using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
        //    {
        //        worknodesSer.SaveChanges();

        //        scop.Complete();
        //    }

        //    //4.0 返回
        //    return WriteSuccess("物理删除成功");
        //}

        //#endregion

        //#region 8.0 节点分支相关方法

        ///// <summary>
        ///// 返回节点的分支列表视图
        ///// </summary>
        ///// <param name="id">wfnid</param>
        ///// <returns></returns>
        //[HttpGet, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult setNodeBranch(int id)
        //{
        //    ViewBag.wfnid = id;
        //    return View();
        //}

        ///// <summary>
        ///// 根据wfnid获取此节点下面的所有分支数据 (来源wfworkBranch表)
        ///// </summary>
        ///// <param name="id">wfnid</param>
        ///// <returns></returns>
        //[HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult getBranchList(int id)
        //{
        //    //1.0 
        //    var list = workbranchSer.QueryJoin(c => c.wfnID == id, new string[] { "wfWorkNodes" }).Select(
        //             c => new
        //             {
        //                 c.wfbID,
        //                 c.wfnToken,
        //                 c.wfWorkNodes1.wfNodeTitle
        //             }
        //             );

        //    return Json(new { Rows = list });
        //}

        ///// <summary>
        ///// 新增节点下面的分支数据
        ///// </summary>
        ///// <param name="id">wfnid</param>
        ///// <returns></returns>
        //[HttpGet, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult addBranch(int id)
        //{
        //    //1.0 将ViewBag.nodes 赋值
        //    var nodes = worknodesSer.QueryWhere(c => c.wfnID == id).FirstOrDefault().wfWork.wfWorkNodes;
        //    SelectList nlist = new SelectList(nodes, "wfnID", "wfNodeTitle");
        //    ViewBag.nodes = nlist;

        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    dic.Add("true", "true");
        //    dic.Add("false", "false");
        //    SelectList tlist = new SelectList(dic, "key", "value");
        //    ViewBag.tokens = tlist;

        //    return View();
        //}

        //[HttpPost, WebHelper.Attrs.SkipCheckPermiss]
        //public ActionResult addBranch(int id, wfWorkBranchView model)
        //{
        //    //1.0 补全数据
        //    model.wfnID = id;
        //    model.fCreateTime = DateTime.Now;
        //    model.fUpdateTime = DateTime.Now;
        //    model.fCreatorID = UserMgr.GetUserInfo().uID;

        //    //2.0 
        //    workbranchSer.Add(model.EntityMap());
        //    workbranchSer.SaveChanges();

        //    return WriteSuccess("新增分支成功");
        //}

        //#endregion

    }
}
