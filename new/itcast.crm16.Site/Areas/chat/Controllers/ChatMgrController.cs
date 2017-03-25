using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.chat.Controllers
{
    using WcfChatServer;
    using WebHelper;
    using model;
    using System.Web.WebPages;

    [WebHelper.Attrs.SkipCheckPermiss]
    public class ChatMgrController 
    {
        /// <summary>
        /// 供ajax请求获取当前登录用户的聊天消息
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult GetMessage()
        //{
        //    //1.0 调用WCF中的服务方法GetMessage(uid)
        //    ChatManagerClient client = new ChatManagerClient();
        //    ChatMsg[] list = client.GetMessage(UserMgr.GetUserInfo().uID);

        //    //2.0 通过json格式响应回异步对象
        //    return WriteSuccess("ok", list);
        //}
        ///// <summary>
        ///// 供ajax请求设置当前登录用户的发送消息
        ///// </summary>
        ///// <returns></returns>
        ////[HttpPost]
        //public ActionResult SetMessage()
        //{
        //    //1.0 从浏览器端传入
        //    string touserid = Request.Params["touid"];
        //    string touserRealName = Request.Params["tourname"];
        //    string msgBody = Request.Params["msgbody"];

        //    //2.0 参数合法性验证
        //    int iuserid = touserid.AsInt();


        //    //3.0 调用WCF中的服务方法SetMessage(ChatMsg)
        //    sysUserInfo user = UserMgr.GetUserInfo();
        //    //3.0.1 实例化ChatMsg的对象
        //    var chatmsg = new ChatMsg()
        //    {
        //        ToUserId = iuserid,
        //        ToRealName = touserRealName,
        //        FromUserId = user.uID,
        //        FromRealName = user.uRealName,
        //        MessageBody = msgBody,
        //        SendTime = DateTime.Now
        //    };

        //    //3.0.2 将消息转发给聊天服务端
        //    ChatManagerClient client = new ChatManagerClient();
        //    client.SetMessage(chatmsg);

        //    //4.0 
        //    return WriteSuccess("消息发送成功");
        //}

        //[HttpPost]
        //public ActionResult GetHisotryMsg()
        //{
        //    //1.0 接收异步对象发送过来的开始时间和结束时间
        //    string begintime = Request.Form["begintime"];
        //    string endtime = Request.Form["endtime"];
        //    //参数合法性验证
        //    //2.0 获取用户id
        //    int uid = UserMgr.GetUserInfo().uID;

        //    //3.0 调用WCF
        //    ChatManagerClient client = new ChatManagerClient();
        //    var list = client.GetHistoryMsg(uid, DateTime.Parse(begintime), DateTime.Parse(endtime));

        //    //4.0 序列化给异步对象
        //    return WriteSuccess("ok", list);
        //}

    }
}
