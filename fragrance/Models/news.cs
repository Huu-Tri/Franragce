using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace fragrance.Models
{
    public class news
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Type")]
        public string Title { get; set; }

        [StringLength(500)]
        [DisplayName("Image")]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Content")]
        public string Content { get; set; }
        public bool IsActive { get; set; }
        [DisplayName("Created")]
        public DateTime? created_at { get; set; } = DateTime.Now;
    }
}