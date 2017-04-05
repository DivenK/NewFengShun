using itcast.crm16.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itcast.crm16.IServices;
using itcast.crm16.model;
using itcast.crm16.WebHelper.Attrs;
using itcast.crm16.Common;
using System.Text.RegularExpressions;

namespace itcast.crm16.Site.Areas.admin.Controllers
{
    public class memberController : BaseController
    {
        public memberController(IsysMenusServices mSer, IMemberDynamicServices memberSer, IArticleTypeServices articleTypeSer, IMemberMsgServices memberMsgSer) : base(mSer)
        {
            base.memberSer = memberSer;
            base.articleSer = articleTypeSer;
            base.memberMsgSer = memberMsgSer;
        }

        //
        // GET: /admin/member/

        public ActionResult Index()
        {
            //获取会员文章相关类型数据
            List<ArticleType> artList = articleSer.QueryWhere(c => c.TypeFor == 1);
            ViewBag.ArtList = artList;


            List<MemberDynamic> memberList = memberSer.QueryByPage(1, pageSize, out TotalPage, c => c.IsDelete == 0, c => c.id);
            ViewBag.MemberList = memberList;

            base.SetViewBagPage();
            return View();
        }

        public ActionResult GetContentByPage(int typeid, int pageIndex, int pageSize, string searchMsg)
        {
            //获取会员文章相关类型数据
            List<ArticleType> artList = articleSer.QueryWhere(c => c.TypeFor == 1);

            int count = 0;
            List<MemberDynamic> memberList = null;
            System.Linq.Expressions.Expression<Func<MemberMsg, bool>> where = null;
            if (typeid == 0)
            {
                if (string.IsNullOrEmpty(searchMsg))
                {
                    memberList = memberSer.QueryByPage(pageIndex, pageSize, out count, c => c.IsDelete == 0, c => c.id);
                }
                else
                {
                    memberList = memberSer.QueryByPage(pageIndex, pageSize, out count, c => c.Title.Contains(searchMsg) && c.IsDelete == 0, c => c.id);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(searchMsg))
                {
                    memberList = memberSer.QueryByPage(pageIndex, pageSize, out count, c => c.IsDelete == 0 && c.Type == typeid, c => c.id);
                }
                else
                {
                    memberList = memberSer.QueryByPage(pageIndex, pageSize, out count, c => c.Title.Contains(searchMsg) && c.IsDelete == 0 && c.Type == typeid, c => c.id);
                }
            }


            if (memberList != null)
            {
                return WriteSuccess("获取成功", new
                {
                    TotalPage = count,
                    Content = memberList.Select(
                    c => new
                    {
                        c.id,
                        c.IsComment,
                        c.Title,
                        c.Praise,
                        c.Creator,
                        CreateTime = c.CreateTime.ToString("yyyy-MM-dd"),
                        c.Type,
                        c.ImgUrl,
                        typeName = artList.Where(d => d.id == c.Type).FirstOrDefault().typeName
                    })
                });
            }
            else
            {
                return WriteError("获取数据失败");
            }
        }


