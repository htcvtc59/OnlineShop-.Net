namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Nội dung")]
        public string Text { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn")]
        public string Link { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        [Display(Name = "Target")]
        public string Target { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name = "Loại Menu")]
        public int? TypeID { get; set; }
    }
}
