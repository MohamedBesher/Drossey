using System.Linq;
using System.Threading.Tasks;

namespace Drossey.Admin.Services
{
    public interface IEmailSender
    {
        
        Task<bool> SendEmailAsync(string email, string subject, string message);
        
        }
}
