namespace fragrance.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        [Key]
        public int id_ad { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("User name")]
        [Index(IsUnique = true)]
        public string name_ad { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Email")]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string email_ad { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password_ad { get; set; }
        
        [DisplayName("Date created")]
        public DateTime? created_at { get; set; } = DateTime.Now;
    }
}
