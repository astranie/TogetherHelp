using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Controllers;

namespace UI.Models.Register
{
    public class IndexModel : LogViewModel
    {

        [Required(ErrorMessage = "*用户名是必须的")]
        [Remote("IsRepeated", "Register",ErrorMessage = "用户名已被占用")]
        public string Username { get; set; }
        [Remote(nameof(RegisterController.ConfirmPassword),"Register",AdditionalFields =nameof(PasswordConfirm),ErrorMessage ="两次密码不一致")]
        [Required(ErrorMessage = "*密码是必须的")]
        public string Password { get; set; }
        [Required(ErrorMessage = "请确认密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        [Remote(nameof(RegisterController.ConfirmPassword), "Register", AdditionalFields = nameof(Password), ErrorMessage = "两次密码不一致")]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "请输入验证码")]
        public string Captcha { get; set; }
    }
}
