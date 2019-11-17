using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models.Register
{
    public class IndexModel : LogViewModel
    {
        [Required(ErrorMessage = "*用户名是必须的")]
        public string Username { get; set; }
        [Required(ErrorMessage = "*密码是必须的")]
        public string Password { get; set; }
        [Required(ErrorMessage = "请确认密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "请输入验证码")]
        public string Captcha { get; set; }
    }
}
