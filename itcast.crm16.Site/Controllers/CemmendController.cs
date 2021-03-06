﻿using itcast.crm16.IServices;
using itcast.crm16.WebHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itcast.crm16.Site.Controllers
{
    [WebHelper.Attrs.SkipCheckLogin]
    public class CemmendController : BaseController
    {
        //
        // GET: /Cemmend/
        public CemmendController(IsysMenusServices mSer, ICommerce monser) : base(mSer, "/Cemmend")
        {
            base.Commerce = monser;
        }
        public ActionResult Index()
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, 0, 10, true).ToList();
            ViewBag.items= list;
            ViewBag.id = 0;
            return View();
        }
        public ActionResult GetModel(int id=0,int type=0)
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, type, 10, true).ToList();
            ViewBag.items = list;
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetLeader(int id=0)
        {
            base.pageSize = 12;
            var list = Commerce.GetItemModel(1, "", out TotalPage, 0, base.pageSize, true).ToList();
            ViewBag.list = list;
            var lists = Commerce.GetItemModel(1, "", out TotalPage,2, base.pageSize, true).ToList();
            SetViewBagPage();
            ViewBag.items = lists;
            ViewBag.id = id;
            return View();
        }
        public ActionResult AjaxGetLeader(int pageIndex)
        {
            base.pageSize = 12;
            var lists = Commerce.GetItemModel(pageIndex, "", out TotalPage, 2, base.pageSize, true).ToList();
           
            if (lists.Count > 0)
            {
                return WriteSuccess("", lists);
            }
            else
            {
                return WriteError("");
            }
        }

        public ActionResult LeaderModel(int id)
        {
            var list = Commerce.GetItemModel(1, "", out TotalPage, 0, 10, true).OrderByDescending(c => c.Nid).ToList();
            ViewBag.list = list;
            if (id > 0)
            {
            
                var model = Commerce.QueryWhere(c => c.id == id).First();
                ViewBag.Model = model;
            }
            else
            {
                ViewBag.Model = null;
            }
            return View();
        }

    }
}
