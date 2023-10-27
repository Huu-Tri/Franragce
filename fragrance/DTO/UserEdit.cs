using fragrance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fragrance.DTO
{
	public class UserEdit
	{
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		[DisplayName("User name")]
		public string Username { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(20)]
		[DisplayName("Phone")]
		[Phone]
		public string Phone { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[StringLength(255)]
		[DisplayName("Confirm Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public virtual ICollection<user_order> user_order { get; set; }
	}
}