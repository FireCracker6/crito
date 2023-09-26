using System.Diagnostics;
using MimeKit;

namespace Crito.Services
{
    public class MailService : IDisposable
    {
        private readonly string _from;
        private readonly string _smtp;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        private readonly MailKit.Net.Smtp.SmtpClient _client;

        public MailService(string from, string smtp, int port, string username, string password)
        {
            _from = from;
            _smtp = smtp;
            _port = port;
            _username = username;
            _password = password;

            _client = new MailKit.Net.Smtp.SmtpClient();
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            try
            {
                if (!_client.IsConnected)
                {
                    await _client.ConnectAsync(_smtp, _port, MailKit.Security.SecureSocketOptions.StartTls);
                    await _client.AuthenticateAsync(_username, _password);
                }

                await _client.SendAsync(email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                // Consider re-throwing or handling this exception more gracefully in the future
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client.Disconnect(true);
                _client.Dispose();
            }
        }
    }
}
