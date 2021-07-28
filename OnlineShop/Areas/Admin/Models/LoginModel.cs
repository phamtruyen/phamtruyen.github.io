using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Mời nhập password")]
        public string Password  { set; get; }

        public bool RememberME { set; get; }
    }
}