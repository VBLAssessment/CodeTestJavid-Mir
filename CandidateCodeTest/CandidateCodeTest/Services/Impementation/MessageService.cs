using CandidateCodeTest.Common.Interfaces;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CandidateCodeTest.Services.Implementations
{
    public class MessageService : IMessageService
    {
        public ILogWriter _logWriter;
        public MessageService(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }
        public async Task SendEmailAsync()
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 25
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Mails"
            });

            StringBuilder template = new();
            template.AppendLine("Dear Test Member,");
            template.AppendLine("Thanks for Being Here to check this Send Email Project. We hope you liked it.");
            template.AppendLine("- From Javid (The UST Team Member)");

            Email.DefaultSender = sender;
            //Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("javid@mir.com")
                .To("test@test.com", "Test Member")
                .Subject("Thanks!")
                //.UsingTemplate(template.ToString(), new { FirstName = "Test Member", ProjectName = "Send Email Test" })
                .Body(template.ToString())
                //.Body("Thanks for Being Here.")
                .SendAsync();
        }
    }
}