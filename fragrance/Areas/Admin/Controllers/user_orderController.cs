using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using fragrance.DTO;
using fragrance.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace fragrance.Areas.Admin.Controllers
{
    public class user_orderController : Controller
    {
        private FragranceDbContext db = new FragranceDbContext();

        // GET: Admin/user_order
        public ActionResult Index()
        {
            var user_order = db.user_order.OrderByDescending(x => x.id_order).Include(u => u.acc_user);
            return View(user_order.ToList());
        }

        // GET: Admin/user_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            return View(user_order);
        }

        // GET: Admin/user_order/Create
        public ActionResult Create()
        {
            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user");
            return View();
        }

        // POST: Admin/user_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,receiver_oder,status_order,address_order,date_order,phone_order,action_order,id_order_user,created_at")] user_order user_order)
        {
            if (ModelState.IsValid)
            {
                db.user_order.Add(user_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user", user_order.id_order_user);
            return View(user_order);
        }

        // GET: Admin/user_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            var orderActions = Enum.GetValues(typeof(OrderAction)).Cast<OrderAction>();
            var selectList = orderActions.Select(action => new SelectListItem
            {
                Text = action.ToString(),
                Value = ((int)action).ToString()
            });
            ViewBag.actions = new SelectList(selectList, "Value", "Text");
            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user", user_order.id_order_user);
            return View(user_order);
        }

        // POST: Admin/user_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,receiver_oder,status_order,address_order,date_order,phone_order,action_order,id_order_user,created_at")] user_order user_order)
        {
            var code = db.user_order.FirstOrDefault(x => x.id_order == user_order.id_order).code;
            user_order.code = code;
            if (ModelState.IsValid)
            {
                if(user_order.action_order == (int)OrderAction.Delivering)
                {
                    var email = db.user_order.Where(x => x.id_order == user_order.id_order).Include(x => x.acc_user).FirstOrDefault().acc_user.email_user;
                    email = email != null ? email : user_order.acc_user.email_user;
                    var message = new Message(new string[] { email }, "Order Confirm", $"Your order {user_order.code} is on the way, go to https://localhost:44377/Carts/OrderDetailByCode?orderCode={user_order.code} to view and follow your order");
                    SendEmail(message);
                }
                db.user_order.AddOrUpdate(user_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var orderActions = Enum.GetValues(typeof(OrderAction)).Cast<OrderAction>();
            var selectList = orderActions.Select(action => new SelectListItem
            {
                Text = action.ToString(),
                Value = ((int)action).ToString()
            });
            ViewBag.actions = new SelectList(selectList, "Value", "Text");
            ViewBag.id_order_user = new SelectList(db.acc_user, "id_user", "name_user", user_order.id_order_user);
            return View(user_order);
        }

        // GET: Admin/user_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_order user_order = db.user_order.Find(id);
            if (user_order == null)
            {
                return HttpNotFound();
            }
            return View(user_order);
        }

        // POST: Admin/user_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_order user_order = db.user_order.Where(x => x.id_order == id).Include(x => x.order_details).FirstOrDefault();
            db.user_order.Remove(user_order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
