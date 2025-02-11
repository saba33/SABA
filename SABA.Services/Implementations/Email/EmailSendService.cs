using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SABA.Services.Abstractiuons.Email;
using SABA.Services.Models.ResponseModels.Email;
using System.Net;
using System.Net.Mail;

namespace SABA.Services.Implementations.Email
{
    public class EmailSendService : IEmailSendService
    {
        private readonly IConfiguration _configuration;

        public EmailSendService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<EmailSendingResponse> SendEmailAsync(string email, string subject, string body)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
                Credentials = new NetworkCredential(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:AppPassword"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:SenderEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            try
            {
                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);
                return new EmailSendingResponse
                {
                    Message = "Mail was succesfully send",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new EmailSendingResponse
                {
                    Message = "Mail Couldnot be Sent",
                    StatusCode = StatusCodes.Status200OK
                };
            }


        }
    }
}
