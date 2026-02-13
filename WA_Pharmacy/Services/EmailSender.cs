using Microsoft.AspNetCore.Identity.UI.Services;

namespace WA_Pharmacy.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Dummy implementation for development
            return Task.CompletedTask;
        }
    }
}
