using System.Threading.Tasks;

namespace CandidateCodeTest.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Function is used to send email.
        /// </summary>
         Task SendEmailAsync();
    }
}
