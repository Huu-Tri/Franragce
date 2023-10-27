namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product_type()
        {
            products = new HashSet<product>();
        }

        [Key]
        public int id_prt { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Type")]
        public string name_prt { get; set; }

        [StringLength(500)]
        [DisplayName("Image")]
        public string image_prt { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Desc")]
        public string desc_prt { get; set; }

        [StringLength(100)]
        [DisplayName("Gender")]
        public string forgender_prt { get; set; }

        [DisplayName("Created")]
        public DateTime? created_at { get; set; } = DateTime.Now;

		/*[DisplayName("Menu")]
		public int? id_menu { get; set; }*/
        /*[ForeignKey("id_menu")]
		public virtual menu menu { get; set; }*/

        public virtual ICollection<product> products { get; set; }
    }
}
