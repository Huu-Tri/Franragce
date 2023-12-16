using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace fragrance.Models
{
	public class menu
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public menu()
		{
			product_types = new HashSet<product_type>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		[DisplayName("Name")]
		public string Name { get; set; }

		public bool IsActive { get; set; }

		[DisplayName("Created")]
		public DateTime Created { get; set; } = DateTime.Now;

		public virtual ICollection<product_type> product_types { get; set; }
	}
}