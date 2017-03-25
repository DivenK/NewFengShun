using itcast.crm16.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.IServices;
using itcast.crm16.model;
using itcast.crm16.Common;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    public class PolicyController : BaseController
    {
        public PolicyController(IsysMenusServices mSer,IPolicyServices policySer) : base(mSer)
        {
            base.policySer = policySer;
        }

        //
        // GET: /admin/Policy/

        public ActionResult Index()
        {

            List<Policy> policyList = policySer.QueryByPage(1, pageSize, out TotalPage, c => c.IsDelete == false, c => c.id);
            ViewBag.Policy = policyList;

            base.SetViewBagPage();

            return View();
        }

        public ActionResult GetContentByPage(int pageIndex, int pageSize, string searchMsg)
        {
            int count = 0;
            List<Policy> policyList = null;
            if (string.IsNullOrEmpty(searchMsg))
            {
                policyList = policySer.QueryByPage(pageIndex, pageSize, out count, c => c.IsDelete == false, c => c.id);
            }
            else
            {
                policyList = policySer.QueryByPage(pageIndex, pageSize, out count, c => c.Title.Contains(searchMsg), c => c.id);
            }


            if (policyList != null)
            {
                return WriteSuccess("获取成功", new
                {
                    TotalPage = count,
                    Content = policyList.Select(
                    c => new
                    {
                        c.id,
                        c.IsComment,
                        c.Title,
                        c.Praise,
                        c.Creator,
                        CreateTime = c.CreateTime.ToString("yyyy-MM-dd")
                    })
                });
            }
            else
            {
                return WriteError("获取数据失败");
            }
        }

        [ValidateInput(false)]
        public ActionResult Change(int id, string title,  string content,DateTime createtime)
        {
            if (string.IsNullOrEmpty(title))
            {
                return WriteError("标题不能为空");
            }

            if (id == 0)//新增
            {
                Policy addModel = new Policy();
                addModel.Title = title;
                addModel.Content = content;
                addModel.IsComment = true;
                addModel.CreateTime = createtime;
                addModel.Creator = (Session[Keys.uinfo] as sysUserInfo).uLoginName; ;
                policySer.Add(addModel);
                int addRet = policySer.SaveChanges();
                if (addRet == 1)
                {
                    return WriteSuccess("添加成功");
                }
                else
                {
                    return WriteError("添加失败");
                }
            }
            else//修改
            {
                Policy editModel = new Policy();
                editModel.id = id;
                editModel.Title = title;
                editModel.Content = content;
                editModel.CreateTime = createtime;
                editModel.UpdateTime = DateTime.Now;
                editModel.Updator = (Session[Keys.uinfo] as sysUserInfo).uLoginName;
                policySer.Edit(editModel, new string[] { "Title", "Content",  "UpdateTime", "Updator","CreateTime" });
                int editRet = policySer.SaveChanges();
                if (editRet == 1)
                {
                    return WriteSuccess("修改成功");
                }
                else
                {
                    return WriteError("修改失败");
                }
            }
        }


        public ActionResult ChangeComment(int id, int comment)
        {
            if (id <= 0)
            {
                return WriteError("参数异常");
            }
            Policy entity = new Policy() { id = id, IsComment = comment == 1 };
            policySer.Edit(entity, new string[] { "IsComment" });
            int ret = policySer.SaveChanges();
            if (ret == 1)
            {
                return WriteSuccess("修改成功");
            }
            else
            {
                return WriteError("修改失败");
            }
        }


        public ActionResult Del(int id)
        {
            if (id == 0)
            {
                return WriteError("参数异常");
            }
            Policy delModel = new Policy() { id = id, IsDelete = true };
            policySer.Edit(delModel, new string[] { "IsDelete" });
            int delRet = policySer.SaveChanges();
            if (delRet >= 1)
            {
                return WriteSuccess("删除成功");
            }
            else
            {
                return WriteError("删除失败");
            }
        }


        public ActionResult GetPolicyById(int id)
        {
            if (id <= 0)
            {
                return WriteError("参数异常");
            }
            Policy entity = policySer.QueryWhere(c => c.id == id && c.IsDelete ==false).FirstOrDefault();
            if (entity == null)
            {
                return WriteError("数据不存在");
            }
            else
            {
                return WriteSuccess("", new { entity.id, entity.Title, entity.Content, CreateTime=entity.CreateTime.ToString("yyyy-MM-dd") });
            }
        }

    }
}
