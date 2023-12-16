using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fragrance.DTO
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("Nước Hoa Trái Cây Thơm Nức Mũi <3", x)));
            Subject = subject;
            Content = content;
        }
    }

    public class EmailConfiguration
    {
        public string From { get; set; } = "1924801030282@student.tdmu.edu.vn";
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 465;
        public string UserName { get; set; } = "1924801030282@student.tdmu.edu.vn";
        public string Password { get; set; } = "@Congacon123";
    }
}