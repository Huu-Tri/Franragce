namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user_order()
        {
            order_details = new HashSet<order_details>();
        }

        [Key]
        public int id_order { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Receiver")]
        public string receiver_oder { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Status")]
        public string status_order { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Address")]
        public string address_order { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Delivery ")]
        public DateTime? date_order { get; set; } = DateTime.Now.AddDays(7);

        [Required]
        [StringLength(100)]
        [DisplayName("Phone")]
        [Phone]
        public string phone_order { get; set; }
        [DisplayName("Action")]
        public int action_order { get; set; }

        public int? id_order_user { get; set; }

        [DisplayName("Date")]
        public DateTime? created_at { get; set; } = DateTime.Now;

        public virtual acc_user acc_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_details> order_details { get; set; }
    }
}
