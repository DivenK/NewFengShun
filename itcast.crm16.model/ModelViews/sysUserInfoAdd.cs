using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itcast.crm16.model.ModelViews
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 专门提供给add.cshtml使用的实体
    /// </summary>
    public class sysUserInfoAdd
    {
        public int uID { get; set; }
        [System.Web.Mvc.Remote("CheckUserName", "userinfo", HttpMethod = "post", ErrorMessage="当前用户已经存在")]
        [DisplayName("登录名称"), Required(ErrorMessage = "登录名称非空"), StringLength(20, ErrorMessage = "登录名称不能超过20")]
        public string uLoginName { get; set; }
        public string uLoginPWD { get; set; }
        [DisplayName("真实姓名"), Required(ErrorMessage = "真实姓名非空")]
        public string uRealName { get; set; }
        public string uTelphone { get; set; }
        public string uMobile { get; set; }
        public string uEmial { get; set; }
        public string uQQ { get; set; }
        public int uGender { get; set; }
        public int uStatus { get; set; }
        public int uCompanyID { get; set; }
        public Nullable<int> uDepID { get; set; }
        public Nullable<int> uWorkGroupID { get; set; }
        public string uRemark { get; set; }
        public int uCreatorID { get; set; }
        public System.DateTime uCreateTime { get; set; }
        public Nullable<int> uUpdateID { get; set; }
        public System.DateTime uUpdateTime { get; set; }
    }
}
