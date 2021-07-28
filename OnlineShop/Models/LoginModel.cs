using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Nhập email")]
        [Required(ErrorMessage = "Bạn phải nhập tài khoản")]
        public string Email { set; get; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập Mật khẩu")]
        public string Password { set; get; }
    }
}