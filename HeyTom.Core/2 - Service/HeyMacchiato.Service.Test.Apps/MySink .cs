using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.OAuth.Apps
{
    public class MySink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public MySink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            throw new Exception("testts");
            Console.WriteLine(DateTimeOffset.Now.ToString() + " " + message);
        }
    }

    public static class MySinkExtensions
    {
        public static LoggerConfiguration MySink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new MySink(formatProvider));
        }
    }
}
