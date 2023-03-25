namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class acc_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public acc_user()
        {
            user_order = new HashSet<user_order>();
        }

        [Key]
        public int id_user { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("User name")]
        public string name_user { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Email")]
        [EmailAddress]
        public string email_user { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Phone")]
        [Phone]
        public string phone_user { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password_user { get; set; }
        [DisplayName("Created")]
        public DateTime? created_at { get; set; } = DateTime.Now;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_order> user_order { get; set; }
    }
}
