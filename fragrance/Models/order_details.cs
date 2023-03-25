namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_order_dt { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_pro { get; set; }

        public int? amount_order_dt { get; set; }

        public decimal order_details_total { get; set; }

        public virtual user_order user_order { get; set; }

        public virtual product product { get; set; }
    }
}
