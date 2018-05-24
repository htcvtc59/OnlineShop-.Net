namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string ClientID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? ProductID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? TotalPrice { get; set; }

        public bool Status { get; set; }
    }
}
