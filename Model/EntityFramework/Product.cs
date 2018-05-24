namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        public string Code { get; set; }

        [StringLength(250)]
        [Display(Name = "Đường dẫn rút gọn")]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Miêu tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Ảnh")]
        public string Images { get; set; }

        [Column(TypeName = "xml")]
        [Display(Name = "Ảnh sản phẩm")]
        public string MoreImages { get; set; }

        [Display(Name = "Đơn giá")]
        public decimal? Price { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Mã thể loại sản phẩm")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }


        [Display(Name = "Bảo hành")]
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
        public string MetaDescriptions { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }
    }
}
