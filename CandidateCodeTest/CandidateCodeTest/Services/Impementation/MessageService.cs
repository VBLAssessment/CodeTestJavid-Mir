using CandidateCodeTest.Common.Interfaces;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.IO;
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
            var sendMailtoThisPath = Directory.CreateDirectory
                                            (Path.Combine
                                            (Environment.GetFolderPath
                                            (Environment.SpecialFolder.Desktop), "Mails"));

            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 25
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = sendMailtoThisPath.FullName
            });

            StringBuilder template = new();
            template.AppendLine("Dear Test Member,");
            template.AppendLine("Thanks for Being Here to check this Send Email Project. We hope you liked it.");
            template.AppendLine("- From Javid (The UST Team Member)");

            Email.DefaultSender = sender;

            var email = await Email
                .From("javid@mir.com")
                .To("test@test.com", "Test Member")
                .Subject("Thanks!")
                .Body(template.ToString())
                .SendAsync();

            //for the sample output, pease check '/SampleMail' folder in the current project
        }
    }
}