using Serilog;
using Serilog.Events;
using Serilog.Configuration;
using BrainstormSessions.Infrastructure;

namespace BrainstormSessions.Extensions
{
    public static class EmailSinkExtensions
    {
        public static LoggerConfiguration Email(this LoggerSinkConfiguration sinkConfiguration, string from, string to, string smtp, int port, string user, string pass, bool enablesl, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Error)
        {
            return sinkConfiguration.Sink(new EmailSink(from, to, smtp, port, user, pass), restrictedToMinimumLevel);
        }
    }
}
