using SABA.Services.Models.ResponseModels.Email;

namespace SABA.Services.Abstractiuons.Email
{
    public interface IEmailSendService
    {
        public Task<EmailSendingResponse> SendEmailAsync(string email, string subject, string body);
    }
}
