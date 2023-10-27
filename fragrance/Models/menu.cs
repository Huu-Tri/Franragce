using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace fragrance.Models
{
	public partial class menu
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public menu()
		{
			product_types = new HashSet<product_type>();
		}

		[Key]
		public int id_menu { get; set; }

		[Required]
		[StringLength(100)]
		[DisplayName("Type")]
		public string name_menu { get; set; }

		[DisplayName("Created")]
		public DateTime? created_at { get; set; } = DateTime.Now;

		public virtual ICollection<product_type> product_types { get; set; }
	}
}