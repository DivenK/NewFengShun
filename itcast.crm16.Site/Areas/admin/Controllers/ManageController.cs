using itcast.crm16.IServices;
using itcast.crm16.model;
using itcast.crm16.Site.Models;
using itcast.crm16.WebHelper;
using itcast.crm16.WebHelper.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    [SkipCheckLogin]
    public class ManageController : BaseController
    {
        public ManageController(IsysMenusServices mMer, IManageServices manageSer) : base(mMer)
        {
            base.manageSer = manageSer;
        }


        //
        // GET: /admin/Manage/

        public ActionResult Index()
        {
            int count = 0;
            //获取企业数据信息
            List<Manage> manageList = manageSer.QueryByPage(1, 15, out count, c => true, c => c.id);
            ViewBag.manageList = manageList;

            return View();
        }


        public ActionResult GetManageData(int PageIndex, int PageSize, string searchContent)
        {
            int count = 0;
            List<Manage> manageList = null;
            //判断是否模糊查询
            //获取企业数据信息
            if (string.IsNullOrEmpty(searchContent))
            {
                manageList = manageSer.QueryByPage(PageIndex, PageSize, out count, c => true, c => c.id);
            }
            else
            {
                manageList = manageSer.QueryByPage(PageIndex, PageSize, out count, c => c.Title.Contains(searchContent), c => c.id);
            }
            if (manageList != null)
            {
                return WriteSuccess("获取成功", new { TotalPage = count, Content = manageList.Select(c => new { c.id, c.Title, CreateTime= c.CreateTime.ToString("yyyy/MM/dd HH:mm:ss"), c.Creator, c.Author, c.Look }) });
            }
            else
            {
                return WriteError("获取数据失败");
            }
        }

        [ValidateInput(false)]
        public ActionResult Change(int flag,int id, string title,string conter,string author)
        {
            if(string.IsNullOrEmpty(title))
            {
                return WriteError("标题不能为空");
            }
            if(flag==0)//新增
            {
                Manage addModel = new Manage();
                addModel.Title = title;
                addModel.Conter = conter;
                addModel.Author = author;
                addModel.CreateTime = DateTime.Now;
                addModel.Creator = "";
                manageSer.Add(addModel);
               int retAdd=  manageSer.SaveChanges();
                if(retAdd==1)
                {
                   return WriteSuccess("添加成功");
                }
                else
                {
                  return  WriteError("添加失败");
                }
            }
            else
            {
                Manage editModel = manageSer.QueryWhere(c => c.id == id).FirstOrDefault();
                if(editModel==null)
                {
                    return WriteError("该信息不存在");
                }
                editModel.Author = author;
                editModel.Conter = conter;
                editModel.Title = title;
                manageSer.Edit(editModel, new string[] { "Author", "Conter", "Title" });
                int editRet = manageSer.SaveChanges();
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

        public ActionResult ChangeLook(int id)
        {
            Manage changeModel = manageSer.QueryWhere(c => c.id == id).FirstOrDefault();
            if(changeModel==null)
            {
                return WriteError("该信息不存在");
            }
            changeModel.Look = changeModel.Look == 0 ? 1 : 0;
            manageSer.Edit(changeModel, new string[] { "Look" });
            int changeRet = manageSer.SaveChanges();
            if (changeRet == 1)
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
            Manage delModel = manageSer.QueryWhere(c => c.id == id).FirstOrDefault();
            if (delModel == null)
            {
                return WriteError("该信息不存在");
            }
            manageSer.Delete(delModel, false);
            int delRet= manageSer.SaveChanges();
            if (delRet == 1)
            {
                return WriteSuccess("删除成功");
            }
            else
            {
                return WriteError("删除失败");
            }
        }

    }
}
