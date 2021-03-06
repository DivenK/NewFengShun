﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using itcast.crm16.model;
using itcast.crm16.model.ModelViews;

namespace itcast.crm16.Site.Models
{
    public class HomeViewModel
    {
        public List<SiteSetViewModel> ImageList { get; set; }

        public List<New> NewList { get; set; }

        public List<New> NewListType { get; set; }

        public List<CommerceViewModel> vipUserImageList { get; set; }

        public List<MemberDynamic> MemberNewsList { get; set; }
        public List<MemberDynamic> MemberTellList { get; set; }
        public List<MemberDynamic> MemberZhaoList { get; set; }

        public List<FSHistoryViewModel> FSHistoryList { get; set; }
    }
}