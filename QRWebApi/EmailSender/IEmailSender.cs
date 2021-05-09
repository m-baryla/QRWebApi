using System.Threading.Tasks;

namespace QRWebApi.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
