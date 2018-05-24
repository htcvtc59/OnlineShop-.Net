namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public long ID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên của bạn")]
        public string RealName { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Loại tài khoản")]
        public string LevelUser { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
