using fragrance.DTO;
using fragrance.Models;
using fragrance.Service;
using MailKit.Net.Smtp;
using MimeKit;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;

namespace fragrance.Controllers
{
	public class CartsController : Controller
	{
		// GET: Carts
		FragranceDbContext db = new FragranceDbContext();
        public ActionResult Index()
		{
			return View();
		}
		public List<Cart> GetCarts()
		{
			List<Cart> cartList = Session["Cart"] as List<Cart>;
			if (cartList == null)
			{
				cartList = new List<Cart>();
				Session["Cart"] = cartList;
			}
			return cartList;
		}
		public ActionResult AddCart(int ms, string url, int qty = 1)
		{

			List<Cart> cartList = GetCarts();

			Cart sp = cartList.Find(n => n.iPro == ms);
			var productQty = db.products.Where(x => x.id_pr == ms).FirstOrDefault().amount_pr;
			if (sp == null)
			{
				sp = new Cart(ms);
				sp.iQuantity = qty;
				cartList.Add(sp);
			}
			else
			{
				if (productQty > (sp.iQuantity + qty))
				{
					sp.iQuantity += qty;
				}
				else
				{
					ViewBag.MessageAddCart = "Not add to cart error";
				}

			}
			return Redirect(url);
		}

		private double SumCart()
		{
			int iSum = 0;
			List<Cart> cartList = Session["Cart"] as List<Cart>;
			if (cartList != null)
			{
				iSum = cartList.Sum(n => n.iQuantity);
			}
			return iSum;
		}

		private double SumCartMoney()
		{
			double dSumMoney = 0;
			List<Cart> cartList = Session["Cart"] as List<Cart>;
			if (cartList != null)
			{
				dSumMoney = cartList.Sum(n => n.dSumMoney);
			}
			return dSumMoney;
		}

		public ActionResult CartTest()
		{
			return PartialView();
		}


		public ActionResult Cart()
		{
			List<Cart> cartList = GetCarts();
			if (cartList.Count == 0)
			{
				return RedirectToAction("Index", "Fragrance");
			}
			ViewBag.SumCart = SumCart();
			ViewBag.SumCartMoney = SumCartMoney();
			return View(cartList);
		}
		public ActionResult CartPartial()
		{
			ViewBag.SumCart = SumCart();
			ViewBag.SumCartMoney = SumCartMoney();
			return PartialView();
		}

		public ActionResult DeleteProFromCart(int iPro)
		{
			List<Cart> cartList = GetCarts();

			Cart sp = cartList.SingleOrDefault(n => n.iPro == iPro);

			if (sp != null)
			{
				cartList.RemoveAll(n => n.iPro == iPro);
				if (cartList.Count == 0)
				{
					return RedirectToAction("Index", "Fragrance");
				}
			}
			return RedirectToAction("Cart");
		}
        public ActionResult OrderDetailByCode(string orderCode)
        {
			var order = db.user_order.Include(x => x.acc_user).Include(x => x.order_details).FirstOrDefault(x => x.code == orderCode);
			return View(order);
        }
        public ActionResult UpdateCart(int iPro, FormCollection f)
		{
			List<Cart> cartList = GetCarts();
			Cart sp = cartList.SingleOrDefault(n => n.iPro == iPro);
			if (sp != null)
			{
				var qty = int.Parse(f["quantity"].ToString());
				var amount = db.products.FirstOrDefault(x => x.id_pr == iPro).amount_pr;
				if(amount >= qty)
				{
					sp.iQuantity = qty;
					sp.Message = null;
				}
				else
				{
					sp.Message = $"Số lượng sàn phẩm không đủ, tối đa {amount} sản phẩm";
				}
			}
			return RedirectToAction("Cart");
		}

