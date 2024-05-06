using StoreApp.Data.Abstract;
using System.Net;
using System.Net.Mail;

namespace StoreApp.Data.Concrete
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string? _host;

        private readonly int _port;

        private readonly bool _enableSsl;

        private readonly string? _userName;

        private readonly string? _password;

        public SmtpEmailSender(string? host, int port, bool enableSsl, string? userName, string? password)
        {
            _host = host;
            _port = port;
            _enableSsl = enableSsl;
            _userName = userName;
            _password = password;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSsl
            };

            return smtpClient.SendMailAsync(new MailMessage(_userName ?? "", email, subject, message) { IsBodyHtml = true });
        }
    }
}
