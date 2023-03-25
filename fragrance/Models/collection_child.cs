namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class collection_child
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public collection_child()
        {
            products = new HashSet<product>();
        }

        [Key]
        public int id_c_child { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Sub Collection Name")]
        public string name_c_child { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Desc")]
        public string desc_c_child { get; set; }

        public int? id_c_collect { get; set; }

        [DisplayName("Created")]
        public DateTime? created_at { get; set; } = DateTime.Now;

        public virtual collection collection { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
