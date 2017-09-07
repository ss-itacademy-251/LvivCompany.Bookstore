using System.Threading.Tasks;

namespace LvivCompany.Bookstore.BusinessLogic.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
