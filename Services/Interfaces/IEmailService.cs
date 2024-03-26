namespace PustokBook.Services.Interfaces
{
	public interface IEmailService
	{
		public void Send(string header, string body, string mailAddress, bool IsHtml = true);
	}
}
