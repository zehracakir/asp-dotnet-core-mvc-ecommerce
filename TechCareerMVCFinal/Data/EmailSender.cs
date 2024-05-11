using Microsoft.AspNetCore.Identity.UI.Services;

namespace TechCareerMVCFinal.Data
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // email serverim olmadigi icin kayit olmada hata almamak adina boyle bir cozum gelistirdim
            return Task.CompletedTask;
        }
    }
}
