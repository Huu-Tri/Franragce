namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("collection")]
    public partial class collection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public collection()
        {
            collection_child = new HashSet<collection_child>();
        }

        [Key]
        public int id_collect { get; set; }

        [Required]
        [StringLength(250)]
        [DisplayName("Name")]
        public string name_collect { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Desc")]
        public string desc_collect { get; set; }

        [StringLength(500)]
        [DisplayName("Image")]
        public string image_collect { get; set; }
        [DisplayName("Created")]
        public DateTime? created_at { get; set; } = DateTime.Now;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<collection_child> collection_child { get; set; }
    }
}
