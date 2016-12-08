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

    /// <summary>
    /// 关于商会的控制器
    /// </summary>
    [SkipCheckLogin]
    public class CommerceController : BaseController
    {
        public CommerceController(IsysMenusServices mSer,ICommerce Commerce):base(mSer)
        {
            base.Commerce = Commerce;
        }
        //
        // GET: /admin/Commerce/

        public ActionResult Index()
        {
          ViewBag.DateList=GetItemModel(1,"").ToList();
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
        public ActionResult UpdateCommerce(string Name, string Conent,  int id)
        {
            model.Commerce CommerceModel = new model.Commerce();
            CommerceModel.Name = Name;
            CommerceModel.Contents = Conent;
            CommerceModel.id = id;
            CommerceModel.Look = 0;
            CommerceModel.UpdateTime = DateTime.Now;
            try
            {
                if (id > 0)
                {

                    Commerce.Edit(CommerceModel, new string[] { "Name", "Contents" });
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

        public ActionResult SearchCommerce(int index,string name)
        {
            var list = GetItemModel(index,name);
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
        /// <summary>
        /// 获取list将model 转 viewModel
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public IEnumerable <CommerceViewModel> GetItemModel(int index,string name)
        {
            List<Commerce>itemList=null;
            if(string.IsNullOrWhiteSpace(name))
            {
                  itemList=Commerce.QueryByPage(index, pageSize, out TotalPage, c => c.IsDelete==0, c => c.id).ToList<Commerce>();
            }
            else
            {
                itemList = Commerce.QueryByPage(index, pageSize, out TotalPage, c => c.IsDelete == 0&& c.Name.Contains(name), c => c.id).ToList<Commerce>();
            }

            return itemList.Select(d => new CommerceViewModel
            {
                IsDelete = d.IsDelete,
                LookBool = d.Look==0?true:false,
                id=d.id,
                Contents=d.Contents,
                Creater=d.Creater,
                CreateTime=d.CreateTime,
                Look=d.Look,
                Name=d.Name,
                Remark=d.Remark,
                UpdateTime=d.UpdateTime,
                UpdateTimeStr=d.UpdateTime.ToShortDateString(),
            });
        }
    }
}
