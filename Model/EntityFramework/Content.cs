namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name="Tên bài")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên rút gọn")]
        public string MetaTitle { get; set; }

        [StringLength(550)]
        [Display(Name = "Miêu tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Ảnh hiển thị")]
        public string Images { get; set; }

        [Display(Name = "Thể loại tin")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết tin")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Miêu tả tin")]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }
    }
}
