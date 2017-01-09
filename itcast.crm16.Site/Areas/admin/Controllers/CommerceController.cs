using itcast.crm16.IServices;
using itcast.crm16.model;
using itcast.crm16.Site.Models;
using itcast.crm16.WebHelper;
using itcast.crm16.WebHelper.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Areas.admin.Controllers
{

    /// <summary>
    /// 关于商会的控制器
    /// </summary>
    public class CommerceController : BaseController
    {
        public CommerceController(IsysMenusServices mSer,ICommerce Commerce):base(mSer)
        {
            base.Commerce = Commerce;
        }
        //
        // GET: /admin/Commerce/

        public ActionResult Index(int type=0)
        {
            ViewBag.DateList = Commerce.GetItemModel(1, "",out TotalPage, type).ToList();
           SetViewBagPage();
           return View();
        }

        public ActionResult GetModel(int id)
        {
              var model=  Commerce.QueryWhere(c => c.IsDelete == 0 && c.id == id).FirstOrDefault();
              return Json (model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 设置是否展示在前站
        /// </summary>
        /// <param name="id"></param>
        /// <param name="look"></param>
        /// <returns></returns>
        public ActionResult UpdateLook(int id,int look)
        {
            if (id > 0)
            {
                model.Commerce model = new crm16.model.Commerce()
                {
                    id = id,
                    Look = look
                };
                try
                {
                    Commerce.Edit(model, new string[] { "Look" });
                    Commerce.SaveChanges();
                    return WriteSuccess("设置成功");
                }
                catch (Exception ex)
                {
                    return WriteError("设置失败：" + ex.Message);
                }
            }
            return WriteError("设置失败:选择值参数为空，请在尝试");
        }
        [ValidateInput(false)]//防止对文本验证，保存不了HTML元素
        public ActionResult UpdateCommerce(string Name, string Conent,  int id,int type)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return WriteError("标题不能为空！");
            }
            if (string.IsNullOrWhiteSpace(Conent))
            {
                return WriteError("内容不能为空！");
            }
            model.Commerce CommerceModel = new model.Commerce();
            CommerceModel.Name = Name;
            CommerceModel.Contents = Conent;
            CommerceModel.id = id;
            CommerceModel.Look = 0;
            CommerceModel.Type = type;
            CommerceModel.UpdateTime = DateTime.Now;
            if (CommerceModel.Type == 2)//是不是商会领导相册的那些数据。
            {
                CommerceModel.ImageUrl=GetImageUrl(CommerceModel.Contents);
            }
            try
            {
                if (id > 0)
                {

                    Commerce.Edit(CommerceModel, new string[] { "Name", "Contents", "ImageUrl" });
                }
                else
                {
                    CommerceModel.Creater = "User";
                    CommerceModel.CreateTime = DateTime.Now;
                  
                    Commerce.Add(CommerceModel);
                }
                Commerce.SaveChanges();
                return WriteSuccess("成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex.Message);
            }
        }

        public string GetImageUrl(string conters)
        {
            System.Text.RegularExpressions.Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(conters);
             if (matches.Count>0)
            { return matches[0].Groups["imgUrl"].Value; }
            return string.Empty;
        }
        public ActionResult SearchCommerce(int index,string name,int type)
        {
            var list =Commerce.GetItemModel(index,name,out TotalPage,type);
            return Json(new { rowCount = TotalPage, pageCount = ViewBag.PageCount, date = list });
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                model.Commerce model = new crm16.model.Commerce()
                {
                    id = id,
                    IsDelete=1,
                };
                try
                {
                    Commerce.Edit(model, new string[] { "IsDelete" });
                    Commerce.SaveChanges();
                    return WriteSuccess("删除成功");
                }
                catch (Exception ex)
                {
                    return WriteError("删除失败：" + ex.Message);
                }
            }
            return WriteError("删除失败:选择值参数为空，请在尝试");
        }
  
    }
}
