using System.Threading.Tasks;

namespace LvivCompany.Bookstore.BusinessLogic.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }
    }
}