		public ActionResult DeleteCart()
		{
			List<Cart> cartList = GetCarts();
			cartList.Clear();
			return RedirectToAction("Index", "Fragrance");
		}
		[HttpGet]
		public ActionResult Order()
		{

			if (Session["User_Name"] == null || Session["User_Name"].ToString() == "")
			{
				return Redirect("~/User/Login?id=2");
			}

			if (Session["Cart"] == null)
			{
				return RedirectToAction("Index", "Fragrance");
			}
			// lấy hàng từ session
			List<Cart> cartList = GetCarts();
			ViewBag.SumCart = SumCart();
			ViewBag.SumCartMoney = SumCartMoney();
			return View(cartList);
		}
		[HttpPost]
		public ActionResult Order(FormCollection f)
        {
            user_order od = new user_order();
			acc_user us = (acc_user)Session["acc_user"];
			od.code = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            od.id_order_user = us.id_user;
			od.status_order = OrderStatus.Unpaid.ConvertToString();
			od.receiver_oder = f["receiver_oder"];
			od.address_order = f["address_order"];
			od.phone_order = f["phone_order"];
			od.payment_status = f["payment_type"].Equals("paypal") ? PaymentStatus.Pending.ConvertToString() : PaymentStatus.Success.ConvertToString();
			od.action_order = (int)OrderAction.Pending;
			od.payment_method = f["payment_type"];
            db.user_order.Add(od);
			db.SaveChanges();

			List<Cart> cartList = GetCarts();
			foreach (var item in cartList)
			{
				order_details order_details = new order_details();
				order_details.id_order_dt = od.id_order;
				order_details.id_pro = item.iPro;
				order_details.amount_order_dt = item.iQuantity;
				order_details.order_details_total = (decimal)item.dSumMoney;
				db.order_details.Add(order_details);
			}
			db.SaveChanges();

			var type = f["payment_type"];
			if (type.Equals("paypal"))
			{
				return PaymentWithPaypal(null, od.id_order, us.email_user, od.code);
			}
			else
			{
                Message message = new Message(new string[] { us.email_user }, "Order Confirm", $"Thanks you for trusting and order from us, go to https://localhost:44377/Carts/OrderDetailByCode?orderCode={od.code} to view and follow your order");
                SendEmail(message);
                return RedirectToAction("OrderConfirm", "Carts");
			}
		}
		public ActionResult OrderConfirm()
		{
			return View();
		}

		public ActionResult PaymentWithPaypal(string Cancel, int orderId, string email = null, string orderCode = null)
		{
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
			try
			{

				//A resource representing a Payer that funds a payment Payment Method as paypal  
				//Payer Id will be returned when payment proceeds or click to pay
				string payerId = Request.Params["PayerID"];
				if (string.IsNullOrEmpty(payerId))
				{
					//this section will be executed first because PayerID doesn't exist  
					//it is returned by the create function call of the payment class  
					// Creating a payment  
					// baseURL is the url on which paypal sendsback the data.  
					string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Carts/PaymentWithPayPal?";
					//here we are generating guid for storing the paymentID received in session  
					//which will be used in the payment execution  
					var guid = Convert.ToString((new Random()).Next(100000));
					//CreatePayment function gives us the payment approval url  
					//on which payer is redirected for paypal account payment  
					var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, (int)orderId);
					//get links returned from paypal in response to Create function call  
					var links = createdPayment.links.GetEnumerator();
					string paypalRedirectUrl = null;
					while (links.MoveNext())
					{
						Links lnk = links.Current;
						if (lnk.rel.ToLower().Trim().Equals("approval_url"))
						{
							//saving the payapalredirect URL to which user will be redirected for payment  
							paypalRedirectUrl = lnk.href;
						}
					}
                    var message = new Message(new string[] { email }, "Order Confirm", $"Thanks you for trusting and order from us, go to https://localhost:44377/Carts/OrderDetailByCode?orderCode={orderCode} to view and follow your order");
                    SendEmail(message);
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
					return Redirect(paypalRedirectUrl);
				}
				else
				{
					// This function exectues after receving all parameters for the payment  
					var guid = Request.Params["guid"];
					int idOrder = Convert.ToInt32(Request.Params["orderId"]);

					var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
					//If executed payment failed then we will show payment failure message to user  
					if (executedPayment.state.ToLower() != "approved")
					{
						UpdateOrderStatus(idOrder, PaymentStatus.Error.ConvertToString());
						return View("FailureView");
					}
					else
					{
						var order = new user_order();
						if (idOrder > 0)
						{
							UpdateOrderStatus(idOrder, PaymentStatus.Success.ConvertToString());
						}
						Session["Cart"] = null;
						return RedirectToAction("OrderConfirm", "Carts");
					}
				}
			}
			catch (Exception ex)
			{
				return View("FailureView");
			}
		}

