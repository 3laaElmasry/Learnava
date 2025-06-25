

using Microsoft.AspNetCore.Identity.UI.Services;

namespace Learnava.BusinessLogic.Services
{
    public class EmailSenderService : IEmailSender
    {
        public  Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            // Just log or simulate email sending for now
            Console.WriteLine($"To: {email}, Subject: {subject}, Message: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
