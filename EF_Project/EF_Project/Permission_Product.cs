namespace EF_Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permission_Product
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Permission_Number { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_Code { get; set; }

        public int? Quantity { get; set; }

        public int? Expiration_Period { get; set; }

        [StringLength(50)]
        public string Production_Date { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual Product Product { get; set; }
    }
}
