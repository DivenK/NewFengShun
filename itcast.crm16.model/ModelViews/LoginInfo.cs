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
    /// 专门给登录页面使用
    /// </summary>
    public class LoginInfo
    {
        [DisplayName("账号"), Required(ErrorMessage = "账号非空")]
        public string LoginName { get; set; }
        [DisplayName("密码"), Required(ErrorMessage = "密码非空")]
        public string LoginPWD { get; set; }
        [DisplayName("验证码"), Required(ErrorMessage = "验证码非空")]
        public string VCode { get; set; }
        [DisplayName("免登录3天")]
        public bool IsReMember { get; set; }
    }
}
