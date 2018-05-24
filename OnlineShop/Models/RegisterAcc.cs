using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterAcc
    {
        [Required(ErrorMessage = "Bạn phải nhập tên")]
        [StringLength(100, ErrorMessage = "Tên lớn hơn 2 ký tự ", MinimumLength = 2)]
        public string RRealName { get; set; }

        [StringLength(100, ErrorMessage = "Tên đăng nhập lớn hơn 3 ký tự", MinimumLength = 3)]
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        public string RUserName { get; set; }

        [StringLength(100, ErrorMessage = "Mật khẩu lớn hơn 6 ký tự", MinimumLength = 2)]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string RPassword { get; set; }

        [StringLength(100, ErrorMessage = "Mật khẩu lớn hơn 6 ký tự", MinimumLength = 6)]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("RPassword", ErrorMessage = "Mật khẩu không trùng nhau")]
        public string RePassword { get; set; }

        [StringLength(100, ErrorMessage = "Nhập ít nhất", MinimumLength = 4)]
        [Required(ErrorMessage = "Bạn phải nhập Email")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        public string REmail { get; set; }
    }
}