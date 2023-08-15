namespace E_Commerce_MVC.Core.ForEmail
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email);
    }
}
