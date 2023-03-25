namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contact")]
    public partial class contact
    {
        [Key]
        public int id_contact { get; set; }

        [Column(TypeName = "ntext")]
        public string info_contact { get; set; }
    }
}