        [ValidateInput(false)]
        public ActionResult Change(int id, string title, int type, string content, DateTime createtime, string imageUrl,string author="")
        {
            if (string.IsNullOrEmpty(title))
            {
                return WriteError("标题不能为空");
            }

            if (id == 0)//新增
            {
                MemberDynamic addModel = new MemberDynamic();
                addModel.Title = title;
                addModel.Content = content;
                addModel.Type = type;
                addModel.IsComment = true;
                addModel.CreateTime = createtime;
                addModel.Creator = (Session[Keys.uinfo] as sysUserInfo).uLoginName;
                addModel.Creator = author;
                if (!string.IsNullOrEmpty(content))
                    addModel.ImgUrl = imageUrl;
                memberSer.Add(addModel);
                int addRet = memberSer.SaveChanges();
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
                MemberDynamic editModel = new MemberDynamic();
                editModel.id = id;
                editModel.Title = title;
                editModel.Content = content;
                editModel.Type = type;
                editModel.CreateTime = createtime;
                editModel.UpdateTime = DateTime.Now;
                editModel.Updator = (Session[Keys.uinfo] as sysUserInfo).uLoginName;
                editModel.Creator = author;
                if (!string.IsNullOrEmpty(content))
                    editModel.ImgUrl = imageUrl;
                memberSer.Edit(editModel, new string[] { "Title", "Content", "Type", "CreateTime", "UpdateTime", "Updator", "ImgUrl", "Creator"});
                int editRet = memberSer.SaveChanges();
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

        public string GetImageUrl(string conters)
        {
            System.Text.RegularExpressions.Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(conters);
            if (matches.Count > 0)
            { return matches[0].Groups["imgUrl"].Value; }
            return string.Empty;
        }


        public ActionResult Del(int id)
        {
            if (id == 0)
            {
                return WriteError("参数异常");
            }
            MemberDynamic delModel = new MemberDynamic() { id = id, IsDelete = 1 };
            memberSer.Edit(delModel, new string[] { "IsDelete" });
            int delRet = memberSer.SaveChanges();
            if (delRet >= 1)
            {
                return WriteSuccess("删除成功");
            }
            else
            {
                return WriteError("删除失败");
            }
        }

        public ActionResult ChangeComment(int id, int comment)
        {
            if (id <= 0)
            {
                return WriteError("参数异常");
            }
            MemberDynamic entity = new MemberDynamic() { id = id, IsComment = comment == 1 };
            memberSer.Edit(entity, new string[] { "IsComment" });
            int ret = memberSer.SaveChanges();
            if (ret == 1)
            {
                return WriteSuccess("修改成功");
            }
            else
            {
                return WriteError("修改失败");
            }
        }

        public ActionResult GetMemberDyNamicById(int id)
        {
            if (id <= 0)
            {
                return WriteError("参数异常");
            }
            MemberDynamic entity = memberSer.QueryWhere(c => c.id == id && c.IsDelete == 0).FirstOrDefault();
            if (entity == null)
            {
                return WriteError("数据不存在");
            }
            else
            {
                return WriteSuccess("", new { entity.id, entity.Title, entity.Content, entity.Type,CreateTime=entity.CreateTime.ToString("yyyy-MM-dd"),imageUrl=entity.ImgUrl,Author=entity.Creator});
            }
        }

        public ActionResult MemberMsgIndex()
        {
            List<MemberMsg> memberList = memberMsgSer.QueryByPage(1, pageSize, out TotalPage, c => c.IsDelete == false, c => c.Id);
            ViewBag.MemberList = memberList;
            base.SetViewBagPage();
            return View();
        }


        public ActionResult Getmembermsg(int id)
        {
            if (id == 0)
            {
                return WriteError("参数异常");
            }
            MemberMsg entity = memberMsgSer.QueryWhere(c => c.Id == id).FirstOrDefault();
            if (entity == null)
            {
                return WriteError("该数据不存在");
            }
            else
            {
                return WriteSuccess("", new { entity, PassTimeStr = entity.PassTime != null ? entity.PassTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "" });
            }
        }

        public ActionResult GetMemberMsgByPage(int PageIndex, int PageSize, string RealName)
        {
            int count = 0;
            List<MemberMsg> memberList = null;
            if (string.IsNullOrEmpty(RealName))
            {
                memberList = memberMsgSer.QueryByPage(PageIndex, PageSize, out count, c => c.IsDelete == false, c => c.Id);
            }
            else
            {
                memberList = memberMsgSer.QueryByPage(PageIndex, PageSize, out count, c => c.RealName.Contains(RealName) && c.IsDelete == false, c => c.Id);
            }
            return WriteSuccess("", new { PageCount = count, memberList = memberList });
        }

        public ActionResult DelMemberMsg(int id)
        {
            if (id == 0)
            {
                return WriteError("参数异常");
            }
            MemberMsg entity = new MemberMsg() { Id = id };
            entity.IsDelete = true;
            memberMsgSer.Edit(entity, new string[] { "IsDelete" });
            int count = memberMsgSer.SaveChanges();
            if (count == 1)
            {
                return WriteSuccess("删除成功");
            }
            else
            {
                return WriteError("删除失败");
            }
        }

        public ActionResult PassMemberMsg(int id)
        {
            if (id == 0)
            {
                return WriteError("参数异常");
            }
            MemberMsg entity = new MemberMsg() { Id = id };
            entity.IsPass = true;
            memberMsgSer.Edit(entity, new string[] { "IsPass" });
            int count = memberMsgSer.SaveChanges();
            if (count == 1)
            {
                return WriteSuccess("审核成功");
            }
            else
            {
                return WriteError("审核失败");
            }
        }


        public ActionResult FormalMemberMsg(int id)
        {
            if (id == 0)
            {
                return WriteError("参数异常");
            }
            MemberMsg entity = new MemberMsg() { Id = id };
            entity.IsFormal = true;
            memberMsgSer.Edit(entity, new string[] { "IsFormal" });
            int count = memberMsgSer.SaveChanges();
            if (count == 1)
            {
                return WriteSuccess("转正成功");
            }
            else
            {
                return WriteError("转正失败");
            }
        }
    }
}
