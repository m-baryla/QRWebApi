using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace QRWebApi.EmailSender
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content_part1 { get; set; }
        public string Content_part2 { get; set; }
        public string Content_part3 { get; set; }
        public string UserSender { get; set; }

        public Message(IEnumerable<string> to,string subject,string content1, string content2, string content3,string userSender)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content_part1 = content1;
            Content_part2 = content2;
            Content_part3 = content3;
            UserSender = userSender;
        }
    }
}
