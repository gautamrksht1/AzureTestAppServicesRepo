namespace ECommerce.Services.EmailAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task RegisterUserEmailAndLog(string email);
    }
}
