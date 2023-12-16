namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("message")]
    public partial class message
    {
		[Key]
		public int id_mes { get; set; }

        [StringLength(100)]
        public string fullname_mes { get; set; }

        [Column(Order = 0)]
        [StringLength(200)]
        public string email_mes { get; set; }

        [Column(Order = 1, TypeName = "ntext")]
        public string content_mes { get; set; }
    }
}
