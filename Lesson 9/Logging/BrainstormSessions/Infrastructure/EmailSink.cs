using Serilog.Core;
using Serilog.Events;
using System.Net;
using System.Net.Mail;

namespace BrainstormSessions.Infrastructure
{
    public class EmailSink : ILogEventSink
    {
        private readonly string _from;
        private readonly string _to;
        private readonly string _smtp;
        private readonly int _port;
        private readonly string _user;
        private readonly string _pass;

        public EmailSink(string from, string to, string smtp, int port, string user, string pass)
        {
            _from = from;
            _to = to;
            _smtp = smtp;
            _port = port;
            _user = user;
            _pass = pass;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level == LogEventLevel.Error)
                return;


            var message = logEvent.RenderMessage();

            using (var client = new SmtpClient(_smtp, _port))
            {
                client.Credentials = new NetworkCredential(_user, _pass);
                client.EnableSsl = true;

                var mail = new MailMessage(_from, _to)
                {
                    Subject = $"Log: {logEvent.Level}",
                    Body = message
                };

                client.Send(mail);
            }

        }

    }
}
