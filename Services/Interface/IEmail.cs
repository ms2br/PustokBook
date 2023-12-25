namespace PustokBook.Services.Interface
{
    public interface IEmail
    {
        public void SendEmail(string toMail, string header, string body, bool isHTMl = true);
    }
}
