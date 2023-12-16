namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            order_details = new HashSet<order_details>();
        }

        [Key]
        public int id_pr { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Name")]
        public string name_pr { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Image")]
        public string image_pr { get; set; }

        [DisplayName("Volume")]
        public int volume_pr { get; set; }
        [DisplayName("Price")]
        public decimal price_pr { get; set; }
        [DisplayName("Price Origin")]
        public decimal price_origin { get; set; }
        [DisplayName("Amount")]
        public int amount_pr { get; set; }
        [DisplayName("Desc")]
        [Column(TypeName = "ntext")]
        public string desc_pr { get; set; }
        [DisplayName("Notes")]
        [Column(TypeName = "ntext")]
        public string notes_pr { get; set; }
        [DisplayName("Tips")]
        [Column(TypeName = "ntext")]
        public string tips_pr { get; set; }
        [DisplayName("Status")]
        public int status_pr { get; set; }
        [DisplayName("Type")]
        public int? id_pro_typeof { get; set; }
        [DisplayName("Collect")]
        public int? id_pro_coll { get; set; }
        [DisplayName("Created")]
        public DateTime? created_at { get; set; } = DateTime.Now;
        
        public virtual collection_child collection_child { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_details> order_details { get; set; }
        
        public virtual product_type product_type { get; set; }
    }
}
