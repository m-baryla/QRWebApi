using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MimeKit;

namespace QRWebApi.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderConfig _emailSenderConfig;

        public EmailSender(EmailSenderConfig emailSenderConfig)
        {
            _emailSenderConfig = emailSenderConfig;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSenderConfig.MailFrom));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) {Text = message.Content};
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailSenderConfig.MailHost, _emailSenderConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailSenderConfig.EmailUser, _emailSenderConfig.EmailPassword);
                    client.Send(mailMessage);
                }
                catch (Exception)
                {
                    Console.WriteLine();
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
