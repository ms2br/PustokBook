using PustokBook.Services.Interface;
using System.Net;
using System.Net.Mail;

namespace PustokBook.Services.Iplement
{
    public class Email : IEmail
    {
        public IConfiguration _ic { get; }
        public Email(IConfiguration ic)
        {
            _ic = ic;
        }

        public void SendEmail(string toMail, string header, string body, bool isHTMl = true)
        {
            SmtpClient smtpClient = new SmtpClient(_ic["Email:Host"], Convert.ToInt32(_ic["Email:Port"]));

            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_ic["Email:Username"], _ic["Email:Password"]);
            MailAddress from = new MailAddress(_ic["Email:Username"], "PustokBook");
            MailAddress to = new MailAddress(toMail);

            MailMessage message = new MailMessage(from, to);
            message.Subject = header;
            message.Body = body;
            message.From = from;
            message.IsBodyHtml = isHTMl;
            smtpClient.Send(message);
        }
    }
}
