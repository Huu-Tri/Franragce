using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace fragrance.DTO
{
	public enum PaymentType
	{
		Paypal,
		ShipCod
	}
	public class CartRequest
	{
		[Required]
		[StringLength(100)]
		public string Reciver { get; set; }
		[Required]
		[StringLength(100)]
		public string Address { get; set; }
		[Required]
		[Phone]
		public string Phone { get; set; }
		[Required]
		public int PaymentType { get; set; }
	}
}