		private void UpdateOrderStatus(int orderId, string status)
		{
			var order = new user_order();
			order = db.user_order.Where(x => x.id_order == orderId).Include(y => y.order_details).FirstOrDefault();
			if (order == null) throw new Exception("Invalid Order");
			order.status_order = status;
			if(status == PaymentStatus.Success.ConvertToString())
			{
				order.payment_status = OrderStatus.Paid.ConvertToString();
				var orderItem = order.order_details.GroupBy(z => z.id_pro).Select(a => new
				{
					Id = a.Key,
					Qty = (int)a.Sum(s => s.amount_order_dt)
				}).ToList();
				var productIds = orderItem.Select(y => y.Id).ToList();
				var aa = db.products.Where(x => productIds.Contains(x.id_pr)).ToList();
				aa.ForEach(x =>
				{
					var product = orderItem.Where(y => y.Id == x.id_pr).FirstOrDefault();
					if (product == null)
					{
						throw new Exception("Invalid product id");
					}
					x.amount_pr = x.amount_pr - product.Qty;
				});
			}
			else order.payment_status = status;
            db.SaveChanges();
		}

		private PayPal.Api.Payment payment;
		private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution()
			{
				payer_id = payerId
			};
			this.payment = new Payment()
			{
				id = paymentId
			};
			return this.payment.Execute(apiContext, paymentExecution);
		}
		private Payment CreatePayment(APIContext apiContext, string redirectUrl, int orderId)
		{
			var listCartItem = GetCarts();
			// Create item list and add item objects to it  
			var itemList = new ItemList
			{
				items = new List<Item>()
			};
			var aa = listCartItem.Select(x => x.dPricepr).ToList();
			// Adding Item Details like name, currency, price, and quantity  
			foreach (var cartItem in listCartItem)
			{
				itemList.items.Add(new Item
				{
					name = cartItem.sNamepr,
					currency = "USD",  // Assuming currency is always USD, you might want to make it dynamic based on the cart
					price = cartItem.dPricepr.ToString(),  // Format price to string
					quantity = cartItem.iQuantity.ToString(),
					sku = cartItem.iPro.ToString(),
				});
			}

			var payer = new Payer
			{
				payment_method = "paypal"
			};

			// Configure Redirect Urls here with RedirectUrls object  
			var redirUrls = new RedirectUrls
			{
				cancel_url = redirectUrl + "&Cancel=true",
				return_url = redirectUrl + "&orderId=" + orderId,
			};

			// Calculate subtotal
			var subtotal = listCartItem.Sum(item => item.iQuantity * item.dPricepr).ToString();

			// Adding Tax, shipping, and Subtotal details  
			var details = new Details
			{
				tax = "1",  // Assuming tax is always 1, you might want to calculate it based on your business logic
				shipping = "1",  // Assuming shipping is always 1, you might want to calculate it based on your business logic
				subtotal = subtotal
			};

			// Final amount with details  
			var amount = new Amount
			{
				currency = "USD",
				total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString("F"),  // Calculate total dynamically
				details = details
			};

			var transactionList = new List<Transaction>();

			// Adding description about the transaction  
			transactionList.Add(new Transaction
			{
				description = "Transaction description",
				invoice_number = Guid.NewGuid().ToString(),
				amount = amount,
				item_list = itemList
			});

			this.payment = new Payment
			{
				intent = "sale",
				payer = payer,
				transactions = transactionList,
				redirect_urls = redirUrls
			};

			// Create a payment using an APIContext  
			return this.payment.Create(apiContext);
		}
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
			EmailConfiguration _emailConfig = new EmailConfiguration();
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Nộ Long Cước", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(
                    "<div " +
                    "style='" +
                    "padding: 50px; " +
                    "line-height: 2;' >" +
                    "<h2>Dear Sir</h2>" +
                    "<p>{0}</p>" +
                    "</div>"
                , message.Content)

            };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            EmailConfiguration _emailConfig = new EmailConfiguration();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}