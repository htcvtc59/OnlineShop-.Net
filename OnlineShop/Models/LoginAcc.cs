using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class LoginAcc
    {
        [Required(ErrorMessage = "Mời nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}