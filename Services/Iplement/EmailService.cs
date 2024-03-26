using PustokBook.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace PustokBook.Services.Iplement
{
    public class EmailService : IEmailService
    {
        public IConfiguration _configuration { get; }

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string header, string body, string mailAddress, bool IsHtml = true)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
            smtp.EnableSsl = true;
            MailAddress from = new MailAddress(_configuration["Email:Username"], "Pustok");
            MailAddress to = new MailAddress(mailAddress);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = header;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = IsHtml;
            smtp.Send(mailMessage);
        }
    }
}
