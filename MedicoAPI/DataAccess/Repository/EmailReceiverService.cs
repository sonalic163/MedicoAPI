using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MedicoAPI.Models;
using System.Xml.Linq;
using MailKit.Net.Smtp;
using System.Linq.Expressions;

namespace MedicoAPI.DataAccess.Repository
{
    public class EmailReceiverService
    {
        private ApplicationDbContext db;
        private readonly IConfiguration _configuration;
        public EmailReceiverService(ApplicationDbContext db, IConfiguration configuration)
        {
            this.db = db;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(ReceivedEmail received)
        {
            try
            {
                var receiverEmail = _configuration["EmailSettings:ReceiverEmail"];

                var personalizedBody = received.Body;

                if (!string.IsNullOrEmpty(received.Name))
                {
                    personalizedBody = personalizedBody.Replace("{Name}", received.Name);
                }

                if (!string.IsNullOrEmpty(received.PhoneNo))
                {
                    personalizedBody = personalizedBody.Replace("{Phone}", received.PhoneNo);
                }

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(received.From));
                email.To.Add(MailboxAddress.Parse(receiverEmail));
                email.Subject = received.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = personalizedBody
                };

                email.Body = bodyBuilder.ToMessageBody();

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]);
                    await smtpClient.SendAsync(email);
                    await smtpClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex) {
                 ex.ToString();
            }
        }
    }
}