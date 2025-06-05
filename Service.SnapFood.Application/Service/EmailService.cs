using Service.SnapFood.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class EmailService : IEmailService
    {
        private readonly string _fromEmail = "buiductrung32@gmail.com";
        private readonly string _password = "busc hskx sexa kzoe";
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var mail = new MailMessage()
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(_fromEmail, _password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
            }
        }
    }
}
