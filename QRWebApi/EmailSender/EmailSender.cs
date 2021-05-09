using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using QRWebApi.Models;

namespace QRWebApi.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderConfig _emailSenderConfig;

        public EmailSender(EmailSenderConfig emailSenderConfig)
        {
            _emailSenderConfig = emailSenderConfig;
        }
        
        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSenderConfig.MailFrom));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = string.Format("<h2 style='color:black'>{0}</h2>", message.Content) };
            return emailMessage;
        }


        private async Task SendAsync(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailSenderConfig.MailHost, _emailSenderConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailSenderConfig.EmailUser, _emailSenderConfig.EmailPassword);

                await client.SendAsync(mailMessage);
            }
            catch (Exception)
            {
                Console.WriteLine();
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
