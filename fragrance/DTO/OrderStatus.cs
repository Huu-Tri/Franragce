using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fragrance.DTO
{
	public enum OrderStatus
	{
		Unpaid, Paid
	}

	public enum OrderAction
	{
		Pending, Confirm, Processing, Delivering, Received, Success, Cancel
	}
    public enum PaymentStatus
    {
        Pending, Error, Success, Cancel
    }

}